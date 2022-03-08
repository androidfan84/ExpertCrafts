using System.ComponentModel;

namespace NavigationTest.ExpertCrafts
{
    partial class ExpertCraftsSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Settings = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.ItemsToBuy = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ItemId = new System.Windows.Forms.TextBox();
            this.Delete = new System.Windows.Forms.Button();
            this.Quantity = new System.Windows.Forms.TextBox();
            this.Add = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1.SuspendLayout();
            this.Settings.SuspendLayout();
            this.ItemsToBuy.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Settings);
            this.tabControl1.Controls.Add(this.ItemsToBuy);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(542, 462);
            this.tabControl1.TabIndex = 0;
            // 
            // Settings
            // 
            this.Settings.Controls.Add(this.propertyGrid1);
            this.Settings.Location = new System.Drawing.Point(4, 22);
            this.Settings.Name = "Settings";
            this.Settings.Padding = new System.Windows.Forms.Padding(3);
            this.Settings.Size = new System.Drawing.Size(363, 436);
            this.Settings.TabIndex = 0;
            this.Settings.Text = "Settings";
            this.Settings.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(357, 430);
            this.propertyGrid1.TabIndex = 0;
            // 
            // ItemsToBuy
            // 
            this.ItemsToBuy.Controls.Add(this.groupBox1);
            this.ItemsToBuy.Controls.Add(this.dataGridView1);
            this.ItemsToBuy.Location = new System.Drawing.Point(4, 22);
            this.ItemsToBuy.Name = "ItemsToBuy";
            this.ItemsToBuy.Padding = new System.Windows.Forms.Padding(3);
            this.ItemsToBuy.Size = new System.Drawing.Size(534, 436);
            this.ItemsToBuy.TabIndex = 1;
            this.ItemsToBuy.Text = "ItemsToBuy";
            this.ItemsToBuy.UseVisualStyleBackColor = true;
            this.ItemsToBuy.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ItemId);
            this.groupBox1.Controls.Add(this.Delete);
            this.groupBox1.Controls.Add(this.Quantity);
            this.groupBox1.Controls.Add(this.Add);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(528, 73);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Item ID:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ItemId
            // 
            this.ItemId.Location = new System.Drawing.Point(6, 39);
            this.ItemId.Name = "ItemId";
            this.ItemId.Size = new System.Drawing.Size(90, 20);
            this.ItemId.TabIndex = 1;
            // 
            // Delete
            // 
            this.Delete.Enabled = false;
            this.Delete.Location = new System.Drawing.Point(343, 40);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(62, 21);
            this.Delete.TabIndex = 6;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            // 
            // Quantity
            // 
            this.Quantity.Location = new System.Drawing.Point(156, 39);
            this.Quantity.Name = "Quantity";
            this.Quantity.Size = new System.Drawing.Size(44, 20);
            this.Quantity.TabIndex = 2;
            // 
            // Add
            // 
            this.Add.Enabled = false;
            this.Add.Location = new System.Drawing.Point(252, 39);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(51, 23);
            this.Add.TabIndex = 5;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(156, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Quantity:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(3, 82);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(528, 351);
            this.dataGridView1.TabIndex = 7;
            // 
            // ExpertCraftsSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 462);
            this.Controls.Add(this.tabControl1);
            this.Name = "ExpertCraftsSettingsForm";
            this.Text = "ExpertCraftsSettingsForm";
            this.Load += new System.EventHandler(this.ExpertCraftsSettingsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.Settings.ResumeLayout(false);
            this.ItemsToBuy.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox groupBox1;

        private System.Windows.Forms.DataGridView dataGridView1;

        private System.Windows.Forms.BindingSource bindingSource1;

        private System.Windows.Forms.Button Delete;

        private System.Windows.Forms.Button Add;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.TextBox Quantity;

        private System.Windows.Forms.TextBox ItemId;

        private System.Windows.Forms.PropertyGrid propertyGrid1;

        private System.Windows.Forms.TabPage ItemsToBuy;

        private System.Windows.Forms.TabPage Settings;
        private System.Windows.Forms.TabControl tabControl1;

        #endregion
    }
}