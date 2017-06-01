using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace FactoryModule
{
    public partial class AddSection : Form
    {
        public AddSection()
        {
            InitializeComponent();
        }

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

        private void AddSection_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM FactoryFloor";
            comboBoxFloorName.SelectedValue = -1;
            fillCombo(comboBoxFloorName, query, "FactoryFloorName", "FactoryFloorId");
            DataGrid();
        }
        public void DataGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT FactorySection.FactorySectionId AS 'ID', FactorySection.FactorySectionName AS 'Section Name', FactoryFloor.FactoryFloorName AS 'Floor Name' FROM FactorySection INNER JOIN FactoryFloor ON FactorySection.FloorId = FactoryFloor.FactoryFloorId";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonUpdate.Enabled = true;
            textBoxSectionName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            comboBoxFloorName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBoxFloorId.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private int AlreadyHas(string section_name)
        {
            string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection connection = new SqlConnection(conStr);
            string query = "SELECT * FROM FactorySection WHERE FactorySectionName = '" + section_name + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            int rowEffict = Convert.ToInt32(reader.Read());
            connection.Close();
            return rowEffict;

        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxSectionName.Text != "")
            {
                if(AlreadyHas(textBoxSectionName.Text) > 0)
                {
                    MessageBox.Show("Section Name Already Exist....!!!!!!");
                }
                else
                {
                string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
                SqlConnection conn = new SqlConnection(conStr);
                string query = "INSERT INTO FactorySection(FloorId, FactorySectionName) VALUES('" + comboBoxFloorName.SelectedValue.ToString() + "','" + textBoxSectionName.Text + "')";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                DataGrid();
                conn.Close();
                MessageBox.Show("Saction added Successfully");
                textBoxSectionName.Text = comboBoxFloorName.Text = String.Empty;
                }
            }
            else
            {
                MessageBox.Show("Please fill the texbox");
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (textBoxSectionName.Text != "")
            {
                if (AlreadyHas(textBoxSectionName.Text) > 0)
                {
                    MessageBox.Show("Section Name Already Exist....!!!!!!");
                }
                else
                {
                    string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
                    SqlConnection conn = new SqlConnection(conStr);
                    string query = "UPDATE FactorySection SET FloorId = '" + comboBoxFloorName.SelectedValue + "', FactorySectionName = '" + textBoxSectionName.Text + "' WHERE FactorySectionId ='" + textBoxFloorId.Text + "'";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    DataGrid();
                    conn.Close();
                    buttonUpdate.Enabled = false;
                    textBoxSectionName.Text = comboBoxFloorName.Text = String.Empty;
                    MessageBox.Show("Saction Name is Updated");
                }
            }
            else
            {
                MessageBox.Show("Please fill the texbox");
            }
        }
    }
}
