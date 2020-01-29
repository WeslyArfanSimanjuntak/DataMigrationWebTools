using DataMigrationConsoleTools.DataModels;
using DataMigrationConsoleTools.DataModels.PLDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMigrationConsoleTools
{
    public class DataMigrationMapper
    {
        MIGULDBDevEntities dbMGULDBDev = new MIGULDBDevEntities();
        MultiUserDBEntities dbMultiUserDB = new MultiUserDBEntities();
        MIGULDBEntitiesNew dbMGULDB = new MIGULDBEntitiesNew();
        ISLGLDB2Entities islgldb2db = new ISLGLDB2Entities();
        List<sw_identity_type> listDataSwIdentityType;
        public DataMigrationMapper()
        {

            listDataSwIdentityType = this.dbMGULDB.sw_identity_type.ToList();
        }
        public string GetBankCode(string name)
        {
            var listIndosurya = new List<string> {
                "indosurya simpan pinjam",
                "indosurya simpan",
                "indosurya simpan pijam",
                "isp",
                "koperasi indosurya simpan pinjam",
                "koperasi simpan pinjam",
                "koperasi simpan pinjam indosurya",
                "kospin",
                "kospin indosurya",
                "kospin insosurya" };



            string retval = null;
            var temp = name;
            name = name.ToLower().Replace("bank ", "");
            name = name.Trim();
            if (name.ToLower() == "artos indonesia")
            {
                name = "artos ind";
            }
            else if (name.ToLower() == "banten")
            {
                name = "pundi";
            }
            else if (name.ToLower() == "bca tahapan xpresi")
            {
                name = "bca";
            }
            else if (name.ToLower() == "bii")
            {
                name = "bii maybank";
            }
            else if (name.ToLower() == "bnp")
            {
                name = "bnp paribas indonesia";
            }
            else if (name.ToLower() == "btn batara")
            {
                name = "tabungan negara (btn)";
            }
            else if (name.ToLower() == "btn batara")
            {
                name = "purba danarta";
            }
            else if (name.ToLower() == "btpn")
            {
                name = "tabungan pensiunan nasional (btpn)";
            }
            else if (name.ToLower() == "btpn syariah")
            {
                name = "purba danarta";
            }
            else if (name.ToLower() == "capital")
            {
                name = "capital indonesia";
            }
            else if (name.ToLower() == "cimb")
            {
                name = "cimb niaga";
            }
            else if (name.ToLower() == "dbs")
            {
                name = "dbs indonesia";
            }
            else if (name.ToLower() == "fama")
            {
                name = "fama internasional";
            }
            else if (name.ToLower() == "fama bank")
            {
                name = "fama internasional";
            }
            else if (name.ToLower() == "hana")
            {
                name = "bintang manunggal";
            }
            else if (name.ToLower() == "hsbc")
            {
                name = "ekonomi";
            }
            else if (listIndosurya.Where(x => x.ToLower() == name.ToLower()).FirstOrDefault() != null)
            {
                name = "Indosurya Simpan Pinjam";
            }
            else if (name.ToLower() == "mas")
            {
                name = "multi arta sentosa";
            }
            else if (name.ToLower() == "maspion indonesia")
            {
                name = "maspion";
            }
            else if (name.ToLower() == "maspion indosnesia")
            {
                name = "maspion";
            }
            else if (name.ToLower() == "maybank")
            {
                name = "bii maybank";
            }
            else if (name.ToLower() == "may")
            {
                name = "bii maybank";
            }
            else if (name.ToLower() == "mestika dharma")
            {
                name = "mestika";
            }
            else if (name.ToLower() == "nisp")
            {
                name = "ocbc nisp";
            }
            else if (name.ToLower() == "woori saudara")
            {
                name = "woori indonesia";
            }
            else if (name.ToLower() == "nobu")
            {
                name = "alfindo" +
                    "";
            }
            else if (name.ToLower() == "victoria")
            {
                name = "victoria international";
            }
            else if (name.ToLower() == "uob")
            {
                name = "uob indonesia";
            }
            else if (name.ToLower() == "standard chartered")
            {
                name = "stkamurd chartered bank";
            }
            else if (name.ToLower() == "sinar mas")
            {
                name = "sinarmas";
            }
            else if (name.ToLower() == "sampoerna")
            {
                name = "dipo international";
            }
            else if (name.ToLower() == "sahabat sampoerna")
            {
                name = "dipo international";
            }
            else if (name.ToLower() == "qnb")
            {
                name = "qnb kesawan";
            }
            else if (name.ToLower() == "pt pembangunan daerah banten")
            {
                name = "banten";
            }
            else if (name.ToLower() == "permata")
            {
                name = "permata bank";
            }
            else if (name.ToLower() == "panin bank")
            {
                name = "panin";
            }
            else if (name.ToLower() == "papua")
            {
                name = "BPD PAPUA";
            }
            else if (name.ToLower() == "saudara")
            {
                name = "woori indonesia";
            }
            else if (name.ToLower() == "ocbc")
            {
                name = "ocbc nisp";
            }
            else if (name == "pt mayindonesia, tbk")
            {
                name = "bii maybank";
            }
            else if (name == "ocbc nisp syariah")
            {
                name = "ocbc nisp";
            }
            else if (name == "syariah sinarmas")
            {
                name = "sinarmas";
            }
            else if (name == "sinarmas syariah")
            {
                name = "sinarmas";
            }
            else if (name == "bpr lestari")
            {
                name = "BPR Lestari";
            }

            var listData = this.islgldb2db.acc_bank_bi.ToList();
            var selectedData = listData.Where(x => x.nama_bank_bi.ToLower().Replace("bank ", "") == name.ToLower()).FirstOrDefault();
            retval = selectedData != null ? selectedData.kode_bi : null;
            var listBankName = listData.Select(x => x.nama_bank_bi.ToLower().Replace("bank ", "")).ToList().OrderBy(x => x).ToList();
            if (name != null && selectedData == null)
            {
                retval = null;
            }
            return retval;
        }
        public string GetIdTypeCode(string name)
        {
            string retval = null;

            name = name.Trim().ToLower();
            if (name == "ktp")
            {
                name = "KARTU TANDA PENDUDUK";
            }
            else if (name == "pasport")
            {
                name = "PASPOR";
            }
            else if (name == "lainnya")
            {
                name = "DAN LAIN LAIN";
            }
            else if (name == "sim")
            {
                name = "SURAT IJIN MENGEMUDI";
            }
            else if (name == "akta pendirian")
            {
                name = "AKTA PENDIRIAN";
            }
            else if (name == "kitas")
            {
                name = "KARTU IJIN TINGGAL TERBATAS";
            }
            else if (name == "")
            {
                name = null;
            }
            else if (name == "ktp`")
            {
                name = "KARTU TANDA PENDUDUK";
            }

            var selectedData = listDataSwIdentityType.Where(x => x.id_type_name == name).FirstOrDefault();
            retval = selectedData != null ? selectedData.id_type_code : null;
            if (name != null && selectedData == null)
            {
                retval = null;
            }
            return retval;
        }

        public string GetSalutaion(string maritalStatus, string sex)
        {
            string salutation = "";
            if (sex.ToLower() == "f" && maritalStatus.ToLower() == "menikah")
            {
                salutation = "Mrs";
            }
            else if (sex.ToLower() == "f" && maritalStatus.ToLower() == "belum menikah")
            {
                salutation = "Ms";
            }
            else if (sex.ToLower() == "f" && maritalStatus.ToLower() == "Janda/Duda")
            {
                salutation = "Mrs";
            }
            else if (sex.ToLower() == "m" )
            {
                salutation = "Mr";
            }
            if (sex.ToLower() == "f" && (maritalStatus.ToLower() == "widower" || maritalStatus.ToLower() == "widower" || maritalStatus.ToLower() == "married" || maritalStatus.ToLower() == "maried" || maritalStatus.ToLower() == "divorced" ))
            {
                salutation = "Mrs";
            }
            else if (sex.ToLower() == "f" && maritalStatus.ToLower() == "single")
            {
                salutation = "Ms";
            }
            else if (sex.ToLower() == "m")
            {
                salutation = "Mr";
            }
            else
            {
                salutation = "";
            }
            return salutation;
        }
    }
}
