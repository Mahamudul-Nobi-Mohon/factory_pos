using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryModule
{
    public partial class FinishedGoodsDistribution : Form
    {
        public FinishedGoodsDistribution()
        {
            InitializeComponent();
        }

        private void FinishedGoodsDistribution_Load(object sender, EventArgs e)
        {
            Auto_Complete();
            Form_Load();
        }

        private void Form_Load()
        {
            string query = "SELECT * FROM FinGoodsDistributionArea";
            fillCombo(comboBoxRequisitionFor, query, "DistributionAreaName", "DistributionAreaId");

            DateTime now = DateTime.Now;
            textBoxDate.Text = now.ToString("yyyy-MM-dd");

            #region Auto Number Generate
            string SNo = "FINDIS-";
            int no = 0;
            string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection con = new SqlConnection(conStr);
            string query2 = "SELECT COUNT(DISTINCT(DistributionNo)) as 'No' FROM FinGoodsDistribution";
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
            textBoxDistributionNo.Text = SNo;
            #endregion
            textBoxTime.Text = now.ToLongTimeString();
        }
        #region Fill Comboo
        SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());
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
            string sql = "SELECT * FROM FinishedGoods";
            SqlCommand cmd = new SqlCommand(sql, conSS);
            SqlDataReader sdr = null;
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                //col.Add(sdr["RawMaterialId"].ToString());
                col.Add(sdr["FinishedGoodsName"].ToString());

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

        string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
        private void textBoxProductSearch_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            string query = "SELECT * FROM FinishedGoods WHERE FinishedGoodsName = '" + textBoxProductSearch.Text + "'";
            SqlCommand command112 = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader12 = command112.ExecuteReader();
            while (reader12.Read())
            {
                textBoxPdoductCode.Text = reader12["FinishedGoodsUnitId"].ToString();
                textBoxProductName.Text = reader12["FinishedGoodsName"].ToString();
                textBoxCurrentStock.Text = reader12["FinishedGoodsStock"].ToString();
                int unit_id = Convert.ToInt32(reader12["FinishedGoodsUnitId"]);
                GetUnitName(unit_id);
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
            string queryCalq = "SELECT * FROM TempDistribution WHERE ProductID = '" + productcode + "'";
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

        private double Currently_Stock()
        {
            double stock = 0;
            string conStrPross = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection connectionPross = new SqlConnection(conStrPross);
            string queryPross = "SELECT * FROM FinishedGoods WHERE FinishedGoodsId = '" + Convert.ToInt32(textBoxPdoductCode.Text) + "'";
            SqlCommand commandPross = new SqlCommand(queryPross, connectionPross);
            connectionPross.Open();
            SqlDataReader readerPross = commandPross.ExecuteReader();

            while (readerPross.Read())
            {
                stock = Convert.ToDouble(readerPross["FinishedGoodsStock"]);
            }

            readerPross.Close();
            connectionPross.Close();
            return stock;
        }

        public void DataGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ID as 'ID', DistributionNo as 'Distribution No', ProductID as 'ProductID', ProductName as 'Product Name', Quantity as 'Quantity' FROM TempDistribution";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewDistribution.DataSource = dt;
            con.Close();
            dataGridViewDistribution.Columns[2].Width = 140;
            dataGridViewDistribution.Columns["Distribution No"].Width = 196;
        }
        public void UpdateTemp_ProductStock(double total_quantity, int pro_id)
        {
            SqlConnection connection1 = new SqlConnection(conStr);
            string query1 = "UPDATE TempDistribution SET Quantity = '" + total_quantity + "' WHERE ID = '" + pro_id + "'";
            SqlCommand command1 = new SqlCommand(query1, connection1);
            connection1.Open();
            int rowEffict1 = command1.ExecuteNonQuery();
            connection1.Close();
            if (rowEffict1 > 0)
            {
                DataGrid();
            }
        }
        public void InsertDataToTempTable(int temp_pro_id, string temp_name, double temp_quantity, string temp_distribution_no)
        {
            SqlConnection connection = new SqlConnection(conStr);
            string query = "INSERT INTO TempDistribution(ProductID, ProductName, Quantity, DistributionNo) VALUES('" + temp_pro_id + "','" + temp_name + "','" + temp_quantity + "','" + temp_distribution_no + "')";
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
            string querys = "SELECT * FROM TempDistribution";
            SqlCommand commands = new SqlCommand(querys, connections);
            connections.Open();
            SqlDataReader readers = commands.ExecuteReader();

            while (readers.Read())
            {
                ProTotal = ProTotal + Convert.ToDouble(readers["Quantity"]);
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
            if (textBoxRequisitionNo.Text == "")
            {
                MessageBox.Show("Please Give Requisition No");
            }
            
            else if (comboBoxRequisitionFor.Text == "")
            {
                MessageBox.Show("Please Select Distribution Area Name");
            }
           
            else if (textBoxRequestBy.Text == "")
            {
                MessageBox.Show("Please Give Request By Name");
            }
            else if (textBoxQuantity.Text == "")
            {
                MessageBox.Show("Please Fill the quantity..");
            }

            else if (textBoxProductSearch.Text == "")
            {
                MessageBox.Show("Please select any product");
            }

            else if ((Convert.ToDouble(textBoxQuantity.Text) + ExitQuantity) > Currently_Stock())
            {
                MessageBox.Show("Out of Stock!!!..\n Currently Stock = " + textBoxCurrentStock.Text + " only.....!!!!!!");
            }

            else
            {
                string temp_distribution_no = textBoxDistributionNo.Text;
                int DistributionArea_id;
                Int32.TryParse(comboBoxRequisitionFor.SelectedValue.ToString(), out DistributionArea_id);
                string temp_requisition_no = textBoxRequisitionNo.Text;
                string temp_requist_by = textBoxRequestBy.Text;
                string temp_remarks = textBoxRemarks.Text;
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
                    InsertDataToTempTable(temp_pro_id, temp_name, temp_quantity, temp_distribution_no);
                    DataGrid();

                }
                Display_Three_Item();
                string hide = textBoxPdoductCode.Text;
                textBoxProductName.Text = textBoxQuantity.Text = textBoxUnitType.Text = textBoxProductSearch.Text = textBoxCurrentStock.Text = textBoxPdoductCode.Text = "";
                //textBoxPdoductCode.Text = hide;
                this.ActiveControl = textBoxProductSearch;
            }
        }

        public void DistributionFormClosed()
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            string sql = @"TRUNCATE TABLE TempDistribution";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void FinishedGoodsDistribution_FormClosed(object sender, FormClosedEventArgs e)
        {
            DistributionFormClosed();
        }

        private void dataGridViewConsumption_DoubleClick(object sender, EventArgs e)
        {
            // Clear_All();
            //SELECT ID as 'ID', DistributionNo as 'Distribution No', ProductID as 'ProductID', ProductName as 'Product Name', Quantity as 'Quantity' FROM TempDistribution
            textBoxPdoductCode.Text = dataGridViewDistribution.SelectedRows[0].Cells[2].Value.ToString();
            textBoxProductName.Text = dataGridViewDistribution.SelectedRows[0].Cells[3].Value.ToString();
            textBoxQuantity.Text = dataGridViewDistribution.SelectedRows[0].Cells[4].Value.ToString();
            buttonAdd.Visible = false;

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (textBoxRequisitionNo.Text == "")
            {
                MessageBox.Show("Please Fill the Requisition No..");
            }
            else if (textBoxRequestBy.Text == "")
            {
                MessageBox.Show("Please Fill Request By Name");
            }
            else if (textBoxQuantity.Text == "")
            {
                MessageBox.Show("Please Fill the quantity..");
            }

            else
            {
                //Temp Purchase
                int temp_pro_id = Convert.ToInt32(textBoxPdoductCode.Text);
                double temp_quantity = Convert.ToInt32(textBoxQuantity.Text);
                if (temp_quantity < 0.99)
                {
                    MessageBox.Show("Quantity can not be zero!!!..");
                }
                else
                {
                    SqlConnection connection1 = new SqlConnection(conStr);
                    string query1 = "UPDATE TempDistribution SET Quantity = '" + temp_quantity + "' WHERE ProductID = '" + temp_pro_id + "'";
                    SqlCommand command1 = new SqlCommand(query1, connection1);
                    connection1.Open();
                    int rowEffict1 = command1.ExecuteNonQuery();
                    connection1.Close();
                    if (rowEffict1 > 0)
                    {
                        textBoxPdoductCode.Text = textBoxProductName.Text = textBoxQuantity.Text = "";
                        buttonAdd.Visible = true;
                        DataGrid();
                        Display_Three_Item();
                    }
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (textBoxPdoductCode.Text == "")
            {
                MessageBox.Show("Please Select a Product in DataGrid...!!");
            }
            else
            {
                try
                {
                    SqlConnection connection1 = new SqlConnection(conStr);
                    string query1 = @"DELETE FROM TempDistribution WHERE  ProductID = '" + textBoxPdoductCode.Text + "'; ";
                    SqlCommand command1 = new SqlCommand(query1, connection1);
                    connection1.Open();
                    int rowEffict1 = command1.ExecuteNonQuery();
                    connection1.Close();
                    DataGrid();
                    Display_Three_Item();
                    textBoxPdoductCode.Text = textBoxProductName.Text = textBoxQuantity.Text = textBoxUnitType.Text = textBoxRequisitionNo.Text = "";
                    buttonAdd.Visible = true;

                }
                catch (Exception)
                {
                    MessageBox.Show("Please Select a Product in DataGrid...!!");
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DistributionFormClosed();
            Close();
        }

        private void Update_Product_Stock()
        {

            foreach (DataGridViewRow row in dataGridViewDistribution.Rows)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE FinishedGoods SET FinishedGoodsStock =(SELECT FinishedGoodsStock FROM FinishedGoods WHERE FinishedGoodsId = @ProductID) - @Quantity  WHERE FinishedGoodsId = @ProductID", con))
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
        private void Distribution_Details_Insert()
        {
            int requisition_id;
            Int32.TryParse(comboBoxRequisitionFor.SelectedValue.ToString(), out requisition_id);
            foreach (DataGridViewRow row in dataGridViewDistribution.Rows)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO FinGoodsDistribution(DistributionNo, DistributionDate, DistributionTime, DistributionAreaId, FinishedGoodsId, Quantity, DistRequisitionNo, DistRequestBy, DistributionRemarks ) VALUES('" + textBoxDistributionNo.Text + "', '" + textBoxDate.Text + "', '" + textBoxTime.Text + "', '" + requisition_id + "',@ProductID, @Quantity,  '" + textBoxRequisitionNo.Text + "', '" + textBoxRequestBy.Text + "',  '" + textBoxRemarks.Text + "')", con))
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
            textBoxRequisitionNo.Text = String.Empty;
            textBoxRequestBy.Text = String.Empty;
            textBoxRemarks.Text = String.Empty;
            textBoxProductSearch.Text = String.Empty;
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
                    Distribution_Details_Insert();
                    DataGrid();
                    //textBoxSupplierInvoiceNo.Text = textBoxPurchaseNo.Text = "";
                    MessageBox.Show("Distributed Successfully....!!!!");

                    Clear_All_Last();
                    DistributionFormClosed();
                    DataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Form_Load();
            }
        }
    }
}
