using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1911191_Lab04_Bai01.Class
{
    public class SinhVien
    {
        public string mssv;
        public string hoTen;
        public bool phai;
        public DateTime ngaySinh;
        public string lop;
        public string sdt;
        public string email;
        public string diaChi;
        public string hinh;
        public SinhVien()
        {

        }
        public SinhVien(string mssv, string hoTen, bool phai, DateTime ngaySinh, string lop, string sdt, string email, string diaChi, string hinh)
        {
            this.mssv = mssv;
            this.hoTen = hoTen;
            this.phai = phai;
            this.ngaySinh = ngaySinh;
            this.lop = lop;
            this.sdt = sdt;
            this.email = email;
            this.diaChi = diaChi;
            this.hinh = hinh;
        }
    }
}
