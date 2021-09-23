using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1911191_Lab04_Bai01.Class
{
    public delegate int SoSanh(object sv1, object sv2);
    public class QuanLySinhVien
    {
        public List<SinhVien> danhSach;
        public QuanLySinhVien()
        {
            danhSach = new List<SinhVien>();
        }
        public void Them(SinhVien sv)
        {
            this.danhSach.Add(sv);
        }
        public SinhVien this[int index]
        {
            get { return danhSach[index]; }
            set { danhSach[index] = value; }
        }
        public SinhVien Tim(object obj, SoSanh ss)
        {
            SinhVien svResult = null;
            foreach (SinhVien sv in danhSach)
                if (ss(obj, sv) == 0)
                {
                    svResult = sv;
                    break;
                }
            return svResult;
        }
        public void Xoa(object obj, SoSanh ss)
        {
            int i = danhSach.Count - 1;
            for (; i >= 0; i--)
                if (ss(obj, this[i]) == 0)
                    this.danhSach.RemoveAt(i);
        }
        public bool Sua(SinhVien svSua, object obj, SoSanh ss)
        {
            int i, count;
            bool kq = false;
            count = this.danhSach.Count - 1;
            for (i = 0; i < count; i++)
                if (ss(obj, this[i]) == 0)
                {
                    this[i] = svSua;
                    kq = true;
                    break;
                }
            return kq;
        }
        public void DocTuFile()
        {
            string fileName = "..\\..\\Data\\DSSV.txt", t;
            string[] s;
            SinhVien sv;
            StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open));
            while ((t = sr.ReadLine()) != null)
            {
                s = t.Split(';');
                sv = new SinhVien();
                sv.mssv = s[0];
                sv.hoTen = s[1];
                sv.ngaySinh = DateTime.Parse(s[2]);
                sv.diaChi = s[3];
                sv.lop = s[4];
                sv.hinh = s[5];
                sv.phai = false;
                if (s[6] == "1") sv.phai = true;
                else if (s[6] == "0") sv.phai = false;
                sv.sdt = s[7];
                sv.email = s[8];
                Them(sv);
            }
        }
    }
}
