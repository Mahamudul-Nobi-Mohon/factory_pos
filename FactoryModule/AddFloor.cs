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
    public partial class AddFloor : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());

        public AddFloor()
        {
            InitializeComponent();
        }

        private void DataGrid()
        {

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT FactoryFloorId as 'ID', FactoryFloorName as 'Floor Name' FROM FactoryFloor";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewViewFloor.DataSource = dt;
            con.Close();
            dataGridViewViewFloor.Columns["ID"].Width = 70;
            dataGridViewViewFloor.Columns["Floor Name"].Width = 160;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string floor_name = textBoxFloorName.Text;
            
            try
            {
                if (textBoxFloorName.Text == "")
                {
                    MessageBox.Show("Please fill the all field...");
                }
                
                else if (Already_Floor_Name(textBoxFloorName.Text) > 0)
                {
                    Clear_All();
                    MessageBox.Show("Floor Name Already Exist....!!!");
                }

                else
                {
                    string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
                    SqlConnection connection = new SqlConnection(conStr);
                    string query11 = "INSERT INTO FactoryFloor(FactoryFloorName) VALUES('" + floor_name + "')";
                    SqlCommand command = new SqlCommand(query11, connection);
                    connection.Open();
                    int rowEffict11 = command.ExecuteNonQuery();
                    connection.Close();
                    if (rowEffict11 > 0)
                    {
                        textBoxFloorName.Text = string.Empty;
                        DataGrid();
                        MessageBox.Show("Floor Name Add Successfully !");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int Already_Floor_Name(string Floor_Name)
        {
            int exist = 0;
            string query = "SELECT FactoryFloorId FROM FactoryFloor WHERE FactoryFloorName='" + Floor_Name + "'";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                exist = Convert.ToInt32(reader["FactoryFloorId"]);
            }

            reader.Close();
            con.Close();
            return exist;

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (textBoxFloorName.Text == "")
            {
                MessageBox.Show("Please Select a Floor in Datagrid to Edit And then Click Update....!!!");
            }
            else
            {
                if (Already_Floor_Name(textBoxFloorName.Text) > 0)
                {
                    
                    MessageBox.Show("Floor Name Already Exist....!!!");
                }
                else
                {
                    try
                    {
                        SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());

                        string querys = "UPDATE FactoryFloor SET FactoryFloorName = '" + textBoxFloorName.Text + "'  WHERE FactoryFloorId = '" + textBoxFloorId.Text + "'";
                        SqlCommand commands = new SqlCommand(querys, cons);
                        cons.Open();
                        commands.ExecuteNonQuery();
                        cons.Close();
                        DataGrid();
                        buttonUpdate.Enabled = false;
                        textBoxFloorName.Text = "";
                        MessageBox.Show("Floor Name Updated Successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                }
            }
        }

        private void AddFloor_Load(object sender, EventArgs e)
        {
            DataGrid();
        }

        private void Clear_All()
        {
            textBoxFloorName.Text = "";
        }

        private void dataGridViewViewFloor_DoubleClick(object sender, EventArgs e)
        {
            buttonUpdate.Enabled = true;
            Clear_All();
            textBoxFloorId.Text = dataGridViewViewFloor.SelectedRows[0].Cells[0].Value.ToString();
            textBoxFloorName.Text = dataGridViewViewFloor.SelectedRows[0].Cells[1].Value.ToString();
           
        }

    }
}
