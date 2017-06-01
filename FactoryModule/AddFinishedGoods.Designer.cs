namespace FactoryModule
{
    partial class AddFinishedGoods
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
            this.components = new System.ComponentModel.Container();
            this.supplierBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.factoryRawCategoryBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.addUnitComboBox = new System.Windows.Forms.ComboBox();
            this.factoryRawCategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtRowMeterialID = new System.Windows.Forms.TextBox();
            this.texboxfinishedgoodsName = new System.Windows.Forms.TextBox();
            this.lblRowMeterial = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxInitialStock = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.supplierBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.factoryRawCategoryBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.factoryRawCategoryBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // supplierBindingSource
            // 
            this.supplierBindingSource.DataMember = "supplier";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(188, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "View Finished Goods";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(332, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(511, 295);
            this.dataGridView1.TabIndex = 50;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // factoryRawCategoryBindingSource1
            // 
            this.factoryRawCategoryBindingSource1.DataMember = "FactoryRawCategory";
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUpdate.Location = new System.Drawing.Point(208, 157);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 29);
            this.buttonUpdate.TabIndex = 41;
            this.buttonUpdate.Text = "UPDATE";
            this.buttonUpdate.UseVisualStyleBackColor = false;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.Thistle;
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.Location = new System.Drawing.Point(140, 157);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(60, 29);
            this.buttonAdd.TabIndex = 40;
            this.buttonAdd.Text = "ADD";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label6.Location = new System.Drawing.Point(109, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Unit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(76, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Initial Stock";
            // 
            // addUnitComboBox
            // 
            this.addUnitComboBox.DisplayMember = "RawCategoryName";
            this.addUnitComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addUnitComboBox.FormattingEnabled = true;
            this.addUnitComboBox.Location = new System.Drawing.Point(141, 96);
            this.addUnitComboBox.Name = "addUnitComboBox";
            this.addUnitComboBox.Size = new System.Drawing.Size(142, 23);
            this.addUnitComboBox.TabIndex = 6;
            this.addUnitComboBox.ValueMember = "RawCategoryId";
            // 
            // factoryRawCategoryBindingSource
            // 
            this.factoryRawCategoryBindingSource.DataMember = "FactoryRawCategory";
            // 
            // txtRowMeterialID
            // 
            this.txtRowMeterialID.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRowMeterialID.Location = new System.Drawing.Point(79, 160);
            this.txtRowMeterialID.Name = "txtRowMeterialID";
            this.txtRowMeterialID.Size = new System.Drawing.Size(45, 26);
            this.txtRowMeterialID.TabIndex = 11;
            this.txtRowMeterialID.Visible = false;
            // 
            // texboxfinishedgoodsName
            // 
            this.texboxfinishedgoodsName.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.texboxfinishedgoodsName.Location = new System.Drawing.Point(141, 62);
            this.texboxfinishedgoodsName.Name = "texboxfinishedgoodsName";
            this.texboxfinishedgoodsName.Size = new System.Drawing.Size(142, 26);
            this.texboxfinishedgoodsName.TabIndex = 3;
            // 
            // lblRowMeterial
            // 
            this.lblRowMeterial.AutoSize = true;
            this.lblRowMeterial.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblRowMeterial.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowMeterial.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblRowMeterial.Location = new System.Drawing.Point(20, 68);
            this.lblRowMeterial.Name = "lblRowMeterial";
            this.lblRowMeterial.Size = new System.Drawing.Size(115, 16);
            this.lblRowMeterial.TabIndex = 2;
            this.lblRowMeterial.Text = "Finished Goodsl Name";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.Controls.Add(this.buttonUpdate);
            this.panel2.Controls.Add(this.buttonAdd);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.addUnitComboBox);
            this.panel2.Controls.Add(this.txtRowMeterialID);
            this.panel2.Controls.Add(this.textBoxInitialStock);
            this.panel2.Controls.Add(this.texboxfinishedgoodsName);
            this.panel2.Controls.Add(this.lblRowMeterial);
            this.panel2.Location = new System.Drawing.Point(1, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(325, 295);
            this.panel2.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(62, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Add / Edit Finished Goods";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.CadetBlue;
            this.panel6.Controls.Add(this.label5);
            this.panel6.Location = new System.Drawing.Point(332, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(511, 36);
            this.panel6.TabIndex = 51;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.CadetBlue;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(1, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(325, 36);
            this.panel3.TabIndex = 48;
            // 
            // textBoxInitialStock
            // 
            this.textBoxInitialStock.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInitialStock.Location = new System.Drawing.Point(141, 125);
            this.textBoxInitialStock.Name = "textBoxInitialStock";
            this.textBoxInitialStock.Size = new System.Drawing.Size(142, 26);
            this.textBoxInitialStock.TabIndex = 3;
            // 
            // AddFinishedGoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 331);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel3);
            this.Name = "AddFinishedGoods";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Finished Goods";
            this.Load += new System.EventHandler(this.AddFinishedGoods_Load);
            ((System.ComponentModel.ISupportInitialize)(this.supplierBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.factoryRawCategoryBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.factoryRawCategoryBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource supplierBindingSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource factoryRawCategoryBindingSource1;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox addUnitComboBox;
        private System.Windows.Forms.BindingSource factoryRawCategoryBindingSource;
        private System.Windows.Forms.TextBox txtRowMeterialID;
        private System.Windows.Forms.TextBox texboxfinishedgoodsName;
        private System.Windows.Forms.Label lblRowMeterial;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBoxInitialStock;
    }
}