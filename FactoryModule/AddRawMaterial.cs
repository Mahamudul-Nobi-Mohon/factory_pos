using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace FactoryModule
{
    public partial class AddRawMaterial : Form
    {
        public AddRawMaterial()
        {
            InitializeComponent();
            //DataGridViewColumn column = dataGridView1.Columns[0];
            //column.Width = 60;
        }

        private void RawMaterial_Load(object sender, EventArgs e)
        {
           
            string query = "SELECT * FROM FactoryRawCategory";
            
            cbCategory.SelectedValue = -1;
            fillCombo(cbCategory, query, "RawCategoryName", "RawCategoryId");
            fillCombo(cbCategory, query, "RawCategoryName", "RawCategoryId");

            string query2 = "SELECT * FROM FactoryUnit";
            addUnitComboBox.SelectedValue = -1;
            fillCombo(addUnitComboBox, query2, "UnitName", "UnitID");
            fillCombo(addUnitComboBox, query2, "UnitName", "UnitID");
            DataGrid();
        }

        string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();

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

        private int AlreadyHas(string txtMaterialName)
        {
            string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection connection = new SqlConnection(conStr);
            string query = "SELECT * FROM [RawMaterial] WHERE RawMaterialName = '" + txtMaterialName + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            int rowEffict = Convert.ToInt32(reader.Read());
            connection.Close();
            return rowEffict;

        }
        //public void fillCombo2(ComboBox combo, string query, string displayMember, string valueMember)
        //{
        //    SqlCommand command = new SqlCommand(query, con);
        //    SqlDataAdapter adapter = new SqlDataAdapter(command);
        //    DataTable table = new DataTable();
        //    adapter.Fill(table);
        //    combo.DataSource = table;
        //    combo.DisplayMember = displayMember;
        //    combo.ValueMember = valueMember;

        //}
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if(txtMaterialName.Text == "")
            {
                MessageBox.Show("Please fill the texbox");
            }
            else if (AlreadyHas(txtMaterialName.Text) > 0)
            {
                MessageBox.Show("Raw Category Name Already Exist....!!!!!!");
            }
            else
            {
                string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
                SqlConnection conn = new SqlConnection(conStr);
                string query = "INSERT INTO RawMaterial(RawMaterialCatId,RawMaterialName,RawMaterialUnitId) VALUES('" + cbCategory.SelectedValue.ToString() + "','" + txtMaterialName.Text + "','" + addUnitComboBox.SelectedValue + "')";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                DataGrid();
                conn.Close();
                MessageBox.Show("Raw Material added Successfully");
                cbCategory.Text = txtMaterialName.Text = addUnitComboBox.Text = string.Empty;
                cbCategory.Text = null;
            }
           
        }

        public void DataGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT RawMaterial.RawMaterialId AS 'ID', RawMaterial.RawMaterialName AS 'Material Name', FactoryRawCategory.RawCategoryName AS 'Category', FactoryUnit.UnitName AS 'Unit Type' FROM RawMaterial INNER JOIN FactoryUnit ON RawMaterial.RawMaterialUnitId = FactoryUnit.UnitID INNER JOIN FactoryRawCategory ON RawMaterial.RawMaterialCatId = FactoryRawCategory.RawCategoryId";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            dataGridView1.Columns["ID"].Width = 50;
            dataGridView1.Columns["Material Name"].Width = 130;
            dataGridView1.Columns["Category"].Width = 130;
            dataGridView1.Columns["Unit Type"].Width = 140;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (txtMaterialName.Text != "")
            {
                if (AlreadyHas(txtMaterialName.Text) > 0)
                {
                    MessageBox.Show("Raw Material Name Already Exists...");
                }
                else
                {
                    string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
                    SqlConnection conn = new SqlConnection(conStr);
                    string query = "UPDATE RawMaterial SET RawMaterialCatId = '" + cbCategory.SelectedValue + "',RawMaterialName = '" + txtMaterialName.Text + "',RawMaterialUnitId= '" + addUnitComboBox.SelectedValue + "' WHERE RawMaterialId ='" + txtRowMeterialID.Text + "'";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    DataGrid();
                    conn.Close();
                    MessageBox.Show("Row Material is Updated");
                    cbCategory.Text = txtMaterialName.Text = addUnitComboBox.Text = string.Empty;
                    buttonUpdate.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please fill the texbox");
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonUpdate.Enabled = true;
            txtRowMeterialID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtMaterialName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            cbCategory.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            addUnitComboBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            
        }
    }
}
