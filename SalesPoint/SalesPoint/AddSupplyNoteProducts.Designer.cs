namespace SalesPoint
{
    partial class AddSupplyNoteProducts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSupplyNoteProducts));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_permissionID = new System.Windows.Forms.TextBox();
            this.txt_productExpiredt = new System.Windows.Forms.TextBox();
            this.txt_productQuantity = new System.Windows.Forms.TextBox();
            this.combo_ProductIDName = new System.Windows.Forms.ComboBox();
            this.dt_productionDate = new System.Windows.Forms.DateTimePicker();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_SubmitNote = new System.Windows.Forms.Button();
            this.lst_supplyPermissionProducts = new System.Windows.Forms.ListView();
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader33 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_AddItemtoList = new System.Windows.Forms.Button();
            this.btn_deleteItem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Permission ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Product";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 107);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Production date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(34, 145);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Expire duration(Day)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(34, 183);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Quantity";
            // 
            // txt_permissionID
            // 
            this.txt_permissionID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_permissionID.Location = new System.Drawing.Point(245, 28);
            this.txt_permissionID.Name = "txt_permissionID";
            this.txt_permissionID.ReadOnly = true;
            this.txt_permissionID.Size = new System.Drawing.Size(426, 30);
            this.txt_permissionID.TabIndex = 5;
            // 
            // txt_productExpiredt
            // 
            this.txt_productExpiredt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_productExpiredt.Location = new System.Drawing.Point(245, 142);
            this.txt_productExpiredt.Name = "txt_productExpiredt";
            this.txt_productExpiredt.Size = new System.Drawing.Size(426, 30);
            this.txt_productExpiredt.TabIndex = 5;
            this.txt_productExpiredt.TextChanged += new System.EventHandler(this.txt_productExpiredt_TextChanged);
            // 
            // txt_productQuantity
            // 
            this.txt_productQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_productQuantity.Location = new System.Drawing.Point(245, 180);
            this.txt_productQuantity.Name = "txt_productQuantity";
            this.txt_productQuantity.Size = new System.Drawing.Size(426, 30);
            this.txt_productQuantity.TabIndex = 5;
            this.txt_productQuantity.TextChanged += new System.EventHandler(this.txt_productQuantity_TextChanged);
            // 
            // combo_ProductIDName
            // 
            this.combo_ProductIDName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_ProductIDName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combo_ProductIDName.FormattingEnabled = true;
            this.combo_ProductIDName.Location = new System.Drawing.Point(245, 65);
            this.combo_ProductIDName.Name = "combo_ProductIDName";
            this.combo_ProductIDName.Size = new System.Drawing.Size(426, 33);
            this.combo_ProductIDName.TabIndex = 6;
            // 
            // dt_productionDate
            // 
            this.dt_productionDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_productionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_productionDate.Location = new System.Drawing.Point(245, 104);
            this.dt_productionDate.Name = "dt_productionDate";
            this.dt_productionDate.Size = new System.Drawing.Size(426, 30);
            this.dt_productionDate.TabIndex = 7;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.ForeColor = System.Drawing.Color.Red;
            this.btn_cancel.Location = new System.Drawing.Point(39, 320);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(121, 68);
            this.btn_cancel.TabIndex = 8;
            this.btn_cancel.Text = "Cancel note items";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_SubmitNote
            // 
            this.btn_SubmitNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SubmitNote.ForeColor = System.Drawing.Color.Blue;
            this.btn_SubmitNote.Location = new System.Drawing.Point(181, 320);
            this.btn_SubmitNote.Name = "btn_SubmitNote";
            this.btn_SubmitNote.Size = new System.Drawing.Size(187, 68);
            this.btn_SubmitNote.TabIndex = 8;
            this.btn_SubmitNote.Text = "Add products to note";
            this.btn_SubmitNote.UseVisualStyleBackColor = true;
            this.btn_SubmitNote.Click += new System.EventHandler(this.btn_SubmitNote_Click);
            // 
            // lst_supplyPermissionProducts
            // 
            this.lst_supplyPermissionProducts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader22,
            this.columnHeader23,
            this.columnHeader24,
            this.columnHeader25,
            this.columnHeader33});
            this.lst_supplyPermissionProducts.Dock = System.Windows.Forms.DockStyle.Right;
            this.lst_supplyPermissionProducts.FullRowSelect = true;
            this.lst_supplyPermissionProducts.GridLines = true;
            this.lst_supplyPermissionProducts.HideSelection = false;
            this.lst_supplyPermissionProducts.Location = new System.Drawing.Point(683, 0);
            this.lst_supplyPermissionProducts.Name = "lst_supplyPermissionProducts";
            this.lst_supplyPermissionProducts.Size = new System.Drawing.Size(570, 400);
            this.lst_supplyPermissionProducts.TabIndex = 9;
            this.lst_supplyPermissionProducts.UseCompatibleStateImageBehavior = false;
            this.lst_supplyPermissionProducts.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "Permission ID";
            this.columnHeader22.Width = 119;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "Product ID";
            this.columnHeader23.Width = 94;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "Production date";
            this.columnHeader24.Width = 134;
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "Expire duration";
            this.columnHeader25.Width = 126;
            // 
            // columnHeader33
            // 
            this.columnHeader33.Text = "Quantity";
            this.columnHeader33.Width = 241;
            // 
            // btn_AddItemtoList
            // 
            this.btn_AddItemtoList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddItemtoList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_AddItemtoList.Location = new System.Drawing.Point(39, 229);
            this.btn_AddItemtoList.Name = "btn_AddItemtoList";
            this.btn_AddItemtoList.Size = new System.Drawing.Size(329, 68);
            this.btn_AddItemtoList.TabIndex = 10;
            this.btn_AddItemtoList.Text = "Add product to list";
            this.btn_AddItemtoList.UseVisualStyleBackColor = true;
            this.btn_AddItemtoList.Click += new System.EventHandler(this.btn_AddItemtoList_Click);
            // 
            // btn_deleteItem
            // 
            this.btn_deleteItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_deleteItem.ForeColor = System.Drawing.Color.Red;
            this.btn_deleteItem.Location = new System.Drawing.Point(375, 229);
            this.btn_deleteItem.Name = "btn_deleteItem";
            this.btn_deleteItem.Size = new System.Drawing.Size(296, 68);
            this.btn_deleteItem.TabIndex = 11;
            this.btn_deleteItem.Text = "Delete item from list";
            this.btn_deleteItem.UseVisualStyleBackColor = true;
            this.btn_deleteItem.Click += new System.EventHandler(this.btn_deleteItem_Click);
            // 
            // AddSupplyNoteProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 400);
            this.Controls.Add(this.btn_deleteItem);
            this.Controls.Add(this.btn_AddItemtoList);
            this.Controls.Add(this.lst_supplyPermissionProducts);
            this.Controls.Add(this.btn_SubmitNote);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.dt_productionDate);
            this.Controls.Add(this.combo_ProductIDName);
            this.Controls.Add(this.txt_productQuantity);
            this.Controls.Add(this.txt_productExpiredt);
            this.Controls.Add(this.txt_permissionID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddSupplyNoteProducts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add note products";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_permissionID;
        private System.Windows.Forms.TextBox txt_productExpiredt;
        private System.Windows.Forms.TextBox txt_productQuantity;
        private System.Windows.Forms.ComboBox combo_ProductIDName;
        private System.Windows.Forms.DateTimePicker dt_productionDate;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_SubmitNote;
        private System.Windows.Forms.ListView lst_supplyPermissionProducts;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.ColumnHeader columnHeader23;
        private System.Windows.Forms.ColumnHeader columnHeader24;
        private System.Windows.Forms.ColumnHeader columnHeader25;
        private System.Windows.Forms.ColumnHeader columnHeader33;
        private System.Windows.Forms.Button btn_AddItemtoList;
        private System.Windows.Forms.Button btn_deleteItem;
    }
}