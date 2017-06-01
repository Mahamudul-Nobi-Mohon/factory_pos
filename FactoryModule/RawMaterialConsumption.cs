using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace FactoryModule
{
    public partial class RawMaterialConsumption : Form
    {
        public RawMaterialConsumption()
        {
            InitializeComponent();
        }

        public string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
        private void RawMaterialConsumption_Load(object sender, EventArgs e)
        {
            Auto_Complete();
            Form_Load();
        }

        private void Form_Load()
        {
            string query = "SELECT * FROM FactoryFloor";
            fillCombo(comboBoxFloorName, query, "FactoryFloorName", "FactoryFloorId");

            DateTime now = DateTime.Now;
            textBoxDate.Text = now.ToString("yyyy-MM-dd");

            #region Auto Number Generate
            string SNo = "CONFAC-";
            int no = 0;
            string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection con = new SqlConnection(conStr);
            string query2 = "SELECT COUNT(DISTINCT(ConsumptionNo)) as 'No' FROM RawMaterialConsumption";
            SqlCommand command = new SqlCommand(query2, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())

            {
                no = Convert.ToInt32(reader["No"]);
                no = no + 1;
            }

            reader.Close();
            con.Close();
            SNo = SNo + no.ToString("D4");
            textBoxConsumptionNo.Text = SNo;
            #endregion
            textBoxTime.Text = now.ToLongTimeString();
        }
        #region Fill Comboo
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());
        public void fillCombo(ComboBox combo, string query, string displayMember, string valueMember)
        {
            SqlCommand command = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            combo.DataSource = table;
            combo.DisplayMember = displayMember;
            combo.ValueMember = valueMember;
        }
        #endregion

        private void comboBoxFloorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int val;
            Int32.TryParse(comboBoxFloorName.SelectedValue.ToString(), out val);
            string query1 = "SELECT * FROM FactorySection WHERE FloorId = " + val;
            fillCombo(comboBoxSelectSection, query1, "FactorySectionName", "FactorySectionId");
        }

        private void Auto_Complete()
        {
            //Auto Complete search
            textBoxProductSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBoxProductSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection conSS = new SqlConnection(conStr);
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            col.Clear();
            conSS.Open();
            string sql = "SELECT * FROM RawMaterial";
            SqlCommand cmd = new SqlCommand(sql, conSS);
            SqlDataReader sdr = null;
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                //col.Add(sdr["RawMaterialId"].ToString());
                col.Add(sdr["RawMaterialName"].ToString());

            }
            sdr.Close();
            textBoxProductSearch.AutoCompleteCustomSource = col;
            conSS.Close();
        }

        public void GetUnitName(int unit_id)
        {
            string conStr111 = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection connection12111 = new SqlConnection(conStr111);
            string query12111 = "SELECT * FROM FactoryUnit WHERE UnitID = '" + unit_id + "'";
            SqlCommand command112111 = new SqlCommand(query12111, connection12111);

            connection12111.Open();
            SqlDataReader reader12111 = command112111.ExecuteReader();

            while (reader12111.Read())
            {
                textBoxUnitType.Text = reader12111["UnitName"].ToString();
            }
            reader12111.Close();
            connection12111.Close();
        }


        public void GetCategoryName(int cat_id)
        {
            string conStr111 = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection connection12111 = new SqlConnection(conStr111);
            string query12111 = "SELECT * FROM FactoryRawCategory WHERE RawCategoryId = '" + cat_id + "'";
            SqlCommand command112111 = new SqlCommand(query12111, connection12111);

            connection12111.Open();
            SqlDataReader reader12111 = command112111.ExecuteReader();

            while (reader12111.Read())
            {
                textBoxCategoryName.Text = reader12111["RawCategoryName"].ToString();
            }
            reader12111.Close();
            connection12111.Close();
        }
        private void textBoxProductSearch_TextChanged(object sender, EventArgs e)
        {
            //    int id = GetCustId(Custname);

            string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();

            SqlConnection con = new SqlConnection(conStr);
            string query = "SELECT * FROM RawMaterial WHERE RawMaterialName = '" + textBoxProductSearch.Text + "'";
            SqlCommand command112 = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader12 = command112.ExecuteReader();
            while (reader12.Read())
            {
                textBoxPdoductCode.Text = reader12["RawMaterialId"].ToString();
                textBoxProductName.Text = reader12["RawMaterialName"].ToString();
                textBoxCurrentStock.Text = reader12["RawMaterialStock"].ToString();
                int unit_id = Convert.ToInt32(reader12["RawMaterialUnitId"]);
                GetUnitName(unit_id);

                int cat_id = Convert.ToInt32(reader12["RawMaterialCatId"]);
                GetCategoryName(cat_id);
                buttonAdd.Visible = true;
                textBoxQuantity.Text = "";

            }
            reader12.Close();
            con.Close();
        }

        public double Temp_PreviousStock(int productcode)
        {
            double total_quantity = 0;
            string conStrCalq = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection connectionCalq = new SqlConnection(conStrCalq);
            string queryCalq = "SELECT * FROM TempConsumption WHERE TempConsumptionProductID = '" + productcode + "'";
            SqlCommand commandCalq = new SqlCommand(queryCalq, connectionCalq);
            connectionCalq.Open();
            SqlDataReader readerCalq = commandCalq.ExecuteReader();

            while (readerCalq.Read())
            {
                total_quantity = Convert.ToDouble(readerCalq["Quantity"]);
            }
            readerCalq.Close();
            connectionCalq.Close();
            return total_quantity;
        }

        public void UpdateTemp_ProductStock(double total_quantity, int pro_id)
        {
            
        SqlConnection connection1 = new SqlConnection(conStr);
            string query1 = "UPDATE TempConsumption SET TempConsumptionQuantity = '" + total_quantity + "' WHERE TempConsumptionProductID = '" + pro_id + "'";
            SqlCommand command1 = new SqlCommand(query1, connection1);
            connection1.Open();
            int rowEffict1 = command1.ExecuteNonQuery();
            connection1.Close();
            if (rowEffict1 > 0)
            {
                DataGrid();
            }
        }

        public void DataGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TempConsumptionID as 'ID', TempConsumptionProductID as 'ProductID', TempConsumptionProductName as 'Product Name', TempConsumptionQuantity as 'Quantity', TempConsumptionNo as 'Consumption No' FROM TempConsumption";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewConsumption.DataSource = dt;
            con.Close();
            dataGridViewConsumption.Columns[2].Width = 140;
            dataGridViewConsumption.Columns["Consumption No"].Width = 196;
        }

        private double Currently_Stock()
        {
            double stock = 0;
            string conStrPross = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection connectionPross = new SqlConnection(conStrPross);
            string queryPross = "SELECT * FROM RawMaterial WHERE RawMaterialId = '" + Convert.ToInt32(textBoxPdoductCode.Text) + "'";
            SqlCommand commandPross = new SqlCommand(queryPross, connectionPross);
            connectionPross.Open();
            SqlDataReader readerPross = commandPross.ExecuteReader();

            while (readerPross.Read())
            {
                stock = Convert.ToDouble(readerPross["RawMaterialStock"]);
            }

            readerPross.Close();
            connectionPross.Close();
            return stock;
        }

        public void InsertDataToTempTable(int temp_pro_id, string temp_name, double temp_quantity, string temp_consumption_no)
        {
            SqlConnection connection = new SqlConnection(conStr);
            string query = "INSERT INTO TempConsumption(TempConsumptionProductID, TempConsumptionProductName, TempConsumptionQuantity, TempConsumptionNo) VALUES('" + temp_pro_id + "','" + temp_name + "','" + temp_quantity + "','" + temp_consumption_no + "')";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowEffict = command.ExecuteNonQuery();
            connection.Close();

        }

        public void Display_Three_Item()
        {
            double ProTotal = 0;
            int i = 0;
            string conStrs = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection connections = new SqlConnection(conStrs);
            string querys = "SELECT * FROM TempConsumption";
            SqlCommand commands = new SqlCommand(querys, connections);
            connections.Open();
            SqlDataReader readers = commands.ExecuteReader();

            while (readers.Read())
            {
                ProTotal = ProTotal + Convert.ToDouble(readers["TempConsumptionQuantity"]);
                i++;
            }
            readers.Close();
            connections.Close();
            textBoxItemTotal.Text = i.ToString();
            textBoxProductTotal.Text = ProTotal.ToString();
         
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            double ExitQuantity = Temp_PreviousStock(Convert.ToInt32(textBoxPdoductCode.Text));
            if (comboBoxFloorName.Text == "")
            {
                MessageBox.Show("Please Select Floor Name");
            }
            else if (comboBoxSelectSection.Text == "")
            {
                MessageBox.Show("Please Select Section Name");
            }
            else if (textBoxRequisitionNo.Text == "")
            {
                MessageBox.Show("Please Give Requisition No");
            }
            else if (textBoxRequisitionFor.Text == "")
            {
                MessageBox.Show("Please Give Requisition For");
            }
            else if (textBoxRequestBy.Text == "")
            {
                MessageBox.Show("Please Give Request By Name");
            }
            else if (textBoxQuantity.Text == "")
            {
                MessageBox.Show("Please Fill the quantity..");
            }
            else if ((Convert.ToDouble(textBoxQuantity.Text) + ExitQuantity) > Currently_Stock())
            {
                MessageBox.Show("Out of Stock!!!..\n Currently Stock = " + textBoxCurrentStock.Text + " only.....!!!!!!");
            }

            else
            {
                string temp_consumption_no = textBoxConsumptionNo.Text;
                int section_id;
                Int32.TryParse(comboBoxSelectSection.SelectedValue.ToString(), out section_id);
                string temp_requisition_no = textBoxRequisitionNo.Text;
                string temp_requisition_for = textBoxRequisitionFor.Text;
                string temp_requist_by = textBoxRequestBy.Text;
                string temp_remarks =textBoxRemarks.Text;
                int temp_pro_id = Convert.ToInt32(textBoxPdoductCode.Text);
                string temp_name = textBoxProductName.Text;
                double temp_quantity = Convert.ToDouble(textBoxQuantity.Text);

                double temp_stock = Temp_PreviousStock(temp_pro_id);
                if (temp_stock > 0.0)
                {
                    double Quantity_total = temp_stock + temp_quantity;
                    UpdateTemp_ProductStock(Quantity_total, temp_pro_id);
                }

                else
                {
                    InsertDataToTempTable(temp_pro_id, temp_name, temp_quantity, temp_consumption_no);
                    DataGrid();

                }
                Display_Three_Item();
                string hide = textBoxPdoductCode.Text;
                textBoxProductName.Text = textBoxQuantity.Text = textBoxUnitType.Text = textBoxProductSearch.Text = textBoxCurrentStock.Text = textBoxPdoductCode.Text = textBoxCategoryName.Text = "";
                //textBoxPdoductCode.Text = hide;
                this.ActiveControl = textBoxProductSearch;
            }
        }

        public void ConsumptionFormClosed()
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            string sql = @"TRUNCATE TABLE TempConsumption";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void RawMaterialConsumption_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConsumptionFormClosed();
        }

        private void Update_Product_Stock()
        {

            foreach (DataGridViewRow row in dataGridViewConsumption.Rows)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE RawMaterial SET RawMaterialStock =(SELECT RawMaterialStock FROM RawMaterial WHERE RawMaterialId = @ProductID) - @Quantity  WHERE RawMaterialId = @ProductID", con))
                    {

                        cmd.Parameters.AddWithValue("@ProductID", Convert.ToDouble(row.Cells["ProductID"].Value));
                        cmd.Parameters.AddWithValue("@Quantity", Convert.ToDouble(row.Cells["Quantity"].Value));
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }

        }

        private void Consumption_Details_Insert()
        {
            int section_id;
            Int32.TryParse(comboBoxSelectSection.SelectedValue.ToString(), out section_id);
            foreach (DataGridViewRow row in dataGridViewConsumption.Rows)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO RawMaterialConsumption(ConsumptionNo, ConsumptionDate, ConsumptionTime, FactorySectionId, RawMaterialID, Quantity, RequisitionNo, RequestBy, RequisitionFor, ConsumptionRemarks) VALUES('" + textBoxConsumptionNo.Text + "', '" + textBoxDate.Text + "', '" + textBoxTime.Text + "', '" + section_id + "',@ProductID, @Quantity,  '" + textBoxRequisitionNo.Text + "', '" + textBoxRequestBy.Text + "', '" + textBoxRequisitionFor.Text + "', '" + textBoxRemarks.Text + "')", con))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", row.Cells["ProductID"].Value);
                        cmd.Parameters.AddWithValue("@Quantity", row.Cells["Quantity"].Value);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }
        private void Clear_All_Last()
        {
            comboBoxFloorName.Text = String.Empty;
            comboBoxSelectSection.Text = String.Empty;
            textBoxRequisitionNo.Text = String.Empty;
            textBoxRequisitionFor.Text = String.Empty;
            textBoxRequestBy.Text = String.Empty;
            textBoxRemarks.Text = String.Empty;
            textBoxProductSearch.Text = String.Empty;
            textBoxCategoryName.Text = String.Empty;
            textBoxPdoductCode.Text = String.Empty;
            textBoxProductName.Text = String.Empty;
            textBoxUnitType.Text = String.Empty;
            textBoxCurrentStock.Text = String.Empty;
            textBoxQuantity.Text = String.Empty;
            textBoxItemTotal.Text = String.Empty;
            textBoxProductTotal.Text = String.Empty;
        }
        private void buttonIssue_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure, you will Issue these product?", "Issue Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }
            else
            {
                try
                {
                    Update_Product_Stock();
                    Consumption_Details_Insert();
                    DataGrid();
                    //textBoxSupplierInvoiceNo.Text = textBoxPurchaseNo.Text = "";
                    MessageBox.Show("Issue Successfully....!!!!");

                    Clear_All_Last();
                    ConsumptionFormClosed();
                    DataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Form_Load();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
