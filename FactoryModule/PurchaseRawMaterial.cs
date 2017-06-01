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
using System.Web.UI;

namespace FactoryModule
{
    public partial class PurchaseRawMaterial : Form
    {
        public PurchaseRawMaterial()
        {
            InitializeComponent();

        }
       // public string SNo = "PURFAC-";
        public string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
        private void PurchaseRawMaterial_Load(object sender, EventArgs e)
        {
            Form_Load();
            Auto_Complete();

            
        }

        private void Form_Load()
        {

            string query = "SELECT * FROM FactorySupplier";
            fillCombo(comboBoxSupplierName, query, "SupplierName", "SupplierID");

            DateTime now = DateTime.Now;
            textBoxDate.Text = now.ToString("yyyy-MM-dd");

            #region Auto Number Generate
            string SNo = "PURFAC-";
            int no = 0;
            string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection con = new SqlConnection(conStr);
            string query2 = "SELECT COUNT(DISTINCT(PurchaseNo)) as 'No' FROM PurchaseRawMaterial";
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

            textBoxPurchaseNo.Text = SNo;
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

        private void comboBoxSupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxAddress.Text = "";
            int id;
            Int32.TryParse(comboBoxSupplierName.SelectedValue.ToString(), out id);
            string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection con = new SqlConnection(conStr);
            string query = "SELECT SupplierAddress FROM FactorySupplier WHERE SupplierID = " + id;
            SqlCommand command112 = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader12 = command112.ExecuteReader();
            while (reader12.Read())
            {
                textBoxAddress.Text = reader12["SupplierAddress"].ToString();
            }
            reader12.Close();
            con.Close();
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

        private void textBoxProductSearch_TextChanged(object sender, EventArgs e)
        {


            //    int id = GetCustId(Custname);

            string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();

            SqlConnection con = new SqlConnection(conStr);
            string query = "SELECT * FROM RawMaterial WHERE RawMaterialName = '"+textBoxProductSearch.Text+"'" ;
            SqlCommand command112 = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader12 = command112.ExecuteReader();
            while (reader12.Read())
            {
                textBoxPdoductCode.Text = reader12["RawMaterialId"].ToString();
                textBoxProductName.Text = reader12["RawMaterialName"].ToString();
                textBoxCurrentStock.Text = reader12["RawMaterialStock"].ToString();
                textBoxPrice.Text = reader12["RawMaterialPurPrice"].ToString();
                int unit_id = Convert.ToInt32(reader12["RawMaterialUnitId"]);
                GetUnitName(unit_id);
                buttonAdd.Visible = true;
                textBoxQuantity.Text = "";

            }
            reader12.Close();
            con.Close();
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

        public void DataGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ID as 'ID', ProductID as 'ProductID', ProductName as 'Product Name', Quantity as 'Quantity', PurchasePrice as 'Price', PurchaseNo as 'Purchase No', PurchasePriceTotal as 'Total' FROM FactoryTempPurchase";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewPurchase.DataSource = dt;
            con.Close();
        }
        public void UpdateTemp_ProductStock(double total_quantity, double total, int pro_id)
        {
            SqlConnection connection1 = new SqlConnection(conStr);
            string query1 = "UPDATE FactoryTempPurchase SET Quantity = '" + total_quantity + "', PurchasePriceTotal = '" + total + "' WHERE ProductID = '" + pro_id + "'";
            SqlCommand command1 = new SqlCommand(query1, connection1);
            connection1.Open();
            int rowEffict1 = command1.ExecuteNonQuery();
            connection1.Close();
            if (rowEffict1 > 0)
            {
                DataGrid();
            }
        }
        public void Display_Three_Item()
        {
            double ProTotal = 0;
            double AmountTotal = 0;
            int i = 0;
            string conStrs = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection connections = new SqlConnection(conStrs);
            string querys = "SELECT * FROM FactoryTempPurchase";
            SqlCommand commands = new SqlCommand(querys, connections);
            connections.Open();
            SqlDataReader readers = commands.ExecuteReader();

            while (readers.Read())
            {
                ProTotal = ProTotal + Convert.ToDouble(readers["Quantity"]);
                AmountTotal = AmountTotal + Convert.ToDouble(readers["PurchasePriceTotal"]);
                i++;
            }
            readers.Close();
            connections.Close();
            textBoxItemTotal.Text = i.ToString();
            textBoxProductTotal.Text = ProTotal.ToString();
            textBoxInvoiceTotalAmount.Text = AmountTotal.ToString();
        }

        public double Temp_PreviousStock(int productcode)
        {
            double total_quantity = 0;
            string conStrCalq = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection connectionCalq = new SqlConnection(conStrCalq);
            string queryCalq = "SELECT * FROM FactoryTempPurchase WHERE ProductID = '" + productcode + "'";
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

        public void TempPurchase(int pro_id, string name, double qantity, double price, string purchase_no, double price_total)
        {
            SqlConnection connection = new SqlConnection(conStr);
            string query = "INSERT INTO FactoryTempPurchase(ProductID,ProductName,Quantity,PurchasePrice,PurchaseNo,PurchasePriceTotal,PurchaseDate) VALUES('" + pro_id + "','" + name + "','" + qantity + "','" + price + "','" + purchase_no + "','" + price_total + "','" + textBoxDate.Text + "')";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowEffict = command.ExecuteNonQuery();
            connection.Close();

        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if(textBoxAddress.Text == "")
            {
                MessageBox.Show("Please Select Customer Name");
            }
            else if (textBoxSupplierInvoiceNo.Text == "")
            {
                MessageBox.Show("Please Fill the Supplier Invoice No..");
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
                int supplier_id;
                Int32.TryParse(comboBoxSupplierName.SelectedValue.ToString(), out supplier_id);

                //Temp Purchase
                int temp_pro_id = Convert.ToInt32(textBoxPdoductCode.Text);
                string temp_name = textBoxProductName.Text;
                double temp_quantity = Convert.ToDouble(textBoxQuantity.Text);
                double temp_price = Convert.ToDouble(textBoxPrice.Text);
                string temp_purchase_no = textBoxPurchaseNo.Text;
                double temp_purchase_total = Convert.ToDouble(textBoxQuantity.Text) * Convert.ToDouble(textBoxPrice.Text);
                double temp_stock = Temp_PreviousStock(temp_pro_id);
                if (temp_stock > 0.0)
                {
                    double Quantity_total = temp_stock + temp_quantity;
                    double total = Quantity_total * temp_price;
                    UpdateTemp_ProductStock(Quantity_total, total, temp_pro_id);
                }

                else
                {
                    TempPurchase(temp_pro_id, temp_name, temp_quantity, temp_price, temp_purchase_no, temp_purchase_total);
                    DataGrid();

                }
                Display_Three_Item();
                string hide = textBoxPdoductCode.Text;
                textBoxProductName.Text = textBoxQuantity.Text = textBoxPrice.Text = textBoxUnitType.Text = textBoxProductSearch.Text = textBoxCurrentStock.Text = textBoxPdoductCode.Text = "";
                //textBoxPdoductCode.Text = hide;
                this.ActiveControl = textBoxProductSearch;
            }
        }

        private void PurchaseRawMaterial_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormPurchaseClosed();
        }

        private void Purchase_Details_Insert()
        {
            string remarks = "Na";
            int supplier_id;
            Int32.TryParse(comboBoxSupplierName.SelectedValue.ToString(), out supplier_id);
            foreach (DataGridViewRow row in dataGridViewPurchase.Rows)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO PurchaseRawMaterial(PurchaseNo,PurchaseDate,SupplierID,PurchaseSupplierInvoiceNo,PurchaseRemarks,PurchaseProductID,PurchaseProductPrice,PurchaseQuantity,PurchaseTotal) VALUES('" + textBoxPurchaseNo.Text + "', '" + textBoxDate.Text + "','" + supplier_id + "','" + textBoxSupplierInvoiceNo.Text + "','" + remarks + "',@ProductID,@Price,@Quantity,@Total)", con))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", row.Cells["ProductID"].Value);
                        cmd.Parameters.AddWithValue("@Price", row.Cells["Price"].Value);
                        cmd.Parameters.AddWithValue("@Quantity", row.Cells["Quantity"].Value);
                        cmd.Parameters.AddWithValue("@Total", row.Cells["Total"].Value);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }


        }

        private void Update_Product_Stock()
        {

            foreach (DataGridViewRow row in dataGridViewPurchase.Rows)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE RawMaterial SET RawMaterialStock =(SELECT RawMaterialStock FROM RawMaterial WHERE RawMaterialId = @ProductID) + @Quantity  WHERE RawMaterialId = @ProductID", con))
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

        public void FormPurchaseClosed()
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            string sql = @"TRUNCATE TABLE FactoryTempPurchase";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void Clear_All_Last()
        {
            textBoxPdoductCode.Text = textBoxProductName.Text = textBoxInvoiceTotalAmount.Text = textBoxPrice.Text = textBoxQuantity.Text = textBoxItemTotal.Text = textBoxProductTotal.Text = "";
        }

        private void buttonPurchase_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure, you will Purchase these product?", "Purchase Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                    textBoxSupplierInvoiceNo.Text = textBoxPurchaseNo.Text = "";
                    MessageBox.Show("Purchase Successfully....!!!!");
                    
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

        private void dataGridViewPurchase_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBoxPdoductCode.Text = textBoxProductName.Text = textBoxQuantity.Text = textBoxPrice.Text = textBoxUnitType.Text = "";
            textBoxPdoductCode.Text = dataGridViewPurchase.SelectedRows[0].Cells[1].Value.ToString();
            textBoxProductName.Text = dataGridViewPurchase.SelectedRows[0].Cells[2].Value.ToString();
            textBoxQuantity.Text = dataGridViewPurchase.SelectedRows[0].Cells[3].Value.ToString();
            textBoxPrice.Text = dataGridViewPurchase.SelectedRows[0].Cells[4].Value.ToString();
            buttonAdd.Visible = false;
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
                    string query1 = @"DELETE FROM FactoryTempPurchase WHERE  ProductID = '" + textBoxPdoductCode.Text + "'; ";
                    SqlCommand command1 = new SqlCommand(query1, connection1);
                    connection1.Open();
                    int rowEffict1 = command1.ExecuteNonQuery();
                    connection1.Close();
                    DataGrid();
                    Display_Three_Item();
                    textBoxPdoductCode.Text = textBoxProductName.Text = textBoxQuantity.Text = textBoxPrice.Text = textBoxUnitType.Text = textBoxSupplierInvoiceNo.Text = "";

                }
                catch (Exception)
                {
                    MessageBox.Show("Please Select a Product in DataGrid...!!");
                }
            }
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (textBoxSupplierInvoiceNo.Text == "")
            {
                MessageBox.Show("Please Fill the Supplier Invoice No..");
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
                    double temp_price = Convert.ToDouble(textBoxPrice.Text);
                    double total = temp_quantity * temp_price;
                    SqlConnection connection1 = new SqlConnection(conStr);
                    string query1 = "UPDATE FactoryTempPurchase SET Quantity = '" + temp_quantity + "',PurchasePrice  = '" + textBoxPrice.Text + "', PurchasePriceTotal = '" + total + "' WHERE ProductID = '" + temp_pro_id + "'";
                    SqlCommand command1 = new SqlCommand(query1, connection1);
                    connection1.Open();
                    int rowEffict1 = command1.ExecuteNonQuery();
                    connection1.Close();
                    if (rowEffict1 > 0)
                    {
                        textBoxPdoductCode.Text = textBoxProductName.Text = textBoxPrice.Text = textBoxQuantity.Text = "";
                        buttonAdd.Visible = true;
                        DataGrid();
                        Display_Three_Item();

                    }

                }

            }
        }
    }
}
