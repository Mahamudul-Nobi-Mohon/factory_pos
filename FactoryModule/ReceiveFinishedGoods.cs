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
    public partial class ReceiveFinishedGoods : Form
    {
        public ReceiveFinishedGoods()
        {
            InitializeComponent();
            Form_Load();
            Auto_Complete();
        }

        public string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();

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
        private void Form_Load()
        {

            string query = "SELECT * FROM FactoryFloor";
            fillCombo(comboBoxFloorName, query, "FactoryFloorName", "FactoryFloorId");

            DateTime now = DateTime.Now;
            textBoxDate.Text = now.ToString("yyyy-MM-dd");

            #region Auto Number Generate
            string SNo = "RECFIN-";
            int no = 0;
            string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection con = new SqlConnection(conStr);
            string query2 = "SELECT COUNT(DISTINCT(FinishedGoodsReceiveNo)) as 'No' FROM FinishedGoodsReceive";
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
            #endregion

            textBoxReceiveNo.Text = SNo;
            textBoxTime.Text = now.ToLongTimeString();
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
        private void comboBoxFloorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int val;
            Int32.TryParse(comboBoxFloorName.SelectedValue.ToString(), out val);
            string query = "SELECT * FROM FactorySection WHERE FloorId = " + val;

            fillCombo(comboBoxSectionName, query, "FactorySectionName", "FactorySectionId");
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
        private void textBoxProductSearch_TextChanged(object sender, EventArgs e)
        {

            string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();

            SqlConnection con = new SqlConnection(conStr);
            string query = "SELECT * FROM FinishedGoods WHERE FinishedGoodsName = '" + textBoxProductSearch.Text + "'";
            SqlCommand command112 = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader12 = command112.ExecuteReader();
            while (reader12.Read())
            {
                textBoxPdoductCode.Text = reader12["FinishedGoodsId"].ToString();
                textBoxProductName.Text = reader12["FinishedGoodsName"].ToString();
                textBoxCurrentStock.Text = reader12["FinishedGoodsStock"].ToString();
                int unit_id = Convert.ToInt32(reader12["FinishedGoodsUnitId"]);
                GetUnitName(unit_id);
                // buttonAdd.Visible = true;
                textBoxQuantity.Text = "";

            }
            reader12.Close();
            con.Close();
        }
        public void DataGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ID as 'ID', ProductID as 'ProductID', ProductName as 'Product Name', Quantity as 'Quantity', ReceiveNo as 'Receive No' FROM TempFinGdReceive";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewReceive.DataSource = dt;
            con.Close();
            dataGridViewReceive.Columns["Product Name"].Width = 200;
            dataGridViewReceive.Columns["Receive No"].Width = 170;
        }
        public double Temp_PreviousStock(int productcode)
        {
            double total_quantity = 0;
            string conStrCalq = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection connectionCalq = new SqlConnection(conStrCalq);
            string queryCalq = "SELECT * FROM TempFinGdReceive WHERE ProductID = '" + productcode + "'";
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
            string query1 = "UPDATE TempFinGdReceive SET Quantity = '" + total_quantity + "' WHERE ProductID = '" + pro_id + "'";
            SqlCommand command1 = new SqlCommand(query1, connection1);
            connection1.Open();
            int rowEffict1 = command1.ExecuteNonQuery();
            connection1.Close();
            if (rowEffict1 > 0)
            {
                DataGrid();
            }
        }

        public void TempPurchase(int pro_id, string name, double qantity, string receive_no)
        {
            SqlConnection connection = new SqlConnection(conStr);
            string query = "INSERT INTO TempFinGdReceive(ProductID,ProductName,Quantity,ReceiveNo,ReceiveDate) VALUES('" + pro_id + "','" + name + "','" + qantity + "','" + receive_no + "','" + textBoxDate.Text + "')";
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
            string querys = "SELECT * FROM TempFinGdReceive";
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
            if (comboBoxFloorName.Text == "")
            {
                MessageBox.Show("Please Select Floor Name");
            }
            if (comboBoxSectionName.Text == "")
            {
                MessageBox.Show("Please Select Section Name");
            }
            else if (textBoxReceiveInvoiceNo.Text == "")
            {
                MessageBox.Show("Please Fill the Receive Invoice No..");
            }
            else if (textBoxPdoductCode.Text == "")
            {
                MessageBox.Show("Please Select any Product ..!!");
            }
            else if (textBoxQuantity.Text == "")
            {
                MessageBox.Show("Please Fill the quantity..");
            }

            else
            {
                // For Purchase
                int section_id;
                Int32.TryParse(comboBoxSectionName.SelectedValue.ToString(), out section_id);

                //Temp Purchase
                int temp_pro_id = Convert.ToInt32(textBoxPdoductCode.Text);
                string temp_name = textBoxProductName.Text;
                double temp_quantity = Convert.ToDouble(textBoxQuantity.Text);
                string temp_receive_no = textBoxReceiveNo.Text;
                double temp_stock = Temp_PreviousStock(temp_pro_id);
                if (temp_stock > 0.0)
                {
                    double Quantity_total = temp_stock + temp_quantity;
                    UpdateTemp_ProductStock(Quantity_total, temp_pro_id);
                }

                else
                {
                    TempPurchase(temp_pro_id, temp_name, temp_quantity, temp_receive_no);
                    DataGrid();

                }
                Display_Three_Item();       //Display two items for this form
                string hide = textBoxPdoductCode.Text;
                textBoxProductName.Text = textBoxQuantity.Text = textBoxUnitType.Text = textBoxProductSearch.Text = textBoxCurrentStock.Text = textBoxPdoductCode.Text = "";
                //textBoxPdoductCode.Text = hide;
                this.ActiveControl = textBoxProductSearch;
            }
        }

        public void FormPurchaseClosed()
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            string sql = @"TRUNCATE TABLE TempFinGdReceive";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void ReceiveFinishedGoods_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormPurchaseClosed();
        }

        public void Clear_All_Last()
        {
            textBoxPdoductCode.Text = textBoxProductName.Text = textBoxQuantity.Text = textBoxItemTotal.Text = textBoxProductTotal.Text = textBoxRemarks.Text = "";
        }

        private void Update_Product_Stock()
        {

            foreach (DataGridViewRow row in dataGridViewReceive.Rows)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE FinishedGoods SET FinishedGoodsStock =(SELECT FinishedGoodsStock FROM FinishedGoods WHERE FinishedGoodsId = @ProductID) + @Quantity  WHERE FinishedGoodsId = @ProductID", con))
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

        private void Purchase_Details_Insert()
        {
            int section_id;
            Int32.TryParse(comboBoxSectionName.SelectedValue.ToString(), out section_id);
            foreach (DataGridViewRow row in dataGridViewReceive.Rows)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO FinishedGoodsReceive(FinishedGoodsReceiveNo, FinishedGoodsReceiveSectionId, FinishedGoodsReceiveDate, FinishedGoodsReceiveInvoiceNo, FinishedGoodsReceiveProdId, FinishedGoodsReceiveProdQty, FinishedGoodsReceiveRemarks) VALUES('" + textBoxReceiveNo.Text + "', '" + section_id + "', '" + textBoxDate.Text + "','" + textBoxReceiveInvoiceNo.Text + "', @ProductID, @Quantity, '" + textBoxRemarks.Text + "')", con))
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
        private void buttonReceive_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure, you will Receive these product?", "Receive Finished Goods", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }
            else
            {
                try
                {
                    // int countPurchase = CountPurchaseNo();
                    //UpdateTempPurchaseNo(countPurchase + 1);
                    Update_Product_Stock();
                    Purchase_Details_Insert();
                    DataGrid();
                    textBoxReceiveInvoiceNo.Text = textBoxReceiveNo.Text = "";
                    MessageBox.Show("Receive Finished Goods Successfully....!!!!");

                    Clear_All_Last();
                    FormPurchaseClosed();
                    DataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Form_Load();
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
                    string query1 = @"DELETE FROM TempFinGdReceive WHERE  ProductID = '" + textBoxPdoductCode.Text + "'; ";
                    SqlCommand command1 = new SqlCommand(query1, connection1);
                    connection1.Open();
                    int rowEffict1 = command1.ExecuteNonQuery();
                    connection1.Close();
                    DataGrid();
                    Display_Three_Item();
                    textBoxPdoductCode.Text = textBoxProductName.Text = textBoxQuantity.Text = textBoxUnitType.Text = textBoxReceiveInvoiceNo.Text = "";
                    buttonAdd.Visible = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Please Select a Product in DataGrid...!!");
                }
            }
        }

        private void dataGridViewReceive_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBoxPdoductCode.Text = textBoxProductName.Text = textBoxQuantity.Text = textBoxUnitType.Text = "";
            textBoxPdoductCode.Text = dataGridViewReceive.SelectedRows[0].Cells[1].Value.ToString();
            textBoxProductName.Text = dataGridViewReceive.SelectedRows[0].Cells[2].Value.ToString();
            textBoxQuantity.Text = dataGridViewReceive.SelectedRows[0].Cells[3].Value.ToString();
            buttonAdd.Visible = false;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (textBoxReceiveInvoiceNo.Text == "")
            {
                MessageBox.Show("Please Fill the Receive Invoice No..");
            }
            else if (textBoxPdoductCode.Text == "")
            {
                MessageBox.Show("Please Select a Product on Data Tree view..!!");
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
                    string query1 = "UPDATE TempFinGdReceive SET Quantity = '" + temp_quantity + "' WHERE ProductID = '" + temp_pro_id + "'";
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
    }
}