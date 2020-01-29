using DataMigrationConsoleTools.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMigrationConsoleTools
{
    public static class MigrationTools
    {

        // cd_client
        public static void GetSalutation(this cd_client_ client, string maritalStatus, string jenisKelamin)
        {
            var dbMGULDBDev = new MIGULDBDevEntities();
            var dbMultiUserDB = new MultiUserDBEntities();
            var dbMGULDB = new DataModels.PLDataModels.MIGULDBEntitiesNew();
            var listSWMaritalStatus = dbMGULDB.Database.SqlQuery<DataModels.PLDataModels.sw_marital_status>("select * from sw_marital_status").ToList();
            string salutation = "";
            if (jenisKelamin.ToLower() == "f" && maritalStatus.ToLower() == "menikah")
            {
                salutation = "Mrs";
            }
            else if (jenisKelamin.ToLower() == "f" && maritalStatus.ToLower() == "belum menikah")
            {
                salutation = "Ms";
            }
            else if (jenisKelamin.ToLower() == "f" && maritalStatus.ToLower() == "Janda/Duda")
            {
                salutation = "Mrs";
            }
            else if (jenisKelamin.ToLower() == "m")
            {
                salutation = "Mr";
            }
            else
            {
                salutation = "";
            }
            client.salutation = salutation;
            dbMGULDBDev.Dispose();
            dbMultiUserDB.Dispose();
            dbMGULDB.Dispose();
        }
        public static void GetMaritalStatusCode(this cd_client_ client, string maritalStatus, string jenisKelamin)
        {
            var dbMGULDBDev = new MIGULDBDevEntities();
            var dbMultiUserDB = new MultiUserDBEntities();
            var dbMGULDB = new DataModels.PLDataModels.MIGULDBEntitiesNew();
            var listSWMaritalStatus = dbMGULDB.Database.SqlQuery<DataModels.PLDataModels.sw_marital_status>("select * from sw_marital_status").ToList();

            if (maritalStatus.ToLower() == "menikah")
            {
                maritalStatus = "KAWIN";
            }
            else if (maritalStatus.ToLower() == "belum menikah")
            {
                maritalStatus = "BELUM KAWIN";
            }
            else if (maritalStatus.ToLower() == "janda/duda")
            {
                if (jenisKelamin.ToLower() == "f")
                {
                    maritalStatus = "JANDA";
                }
                else if (jenisKelamin.ToLower() == "m")
                {
                    maritalStatus = "DUDA";
                }
            }
            else
            {
                maritalStatus = null;
            }

            var selectedMaritalStatus = listSWMaritalStatus.Where(x => x.marital_status_name == maritalStatus).FirstOrDefault();
            maritalStatus = selectedMaritalStatus != null ? selectedMaritalStatus.marital_status_code : null;
            client.marital_status_code = maritalStatus;

            dbMGULDBDev.Dispose();
            dbMultiUserDB.Dispose();
            dbMGULDB.Dispose();
        }
        public static void GetOccupation(this cd_client_ client, string oldOccupation)
        {
            var dbMGULDBDev = new MIGULDBDevEntities();
            var dbMultiUserDB = new MultiUserDBEntities();
            var dbMGULDB = new DataModels.PLDataModels.MIGULDBEntitiesNew();
            //var listOccupation = dbMGULDB.sw_occupation;
            //var selectedOcc = listOccupation.Where(x => x.occupation_name == oldOccupation).FirstOrDefault();
            //client.occupation_code = selectedOcc != null ? selectedOcc.occupation_code : null;
            string occupationCode;
            if (oldOccupation == "WIRASWASTA")
            {
                occupationCode = "144";
            }
            else if (oldOccupation == "PEMILIK")
            {
                occupationCode = "100";
            }
            else if (oldOccupation == "TNI/Polri")
            {
                occupationCode = "48";
            }
            else if (oldOccupation == "Wiraswasta/Pengusaha")
            {
                occupationCode = "144";
            }
            else if (oldOccupation == "Karyawan Swasta")
            {
                occupationCode = "63";
            }
            else if (oldOccupation == "PELAJAR/MAHASISWA")
            {
                occupationCode = "76";
            }
            else if (oldOccupation == "Pensiunan")
            {
                occupationCode = "213";
            }
            else if (oldOccupation == "Lainnya")
            {
                occupationCode = "";
            }
            else if (oldOccupation == "Wiraswasta/Pengusaha.")
            {
                occupationCode = "144";
            }
            else if (oldOccupation == "Pegawai BUMN/BUMD")
            {
                occupationCode = "";
            }
            else if (oldOccupation == "PNS")
            {
                occupationCode = "62";
            }
            else if (oldOccupation == "Ibu Rumah Tangga")
            {
                occupationCode = "78";
            }
            else
            {
                occupationCode = "";
            }
            client.occupation_code = occupationCode;

            dbMGULDBDev.Dispose();
            dbMultiUserDB.Dispose();
            dbMGULDB.Dispose();
        }
        public static void GetOccupationCategory(this cd_client_ client, string oldOccupation)
        {
            var dbMGULDBDev = new MIGULDBDevEntities();
            var dbMultiUserDB = new MultiUserDBEntities();
            var dbMGULDB = new DataModels.PLDataModels.MIGULDBEntitiesNew();
            //var listOccupation = dbMGULDB.sw_occupation;
            //var selectedOcc = listOccupation.Where(x => x.occupation_name == oldOccupation).FirstOrDefault();
            //client.occupation_code = selectedOcc != null ? selectedOcc.occupation_code : null;
            string occupationCode;
            if (oldOccupation == "WIRASWASTA")
            {
                occupationCode = "8";
            }
            else if (oldOccupation == "PEMILIK")
            {
                occupationCode = "126";
            }
            else if (oldOccupation == "TNI/Polri")
            {
                occupationCode = "24";
            }
            else if (oldOccupation == "Wiraswasta/Pengusaha")
            {
                occupationCode = "8";
            }
            else if (oldOccupation == "Karyawan Swasta")
            {
                occupationCode = "11";
            }
            else if (oldOccupation == "PELAJAR/MAHASISWA")
            {
                occupationCode = "44";
            }
            else if (oldOccupation == "Pensiunan")
            {
                occupationCode = "48";
            }
            else if (oldOccupation == "Lainnya")
            {
                occupationCode = "0";
            }
            else if (oldOccupation == "Wiraswasta/Pengusaha.")
            {
                occupationCode = "8";
            }
            else if (oldOccupation == "Pegawai BUMN/BUMD")
            {
                occupationCode = "58";
            }
            else if (oldOccupation == "PNS")
            {
                occupationCode = "12";
            }
            else if (oldOccupation == "Ibu Rumah Tangga")
            {
                occupationCode = "9";
            }
            else
            {
                occupationCode = "";
            }
            client.category_code = occupationCode;

            dbMGULDBDev.Dispose();
            dbMultiUserDB.Dispose();
            dbMGULDB.Dispose();
        }
        public static void GetMailProvinceCode(this cd_client_ client, string provinceName)
        {
            var dbMGULDBDev = new MIGULDBDevEntities();
            var dbMultiUserDB = new MultiUserDBEntities();
            var dbMGULDB = new DataModels.PLDataModels.MIGULDBEntitiesNew();
            if (provinceName.ToLower() == "ambon")
            {
                provinceName = "MALUKU";
            }
            else if (provinceName.ToLower() == "bangka belitung")
            {
                provinceName = "KEPULAUAN BANGKA-BELITUNG";
            }
            else if (provinceName.ToLower() == "DI YOGYAKARTA".ToLower())
            {
                provinceName = "DAERAH ISTIMEWA YOGYAKARTA";
            }
            else if (provinceName.ToLower() == "NUSA TENGGARA TIMUR (NTT)".ToLower())
            {
                provinceName = "NUSA TENGGARA TIMUR";
            }
            var listProvince = dbMGULDB.sw_province.ToList();
            var selectedProvince = listProvince.Where(x => x.province_name.ToLower() == provinceName.ToLower()).FirstOrDefault();
            client.mail_province_code = selectedProvince != null ? selectedProvince.province_code : null;
            if(provinceName != null && selectedProvince == null)
            {
                client.mail_province_code = null;
            }
            dbMGULDBDev.Dispose();
            dbMultiUserDB.Dispose();
            dbMGULDB.Dispose();
        }
        public static void GetResidenceProvinceCode(this cd_client_ client, string provinceName)
        {
            if (provinceName.ToLower() == "ambon") {
                provinceName = "MALUKU";
            }
            else if (provinceName.ToLower() == "bangka belitung")
            {
                provinceName = "KEPULAUAN BANGKA-BELITUNG";
            }
            else if (provinceName.ToLower() == "DI YOGYAKARTA".ToLower())
            {
                provinceName = "DAERAH ISTIMEWA YOGYAKARTA";
            }
            else if (provinceName.ToLower() == "NUSA TENGGARA TIMUR (NTT)".ToLower())
            {
                provinceName = "NUSA TENGGARA TIMUR";
            }

            var dbMGULDBDev = new MIGULDBDevEntities();
            var dbMultiUserDB = new MultiUserDBEntities();
            var dbMGULDB = new DataModels.PLDataModels.MIGULDBEntitiesNew();

            var listProvince = dbMGULDB.sw_province.ToList();
            var selectedProvince = listProvince.Where(x => x.province_name.ToLower() == provinceName.ToLower()).FirstOrDefault();
            client.residence_province_code = selectedProvince != null ? selectedProvince.province_code : null;
            if (provinceName != null && selectedProvince == null)
            {
                client.residence_province_code = null;
            }
            dbMGULDBDev.Dispose();
            dbMultiUserDB.Dispose();
            dbMGULDB.Dispose();
        }
        public static void GetResidenceCity(this cd_client_ client, string name)
        {
            name = name.Trim();
            if (name.ToLower() == "bandar lampung")
            {
                name = "bandarlampung";
            }
            else if (name.ToLower() == "pangkal pinang")
            {
                name = "pangkalpinang";
            }
            else if (name.ToLower() == "tangerang selatan")
            {
                name = "tangerang";
            }
           

            var dbMGULDBDev = new MIGULDBDevEntities();
            var dbMultiUserDB = new MultiUserDBEntities();
            var dbMGULDB = new DataModels.PLDataModels.MIGULDBEntitiesNew();

            var listData = dbMGULDB.sw_city.ToList();
            var selectedData = listData.Where(x => x.city_name.ToLower() == name.ToLower()).FirstOrDefault();
            client.residence_city = selectedData != null ? selectedData.city_code : null;
            if (name != null && selectedData == null)
            {
                client.residence_city = null;
            }
            dbMGULDBDev.Dispose();
            dbMultiUserDB.Dispose();
            dbMGULDB.Dispose();
        }
        public static void GetMailCity(this cd_client_ client, string name)
        {
            name = name.Trim();
            if (name.ToLower() == "bandar lampung")
            {
                name = "bandarlampung";
            }
            else if (name.ToLower() == "pangkal pinang")
            {
                name = "pangkalpinang";
            }
            else if (name.ToLower() == "tangerang selatan")
            {
                name = "tangerang";
            }


            var dbMGULDBDev = new MIGULDBDevEntities();
            var dbMultiUserDB = new MultiUserDBEntities();
            var dbMGULDB = new DataModels.PLDataModels.MIGULDBEntitiesNew();

            var listData = dbMGULDB.sw_city.ToList();
            var selectedData = listData.Where(x => x.city_name.ToLower() == name.ToLower()).FirstOrDefault();
            client.mail_city = selectedData != null ? selectedData.city_code : null;
            if (name != null && selectedData == null)
            {
                client.mail_city = null;
            }
            dbMGULDBDev.Dispose();
            dbMultiUserDB.Dispose();
            dbMGULDB.Dispose();
        }
    }
}
