using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesPoint.CrystalReports;

namespace SalesPoint
{
    public partial class Home : Form
    {
        salesPointEntities1 ent;
        company CompanyInfo;
        List<string> unitsList;
        public struct Data
        {
            public int ProductID;
            public int WareHouse_FromID;
            public int WareHouse_ToID;
            public int ProductQuantityInStock;
            public int ProductQuantityToTransfer;
            public DateTime? ProductionDate;
            public int? ExpireDuration;
            public int subID;
            public DateTime? permissionDate;
        }
        List<Data> dataList;
        public Home()
        {
            InitializeComponent();
            ent = new salesPointEntities1();
            unitsList = new List<string>();
            dataList = new List<Data>();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            var result = ent.companies.Select(cmp => cmp).ToList();
            if (result.Count == 0)
            {
                tabctrl_home.Enabled = false;
                grp_Create.Visible = true;
            }
            else
            {
                lbl_cmpname.Visible = true;
                lbl_welcome.Visible = true;
                lbl_address.Visible = true;
                var company = ent.companies.Select(cmp => cmp).ToList().First();
                lbl_cmpname.Text = company.cm_name + " company";
                lbl_address.Text = "Address: " + company.cmp_address;
                CompanyInfo = ent.companies.Select(cmp => cmp).ToList().First();

            }

        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            string name = txt_cmpname.Text;
            string address = txt_cmpaddress.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Fill all fields first !");
            }
            else
            {
                company cmp = new company() { cm_name = name, cmp_address = address };
                ent.companies.Add(cmp);
                ent.SaveChanges();

                lbl_cmpname.Visible = true;
                lbl_address.Visible = true;
                lbl_cmpname.Text = name + " company";
                lbl_address.Text = "Address: " + address;

                tabctrl_home.Enabled = true;
                grp_Create.Visible = false;
                MessageBox.Show("Company created");
            }
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            DisplayWareHouses();
        }

        private void btn_display_Click(object sender, EventArgs e)
        {
            if (txt_warehouseName.Text != "")
            {
                string name = txt_warehouseName.Text;
                try
                {
                    var result = ent.warehouses.Where(war => war.w_name == name).ToList().First();
                    if (result != null)
                    {
                        txt_warehouseId.Text = result.w_id.ToString();
                        txt_managerName.Text = result.w_manager_name;
                        txt_warehouseName.Text = result.w_name;
                        txt_warehouseaddress.Text = result.w_address;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Not found!!");

                }

            }
            else
            {
                MessageBox.Show("Enter warehouse name to search with!");
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_warehouseId.Text != "")
            {
                int WH_id = int.Parse(txt_warehouseId.Text);
                warehouse WH = ent.warehouses.Where(W => W.w_id == WH_id).ToList().First();                
                //var WHP = ent.WarehouseProductMovings.Where(wh => wh.supplieR_WH == WH.w_id).ToList();    
                
                //foreach (var item in WHP)
                //{
                //    ent.WarehouseProductMovings.Remove(item);
                //}
                //ent.SaveChanges();

                //var SP = ent.supplyingPermissions.Where(s => s.w_id == WH.w_id).ToList();
                //foreach (var item in SP)
                //{
                //    ent.supplyingPermissions.Remove(item);
                //}
                                
                ent.warehouses.Remove(WH);
                
                DisplayWareHouses();
            }
            else
            {
                MessageBox.Show("chooce item to delete!!");
            }
        }
        private void lst_warehouses_MouseClick(object sender, MouseEventArgs e)
        {
            if (lst_warehouses.SelectedItems.Count > 0)
            {
                txt_warehouseId.Text = lst_warehouses.SelectedItems[0].SubItems[0].Text;
                txt_managerName.Text = lst_warehouses.SelectedItems[0].SubItems[1].Text;
                txt_warehouseName.Text = lst_warehouses.SelectedItems[0].SubItems[2].Text;
                txt_warehouseaddress.Text = lst_warehouses.SelectedItems[0].SubItems[3].Text;
            }
        }
        private void DisplayWareHouses()
        {
            lst_warehouses.Items.Clear();
            var result = ent.warehouses.Select(war => war).ToList();
            if (result.Count > 0)
            {

                foreach (var warehouse in result)
                {
                    string[] item = { warehouse.w_id.ToString(), warehouse.w_manager_name,warehouse.w_name
                    ,warehouse.w_address};
                    ListViewItem warehouseItem = new ListViewItem(item);
                    lst_warehouses.Items.Add(warehouseItem);
                }

            }

            txt_warehouseaddress.Text = txt_warehouseId.Text
                = txt_warehouseName.Text = txt_managerName.Text = string.Empty;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_managerName.Text != ""
                && txt_warehouseaddress.Text != ""
                && txt_warehouseId.Text != ""
                && txt_warehouseName.Text != "")
            {
                bool ok = false;
                int WH_ID = int.Parse(txt_warehouseId.Text);
                string Name = txt_warehouseName.Text;
                var result = ent.warehouses.Where(W => W.w_id == WH_ID).ToList();

                foreach (var item in result)
                {
                    if (item.w_id == WH_ID && item.w_name == Name)
                    {
                        ok = true;

                    }
                    else
                    {
                        var WH = ent.warehouses.Where(w => w.w_name == Name).ToList();
                        if (WH.Count > 0)
                        {
                            MessageBox.Show("Warehouse name already exists!");
                        }
                        else
                        {
                            ok = true;
                        }

                    }
                    if (ok)
                    {
                        warehouse WH = ent.warehouses.Where(WHID => WHID.w_id == WH_ID).ToList().First();
                        WH.w_address = txt_warehouseaddress.Text;
                        WH.w_manager_name = txt_managerName.Text;
                        WH.w_name = txt_warehouseName.Text;
                        ent.SaveChanges();
                        DisplayWareHouses();
                    }
                }


            }
            else
            {
                MessageBox.Show("Enter all fields data!!");
            }

        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            if (txt_managerName.Text != ""
               && txt_warehouseaddress.Text != ""
               && txt_warehouseName.Text != "")
            {
                string Name = txt_warehouseName.Text;
                var result = ent.warehouses.Where(W => W.w_name == Name).ToList();
                if (result.Count > 0)
                {
                    MessageBox.Show("Warehouse already exists!");
                }
                else
                {
                    warehouse WH = new warehouse()
                    {
                        w_address = txt_warehouseaddress.Text
                       ,
                        w_manager_name = txt_managerName.Text,
                        w_name = txt_warehouseName.Text,
                        cmp_name = CompanyInfo.cm_name
                    };
                    ent.warehouses.Add(WH);
                    ent.SaveChanges();
                    DisplayWareHouses();
                }


            }
            else
            {
                MessageBox.Show("Enter all fields data!!");
            }
        }

        private void lst_customers_Enter(object sender, EventArgs e)
        {
            DisplayCustomers();
        }

        private void DisplayCustomers()
        {
            lst_customers.Items.Clear();
            var result = ent.customers.Select(cust => cust).ToList();
            if (result.Count > 0)
            {

                foreach (var customer in result)
                {
                    string[] item = { customer.cust_id.ToString(), customer.cust_name,customer.cust_fax
                    ,customer.cust_email,customer.cust_website,customer.cust_phone,customer.cust_mobile};
                    ListViewItem customerItem = new ListViewItem(item);
                    lst_customers.Items.Add(customerItem);
                }

            }

            foreach (GroupBox gb in new GroupBox[] { grp_customers })
            {
                foreach (TextBox tb in gb.Controls.OfType<TextBox>())
                {
                    tb.Text = string.Empty;
                }
            }
        }

        private void btn_customerSearch_Click(object sender, EventArgs e)
        {
            if (txt_customerName.Text != "")
            {
                string name = txt_customerName.Text;
                try
                {
                    var result = ent.customers.Where(cust => cust.cust_name == name).ToList().First();
                    if (result != null)
                    {
                        txt_customerID.Text = result.cust_id.ToString();
                        txt_customerName.Text = result.cust_name;
                        txt_customerEmail.Text = result.cust_email;
                        txt_customerMobile.Text = result.cust_mobile;
                        txt_customerWebsite.Text = result.cust_website;
                        txt_customerPhone.Text = result.cust_phone;
                        txt_customerFax.Text = result.cust_fax;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Not found!!");

                }

            }
            else
            {
                MessageBox.Show("Enter customer name to search with!");
            }
        }

        private void btn_customerInsert_Click(object sender, EventArgs e)
        {
            bool ok = true;

            foreach (GroupBox gb in new GroupBox[] { grp_customers })
            {
                foreach (TextBox tb in gb.Controls.OfType<TextBox>())
                {
                    if (tb.Name != "txt_customerID")
                    {
                        if (string.IsNullOrEmpty(tb.Text))
                        {
                            ok = false;
                        }
                    }


                }
            }
            if (ok)
            {
                customer cust = new customer()
                {
                    cust_email = txt_customerEmail.Text,
                    cust_fax = txt_customerFax.Text,
                    cust_mobile = txt_customerMobile.Text,
                    cust_name = txt_customerName.Text,
                    cust_phone = txt_customerPhone.Text,
                    cust_website = txt_customerWebsite.Text,
                    cmp_name = CompanyInfo.cm_name
                };
                ent.customers.Add(cust);
                ent.SaveChanges();
                DisplayCustomers();
            }
            else
            {
                MessageBox.Show("Enter all fields data!!");
            }

        }
        private void btn_customerUpdate_Click(object sender, EventArgs e)
        {
            bool ok = true;
            foreach (GroupBox gb in new GroupBox[] { grp_customers })
            {
                foreach (TextBox tb in gb.Controls.OfType<TextBox>())
                {
                    if (tb.Name != "txt_customerID")
                    {
                        if (string.IsNullOrEmpty(tb.Text))
                        {
                            ok = false;
                        }
                    }


                }
            }
            if (txt_customerID.Text != "")
            {
                if (ok)
                {
                    int cust_id = int.Parse(txt_customerID.Text);
                    customer cust = ent.customers.Where(cus => cus.cust_id == cust_id).ToList().First();
                    cust.cust_name = txt_customerName.Text;
                    cust.cust_website = txt_customerWebsite.Text;
                    cust.cust_phone = txt_customerPhone.Text;
                    cust.cust_mobile = txt_customerMobile.Text;
                    cust.cust_fax = txt_customerFax.Text;
                    cust.cust_email = txt_customerEmail.Text;

                    ent.SaveChanges();
                    DisplayCustomers();

                }
                else
                {
                    MessageBox.Show("Enter all fields data!!");
                }
            }
            else
            {
                MessageBox.Show("Select customer to update");
            }

        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            DisplayCustomers();
        }

        private void lst_customers_MouseClick(object sender, MouseEventArgs e)
        {
            if (lst_customers.SelectedItems.Count > 0)
            {
                txt_customerID.Text = lst_customers.SelectedItems[0].SubItems[0].Text;
                txt_customerName.Text = lst_customers.SelectedItems[0].SubItems[1].Text;
                txt_customerFax.Text = lst_customers.SelectedItems[0].SubItems[2].Text;
                txt_customerEmail.Text = lst_customers.SelectedItems[0].SubItems[3].Text;
                txt_customerWebsite.Text = lst_customers.SelectedItems[0].SubItems[4].Text;
                txt_customerPhone.Text = lst_customers.SelectedItems[0].SubItems[5].Text;
                txt_customerMobile.Text = lst_customers.SelectedItems[0].SubItems[6].Text;

            }
        }

        private void btn_customerDelete_Click(object sender, EventArgs e)
        {
            if (txt_customerID.Text != "")
            {
                int cust_id = int.Parse(txt_customerID.Text);
                customer cust = ent.customers.Where(cu => cu.cust_id == cust_id).ToList().First();
                ent.customers.Remove(cust);
                ent.SaveChanges();
                DisplayCustomers();
            }
            else
            {
                MessageBox.Show("chooce item to delete!!");
            }
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            DisplaySuppliers();
        }

        private void DisplaySuppliers()
        {
            lst_suppliers.Items.Clear();
            var result = ent.suppliers.Select(sup => sup).ToList();
            if (result.Count > 0)
            {

                foreach (var supplier in result)
                {
                    string[] item = { supplier.sup_id.ToString(), supplier.sup_name,supplier.sup_fax
                    ,supplier.sup_email,supplier.sup_website,supplier.sup_phone,supplier.sup_mobile };
                    ListViewItem supplierItem = new ListViewItem(item);
                    lst_suppliers.Items.Add(supplierItem);
                }

            }

            foreach (GroupBox gb in new GroupBox[] { grp_suppliers })
            {
                foreach (TextBox tb in gb.Controls.OfType<TextBox>())
                {
                    tb.Text = string.Empty;
                }
            }
        }

        private void btn_searchSupplier_Click(object sender, EventArgs e)
        {
            if (txt_supplierName.Text != "")
            {
                string name = txt_supplierName.Text;
                try
                {
                    var result = ent.suppliers.Where(supp => supp.sup_name == name).ToList().First();
                    if (result != null)
                    {
                        txt_supplierID.Text = result.sup_id.ToString();
                        txt_supplierName.Text = result.sup_name;
                        txt_supplierEmail.Text = result.sup_email;
                        txt_supplierMobile.Text = result.sup_mobile;
                        txt_supplierWebsite.Text = result.sup_website;
                        txt_supplierPhone.Text = result.sup_phone;
                        txt_supplierFax.Text = result.sup_fax;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Not found!!");

                }

            }
            else
            {
                MessageBox.Show("Enter supplier name to search with!");
            }
        }

        private void btn_deleteSupplier_Click(object sender, EventArgs e)
        {
            if (txt_supplierName.Text != "")
            {
                int sup_id = int.Parse(txt_supplierID.Text);
                supplier supp = ent.suppliers.Where(sup => sup.sup_id == sup_id).ToList().First();
                ent.suppliers.Remove(supp);
                ent.SaveChanges();
                DisplaySuppliers();
            }
            else
            {
                MessageBox.Show("chooce item to delete!!");
            }
        }

        private void btn_insertSupplier_Click(object sender, EventArgs e)
        {
            bool ok = true;

            foreach (GroupBox gb in new GroupBox[] { grp_suppliers })
            {
                foreach (TextBox tb in gb.Controls.OfType<TextBox>())
                {
                    if (tb.Name != "txt_supplierID")
                    {
                        if (string.IsNullOrEmpty(tb.Text))
                        {
                            ok = false;
                        }
                    }


                }
            }
            if (ok)
            {
                supplier supp = new supplier()
                {
                    sup_email = txt_supplierEmail.Text,
                    sup_fax = txt_supplierFax.Text,
                    sup_mobile = txt_supplierMobile.Text,
                    sup_name = txt_supplierName.Text,
                    sup_phone = txt_supplierPhone.Text,
                    sup_website = txt_supplierWebsite.Text,
                    cmp_name = CompanyInfo.cm_name
                };
                ent.suppliers.Add(supp);
                ent.SaveChanges();
                DisplaySuppliers();
            }
            else
            {
                MessageBox.Show("Enter all fields data!!");
            }
        }

        private void lst_suppliers_MouseClick(object sender, MouseEventArgs e)
        {
            if (lst_suppliers.SelectedItems.Count > 0)
            {
                txt_supplierID.Text = lst_suppliers.SelectedItems[0].SubItems[0].Text;
                txt_supplierName.Text = lst_suppliers.SelectedItems[0].SubItems[1].Text;
                txt_supplierFax.Text = lst_suppliers.SelectedItems[0].SubItems[2].Text;
                txt_supplierEmail.Text = lst_suppliers.SelectedItems[0].SubItems[3].Text;
                txt_supplierWebsite.Text = lst_suppliers.SelectedItems[0].SubItems[4].Text;
                txt_supplierPhone.Text = lst_suppliers.SelectedItems[0].SubItems[5].Text;
                txt_supplierMobile.Text = lst_suppliers.SelectedItems[0].SubItems[6].Text;

            }
        }

        private void btn_updateSupplier_Click(object sender, EventArgs e)
        {
            bool ok = true;
            foreach (GroupBox gb in new GroupBox[] { grp_suppliers })
            {
                foreach (TextBox tb in gb.Controls.OfType<TextBox>())
                {
                    if (tb.Name != "txt_supplierID")
                    {
                        if (string.IsNullOrEmpty(tb.Text))
                        {
                            ok = false;
                        }
                    }


                }
            }
            if (txt_supplierID.Text != "")
            {
                if (ok)
                {
                    int sup_id = int.Parse(txt_supplierID.Text);
                    supplier sup = ent.suppliers.Where(cus => cus.sup_id == sup_id).ToList().First();
                    sup.sup_name = txt_supplierName.Text;
                    sup.sup_website = txt_supplierWebsite.Text;
                    sup.sup_phone = txt_supplierPhone.Text;
                    sup.sup_mobile = txt_supplierMobile.Text;
                    sup.sup_fax = txt_supplierFax.Text;
                    sup.sup_email = txt_supplierEmail.Text;

                    ent.SaveChanges();
                    DisplaySuppliers();

                }
                else
                {
                    MessageBox.Show("Enter all fields data!!");
                }
            }
            else
            {
                MessageBox.Show("Select supplier to update");
            }


        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            DisplaySupplyNote();
        }
        public void DisplaySupplyNote()
        {
            lst_supplyNote.Items.Clear();
            var result = ent.supplyingPermissions.Select(sup => sup).ToList();
            if (result.Count > 0)
            {
                foreach (var supplyNote in result)
                {
                    if (supplyNote.sup_id != null)
                    {
                        string[] item = { supplyNote.permission_id.ToString(),
                        supplyNote.supplier.sup_name,supplyNote.warehouse.w_name,
                        supplyNote.permission_date.ToString() };
                        ListViewItem supplyNoteItem = new ListViewItem(item);
                        lst_supplyNote.Items.Add(supplyNoteItem);
                    }

                }

            }

            txt_supplyNoteID.Text = string.Empty;
        }
        private void cmbo_supplyNoteSupplier_DropDown(object sender, EventArgs e)
        {
            cmbo_supplyNoteSupplier.Items.Clear();
            var result = ent.suppliers.Select(supNote => supNote).ToList();
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    cmbo_supplyNoteSupplier.Items.Add($"ID:{item.sup_id}:Name:{item.sup_name}");
                }
            }

        }

        private void cmbo_supplyNoteWarehouse_DropDown(object sender, EventArgs e)
        {
            cmbo_supplyNoteWarehouse.Items.Clear();
            var result = ent.warehouses.Select(WH => WH).ToList();
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    cmbo_supplyNoteWarehouse.Items.Add($"ID:{item.w_id}:Name:{item.w_name}");
                }
            }
        }

        private void btn_supplyNoteInsert_Click(object sender, EventArgs e)
        {
            if (cmbo_supplyNoteSupplier.Text != "" && cmbo_supplyNoteWarehouse.Text
                != "" && dt_supplyNoteDate.Text != "")
            {
                int supID;
                int WH_ID;
                DateTime PerDate;
                supID = int.Parse(cmbo_supplyNoteSupplier.Text.Split(':')[1]);
                WH_ID = int.Parse(cmbo_supplyNoteWarehouse.Text.Split(':')[1]);
                PerDate = dt_supplyNoteDate.Value;

                supplyingPermission supNote = new supplyingPermission()
                {
                    sup_id = supID,
                    w_id = WH_ID,
                    permission_date = PerDate
                };
                ent.supplyingPermissions.Add(supNote);
                ent.SaveChanges();
                DisplaySupplyNote();
            }
            else
            {
                MessageBox.Show("Select all fields first !!");
            }
        }

        private void btn_supplyNoteSearch_Click(object sender, EventArgs e)
        {
            if (cmbo_supplyNoteSupplier.Text != "" && cmbo_supplyNoteWarehouse.Text != "")
            {
                string supplier = cmbo_supplyNoteSupplier.Text.Split(':')[3];
                string warehouse = cmbo_supplyNoteWarehouse.Text.Split(':')[3];
                var result = ent.supplyingPermissions.Where(supnote => supnote.supplier.sup_name == supplier
                && supnote.warehouse.w_name == warehouse).ToList();

                if (result.Count > 0)
                {
                    lst_supplyNote.Items.Clear();
                    foreach (var supplyNote in result)
                    {
                        string[] item = { supplyNote.permission_id.ToString(),
                        supplyNote.supplier.sup_name,supplyNote.warehouse.w_name,
                        supplyNote.permission_date.ToString() };
                        ListViewItem supplyNoteItem = new ListViewItem(item);
                        lst_supplyNote.Items.Add(supplyNoteItem);
                    }
                }
                else
                {
                    MessageBox.Show("Not found");
                }
            }
            else if (cmbo_supplyNoteSupplier.Text != "" && cmbo_supplyNoteWarehouse.Text == "")
            {
                string supplier = cmbo_supplyNoteSupplier.Text.Split(':')[3];
                var result = ent.supplyingPermissions.Where(supnote => supnote.supplier.sup_name == supplier).ToList();

                if (result.Count > 0)
                {
                    lst_supplyNote.Items.Clear();
                    foreach (var supplyNote in result)
                    {
                        string[] item = { supplyNote.permission_id.ToString(),
                        supplyNote.supplier.sup_name,supplyNote.warehouse.w_name,
                        supplyNote.permission_date.ToString() };
                        ListViewItem supplyNoteItem = new ListViewItem(item);
                        lst_supplyNote.Items.Add(supplyNoteItem);
                    }
                }
                else
                {
                    MessageBox.Show("Not found");
                }

            }
            else if (cmbo_supplyNoteSupplier.Text == "" && cmbo_supplyNoteWarehouse.Text != "")
            {
                string warehouse = cmbo_supplyNoteWarehouse.Text.Split(':')[3];
                var result = ent.supplyingPermissions.Where(supnote => supnote.warehouse.w_name == warehouse).ToList();

                if (result.Count > 0)
                {
                    lst_supplyNote.Items.Clear();
                    foreach (var supplyNote in result)
                    {
                        string[] item = { supplyNote.permission_id.ToString(),
                        supplyNote.supplier.sup_name,supplyNote.warehouse.w_name,
                        supplyNote.permission_date.ToString() };
                        ListViewItem supplyNoteItem = new ListViewItem(item);
                        lst_supplyNote.Items.Add(supplyNoteItem);
                    }
                }
                else
                {
                    MessageBox.Show("Not found");
                }
            }
            else
            {
                MessageBox.Show("Select item(s)[supplier - warehouse] to search with!!");
            }
        }

        private void btn_supplyNoteDelete_Click(object sender, EventArgs e)
        {
            if (txt_supplyNoteID.Text != "")
            {
                int supNote_ID = int.Parse(txt_supplyNoteID.Text);
                supplyingPermission supPer = ent.supplyingPermissions.Where(supnote => supnote.permission_id == supNote_ID).ToList().First();
                ent.supplyingPermissions.Remove(supPer);
                ent.SaveChanges();
                DisplaySupplyNote();
            }
            else
            {
                MessageBox.Show("Select item to delete!");
            }
        }

        private void btn_supplyNoteUpdate_Click(object sender, EventArgs e)
        {
            if (txt_supplyNoteID.Text != ""
                && cmbo_supplyNoteSupplier.Text != ""
                && cmbo_supplyNoteWarehouse.Text != "")
            {
                int SupNote_id = int.Parse(txt_supplyNoteID.Text);
                supplyingPermission sup_Note = ent.supplyingPermissions.Where(supNote => supNote.permission_id == SupNote_id).ToList().First();

                sup_Note.sup_id = int.Parse(cmbo_supplyNoteSupplier.Text.Split(':')[1]);
                sup_Note.w_id = int.Parse(cmbo_supplyNoteWarehouse.Text.Split(':')[1]);
                sup_Note.permission_date = dt_supplyNoteDate.Value;
                ent.SaveChanges();
                DisplaySupplyNote();
            }
            else
            {
                MessageBox.Show("Select all fields data!!");
            }
        }

        private void lst_supplyNote_MouseClick(object sender, MouseEventArgs e)
        {
            if (lst_supplyNote.SelectedItems.Count > 0)
            {
                txt_supplyNoteID.Text = lst_supplyNote.SelectedItems[0].SubItems[0].Text;
            }
        }

        private void btn_supplyNoteDisplayAll_Click(object sender, EventArgs e)
        {
            DisplaySupplyNote();
        }

        private void btn_addProducts_Click(object sender, EventArgs e)
        {
            if (
                cmbo_supplyNoteSupplier.Text != ""
                && cmbo_supplyNoteWarehouse.Text != "")
            {
                int supplierID = int.Parse(cmbo_supplyNoteSupplier.Text.Split(':')[1]);
                int warehouseID = int.Parse(cmbo_supplyNoteWarehouse.Text.Split(':')[1]);
                DateTime NoteDate = dt_supplyNoteDate.Value;
                AddSupplyNoteProducts frm = new AddSupplyNoteProducts(supplierID, warehouseID, NoteDate, this);
                frm.Show();

            }
            else
            {
                MessageBox.Show("Enter all fields first!");
            }

        }

        private void tabPage8_Enter(object sender, EventArgs e)
        {
            displayUnits();
        }
        private void displayUnits()
        {
            lst_units.Items.Clear();

            var result = ent.measureUnits.Select(units => units).ToList();
            if (result.Count > 0)
            {

                foreach (var unit in result)
                {
                    string[] item = { unit.m_id.ToString(), unit.m_unit };
                    ListViewItem unitItem = new ListViewItem(item);
                    lst_units.Items.Add(unitItem);
                }

            }
            txt_unitID.Text = string.Empty;
        }

        private void btn_unitInsert_Click(object sender, EventArgs e)
        {
            if (cmbo_unit.Text != "")
            {
                string unit;
                unit = cmbo_unit.Text;

                var result = ent.measureUnits.Where(u => u.m_unit == unit).ToList();
                if (result.Count == 0)
                {
                    measureUnit Munit = new measureUnit() { m_unit = unit };
                    ent.measureUnits.Add(Munit);
                    ent.SaveChanges();
                    displayUnits();
                }
                else
                {
                    MessageBox.Show("Unit already exists in database");
                }

            }
            else
            {
                MessageBox.Show("Enter missing field first !!");
            }
        }

        private void btn_addUnitToList_Click(object sender, EventArgs e)
        {
            if (txt_newUnit.Text != "")
            {
                cmbo_unit.Items.Add(txt_newUnit.Text);
                txt_newUnit.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Enter unit first!!");
            }
        }

        private void lst_units_MouseClick(object sender, MouseEventArgs e)
        {
            if (lst_units.SelectedItems.Count > 0)
            {
                txt_unitID.Text = lst_units.SelectedItems[0].SubItems[0].Text;
            }
        }

        private void btn_unitDelete_Click(object sender, EventArgs e)
        {
            if (txt_unitID.Text != "")
            {
                int unitID = int.Parse(txt_unitID.Text);
                measureUnit unit = ent.measureUnits.Where(un => un.m_id == unitID).ToList().First();
                ent.measureUnits.Remove(unit);
                ent.SaveChanges();
                displayUnits();
            }
            else
            {
                MessageBox.Show("Select unit to delete");
            }
        }

        private void tabPage7_Enter(object sender, EventArgs e)
        {
            Displayproducts();
        }
        private void Displayproducts()
        {
            lst_product.Items.Clear();

            var products = ent.products.Select(prod => prod).ToList();
            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    var result = ent.Product_unit.Where(u => u.p_code == product.p_code).ToList();
                    string units = "";
                    foreach (var unit in result)
                    {
                        units += unit.measureUnit.m_unit + " ,";
                    }
                    units = units.TrimEnd(',');                    
                    string[] item = { product.p_code.ToString(), product.p_name, product.quantity.ToString(), units, product.w_id.ToString(), product.production_date.ToString(), product.expire_duration.ToString(),product.transfered.ToString()};
                    ListViewItem productItem = new ListViewItem(item);
                    lst_product.Items.Add(productItem);
                }

            }

            foreach (GroupBox gb in new GroupBox[] { grp_product })
            {
                foreach (TextBox tb in gb.Controls.OfType<TextBox>())
                {
                    tb.Text = string.Empty;
                }
            }
            lbl_units.Text = string.Empty;

        }

        private void btn_productInsert_Click(object sender, EventArgs e)
        {
            if (txt_productQuantity.Text == "")
            {
                txt_productQuantity.Text = "0";
            }
            if (txt_productName.Text != ""
                 && combo_wareHouses.Text != ""
                )
            {

                if (unitsList.Count > 0)
                {
                    string name;
                    int Pquantity;
                    int warehouseID;
                    if (int.TryParse(txt_productQuantity.Text, out Pquantity))
                    {
                        name = txt_productName.Text;
                        warehouseID = int.Parse(combo_wareHouses.Text.Split(':')[1]);

                        //insert products
                        product pdct = new product() { p_name = name, quantity = Pquantity, w_id = warehouseID };
                        ent.products.Add(pdct);
                        ent.SaveChanges();

                        var lastID = ent.products.Max(p => p.p_code);
                        //add product to stock
                        Contain cnt = new Contain() { p_code = lastID, quantity = Pquantity, W_code = warehouseID };
                        ent.Contains.Add(cnt);
                        ent.SaveChanges();
                        //insert units in produc-unit table
                        foreach (var item in unitsList)
                        {
                            var unitID = ent.measureUnits.Where(unit => unit.m_unit == item).ToList().First();
                            Product_unit punit = new Product_unit() { p_code = lastID, m_id = unitID.m_id };
                            ent.Product_unit.Add(punit);


                        }

                        ent.SaveChanges();
                        unitsList.Clear();
                        Displayproducts();
                    }
                    else { MessageBox.Show("Enter quantity in right format (numbers only)"); }

                }
                else
                {
                    MessageBox.Show("Must assign at least one measure unit!!");
                }

            }
            else
            {
                MessageBox.Show("Enter missing fields !!");
            }
        }

        private void btn_assignUnits_Click(object sender, EventArgs e)
        {
            if (cmbo_Assignunits.Text != "")
            {
                string unit = cmbo_Assignunits.Text;
                if (unitsList.Contains(unit))
                {
                    MessageBox.Show("unit already assigned");
                }
                else
                {
                    unitsList.Add(unit);
                    lbl_units.Text += unit + " , ";
                }

            }
            else
            {
                MessageBox.Show("Select unit first!");
            }

        }

        private void cmbo_units_DropDown(object sender, EventArgs e)
        {
            cmbo_Assignunits.Items.Clear();
            var units = ent.measureUnits.Select(u => u.m_unit).ToList();
            foreach (var item in units)
            {
                cmbo_Assignunits.Items.Add(item);
            }
        }

        private void lst_product_MouseClick(object sender, MouseEventArgs e)
        {
            if (lst_product.SelectedItems.Count > 0)
            {
                txt_productID.Text = lst_product.SelectedItems[0].SubItems[0].Text;
                txt_productName.Text = lst_product.SelectedItems[0].SubItems[1].Text;
                txt_productQuantity.Text = lst_product.SelectedItems[0].SubItems[2].Text;
                txt_productUnits.Text = lst_product.SelectedItems[0].SubItems[3].Text;
            }

        }

        private void btn_productSearch_Click(object sender, EventArgs e)
        {
            if (txt_productName.Text != "")
            {
                string name = txt_productName.Text;

                var productsList = ent.products.Where(pro => pro.p_name == name).ToList();
                if (productsList.Count > 0)
                {
                    lst_product.Items.Clear();

                    foreach (var product in productsList)
                    {
                        var result = ent.Product_unit.Where(u => u.p_code == product.p_code).ToList();
                        string units = "";
                        foreach (var unit in result)
                        {
                            units += unit.measureUnit.m_unit + " ,";
                        }
                        units = units.TrimEnd(',');
                        string[] item = { product.p_code.ToString(), product.p_name, product.quantity.ToString(), units };
                        ListViewItem productItem = new ListViewItem(item);
                        lst_product.Items.Add(productItem);
                    }
                }
                else
                {
                    MessageBox.Show("Not found!!");
                }
            }
            else
            {
                MessageBox.Show("Enter product name to search with!");
            }
        }

        private void btn_productUpdate_Click(object sender, EventArgs e)
        {

            if (txt_productID.Text != "" && txt_productName.Text != "")
            {
                int product_id = int.Parse(txt_productID.Text);
                product prodct = ent.products.Where(pro => pro.p_code == product_id).ToList().First();
                prodct.p_name = txt_productName.Text;
                ent.SaveChanges();
                if (txt_productQuantity.Text != "")
                {
                    MessageBox.Show("Item name updated , but quantity can't be updated from here.\nUse supply note or dismiss note instead.");
                }
                Displayproducts();

            }
            else
            {
                MessageBox.Show("Select product to update and Enter all fields data");
            }

        }

        private void btn_productDelete_Click(object sender, EventArgs e)
        {
            if (txt_productID.Text != "")
            {
                DialogResult DR = MessageBox.Show("Are you sure?\nThis will delete the product and all RELATED DOCUMENT\nBe carefull!!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (DR == DialogResult.OK)
                {
                    int productID = int.Parse(txt_productID.Text);
                    product productItem = ent.products.Where(pro => pro.p_code == productID).ToList().First();
                    ent.products.Remove(productItem);
                    ent.SaveChanges();
                    Displayproducts();
                }

            }
            else
            {
                MessageBox.Show("Select product to delete");
            }
        }

        private void btn_productDisplayAll_Click(object sender, EventArgs e)
        {
            Displayproducts();
        }

        private void lst_supplyNote_MouseLeave(object sender, EventArgs e)
        {
            pnl_supplyProducts.Visible = false;
        }

        private void lst_supplyNote_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (lst_supplyNote.SelectedItems.Count > 0)
            {
                lst_supplyProducts.Items.Clear();
                pnl_supplyProducts.Visible = true;
                pnl_supplyProducts.BringToFront();
                int supplyNoteId = int.Parse(lst_supplyNote.SelectedItems[0].SubItems[0].Text);
                var result = ent.supplyingPermisionProducts.Where(supPerPro => supPerPro.permission_id == supplyNoteId).ToList();
                if (result.Count > 0)
                {
                    foreach (var SupPerPro in result)
                    {
                        string[] item = { SupPerPro.permission_id.ToString(), SupPerPro.p_code.ToString(),
                    SupPerPro.productuction_date.ToString(),SupPerPro.expire_duration.ToString(),SupPerPro.quantity.ToString()};
                        ListViewItem Lstitem = new ListViewItem(item);
                        lst_supplyProducts.Items.Add(Lstitem);
                    }

                }

            }
        }

        private void combo_wareHouses_DropDown(object sender, EventArgs e)
        {
            var result = ent.warehouses.Select(war => war).ToList();
            if (result.Count > 0)
            {
                combo_wareHouses.Items.Clear();
                foreach (var item in result)
                {
                    combo_wareHouses.Items.Add($"ID:{item.w_id}:Name:{item.w_name}");
                }
            }
        }

        private void tabPage6_Enter(object sender, EventArgs e)
        {
            DisplayDismissNotes();
        }
        public void DisplayDismissNotes()
        {
            lst_dismissNote.Items.Clear();
            var result = ent.dismissingPermissions.Select(dis => dis).ToList();
            if (result.Count > 0)
            {
                foreach (var dismissNote in result)
                {
                    if (dismissNote.cust_id != null)
                    {
                        string[] item = { dismissNote.permission_id.ToString(),
                        dismissNote.customer.cust_name,dismissNote.warehouse.w_name,
                        dismissNote.Permission_Date.ToString() };
                        ListViewItem dismissNoteItem = new ListViewItem(item);
                        lst_dismissNote.Items.Add(dismissNoteItem);
                    }

                }

            }

            txt_dismissID.Text = string.Empty;
        }

        private void combo_dismissCustomer_DropDown(object sender, EventArgs e)
        {
            combo_dismissCustomer.Items.Clear();
            var result = ent.customers.Select(cusNote => cusNote).ToList();
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    combo_dismissCustomer.Items.Add($"ID:{item.cust_id}:Name:{item.cust_name}");
                }
            }
        }

        private void combo_dismissWarehouse_DropDown(object sender, EventArgs e)
        {
            combo_dismissWarehouse.Items.Clear();
            var result = ent.warehouses.Select(WH => WH).ToList();
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    combo_dismissWarehouse.Items.Add($"ID:{item.w_id}:Name:{item.w_name}");
                }
            }
        }

        private void lst_dismissNote_MouseClick(object sender, MouseEventArgs e)
        {
            if (lst_dismissNote.SelectedItems.Count > 0)
            {
                txt_dismissID.Text = lst_dismissNote.SelectedItems[0].SubItems[0].Text;
            }
        }

        private void btn_dismissSearch_Click(object sender, EventArgs e)
        {
            if (combo_dismissCustomer.Text != "" && combo_dismissWarehouse.Text != "")
            {
                string customer = combo_dismissCustomer.Text.Split(':')[3];
                string warehouse = combo_dismissWarehouse.Text.Split(':')[3];
                var result = ent.dismissingPermissions.Where(supnote => supnote.customer.cust_name == customer
                && supnote.warehouse.w_name == warehouse).ToList();

                if (result.Count > 0)
                {
                    lst_dismissNote.Items.Clear();
                    foreach (var dismissNote in result)
                    {
                        string[] item = { dismissNote.permission_id.ToString(),
                        dismissNote.customer.cust_name,dismissNote.warehouse.w_name,
                        dismissNote.Permission_Date.ToString() };
                        ListViewItem dismissNoteItem = new ListViewItem(item);
                        lst_dismissNote.Items.Add(dismissNoteItem);
                    }
                }
                else
                {
                    MessageBox.Show("Not found");
                }
            }
            else if (combo_dismissCustomer.Text != "" && combo_dismissWarehouse.Text == "")
            {
                string customer = combo_dismissCustomer.Text.Split(':')[3];
                var result = ent.dismissingPermissions.Where(cusnote => cusnote.customer.cust_name == customer).ToList();

                if (result.Count > 0)
                {
                    lst_dismissNote.Items.Clear();
                    foreach (var dismissNote in result)
                    {
                        string[] item = { dismissNote.permission_id.ToString(),
                        dismissNote.customer.cust_name,dismissNote.warehouse.w_name,
                        dismissNote.Permission_Date.ToString() };
                        ListViewItem dismissNoteItem = new ListViewItem(item);
                        lst_dismissNote.Items.Add(dismissNoteItem);
                    }
                }
                else
                {
                    MessageBox.Show("Not found");
                }

            }
            else if (combo_dismissCustomer.Text == "" && combo_dismissWarehouse.Text != "")
            {
                string warehouse = combo_dismissWarehouse.Text.Split(':')[3];
                var result = ent.dismissingPermissions.Where(supnote => supnote.warehouse.w_name == warehouse).ToList();

                if (result.Count > 0)
                {
                    lst_supplyNote.Items.Clear();
                    foreach (var dismissNote in result)
                    {
                        string[] item = { dismissNote.permission_id.ToString(),
                        dismissNote.customer.cust_name,dismissNote.warehouse.w_name,
                        dismissNote.Permission_Date.ToString() };
                        ListViewItem dismissNoteItem = new ListViewItem(item);
                        lst_dismissNote.Items.Add(dismissNoteItem);
                    }
                }
                else
                {
                    MessageBox.Show("Not found");
                }
            }
            else
            {
                MessageBox.Show("Select item(s)[customer - warehouse] to search with!!");
            }
        }

        private void btn_dismissDisplayAll_Click(object sender, EventArgs e)
        {
            DisplayDismissNotes();
        }

        private void btn_dismissDelete_Click(object sender, EventArgs e)
        {
            if (txt_dismissID.Text != "")
            {
                int dissNote_ID = int.Parse(txt_dismissID.Text);
                dismissingPermission dissPer = ent.dismissingPermissions.Where(dissnote => dissnote.permission_id == dissNote_ID).ToList().First();
                ent.dismissingPermissions.Remove(dissPer);
                ent.SaveChanges();
                DisplayDismissNotes();
            }
            else
            {
                MessageBox.Show("Select item to delete!");
            }
        }

        private void btn_dismissUpdate_Click(object sender, EventArgs e)
        {
            if (txt_dismissID.Text != ""
                && combo_dismissCustomer.Text != ""
                && combo_dismissWarehouse.Text != "")
            {
                int dismissNote_id = int.Parse(txt_dismissID.Text);
                dismissingPermission diss_Note = ent.dismissingPermissions.Where(dissNote => dissNote.permission_id == dismissNote_id).ToList().First();

                diss_Note.cust_id = int.Parse(combo_dismissCustomer.Text.Split(':')[1]);
                diss_Note.w_id = int.Parse(combo_dismissWarehouse.Text.Split(':')[1]);
                diss_Note.Permission_Date = dt_dismissDT.Value;
                ent.SaveChanges();
                DisplayDismissNotes();
            }
            else
            {
                MessageBox.Show("Select all fields data!!");
            }
        }

        private void btn_dismissInsert_Click(object sender, EventArgs e)
        {
            if (combo_dismissCustomer.Text != "" && combo_dismissWarehouse.Text
                != "" && dt_dismissDT.Text != "")
            {
                int dismissID;
                int WH_ID;
                DateTime PerDate;
                dismissID = int.Parse(combo_dismissCustomer.Text.Split(':')[1]);
                WH_ID = int.Parse(combo_dismissWarehouse.Text.Split(':')[1]);
                PerDate = dt_dismissDT.Value;
                dismissingPermission dissNote = new dismissingPermission()
                {
                    cust_id = dismissID,
                    w_id = WH_ID,
                    Permission_Date = PerDate
                };
                ent.dismissingPermissions.Add(dissNote);
                ent.SaveChanges();
                DisplayDismissNotes();
            }
            else
            {
                MessageBox.Show("Select all fields first !!");
            }
        }

        private void lst_dismissNote_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lst_dismissNote.SelectedItems.Count > 0)
            {
                lst_dismissNotesProducts.Items.Clear();
                pnl_dismissNotes.Visible = true;
                pnl_dismissNotes.BringToFront();
                int dismissNoteId = int.Parse(lst_dismissNote.SelectedItems[0].SubItems[0].Text);
                var result = ent.dismissingPermisionProducts.Where(dissPerPro => dissPerPro.permission_id == dismissNoteId).ToList();
                if (result.Count > 0)
                {
                    foreach (var dissPerPro in result)
                    {
                        string[] item = { dissPerPro.permission_id.ToString(), dissPerPro.p_code.ToString(),
                    dissPerPro.quantity.ToString()};
                        ListViewItem Lstitem = new ListViewItem(item);
                        lst_dismissNotesProducts.Items.Add(Lstitem);
                    }

                }

            }
        }

        private void lst_dismissNote_MouseLeave(object sender, EventArgs e)
        {
            pnl_dismissNotes.Visible = false;
        }

        private void btn_dismissAdd_Click(object sender, EventArgs e)
        {
            if (
               combo_dismissCustomer.Text != ""
               && combo_dismissWarehouse.Text != "")
            {
                int customerID = int.Parse(combo_dismissCustomer.Text.Split(':')[1]);
                int warehouseID = int.Parse(combo_dismissWarehouse.Text.Split(':')[1]);
                DateTime NoteDate = dt_dismissDT.Value;
                AddDismissNoteProducts frm = new AddDismissNoteProducts(customerID, warehouseID, NoteDate, this);
                frm.Show();

            }
            else
            {
                MessageBox.Show("Enter all fields first!");
            }
        }

        private void tabPage9_Enter(object sender, EventArgs e)
        {
            DisplayAllStock();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string IDText = txt_stock_ID.Text, name = txt_stock_Name.Text, warehouse = txt_stock_warehouse.Text;
            if (IDText != "" && name != "" && warehouse != "")
            {
                int ID;
                if (int.TryParse(IDText, out ID))
                {
                    var result = ent.Contains.Where(cnt => cnt.p_code == ID &&
                    cnt.warehouse.w_name.Contains(warehouse) && cnt.product.p_name.Contains(name)).ToList();
                    displaySearchResult(result);
                }
                else
                {
                    MessageBox.Show("Invalid format of ID!!");
                    txt_stock_ID.Text = string.Empty;
                }

            }
            else if (IDText == "" && name != "" && warehouse != "")
            {

                var result = ent.Contains.Where(cnt =>
                cnt.warehouse.w_name.Contains(warehouse) && cnt.product.p_name.Contains(name)).ToList();
                displaySearchResult(result);

            }
            else if (IDText != "" && name == "" && warehouse != "")
            {
                int ID;
                if (int.TryParse(IDText, out ID))
                {
                    var result = ent.Contains.Where(cnt => cnt.p_code == ID &&
                    cnt.warehouse.w_name.Contains(warehouse)).ToList();
                    displaySearchResult(result);
                }
                else
                {
                    MessageBox.Show("Invalid format of ID!!");
                    txt_stock_ID.Text = string.Empty;
                }
            }
            else if (IDText != "" && name != "" && warehouse == "")
            {
                int ID;
                if (int.TryParse(IDText, out ID))
                {
                    var result = ent.Contains.Where(cnt => cnt.p_code == ID && cnt.product.p_name.Contains(name)).ToList();
                    displaySearchResult(result);
                }
                else
                {
                    MessageBox.Show("Invalid format of ID!!");
                    txt_stock_ID.Text = string.Empty;
                }
            }
            else if (IDText != "" && name == "" && warehouse == "")
            {
                int ID;
                if (int.TryParse(IDText, out ID))
                {
                    var result = ent.Contains.Where(cnt => cnt.p_code == ID).ToList();
                    displaySearchResult(result);
                }
                else
                {
                    MessageBox.Show("Invalid format of ID!!");
                    txt_stock_ID.Text = string.Empty;
                }
            }
            else if (IDText == "" && name != "" && warehouse == "")
            {

                var result = ent.Contains.Where(cnt => cnt.product.p_name.Contains(name)).ToList();
                displaySearchResult(result);

            }
            else if (IDText == "" && name == "" && warehouse != "")
            {

                var result = ent.Contains.Where(cnt => cnt.warehouse.w_name.Contains(warehouse)).ToList();
                displaySearchResult(result);

            }
            else
            {
                MessageBox.Show("Enter value to search with !");
            }
        }
        private void displaySearchResult(List<Contain> cnt)
        {
            lst_stock.Items.Clear();
            foreach (var stock in cnt)
            {
                string[] item = {stock.warehouse.w_name,
                        stock.p_code.ToString(),
                        stock.product.p_name,stock.quantity.ToString() };
                ListViewItem lst = new ListViewItem(item);
                lst_stock.Items.Add(lst);
            }
        }

        private void btn_stockAll_Click(object sender, EventArgs e)
        {
            DisplayAllStock();
        }

        private void DisplayAllStock()
        {
            lst_stock.Items.Clear();
            var result = ent.Contains.Select(cnt => cnt).ToList();
            if (result.Count > 0)
            {
                foreach (var stock in result)
                {
                    string[] item = {stock.warehouse.w_name,
                        stock.p_code.ToString(),
                        stock.product.p_name,stock.quantity.ToString(),stock.transfered.ToString() };
                    ListViewItem lst = new ListViewItem(item);
                    lst_stock.Items.Add(lst);

                }
            }
        }

        private void combo_From_DropDown(object sender, EventArgs e)
        {
            bool filter = false;
            int WH_ID = 0;
            List<warehouse> result;
            combo_From.Items.Clear();
            if (combo_To.Text != "")
            {
                filter = true;
                WH_ID = int.Parse(combo_To.Text.Split(':')[1]);
            }
            if (filter)
            {
                result = ent.warehouses.Where(war => war.w_id != WH_ID).ToList();
            }
            else
            {
                result = ent.warehouses.Select(war => war).ToList();
            }


            foreach (var item in result)
            {
                combo_From.Items.Add($"ID:{item.w_id}:Name:{item.w_name}");
            }
        }

        private void combo_To_DropDown(object sender, EventArgs e)
        {
            if (combo_From.Text != "")
            {
                combo_To.Items.Clear();
                int WH_ID = int.Parse(combo_From.Text.Split(':')[1]);
                var result = ent.warehouses.Where(war => war.w_id != WH_ID).ToList();

                foreach (var item in result)
                {
                    combo_To.Items.Add($"ID:{item.w_id}:Name:{item.w_name}");
                }
            }
            else
            {
                MessageBox.Show("Select \"From\" first!");
            }

        }

        private void cmbo_TransferProduct_DropDown(object sender, EventArgs e)
        {
            if (combo_From.Text != "" && combo_To.Text != ""
                && combo_TransferSupplier.Text != ""
                /*&& txt_TransferExpire.Text != ""*/)
            {
                cmbo_TransferProduct.Items.Clear();
                int WH_ID_FROM = int.Parse(combo_From.Text.Split(':')[1]);
                int WH_ID_TO = int.Parse(combo_To.Text.Split(':')[1]);
                int SUP_ID = int.Parse(combo_TransferSupplier.Text.Split(':')[1]);

                //DateTime Production = dt_TransferProduction.Value.Date;
                //string Customdt = Production.ToShortDateString();

                var su = ent.supplyingPermisionProducts.Where(supPro => supPro.supplyingPermission.sup_id == SUP_ID
                && supPro.product.w_id == WH_ID_FROM).ToList();
                foreach (var item in su)
                {
                    var pr = ent.products.Where(p => p.p_code == item.p_code).First();
                    cmbo_TransferProduct.Items.Add($"ID:{pr.p_code}:Name:{pr.p_name}:Quantity:{pr.quantity}:Ex:{item.expire_duration}");

                }


                //foreach (var item in su)
                //{
                //    cmbo_TransferProduct.Items.Add($"ID:{item.p_code}:Name:{item.product.p_name}:Quantity:{item.quantity}:Ex:{item.expire_duration}");
                //}
            }
            else
            {
                MessageBox.Show("Select and enter all fields first !");
            }

        }

        private void combo_TransferSupplier_DropDown(object sender, EventArgs e)
        {
            combo_TransferSupplier.Items.Clear();
            var result = ent.suppliers.Select(sup => sup).ToList();
            foreach (var item in result)
            {
                combo_TransferSupplier.Items.Add($"ID:{item.sup_id}:Name:{item.sup_name}");
            }
            
        }

        private void txt_TransferExpire_TextChanged(object sender, EventArgs e)
        {
            int duration;
            if (txt_TransferExpire.Text != "")
            {
                if (int.TryParse(txt_TransferExpire.Text, out duration))
                {
                    if (duration < 1)
                    {
                        txt_TransferExpire.Text = string.Empty;
                        MessageBox.Show("Value can't be negative or zero!!");
                    }
                }
                else
                {
                    txt_TransferExpire.Text = string.Empty;
                    MessageBox.Show("Enter value in correct format!");
                }
            }
        }

        private void txt_TransferQuantity_TextChanged(object sender, EventArgs e)
        {
            int quantity;
            if (cmbo_TransferProduct.Text != "")
            {
                if (txt_TransferQuantity.Text != "")
                {
                    if (int.TryParse(txt_TransferQuantity.Text, out quantity))
                    {
                        if (quantity < 1)
                        {
                            txt_TransferQuantity.Text = string.Empty;
                            MessageBox.Show("Value can't be negative or zero!!");
                        }
                        else
                        {
                            int WH_ID = int.Parse(combo_From.Text.Split(':')[1]);
                            int PRO_ID = int.Parse(cmbo_TransferProduct.Text.Split(':')[1]);
                            var product = ent.Contains.Where(cnt => cnt.W_code == WH_ID && cnt.p_code == PRO_ID).ToList().First();

                            if (quantity > product.quantity)
                            {
                                txt_TransferQuantity.Text = string.Empty;
                                MessageBox.Show("Quantity entered is greater than wuantity in stock !!");
                            }
                        }
                    }
                    else
                    {
                        txt_TransferQuantity.Text = string.Empty;
                        MessageBox.Show("Enter value in correct format!");
                    }
                }
            }
            else
            {
                if (txt_TransferQuantity.Text != "")
                {
                    txt_TransferQuantity.Text = string.Empty;
                    MessageBox.Show("Select product first");
                }

            }

        }

        private void btn_TransferAddProduct_Click(object sender, EventArgs e)
        {
            bool proceed = false;
            if (txt_TransferQuantity.Text != ""
                && combo_To.Text!=""
                && combo_From.Text!="" 
                && combo_TransferSupplier.Text!=""
                && cmbo_TransferProduct.Text!="")
            {

                
                int P_ID;
                int WH_ID_From;
                int WH_ID_To;
                int PQuantityTOTransfer;
                int ProductQuantityInStock;
                int SubID;

                DateTime? permissionDate;
                DateTime? productProduction;
                int? productExpire;

                WH_ID_From = int.Parse(combo_From.Text.Split(':')[1]);
                P_ID = int.Parse(cmbo_TransferProduct.Text.Split(':')[1]);
                var OK = dataList.Where(i => i.ProductID == P_ID && i.WareHouse_FromID == WH_ID_From).ToList();
                if (OK.Count>0) {
                    ClearComboItems();
                    MessageBox.Show("Exact item ID at Exact Warehouse ID already exists !!");
                }
                else {
                   
                    WH_ID_To = int.Parse(combo_To.Text.Split(':')[1]);
                    SubID = int.Parse(combo_TransferSupplier.Text.Split(':')[1]);

                    PQuantityTOTransfer = int.Parse(txt_TransferQuantity.Text);
                    ProductQuantityInStock = int.Parse(cmbo_TransferProduct.Text.Split(':')[5]);

                    productProduction = ent.Contains.Where(cnt => cnt.p_code == P_ID && cnt.W_code == WH_ID_From).Select(pro => pro.product.production_date).First();
                    productExpire = ent.Contains.Where(cnt => cnt.p_code == P_ID && cnt.W_code == WH_ID_From).Select(pro => pro.product.expire_duration).First();
                    permissionDate = ent.supplyingPermissions.Where(sup => sup.w_id== WH_ID_From && sup.sup_id == SubID).Select(sup=>sup.permission_date).First();

                    Data dt = new Data()
                    {
                        ProductID = P_ID,
                        ProductQuantityToTransfer = PQuantityTOTransfer,
                        WareHouse_FromID = WH_ID_From,
                        WareHouse_ToID = WH_ID_To,
                        ProductionDate = productProduction,
                        ExpireDuration = productExpire,
                        ProductQuantityInStock = ProductQuantityInStock,
                        subID = SubID,
                        permissionDate = permissionDate
                    };
                    dataList.Add(dt);
                    proceed = true;
                    ClearComboItems();
                }
                
            }
            else
            {
                MessageBox.Show("Missing required data");
            }
            if (proceed) {
                RefreshProductTransferList();
            }
           

        }

        private void btn_TransferItemDelete_Click(object sender, EventArgs e)
        {
            if (lst_ProductsToTransfer.SelectedItems.Count > 0)
            {
                int idToDlete = int.Parse(lst_ProductsToTransfer.SelectedItems[0].SubItems[0].Text);
                dataList.Remove(dataList.Find(i => i.ProductID == idToDlete));
                RefreshProductTransferList();
            }
            else {
                MessageBox.Show("Select item to delete !!");
            }
        }

        private void RefreshProductTransferList() {
          
                lst_ProductsToTransfer.Items.Clear();
                foreach (var record in dataList)
                {
                    string[] item = { record.ProductID.ToString(),
                        record.ProductQuantityToTransfer.ToString(),
                        record.ProductionDate.ToString(),
                        record.ExpireDuration.ToString() };
                    ListViewItem lst = new ListViewItem(item);
                    lst_ProductsToTransfer.Items.Add(lst);
                }
            
        }

        private void ClearComboItems() {             
            combo_TransferSupplier.Items.Clear();
            combo_To.Items.Clear();
            combo_From.Items.Clear();
            cmbo_TransferProduct.Items.Clear();
            txt_TransferQuantity.Text=String.Empty;
        }

        private void btn_Transfer_Click(object sender, EventArgs e)
        {
            if (dataList.Count > 0)
            {

                foreach (var ProductRow in dataList)
                {
                    var ContainItem = ent.Contains.Where(cnt => cnt.p_code == ProductRow.ProductID
                    &&
                    cnt.W_code == ProductRow.WareHouse_ToID).ToList().FirstOrDefault();
                    //if exists
                    if (ContainItem != null)
                    {
                        ContainItem.quantity += ProductRow.ProductQuantityToTransfer;
                        ent.SaveChanges();
                    }
                    //if not exists
                    else
                    {
                        Contain cnt = new Contain() {
                            W_code = ProductRow.WareHouse_ToID,
                            quantity = ProductRow.ProductQuantityToTransfer,
                            p_code = ProductRow.ProductID,
                            transfered = true
                        };
                        ent.Contains.Add(cnt);
                        ent.SaveChanges();

                        product p = new product() { 
                        transfered = true,
                        p_code=ProductRow.ProductID,
                        quantity=ProductRow.ProductQuantityToTransfer,
                        p_name = ent.products.Where(pr=>pr.p_code==ProductRow.ProductID).First().p_name,
                        production_date = ent.products.Where(pr => pr.p_code == ProductRow.ProductID).First().production_date,
                        expire_duration = ent.products.Where(pr => pr.p_code == ProductRow.ProductID).First().expire_duration,
                        w_id=ProductRow.WareHouse_ToID
                        };
                        ent.products.Add(p);
                        ent.SaveChanges();

                        //add supply permision
                        supplyingPermission sp = new supplyingPermission() {
                            permission_date = DateTime.Now,
                            sup_id=ProductRow.subID,
                            w_id=ProductRow.WareHouse_ToID                        
                        };
                        ent.supplyingPermissions.Add(sp);                       

                        //add supply permission products
                        var lastID = ent.supplyingPermissions.Max(ID => ID.permission_id);
                        supplyingPermisionProduct spP = new supplyingPermisionProduct() {

                        expire_duration= ProductRow.ExpireDuration,
                        p_code = ProductRow.ProductID,
                        permission_id= lastID,
                        quantity=ProductRow.ProductQuantityToTransfer,
                        productuction_date=ProductRow.permissionDate
                        };
                        ent.supplyingPermisionProducts.Add(spP);

                        ent.SaveChanges();

                        

                    }
                    //clean up

                    if (ProductRow.ProductQuantityToTransfer == ProductRow.ProductQuantityInStock)
                    {
                        //delete from product & stock
                        var result = ent.products.Where(p => p.p_code == ProductRow.ProductID && p.w_id
                        ==
                        ProductRow.WareHouse_FromID).First();
                        ent.products.Remove(result);



                        ent.SaveChanges();
                    }
                    else if(ProductRow.ProductQuantityToTransfer < ProductRow.ProductQuantityInStock) {
                        var result = ent.products.Where(p => p.p_code == ProductRow.ProductID && p.w_id
                       ==
                       ProductRow.WareHouse_FromID).First();
                        result.quantity -= ProductRow.ProductQuantityToTransfer;
                        ent.SaveChanges();
                        var result2 = ent.Contains.Where(p => p.p_code == ProductRow.ProductID && p.W_code
                     ==
                     ProductRow.WareHouse_FromID).First();
                        result2.quantity -= ProductRow.ProductQuantityToTransfer;

                        ent.SaveChanges();

                    }

                    

                }
                lst_ProductsToTransfer.Items.Clear();
                dataList.Clear();
            }
            else {
                MessageBox.Show("No items to transfer");
            }
        }

        private void btn_AppRefresh_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btn_ShowReport_Click(object sender, EventArgs e)
        {
            if (combo_ReportSelection.SelectedIndex != -1)
            {
                int index = combo_ReportSelection.SelectedIndex+1;                
                string result;
                int day;
                switch (index)
                {
                    case 1:
                       
                        stiReport1.Compile();
                       // pr = new Prompt("Enter warehouse name","");
                        result =Prompt.ShowDialog("Enter warehouse name", "");                        
                        stiReport1["@war"]= result;
                        stiReport1.Show();
                        stiViewerControl.Report=stiReport1;
                        break;
                    case 2:
                        
                        stiReport2.Compile();                        
                        result = Prompt.ShowDialog("Enter warehouses name seperated with comma , ", "");
                        result = result.TrimStart(',');
                        result = result.TrimEnd(',');                        
                        stiReport2["@tables"] = result;
                        result = Prompt.ShowDatePicker("Enter first production date").ToString();                          
                        stiReport2["@StartTime"] = result;
                        result = Prompt.ShowDatePicker("Enter second production date").ToString();                          
                        stiReport2["@EndTime"] = result;
                        stiReport2.Show();
                        stiViewerControl.Report = stiReport2;                        
                        break;
                    case 3:
                        
                        stiReport3.Compile();
                        result = Prompt.ShowDialog("Enter warehouses name seperated with comma , ", "");
                        result = result.TrimStart(',');
                        result = result.TrimEnd(',');
                        stiReport3["@tables"] = result;
                        stiReport3["@tables2"] = result;
                        result = Prompt.ShowDatePicker("Enter first transaction date").ToString();
                        stiReport3["@StartTime"] = result;
                        stiReport3["@StartTime2"] = result;
                        result = Prompt.ShowDatePicker("Enter second transaction date").ToString();
                        stiReport3["@EndTime"] = result;
                        stiReport3["@EndTime2"] = result;
                        stiReport3.Show();
                        stiViewerControl.Report = stiReport3;
                        break;

                    case 4:
                        day = 0;
                        result = Prompt.ShowDialog("Enter number of days", "");

                        if (int.TryParse(result, out day))
                        {
                            stiReport4.Compile();
                            stiReport4["@TimeDurationMonth"] = day.ToString();
                            stiReport4.Show();
                            stiViewerControl.Report = stiReport4;
                        }
                        else {
                            MessageBox.Show("Enter numebr of days with corerct format !");
                        }

                        break;

                    case 5:
                        day = 0;
                        result = Prompt.ShowDialog("Enter number of days reamining", "");

                        if (int.TryParse(result, out day))
                        {
                            stiReport5.Compile();
                            stiReport5["@TimeDurationMonth"] = day.ToString();
                            stiReport5.Show();
                            stiViewerControl.Report = stiReport5;
                        }
                        else
                        {
                            MessageBox.Show("Enter numebr of days with corerct format !");
                        }

                        break;
                    default:
                        break;
                }

            }
            else {
                MessageBox.Show("Select report to show!");
            }
            
        }
    }
    #region custom prompt
    public  class Prompt : IDisposable
    {
        private static Form prompt { get; set; }
        public string Result { get; }

        public Prompt(string text, string caption)
        {
            Result = ShowDialog(text, caption);
            Dispose();
        }
        
        public static string ShowDialog(string text, string caption)
        {
            prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                TopMost = true
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        public static DateTime  ShowDatePicker(string text)
        {
            prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,                
                StartPosition = FormStartPosition.CenterScreen,
                TopMost = true
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            DateTimePicker DTicker = new DateTimePicker() { Left = 50, Top = 50, Width = 400 };
            
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(DTicker);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? DTicker.Value : DateTime.Now;
        }

        public  void Dispose()
        {
            
            if (prompt != null)
            {
                prompt.Dispose();
            }
        }
    }
    #endregion
}
