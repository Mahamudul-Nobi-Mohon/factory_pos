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
    public partial class AddFinishedGoods : Form
    {
        public AddFinishedGoods()
        {
            InitializeComponent();
        }
        private void AddFinishedGoods_Load(object sender, EventArgs e)
        {
            string query2 = "SELECT * FROM FactoryUnit";
            addUnitComboBox.SelectedValue = -1;
            fillCombo(addUnitComboBox, query2, "UnitName", "UnitID");
            DataGrid();
            buttonUpdate.Enabled = false;
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());
        private int AlreadyHas(string txtMaterialName)
        {
            string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection connection = new SqlConnection(conStr);
            string query = "SELECT * FROM [FinishedGoods] WHERE FinishedGoodsName = '" + txtMaterialName + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            int rowEffict = Convert.ToInt32(reader.Read());
            connection.Close();
            return rowEffict;

        }

        public void DataGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT FinishedGoods.FinishedGoodsId AS 'ID', FinishedGoods.FinishedGoodsName AS 'Goods Name', FactoryUnit.UnitName AS 'Unit Type', FinishedGoods.FinishedGoodsStock AS 'Current Stock' FROM FinishedGoods INNER JOIN FactoryUnit ON FinishedGoods.FinishedGoodsUnitId = FactoryUnit.UnitID";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            dataGridView1.Columns["ID"].Width = 50;
            dataGridView1.Columns["Goods Name"].Width = 130;
            dataGridView1.Columns["Unit Type"].Width = 140;
            dataGridView1.Columns["Current Stock"].Width = 135;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (texboxfinishedgoodsName.Text == "")
            {
                MessageBox.Show("Please fill the texbox");
            }
            else if (AlreadyHas(texboxfinishedgoodsName.Text) > 0)
            {
                MessageBox.Show("This Name is Already Exist....!!!!!!");
            }
            else
            {
                string con = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
                SqlConnection conn = new SqlConnection(con);
                string query = "INSERT INTO FinishedGoods(FinishedGoodsName, FinishedGoodsUnitId, FinishedGoodsStock) VALUES('" + texboxfinishedgoodsName.Text + "','" + addUnitComboBox.SelectedValue + "', '"+ textBoxInitialStock .Text+ "')";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                DataGrid();
                conn.Close();
                MessageBox.Show("Finished Goods Added Successfully");
                textBoxInitialStock.Text = texboxfinishedgoodsName.Text = addUnitComboBox.Text = string.Empty;
                
            }
        }

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


        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonUpdate.Enabled = true;
            textBoxInitialStock.Enabled = false;
            txtRowMeterialID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            texboxfinishedgoodsName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            addUnitComboBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBoxInitialStock.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (texboxfinishedgoodsName.Text != "")
            {
                if (AlreadyHas(texboxfinishedgoodsName.Text) > 0)
                {
                    MessageBox.Show("Raw Material Name Already Exists...");
                }
                else
                {
                    string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
                    SqlConnection conn = new SqlConnection(conStr);
                    string query = "UPDATE FinishedGoods SET FinishedGoodsName = '" + texboxfinishedgoodsName.Text + "', FinishedGoodsUnitId= '" + addUnitComboBox.SelectedValue + "' WHERE FinishedGoodsId ='" + txtRowMeterialID.Text + "'";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    DataGrid();
                    conn.Close();
                    MessageBox.Show("Finished Goods is Updated");
                    texboxfinishedgoodsName.Text = addUnitComboBox.Text = textBoxInitialStock.Text = string.Empty;
                    textBoxInitialStock.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Please fill the texbox");
            }
        }
    }
}
