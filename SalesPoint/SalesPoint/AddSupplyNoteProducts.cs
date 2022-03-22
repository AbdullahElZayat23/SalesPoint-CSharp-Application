using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesPoint
{
    public partial class AddSupplyNoteProducts : Form
    {

        salesPointEntities1 ent;
        int Sup_ID;
        int war_ID;
        DateTime NoteDate;
        int NoteID;

        public struct Notedata
        {
            public int productID;
            public DateTime ProductionDate;
            public int ExpirationDuration;
            public int quantity;
            public int Wh_ID;
        }
        List<Notedata> noteProducts;
        Home hme;
        public AddSupplyNoteProducts(int supplierID, int warehouseID, DateTime NoteDT, Home hm)
        {
            InitializeComponent();
            noteProducts = new List<Notedata>();
            ent = new salesPointEntities1();
            Sup_ID = supplierID;
            war_ID = warehouseID;
            NoteDate = NoteDT;
            hme = hm;
            //add temporary item
            supplyingPermission sp = new supplyingPermission();
            ent.supplyingPermissions.Add(sp);
            ent.SaveChanges();
            NoteID = ent.supplyingPermissions.Max(per => per.permission_id);
            //end adding temporary item
            txt_permissionID.Text = NoteID.ToString();
            var result = ent.products.Where(pro => pro.w_id == war_ID).ToList();
            foreach (var item in result)
            {                
                combo_ProductIDName.Items.Add($"ID:{item.p_code}:Name:{item.p_name}:WareHouse:{item.warehouse.w_name}:Quantity:{item.quantity}");
            }

        }

        private void txt_productQuantity_TextChanged(object sender, EventArgs e)
        {

            int quantity;
            if (combo_ProductIDName.Text != "")
            {
                if (txt_productQuantity.Text != "")
                {
                    if (int.TryParse(txt_productQuantity.Text.Trim(), out quantity))
                    {                       
                        if (quantity < 1)
                        {
                            txt_productQuantity.Text = string.Empty;
                            MessageBox.Show("Quantity entered is negative or zero!!");
                        }
                    }
                    else
                    {
                        txt_productQuantity.Text = string.Empty;
                        MessageBox.Show("Enter quantity in right format");

                    }
                }


            }
            else
            {
                MessageBox.Show("Choose product first");
            }

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            var result = ent.supplyingPermissions.Where(sup => sup.permission_id == NoteID).ToList().First();
            ent.supplyingPermissions.Remove(result);
            ent.SaveChanges();
            this.Close();

        }

        private void btn_AddItemtoList_Click(object sender, EventArgs e)
        {
            if (combo_ProductIDName.Text != ""
                && txt_productExpiredt.Text != ""
                && txt_productQuantity.Text != "")
            {
                int productID;
                int productExpireDuration;
                int productQuantity;                
                int WH_ID;
                string name = combo_ProductIDName.Text.Split(':')[5];
                productID = int.Parse(combo_ProductIDName.Text.Split(':')[1]);
                
                var WH = ent.warehouses.Where(war=>war.w_name== name).ToList().First();
                WH_ID = WH.w_id;

                productExpireDuration = int.Parse(txt_productExpiredt.Text);
                productQuantity = int.Parse(txt_productQuantity.Text);
                bool ok = true;
                if (noteProducts.Count > 0)
                {
                    foreach (var product in noteProducts)
                    {
                        if (product.productID == productID)
                        {
                            ok = false;
                            MessageBox.Show("Product already exists in the list!!");
                        }

                    }
                }
                if (ok)
                {
                    Notedata nt = new Notedata();
                    nt.productID = productID;
                    nt.ProductionDate = dt_productionDate.Value;
                    nt.quantity = productQuantity;
                    nt.ExpirationDuration = productExpireDuration;
                    nt.Wh_ID = WH_ID;
                    noteProducts.Add(nt);
                    string[] item = {txt_permissionID.Text, productID.ToString(),
                    dt_productionDate.Value.ToString(),productExpireDuration+" Months",
                    productQuantity.ToString()};
                    ListViewItem lstI = new ListViewItem(item);
                    lst_supplyPermissionProducts.Items.Add(lstI);
                }


            }
            else
            {
                MessageBox.Show("Enter fields data!!");
            }

        }

        private void txt_productExpiredt_TextChanged(object sender, EventArgs e)
        {
            int months;
            if (txt_productExpiredt.Text != "")
            {
                if (!int.TryParse(txt_productExpiredt.Text, out months))
                {
                    txt_productExpiredt.Text = string.Empty;
                    MessageBox.Show("Enter duration in correct format!!");
                }
                else if (int.Parse(txt_productExpiredt.Text) < 1)
                {
                    txt_productExpiredt.Text = string.Empty;
                    MessageBox.Show("Duration can't be zero or negative!!");
                }

            }

        }

        private void btn_deleteItem_Click(object sender, EventArgs e)
        {
            if (lst_supplyPermissionProducts.SelectedItems.Count > 0)
            {
                int index = lst_supplyPermissionProducts.SelectedItems[0].Index;
                int id = int.Parse(lst_supplyPermissionProducts.SelectedItems[0].SubItems[1].Text);
                var item = noteProducts.Where(nt => nt.productID == id).ToList().First();
                noteProducts.Remove(item);
                lst_supplyPermissionProducts.Items.RemoveAt(index);
            }
        }

        private void btn_SubmitNote_Click(object sender, EventArgs e)
        {

            var result = ent.supplyingPermissions.Where(sup => sup.permission_id == NoteID).ToList().First();
            result.sup_id = Sup_ID;
            result.w_id = war_ID;
            result.permission_date = NoteDate;
            ent.SaveChanges();
            supplyingPermisionProduct supProduts;
            foreach (var item in noteProducts)
            {
                supProduts = new supplyingPermisionProduct()
                {
                    permission_id = NoteID,
                    p_code = item.productID,
                    productuction_date = item.ProductionDate,
                    expire_duration = item.ExpirationDuration,
                    quantity = item.quantity
                };
                ent.supplyingPermisionProducts.Add(supProduts);
            }
            ent.SaveChanges();
            //update stock and product tables with new value
            foreach (var item in noteProducts)
            {
                var stock = ent.Contains.Where(stk => stk.p_code == item.productID && stk.W_code == item.Wh_ID).ToList().First();
                stock.quantity += item.quantity;

                var product = ent.products.Where(pro => pro.p_code == item.productID).ToList().First();
                product.quantity += item.quantity;
                product.production_date = item.ProductionDate;
                product.expire_duration = item.ExpirationDuration;
                ent.SaveChanges();
            }
            ent.SaveChanges();
            hme.DisplaySupplyNote();
            this.Close();

        }
    }
}
