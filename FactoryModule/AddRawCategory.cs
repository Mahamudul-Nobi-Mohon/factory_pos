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
//using System.Data;
using System.Data.SqlClient;

namespace FactoryModule
{
    public partial class AddRawCategory : Form
    {
        public AddRawCategory()
        {
            InitializeComponent();
        }

        private int AlreadyHas(string RawCategoryName)
        {
            string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
            SqlConnection connection = new SqlConnection(conStr);
            string query = "SELECT * FROM [FactoryRawCategory] WHERE RawCategoryName = '" + RawCategoryName + "'";
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
            cmd.CommandText = "SELECT RawCategoryId as 'Category Id', RawCategoryName as 'Category Name' FROM [FactoryRawCategory]";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewViewUnit.DataSource = dt;
            con.Close();
            dataGridViewViewUnit.Columns["Category Id"].Width = 70;
            dataGridViewViewUnit.Columns["Category Name"].Width = 245;
           
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string unit_name = textBoxRawCategory.Text;
                if (unit_name == "")
                {
                    MessageBox.Show("Please Fill The Text Box.");
                }
                else if (AlreadyHas(textBoxRawCategory.Text) > 0)
                {
                    MessageBox.Show("Raw Category Name Already exit....!!!!!!");
                }
                else
                {

                    string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
                    SqlConnection connection = new SqlConnection(conStr);
                    string query = "INSERT INTO FactoryRawCategory(RawCategoryName) VALUES('" + unit_name + "')";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowEffict = command.ExecuteNonQuery();
                    connection.Close();
                    if (rowEffict > 0)
                    {

                        textBoxRawCategory.Text = string.Empty;
                        DataGrid();
                        MessageBox.Show("Raw Category Name Added Successfully !");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddRawCategory_Load(object sender, EventArgs e)
        {
            DataGrid();
        }


        private void dataGridViewViewUnit_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            buttonUpdate.Enabled = true;
            textBoxUnitID.Text = dataGridViewViewUnit.SelectedRows[0].Cells[0].Value.ToString();
            textBoxRawCategory.Text = dataGridViewViewUnit.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string new_unit_name = textBoxRawCategory.Text;
                if (new_unit_name == "")
                {
                    MessageBox.Show("Please Fill the text box.");
                }
                else if (AlreadyHas(new_unit_name) > 0)
                {
                    MessageBox.Show("Already Exist....!!!!!!");
                }
                else
                {
                    string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
                    SqlConnection connection = new SqlConnection(conStr);
                    string query = "UPDATE FactoryRawCategory SET RawCategoryName = '" + new_unit_name + "' WHERE RawCategoryId ='" + textBoxUnitID.Text + "'";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    int rowEffict = command.ExecuteNonQuery();
                    connection.Close();
                    if (rowEffict > 0)
                    {
                        textBoxRawCategory.Text = textBoxRawCategory.Text = string.Empty;
                        DataGrid();
                        buttonUpdate.Enabled = false;
                        MessageBox.Show("Update Successfully !");
                        
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewViewUnit_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonUpdate.Enabled = true;
            textBoxUnitID.Text = dataGridViewViewUnit.SelectedRows[0].Cells[0].Value.ToString();
            textBoxRawCategory.Text = dataGridViewViewUnit.SelectedRows[0].Cells[1].Value.ToString();
        }

    }
}
