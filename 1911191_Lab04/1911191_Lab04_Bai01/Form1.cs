using _1911191_Lab04_Bai01.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1911191_Lab04_Bai01
{
    public partial class frmMain : Form
    {
        QuanLySinhVien qlsv;

        public frmMain()
        {
            InitializeComponent();
        }
        private SinhVien GetSinhVien()
        {
            SinhVien sv = new SinhVien();
            bool gt = true;
            sv.mssv = mtMSSV.Text;
            sv.hoTen = txtHoTen.Text;
            sv.diaChi = txtDC.Text;
            sv.email = txtEmail.Text;
            sv.sdt = mtSDT.Text;
            sv.lop = cbLop.Text;
            sv.ngaySinh = dtNgaySinh.Value;
            if (rbNam.Checked)
            {
                gt = true;
            }
            else
            {
                gt = false;
            }
            sv.phai = gt;
            sv.hinh = txtHinh.Text;
            return sv;
        }

        private SinhVien GetSVList(ListViewItem lvi)
        {
            SinhVien sv = new SinhVien();
            sv.mssv = lvi.SubItems[0].Text;
            sv.hoTen = lvi.SubItems[1].Text;
            sv.phai = lvi.SubItems[2].Text == "Nữ" ? false : true;
            sv.ngaySinh = DateTime.Parse(lvi.SubItems[3].Text);
            sv.lop = lvi.SubItems[4].Text;
            sv.sdt = lvi.SubItems[5].Text;
            sv.email = lvi.SubItems[6].Text;
            sv.diaChi = lvi.SubItems[7].Text;
            sv.hinh = lvi.SubItems[8].Text;
            return sv;
        }
        private void ThietLapThongTin(SinhVien sv)
        {
            mtMSSV.Text = sv.mssv;
            txtHoTen.Text = sv.hoTen;
            if (sv.phai)
                rbNam.Checked = true;
            else
                rbNu.Checked = true;
            dtNgaySinh.Value = sv.ngaySinh;
            cbLop.Text = sv.lop;
            mtSDT.Text = sv.sdt;
            txtEmail.Text = sv.email;
            txtDC.Text = sv.diaChi;
            txtHinh.Text = sv.hinh;
        }
        private void ThemSV(SinhVien sv)
        {
            ListViewItem lvi = new ListViewItem(sv.mssv);
            lvi.SubItems.Add(sv.hoTen);
            string gt = "Nữ";
            if (!sv.phai)
                gt = "Nữ";
            else
                gt = "Nam";
            lvi.SubItems.Add(gt);
            lvi.SubItems.Add(sv.ngaySinh.ToShortDateString());
            lvi.SubItems.Add(sv.lop);
            lvi.SubItems.Add(sv.sdt);
            lvi.SubItems.Add(sv.email);
            lvi.SubItems.Add(sv.diaChi);
            lvi.SubItems.Add(sv.hinh);
            lvDSSV.Items.Add(lvi);
        }
        private void LoadListView()
        {
            lvDSSV.Items.Clear();
            foreach (SinhVien sv in qlsv.danhSach)
            {
                ThemSV(sv);
            }
        }
        private int SoSanhTheoMa(object o1, object o2)
        {
            SinhVien sv = o2 as SinhVien;
            return sv.mssv.CompareTo(o1);
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            qlsv = new QuanLySinhVien();
            qlsv.DocTuFile();
            LoadListView();
            tsslSoSV.Text = "Tổng số sinh viên là: " + lvDSSV.Items.Count;
        }
        private void btnDefault_Click(object sender, EventArgs e)
        {
            mtMSSV.Text = "";
            txtHoTen.Text = "";
            txtEmail.Text = "";
            dtNgaySinh.Value = DateTime.Now;
            txtDC.Text = "";
            txtHinh.Text = "";
            rbNam.Checked = true;
            pbHinh.ImageLocation = "";
            pbHinh.Image = null;
            cbLop.Text = cbLop.Items[0].ToString();
            mtSDT.Text = "";
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnHinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseIMG = new OpenFileDialog();
            browseIMG.Filter = "Các file hình ảnh|.jpg; .jpeg; .gif; .bmp; .png";
            browseIMG.InitialDirectory = Environment.CurrentDirectory;
            if (browseIMG.ShowDialog() == DialogResult.OK)
            {
                txtHinh.Text = browseIMG.FileName;
                pbHinh.Image = new Bitmap(browseIMG.FileName);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SinhVien sv = GetSinhVien();
            SinhVien kqThem = qlsv.Tim(sv.mssv, delegate (object o1, object o2)
            {
                return (o2 as SinhVien).mssv.CompareTo(o1.ToString());
            });
            if (kqThem != null)
            {
                if (qlsv.Sua(sv, sv.mssv, SoSanhTheoMa))
                {
                    this.LoadListView();
                }
            }
            else
            {
                qlsv.Them(sv);
                this.LoadListView();
            }
        }
        private void lvDSSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = this.lvDSSV.SelectedItems.Count;
            if (count > 0)
            {
                ListViewItem lvi = this.lvDSSV.SelectedItems[0];
                SinhVien sv = GetSVList(lvi);
                ThietLapThongTin(sv);
            }
        }

        private void lvDSSV_MouseDown(object sender, MouseEventArgs e)
        {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    ListViewItem item = lvDSSV.GetItemAt(e.X, e.Y);
                    if (item != null)
                    {
                        item.Selected = true;
                        cmsRightClick.Show(lvDSSV, e.Location);
                    }
                }
        }
        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count, i;
            ListViewItem lvi;
            count = this.lvDSSV.Items.Count - 1;
            for (i = count; i >= 0; i--)
            {
                lvi = this.lvDSSV.Items[i];
                if (lvi.Checked)
                {
                    qlsv.Xoa(lvi.SubItems[0].Text, SoSanhTheoMa);
                }
            }
            this.LoadListView();
            this.btnDefault.PerformClick();
        }
    }
}
