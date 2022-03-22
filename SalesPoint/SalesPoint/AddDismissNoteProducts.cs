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
    public partial class AddDismissNoteProducts : Form
    {

        salesPointEntities1 ent;
        int Sup_ID;
        int war_ID;
        DateTime NoteDate;
        int NoteID;

        public struct Notedata
        {
            public int productID;           
            public int quantity;
            public int Wh_ID;
        }
        List<Notedata> noteProducts;
        Home hme;
        public AddDismissNoteProducts(int customerID, int warehouseID, DateTime NoteDT, Home hm)
        {
            InitializeComponent();
            noteProducts = new List<Notedata>();
            ent = new salesPointEntities1();
            Sup_ID = customerID;
            war_ID = warehouseID;
            NoteDate = NoteDT;
            hme = hm;
            //add temporary item
            dismissingPermission sp = new dismissingPermission();
            ent.dismissingPermissions.Add(sp);
            ent.SaveChanges();
            NoteID = ent.dismissingPermissions.Max(per => per.permission_id);
            //end adding temporary item
            txt_permissionID.Text = NoteID.ToString();
            var result = ent.products.Where(pro => pro.w_id== war_ID).ToList();
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
                        int productID = int.Parse(combo_ProductIDName.Text.Split(':')[1]);
                        string WH_Name = combo_ProductIDName.Text.Split(':')[5];
                        var productQuantity = ent.Contains.Where(cnt=>cnt.p_code== productID && cnt.warehouse.w_name==WH_Name).ToList().First();
                        if (quantity < 1)
                        {
                            txt_productQuantity.Text = string.Empty;
                            MessageBox.Show("Quantity entered is negative or zero!!");
                        }
                        else if(quantity>productQuantity.quantity)
                        {
                            txt_productQuantity.Text = string.Empty;
                            MessageBox.Show("Quantity entered is greater than quatity in stock!!");
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
            var result = ent.dismissingPermissions.Where(sup => sup.permission_id == NoteID).ToList().First();
            ent.dismissingPermissions.Remove(result);
            ent.SaveChanges();
            this.Close();

        }

        private void btn_AddItemtoList_Click(object sender, EventArgs e)
        {
            if (combo_ProductIDName.Text != ""               
                && txt_productQuantity.Text != "")
            {
                int productID;
                int productQuantity;
                int WH_ID;
                string name = combo_ProductIDName.Text.Split(':')[5];
                productID = int.Parse(combo_ProductIDName.Text.Split(':')[1]);

                var WH = ent.warehouses.Where(war => war.w_name == name).ToList().First();
                WH_ID = WH.w_id;
               
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
                    nt.quantity = productQuantity;                   
                    nt.Wh_ID = WH_ID;
                    noteProducts.Add(nt);
                    string[] item = {txt_permissionID.Text, productID.ToString(),                    
                    productQuantity.ToString()};
                    ListViewItem lstI = new ListViewItem(item);
                    lst_dismissPermissionProducts.Items.Add(lstI);
                }


            }
            else
            {
                MessageBox.Show("Enter fields data!!");
            }

        }

        private void btn_deleteItem_Click(object sender, EventArgs e)
        {
            if (lst_dismissPermissionProducts.SelectedItems.Count > 0)
            {
                int index = lst_dismissPermissionProducts.SelectedItems[0].Index;
                int id = int.Parse(lst_dismissPermissionProducts.SelectedItems[0].SubItems[1].Text);
                var item = noteProducts.Where(nt => nt.productID == id).ToList().First();
                noteProducts.Remove(item);
                lst_dismissPermissionProducts.Items.RemoveAt(index);
            }
        }

        private void btn_SubmitNote_Click(object sender, EventArgs e)
        {

            var result = ent.dismissingPermissions.Where(sup => sup.permission_id == NoteID).ToList().First();
            result.cust_id = Sup_ID;
            result.w_id = war_ID;
            result.Permission_Date = NoteDate;
            ent.SaveChanges();
            dismissingPermisionProduct dismissProduts;
            foreach (var item in noteProducts)
            {
                dismissProduts = new dismissingPermisionProduct()
                {
                    permission_id = NoteID,
                    p_code = item.productID,                    
                    quantity = item.quantity
                };
                ent.dismissingPermisionProducts.Add(dismissProduts);
            }
            ent.SaveChanges();
            //update stock and product tables with new value
            foreach (var item in noteProducts)
            {
                var stock = ent.Contains.Where(stk => stk.p_code == item.productID && stk.W_code == item.Wh_ID).ToList().First();
                stock.quantity -= item.quantity;

                var product = ent.products.Where(pro => pro.p_code == item.productID).ToList().First();
                product.quantity -= item.quantity;
                ent.SaveChanges();
            }
            hme.DisplayDismissNotes();
            this.Close();

        }
    }
}
