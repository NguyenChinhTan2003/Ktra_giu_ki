using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL.Entities;
namespace GUI
{
    
    public partial class Form1 : Form
    {
        private readonly SanphamService sanphamService = new SanphamService();
        private readonly LoaiSPService loaiSPService = new LoaiSPService();
        public Form1()
        {
            InitializeComponent();
        }

        private void dgvQLSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
            string Masp = txtMasp.Text;
            
            txtMasp.Text = row.Cells[0].Value.ToString();
            txtTensp.Text= row.Cells[1].Value.ToString();
            dtpNN.Text=row.Cells[2].Value.ToString();
            cmbLoai.Text= row.Cells[3].Value.ToString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var listsanphams = sanphamService.GetAll();
                var listLoaiSPs = loaiSPService.GetAll();
                FillLoaispCombobox(listLoaiSPs);
                BindGrid(listsanphams);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FillLoaispCombobox(List<LoaiSP> listloaiSPs)
        {
            listloaiSPs.Insert(0, new LoaiSP());
            this.cmbLoai.DataSource = listloaiSPs;
            this.cmbLoai.DisplayMember = "TenLoai";
            this.cmbLoai.ValueMember = "MaLoai";
        }
        private void BindGrid(List<Sanpham> listsanphams)
        {
            dgvSanPham.Rows.Clear();
            foreach(var item in listsanphams)
            {
                int index = dgvSanPham.Rows.Add();
                dgvSanPham.Rows[index].Cells[0].Value = item.MaSP;
                dgvSanPham.Rows[index].Cells[1].Value = item.TenSP;
                dgvSanPham.Rows[index].Cells[2].Value = item.Ngaynhap;
                if(item.MaLoai != null)
                dgvSanPham.Rows[index].Cells[3].Value = item.LoaiSP.TenLoai;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn thoát không!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            if(dr == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
            dgvSanPham.Rows.Add(txtMasp.Text, txtTensp.Text, Convert.ToDateTime(dtpNN.Text), cmbLoai.Text);

            Sanpham s = new Sanpham();
            string masp = txtMasp.Text;
            string tensp = txtTensp.Text;
            DateTime ngaynhap = DateTime.Parse(dtpNN.Text);
            string loai = cmbLoai.Text;
            sanphamService.Add(s);
            MessageBox.Show("Thêm dũ liệu thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo);



                if (result == DialogResult.Yes)
                {
                   
                    dgvSanPham.Rows.RemoveAt(dgvSanPham.SelectedRows[0].Index);


                    string masp=txtMasp.Text;

                    if (masp!= null)
                    {

                        sanphamService.delete(masp);
                    }
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
               
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(dgvSanPham.SelectedRows.Count > 0)
            {
                string masp=txtMasp.Text;
                string tensp = txtTensp.Text;
                DateTime ngaynhap = DateTime.Parse(dtpNN.Text);
                string loai = cmbLoai.Text;
                Sanpham s = new Sanpham();
                sanphamService.Update(s);
            }
        }
    }
}
