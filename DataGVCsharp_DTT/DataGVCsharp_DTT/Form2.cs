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
        BindingSource bs;
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
            string query = String.Format("SELECT COUNT(MASP) FROM CTHOADON WHERE MAHD='{0}'", cb_mahd.Text);
            SqlCommand cmd = new SqlCommand(query, conn);
            int tt = 0;
            cmd.Connection.Open();
            tt = Convert.ToInt32(cmd.ExecuteScalar());
            txt_tssp.Text = tt.ToString();
            conn.Close();
        }
        private void SyncBSource(object dt)
        {
           
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
            bs = new BindingSource();
            bs.DataSource = dt;
            bindNavigator.BindingSource = bs;
            cb_mahd.DataSource = bs;
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

        private void dataGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {

        }

        private void btn_gxcsdl_Click(object sender, EventArgs e)
        {

        }
    }
}
