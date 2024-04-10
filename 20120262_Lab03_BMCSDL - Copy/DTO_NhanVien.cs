using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20120262_Lab04_BMCSDL.DTO
{
    internal class DTO_NhanVien
    {
        public String maNV;
        public String hotenNV;
        public String emailNV;
        public byte[] Luong;
        public String unameNV;
        public byte[] passNV;
        public DTO_NhanVien() { }
        public DTO_NhanVien(DataRow dr)
        {
            maNV = (String)dr["MANV"];
            hotenNV = (String)dr["HOTEN"];
            emailNV = (String)dr["EMAIL"];
            Luong = (byte[])dr["LUONG"];
            unameNV = (String)dr["TENDN"];
            passNV = (byte[])dr["MATKHAU"];

        }
    }
}
