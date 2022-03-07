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
namespace DataGVCsharp_DTT
{
    public partial class Form2 : Form
    {
        SqlConnection conn = new SqlConnection(@"Server=.;Database=QL_HOA_DON;Integrated Security=true");
        public Form2()
        {
            InitializeComponent();
        }
        private void clearDataBind()
        {
            txt_tsl.DataBindings.Clear();
            txt_tssp.DataBindings.Clear();
        }
        private void sumAllCalculator()
        {
            int tsl = 0;
            for (int i = 0; i <= dataGV.RowCount - 1; i++)
            {
                tsl += Convert.ToInt32(dataGV.Rows[i].Cells["SOLUONG"].Value);
            }
            txt_tsl.Text = tsl.ToString();
            txt_tssp.Text = (dataGV.RowCount - 1).ToString();
        }
        private void gridDataFlow()
        {
            string query = String.Format("SELECT * FROM CTHOADON WHERE MAHD='{0}'", cb_mahd.Text);
            SqlDataAdapter adapGrid = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapGrid.Fill(dt);
            clearDataBind();
            dataGV.DataSource = dt;
            sumAllCalculator();
        }
        private void activeDataSQL()
        {
            SqlDataAdapter adap = new SqlDataAdapter("SELECT * FROM HOADON", conn);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            cb_mahd.DataSource = dt;
            cb_mahd.DisplayMember = "MAHD";
            cb_mahd.ValueMember = "MAHD";
            gridDataFlow();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            activeDataSQL();
        }

        private void cb_mahd_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridDataFlow();
        }

        private void txt_tssp_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
