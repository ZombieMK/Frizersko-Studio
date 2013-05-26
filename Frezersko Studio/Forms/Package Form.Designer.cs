namespace Frezersko_Studio.Forms
{
    partial class packageForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.removeFromPackageBtn = new System.Windows.Forms.Button();
            this.addToPackageBtn = new System.Windows.Forms.Button();
            this.addedSerivesList = new System.Windows.Forms.ListBox();
            this.addedServicesLbl = new System.Windows.Forms.Label();
            this.servicesList = new System.Windows.Forms.ListBox();
            this.servicesLbl = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.priceLbl = new System.Windows.Forms.Label();
            this.priceTxt = new System.Windows.Forms.TextBox();
            this.nameLbl = new System.Windows.Forms.Label();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.doneBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.removeFromPackageBtn);
            this.groupBox1.Controls.Add(this.addToPackageBtn);
            this.groupBox1.Controls.Add(this.addedSerivesList);
            this.groupBox1.Controls.Add(this.addedServicesLbl);
            this.groupBox1.Controls.Add(this.servicesList);
            this.groupBox1.Controls.Add(this.servicesLbl);
            this.groupBox1.Controls.Add(this.cancelBtn);
            this.groupBox1.Controls.Add(this.priceLbl);
            this.groupBox1.Controls.Add(this.priceTxt);
            this.groupBox1.Controls.Add(this.nameLbl);
            this.groupBox1.Controls.Add(this.nameTxt);
            this.groupBox1.Controls.Add(this.doneBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 279);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Package Info";
            // 
            // removeFromPackageBtn
            // 
            this.removeFromPackageBtn.Location = new System.Drawing.Point(196, 202);
            this.removeFromPackageBtn.Name = "removeFromPackageBtn";
            this.removeFromPackageBtn.Size = new System.Drawing.Size(58, 23);
            this.removeFromPackageBtn.TabIndex = 38;
            this.removeFromPackageBtn.Text = "Remove";
            this.removeFromPackageBtn.UseVisualStyleBackColor = true;
            this.removeFromPackageBtn.Click += new System.EventHandler(this.removeFromPackageBtn_Click);
            // 
            // addToPackageBtn
            // 
            this.addToPackageBtn.Location = new System.Drawing.Point(38, 202);
            this.addToPackageBtn.Name = "addToPackageBtn";
            this.addToPackageBtn.Size = new System.Drawing.Size(58, 23);
            this.addToPackageBtn.TabIndex = 37;
            this.addToPackageBtn.Text = "Add";
            this.addToPackageBtn.UseVisualStyleBackColor = true;
            this.addToPackageBtn.Click += new System.EventHandler(this.addToPackageBtn_Click);
            // 
            // addedSerivesList
            // 
            this.addedSerivesList.FormattingEnabled = true;
            this.addedSerivesList.Location = new System.Drawing.Point(158, 88);
            this.addedSerivesList.Name = "addedSerivesList";
            this.addedSerivesList.Size = new System.Drawing.Size(125, 108);
            this.addedSerivesList.TabIndex = 36;
            // 
            // addedServicesLbl
            // 
            this.addedServicesLbl.AutoSize = true;
            this.addedServicesLbl.Location = new System.Drawing.Point(158, 72);
            this.addedServicesLbl.Name = "addedServicesLbl";
            this.addedServicesLbl.Size = new System.Drawing.Size(85, 13);
            this.addedServicesLbl.TabIndex = 35;
            this.addedServicesLbl.Text = "Added Services:";
            // 
            // servicesList
            // 
            this.servicesList.FormattingEnabled = true;
            this.servicesList.Location = new System.Drawing.Point(6, 88);
            this.servicesList.Name = "servicesList";
            this.servicesList.Size = new System.Drawing.Size(125, 108);
            this.servicesList.TabIndex = 34;
            // 
            // servicesLbl
            // 
            this.servicesLbl.AutoSize = true;
            this.servicesLbl.Location = new System.Drawing.Point(6, 72);
            this.servicesLbl.Name = "servicesLbl";
            this.servicesLbl.Size = new System.Drawing.Size(51, 13);
            this.servicesLbl.TabIndex = 33;
            this.servicesLbl.Text = "Services:";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(161, 246);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(58, 23);
            this.cancelBtn.TabIndex = 32;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // priceLbl
            // 
            this.priceLbl.AutoSize = true;
            this.priceLbl.Location = new System.Drawing.Point(158, 16);
            this.priceLbl.Name = "priceLbl";
            this.priceLbl.Size = new System.Drawing.Size(34, 13);
            this.priceLbl.TabIndex = 31;
            this.priceLbl.Text = "Price:";
            // 
            // priceTxt
            // 
            this.priceTxt.Location = new System.Drawing.Point(158, 32);
            this.priceTxt.Name = "priceTxt";
            this.priceTxt.Size = new System.Drawing.Size(125, 20);
            this.priceTxt.TabIndex = 30;
            this.priceTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.priceTxt_KeyPress);
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Location = new System.Drawing.Point(6, 18);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(38, 13);
            this.nameLbl.TabIndex = 29;
            this.nameLbl.Text = "Name:";
            // 
            // nameTxt
            // 
            this.nameTxt.Location = new System.Drawing.Point(6, 34);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.Size = new System.Drawing.Size(125, 20);
            this.nameTxt.TabIndex = 28;
            // 
            // doneBtn
            // 
            this.doneBtn.Location = new System.Drawing.Point(225, 246);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(58, 23);
            this.doneBtn.TabIndex = 27;
            this.doneBtn.Text = "Done";
            this.doneBtn.UseVisualStyleBackColor = true;
            this.doneBtn.Click += new System.EventHandler(this.doneBtn_Click);
            // 
            // packageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(314, 299);
            this.Controls.Add(this.groupBox1);
            this.Name = "packageForm";
            this.Text = "Package";
            this.Load += new System.EventHandler(this.packageForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label priceLbl;
        private System.Windows.Forms.TextBox priceTxt;
        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.TextBox nameTxt;
        private System.Windows.Forms.Button doneBtn;
        private System.Windows.Forms.Label servicesLbl;
        private System.Windows.Forms.ListBox servicesList;
        private System.Windows.Forms.Label addedServicesLbl;
        private System.Windows.Forms.ListBox addedSerivesList;
        private System.Windows.Forms.Button addToPackageBtn;
        private System.Windows.Forms.Button removeFromPackageBtn;
    }
}