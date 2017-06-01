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
    public partial class AddDistributionArea : Form
    {
        public AddDistributionArea()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());
        private int Already_Distribution_Name(string DistArea_Name)
        {
            int exist = 0;
            string query = "SELECT DistributionAreaId FROM FinGoodsDistributionArea WHERE DistributionAreaName='" + DistArea_Name + "'";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                exist = Convert.ToInt32(reader["DistributionAreaId"]);
            }

            reader.Close();
            con.Close();
            return exist;

        }
        private void DataGrid()
        {

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT DistributionAreaId as 'ID', DistributionAreaName as 'Distribution Area Name' FROM FinGoodsDistributionArea";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewDistArea.DataSource = dt;
            con.Close();
            dataGridViewDistArea.Columns["ID"].Width = 70;
            dataGridViewDistArea.Columns["Distribution Area Name"].Width = 160;
        }

        private void Clear_All()
        {
            textBoxDistributionArea.Text = "";
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string DistributionArea_name = textBoxDistributionArea.Text;

            try
            {
                if (textBoxDistributionArea.Text == "")
                {
                    MessageBox.Show("Please fill the all field...");
                }

                else if (Already_Distribution_Name(textBoxDistributionArea.Text) > 0)
                {
                    Clear_All();
                    MessageBox.Show("Distribution Area Name Already Exist....!!!");
                }

                else
                {
                    string conStr = ConfigurationManager.ConnectionStrings["PosConString"].ToString();
                    SqlConnection connection = new SqlConnection(conStr);
                    string query11 = "INSERT INTO FinGoodsDistributionArea(DistributionAreaName) VALUES('" + DistributionArea_name + "')";
                    SqlCommand command = new SqlCommand(query11, connection);
                    connection.Open();
                    int rowEffict11 = command.ExecuteNonQuery();
                    connection.Close();
                    if (rowEffict11 > 0)
                    {
                        textBoxDistributionArea.Text = string.Empty;
                        DataGrid();
                        MessageBox.Show("Distribution Area Add Successfully !");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddDistributionArea_Load(object sender, EventArgs e)
        {
            DataGrid();
        }

        private void dataGridViewDistArea_DoubleClick(object sender, EventArgs e)
        {
            buttonUpdate.Enabled = true;
            Clear_All();
            textBoxFloorId.Text = dataGridViewDistArea.SelectedRows[0].Cells[0].Value.ToString();
            textBoxDistributionArea.Text = dataGridViewDistArea.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (textBoxDistributionArea.Text == "")
            {
                MessageBox.Show("Please Select a Floor in Datagrid to Edit And then Click Update....!!!");
            }
            else
            {
                if (Already_Distribution_Name(textBoxDistributionArea.Text) > 0)
                {

                    MessageBox.Show("Floor Name Already Exist....!!!");
                }
                else
                {
                    try
                    {
                        SqlConnection cons = new SqlConnection(ConfigurationManager.ConnectionStrings["PosConString"].ToString());

                        string querys = "UPDATE FinGoodsDistributionArea SET DistributionAreaName = '" + textBoxDistributionArea.Text + "'  WHERE DistributionAreaId = '" + textBoxFloorId.Text + "'";
                        SqlCommand commands = new SqlCommand(querys, cons);
                        cons.Open();
                        commands.ExecuteNonQuery();
                        cons.Close();
                        DataGrid();
                        buttonUpdate.Enabled = false;
                        textBoxDistributionArea.Text = "";
                        MessageBox.Show("Distribution Area Updated Successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                }
            }
        }
    }
}
