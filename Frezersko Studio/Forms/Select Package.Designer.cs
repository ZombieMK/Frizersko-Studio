namespace Frezersko_Studio.Forms
{
    partial class selectPackage
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
            this.packagesList = new System.Windows.Forms.ListBox();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.doneBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.employeeList = new System.Windows.Forms.ComboBox();
            this.servicesList = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.packagesList);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 293);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select one of the packages";
            // 
            // packagesList
            // 
            this.packagesList.FormattingEnabled = true;
            this.packagesList.Location = new System.Drawing.Point(7, 20);
            this.packagesList.Name = "packagesList";
            this.packagesList.Size = new System.Drawing.Size(210, 264);
            this.packagesList.TabIndex = 0;
            this.packagesList.SelectedIndexChanged += new System.EventHandler(this.packagesList_SelectedIndexChanged);
            // 
            // cancelbtn
            // 
            this.cancelbtn.Location = new System.Drawing.Point(297, 311);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 2;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // doneBtn
            // 
            this.doneBtn.Location = new System.Drawing.Point(384, 311);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(75, 23);
            this.doneBtn.TabIndex = 1;
            this.doneBtn.Text = "Done";
            this.doneBtn.UseVisualStyleBackColor = true;
            this.doneBtn.Click += new System.EventHandler(this.doneBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.employeeList);
            this.groupBox2.Controls.Add(this.servicesList);
            this.groupBox2.Location = new System.Drawing.Point(242, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(224, 293);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Service";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select employee for the service";
            // 
            // employeeList
            // 
            this.employeeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.employeeList.FormattingEnabled = true;
            this.employeeList.Location = new System.Drawing.Point(5, 264);
            this.employeeList.Name = "employeeList";
            this.employeeList.Size = new System.Drawing.Size(210, 21);
            this.employeeList.TabIndex = 1;
            this.employeeList.SelectedIndexChanged += new System.EventHandler(this.employeeList_SelectedIndexChanged);
            // 
            // servicesList
            // 
            this.servicesList.FormattingEnabled = true;
            this.servicesList.Location = new System.Drawing.Point(7, 20);
            this.servicesList.Name = "servicesList";
            this.servicesList.Size = new System.Drawing.Size(210, 212);
            this.servicesList.TabIndex = 0;
            this.servicesList.SelectedIndexChanged += new System.EventHandler(this.servicesList_SelectedIndexChanged);
            // 
            // selectPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(480, 345);
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "selectPackage";
            this.Text = "Select Packages";
            this.Load += new System.EventHandler(this.selectPackage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button doneBtn;
        private System.Windows.Forms.ListBox packagesList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox servicesList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox employeeList;
    }
}