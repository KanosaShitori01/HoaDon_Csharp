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
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Server=.;Database=QL_CAYXANG;Integrated Security=true");
        public Form1()
        {
            InitializeComponent();
        }
        private void clearDataBind()
        {
            txt_tenhang.DataBindings.Clear();
            txt_dongia.DataBindings.Clear();
            txt_dvt.DataBindings.Clear();
        }
        private void addDataBind(object dt)
        {
            txt_tenhang.DataBindings.Add("Text", dt, "TENHANG");
            txt_dongia.DataBindings.Add("Text", dt, "DONGIA");
            txt_dvt.DataBindings.Add("Text", dt, "DONVITINH");
        }
        private void activeSQLData()
        {
            SqlDataAdapter adap = new SqlDataAdapter("SELECT * FROM HANGHOA", conn);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            cb_mahh.DataSource = dt;
            cb_mahh.DisplayMember = "MAHH";
            cb_mahh.ValueMember = "MAHH";
            clearDataBind();
            addDataBind(dt);
            GridDataFlow();
        }
        public void GridDataFlow()
        {
            string query = String.Format("SELECT HOADON.SOHD, HOADON.NGAY, CAYXANG.TENCAY, CHITIETBANLE.SOLUONG," +
            "HANGHOA.DONGIA, HANGHOA.DONGIA * CHITIETBANLE.SOLUONG AS THANHTIEN FROM" + " " +
            "CAYXANG, CHITIETBANLE, HANGHOA, HOADON" + " " +
            "WHERE CAYXANG.MACAY = HOADON.MACAY AND" + " " +
            "CHITIETBANLE.MAHH = HANGHOA.MAHH AND" + " " +
            "CHITIETBANLE.SOHD = HOADON.SOHD AND" + " " +
            "HANGHOA.MAHH = '{0}'", cb_mahh.Text);
            SqlDataAdapter adapGrid = new SqlDataAdapter(query, conn);
            DataTable dataTB = new DataTable();
            adapGrid.Fill(dataTB);
            dataGV.DataSource = dataTB;
            sumCalculator();
        }
        private void sumCalculator()
        {
            int sumSL = 0;
            int sumTT = 0;
            for (int i = 0; i <= dataGV.RowCount - 1; i++)
            {
                sumSL += Convert.ToInt32(dataGV.Rows[i].Cells["SOLUONG"].Value);
                sumTT += Convert.ToInt32(dataGV.Rows[i].Cells["THANHTIEN"].Value);
            }
            lb_soluong.Text = sumSL.ToString();
            lb_thanhtien.Text = sumTT.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            activeSQLData();
        }

        private void lb_soluong_Click(object sender, EventArgs e)
        {

        }

        private void cb_mahh_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            GridDataFlow();
        }
    }
}
