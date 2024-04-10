using _20120262_Lab04_BMCSDL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20120262_Lab04_BMCSDL.DAO
{
    internal class DAO_NhanVien
    {
        private static DAO_NhanVien Instance = null;
        public static DAO_NhanVien get_Instance()
        {
            if (Instance == null)
            {
                Instance = new DAO_NhanVien();
            }
            return Instance;
        }
        public List<DTO_NhanVien> get_List()
        {
            List<DTO_NhanVien> ketQua = new List<DTO_NhanVien>();
            DBConnect dp = new DBConnect();
            String query = "exec SP_SEL_ENCRYPT_NHANVIEN";
            DataTable dt = dp.ExecuteQuery(query);
            foreach (DataRow dtr in dt.Rows)
            {
                DTO_NhanVien NV = new DTO_NhanVien(dtr);
                ketQua.Add(NV);
            }

            return ketQua;
        }
        public void AddNhanVien(DTO_NhanVien nv)
        {
            DBConnect dp = new DBConnect();
            object[] data = new object[6] { nv.maNV, nv.hotenNV, nv.emailNV, nv.Luong, nv.unameNV, nv.passNV };
            dp.ExecuteNonQuery("EXEC SP_INS_ENCRYPT_NHANVIEN  @MANV , @HOTEN , @EMAIL , @LUONG , @TENDN , @MATKHAU", data);
        }
        public void DelNhanVien(DTO_NhanVien nv)
        {
            DBConnect dp = new DBConnect();
            String query = "EXEC SP_DEL_NHANVIEN '" + nv.maNV + "' ";
            dp.ExecuteNonQuery(query);
        }
        public void ChangInfoNhanVien(DTO_NhanVien nv)
        {
            DBConnect dp = new DBConnect();
            object[] data = new object[6] { nv.maNV, nv.hotenNV, nv.emailNV, nv.Luong, nv.unameNV, nv.passNV };
            dp.ExecuteNonQuery("EXEC SP_UPD_NHANVIEN  @MANV , @HOTEN , @EMAIL , @LUONG , @TENDN , @MATKHAU", data);
        }
    }

}
