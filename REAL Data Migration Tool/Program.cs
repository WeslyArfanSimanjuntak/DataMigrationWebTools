using REAL_Data_Migration_Tool.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REAL_Data_Migration_Tool
{
    class Program
    {
        static void Main(string[] args)
        {

        }
        static void StartDataMigrationPolisInvesta()
        {
            Console.WriteLine("Polis Investa Data Migration Is Starting..");
            Console.WriteLine(DateTime.Now);

            var dbOldToNewMapping = new OLDToNEWMappingEntities();
            var dbMultiUserDB = new MultiUserDBEntities();


            var tempDate = DateTime.Now.ToString("yyyy-MM-dd");
            var PPdistinctNameAndTTL = dbMultiUserDB.Database.SqlQuery<string>("SELECT  DISTINCT (replace(replace(REPLACE(tt.Nama_PP, ' ', ''), '.', ''), ',', '')+LEFT(CONVERT(VARCHAR,tt.Tgl_PP, 120), 10)) FROM PolisInvesta tt ORDER BY 1").ToList();
            var TTdistinctNameAndTTL = dbMultiUserDB.Database.SqlQuery<string>("SELECT distinct (replace(replace(REPLACE(PolisInvesta.Nama_TT, ' ', ''), '.', ''), ',', '') + LEFT(CONVERT(VARCHAR, PolisInvesta.Tgl_TT, 120), 10)) nameTTL FROM PolisInvesta").ToList();
            var AllClientDistinct = new List<string>();
            AllClientDistinct.AddRange(PPdistinctNameAndTTL);
            AllClientDistinct.AddRange(TTdistinctNameAndTTL);
            AllClientDistinct = AllClientDistinct.Select(x => x.ToLower()).Distinct().OrderBy(x => x).ToList();
            var listCdClient = new List<NewClient>();
            int counter = 0;
            var lastClient = dbOldToNewMapping.NewClient.OrderByDescending(x => x.client_no).FirstOrDefault();
            counter = lastClient != null ? Convert.ToInt32(lastClient.client_no) : 0;
            var listPolisInvesta = dbMultiUserDB.PolisInvesta.ToList();

            Console.WriteLine("Data Migration Is Started");

            //bersihkan data dari table cd_client
            foreach (var item in AllClientDistinct)
            {
                counter++;
                var cdClient = new NewClient();
                cdClient.client_id_temp = item;
                cdClient.client_no = counter.ToString().PadLeft(10, '0');

                var polisInvesta = listPolisInvesta.Where(x => x.Nama_PP.ToLower().Replace(" ", "").Replace(",", "").Replace(".", "") + (x.Tgl_PP.HasValue ? x.Tgl_PP.Value.ToString("yyyy-MM-dd") : "") == item).FirstOrDefault();
                if (polisInvesta != null)
                {
                    //Data PemegangPolis sebagai Client
                    cdClient.policy_no = polisInvesta.Polis;
                    cdClient.policy_product = "Investa";
                    cdClient.ppOrttOrbf = "PP";
                    cdClient.GetSalutation(polisInvesta.Marital_PP, polisInvesta.Jk_PP);
                    cdClient.birth_place = "";
                    cdClient.birth_date = polisInvesta.Tgl_PP.HasValue ? polisInvesta.Tgl_PP.Value : Convert.ToDateTime("01/01/01");
                    cdClient.smoker_status = "N";
                    cdClient.residence_address1 = polisInvesta.Alamat1_PP;
                    cdClient.account_holder = polisInvesta.NamaRek;
                    cdClient.account_holder_relation_code = "";
                    cdClient.mobile_phone = polisInvesta.Hp_PP ?? "";
                    cdClient.email = (polisInvesta.Email_PP ?? "").ToLower();
                    cdClient.mother_name = polisInvesta.Ibu_PP;
                    cdClient.client_name = polisInvesta.Nama_PP;
                    cdClient.gender_code = polisInvesta.Jk_PP.ToUpper();
                    cdClient.GetMaritalStatusCode(polisInvesta.Marital_PP, polisInvesta.Jk_PP);
                    cdClient.GetOccupation(polisInvesta.Occ_PP);
                    cdClient.GetOccupationCategory(polisInvesta.Occ_PP);
                    cdClient.position_code = null;
                    cdClient.client_type_code = null;
                    cdClient.religion_code = null;
                    cdClient.cigarete_perday = null;
                    cdClient.residence_address1 = polisInvesta.Alamat1_PP;
                    cdClient.residence_address2 = polisInvesta.Alamat2_PP;
                    cdClient.residence_phone_no = null;
                    cdClient.residence_fax = null;
                    cdClient.GetResidenceCity(polisInvesta.Kota_PP);
                    cdClient.residence_postal_code = polisInvesta.Kode_Pos1;
                    cdClient.GetResidenceProvinceCode(polisInvesta.Prov_PP);
                    cdClient.office_address1 = null;
                    cdClient.office_address2 = null;
                    cdClient.office_phone_no = null;
                    cdClient.office_fax = null;
                    cdClient.office_city = null;
                    cdClient.office_province_code = null;
                    cdClient.office_postal_code = null;
                    cdClient.mail_address1 = polisInvesta.Alamat1_PP;
                    cdClient.mail_address2 = polisInvesta.Alamat2_PP;
                    cdClient.GetMailCity(polisInvesta.Kota_PP);
                    cdClient.GetMailProvinceCode(polisInvesta.Prov_PP);
                    cdClient.mail_postal_code = polisInvesta.Kode_Pos1;
                    cdClient.bi_code = dataMigrationMapper.GetBankCode(polisInvesta.NamaBank);
                    cdClient.bank_account_no = polisInvesta.NoRek.Replace("-", "").Replace(" ", "");
                    cdClient.bank_branch = null;
                    cdClient.bank_city = null;
                    cdClient.id_type_code = dataMigrationMapper.GetIdTypeCode(polisInvesta.jID_PP);
                    cdClient.no_id = polisInvesta.nID_PP.Replace(" ", "").Replace("-", "").Trim();
                    cdClient.id_issue_date = null;
                    cdClient.id_expire_date = null;
                    cdClient.height = null;
                    cdClient.weight = null;
                    cdClient.monthly_cost = null;
                    cdClient.client_status = null;
                    cdClient.npwp = polisInvesta.NPWP_PP.Replace(" ", "").Replace("-", "").Trim();
                    cdClient.npwp_name = null;
                    cdClient.npwp_address = null;
                    cdClient.front_title = null;
                    cdClient.back_title = null;
                    cdClient.mother_birth_date = null;

                }
                else
                {
                    // Data Tertanggung Untuk dimasukkan sebagai client
                    polisInvesta = listPolisInvesta.Where(x => x.Nama_TT.ToLower().Replace(" ", "").Replace(",", "").Replace(".", "") + (x.Tgl_TT.HasValue ? x.Tgl_TT.Value.ToString("yyyy-MM-dd") : "") == item).FirstOrDefault();
                    cdClient.policy_no = polisInvesta.Polis;
                    cdClient.policy_product = "Investa";
                    cdClient.ppOrttOrbf = "TT";
                    cdClient.GetSalutation(polisInvesta.Marital, polisInvesta.Jkelamin_TT);
                    cdClient.birth_place = "";
                    cdClient.birth_date = polisInvesta.Tgl_TT.HasValue ? polisInvesta.Tgl_TT.Value : Convert.ToDateTime("01/01/01");
                    cdClient.smoker_status = "N";
                    cdClient.residence_address1 = polisInvesta.Alamat1_Tt;
                    cdClient.account_holder = polisInvesta.NamaRek;
                    cdClient.account_holder_relation_code = "";
                    cdClient.mobile_phone = polisInvesta.Hp_TT ?? "";
                    cdClient.email = (polisInvesta.Email_TT ?? "").ToLower();
                    cdClient.mother_name = polisInvesta.Ibu_TT;
                    cdClient.client_name = polisInvesta.Nama_TT;
                    cdClient.gender_code = polisInvesta.Jkelamin_TT.ToUpper();
                    cdClient.GetMaritalStatusCode(polisInvesta.Marital, polisInvesta.Jkelamin_TT);
                    cdClient.GetOccupation(polisInvesta.Occ_TT);
                    cdClient.GetOccupationCategory(polisInvesta.Occ_TT);
                    cdClient.position_code = null;
                    cdClient.client_type_code = null;
                    cdClient.religion_code = null;
                    cdClient.cigarete_perday = null;
                    cdClient.residence_address1 = polisInvesta.Alamat1_Tt;
                    cdClient.residence_address2 = polisInvesta.Alamat2_Tt;
                    cdClient.residence_phone_no = null;
                    cdClient.residence_fax = null;
                    cdClient.GetResidenceCity(polisInvesta.Kota_TT);
                    cdClient.residence_postal_code = polisInvesta.Kode_Pos;
                    cdClient.GetResidenceProvinceCode(polisInvesta.Prov_TT);
                    cdClient.office_address1 = null;
                    cdClient.office_address2 = null;
                    cdClient.office_phone_no = null;
                    cdClient.office_fax = null;
                    cdClient.office_city = null;
                    cdClient.office_province_code = null;
                    cdClient.office_postal_code = null;
                    cdClient.mail_address1 = polisInvesta.Alamat1_Tt;
                    cdClient.mail_address2 = polisInvesta.Alamat2_Tt;
                    cdClient.GetMailCity(polisInvesta.Kota_TT);
                    cdClient.GetMailProvinceCode(polisInvesta.Prov_TT);
                    cdClient.mail_postal_code = polisInvesta.Kode_Pos;
                    //cdClient.bi_code = dataMigrationMapper.GetBankCode(polisInvesta.NamaBank);
                    //cdClient.bank_account_no = polisInvesta.NoRek.Replace("-", "").Replace(" ", "");
                    //cdClient.bank_branch = null;
                    //cdClient.bank_city = null;
                    cdClient.id_type_code = dataMigrationMapper.GetIdTypeCode(polisInvesta.jID_TT);
                    cdClient.no_id = polisInvesta.nID_TT.Replace(" ", "").Replace("-", "").Trim();
                    cdClient.id_issue_date = null;
                    cdClient.id_expire_date = null;
                    cdClient.height = null;
                    cdClient.weight = null;
                    cdClient.monthly_cost = null;
                    cdClient.client_status = null;
                    cdClient.npwp = polisInvesta.NPWP_TT.Replace(" ", "").Replace("-", "").Trim();
                    cdClient.npwp_name = null;
                    cdClient.npwp_address = null;
                    cdClient.front_title = null;
                    cdClient.back_title = null;
                    cdClient.mother_birth_date = null;
                }

                listCdClient.Add(cdClient);
            }


            // Add Beneficiary To Client
            // Tuple information=> nopolis, beneficiary order, name
            var allBeneficiaryNameTuple = new List<(string noPolis, string benType, string name)>();
            List<(string client, DateTime startDate, DateTime? endDate, string plan, string uploadMember)> newDataType = new List<(String client, DateTime startDate, DateTime? endDate, string plan, string uploadMember)>();
            foreach (var item in dbMultiUserDB.PolisInvesta.ToList().Where(x => x.BenNama1 != null))
            {
                allBeneficiaryNameTuple.Add((item.Polis, "BF1", item.BenNama1.Trim()));
            }
            foreach (var item in dbMultiUserDB.PolisInvesta.ToList().Where(x => x.BenNama2 != null))
            {
                allBeneficiaryNameTuple.Add((item.Polis, "BF2", item.BenNama2.Trim()));
            }
            foreach (var item in dbMultiUserDB.PolisInvesta.ToList().Where(x => x.BenNama3 != null))
            {
                allBeneficiaryNameTuple.Add((item.Polis, "BF3", item.BenNama3.Trim()));
            }
            foreach (var item in dbMultiUserDB.PolisInvesta.ToList().Where(x => x.BenNama4 != null))
            {
                allBeneficiaryNameTuple.Add((item.Polis, "BF4", item.BenNama4.Trim()));
            }
            var listClientBene = new List<NewClient>();
            foreach (var item in allBeneficiaryNameTuple.Where(x => x.name != string.Empty))
            {
                var clientBeneficiary = new NewClient();
                clientBeneficiary.policy_no = item.noPolis;
                clientBeneficiary.client_name = item.name;
                clientBeneficiary.ppOrttOrbf = item.benType;
                if (listClientBene.Where(x => x.client_name == clientBeneficiary.client_name).FirstOrDefault() == null)
                {
                    counter++;

                    clientBeneficiary.policy_product = "Investa";
                    clientBeneficiary.client_id_temp = clientBeneficiary.client_name.Replace(" ", "").ToLower() + clientBeneficiary.policy_no;
                    clientBeneficiary.client_no = counter.ToString().PadLeft(10, '0');
                    clientBeneficiary.salutation = "";
                    clientBeneficiary.birth_place = "";
                    clientBeneficiary.birth_date = Convert.ToDateTime("01/01/01");
                    clientBeneficiary.residence_address1 = "";
                    clientBeneficiary.account_holder = "";
                    clientBeneficiary.account_holder_relation_code = "";
                    clientBeneficiary.mobile_phone = "";
                    clientBeneficiary.email = "";
                    clientBeneficiary.mother_name = "";
                    clientBeneficiary.smoker_status = "N";
                    listClientBene.Add(clientBeneficiary);
                }
            }
            dbOldToNewMapping.NewClient.AddRange(listCdClient);
            dbOldToNewMapping.NewClient.AddRange(listClientBene);
            dbOldToNewMapping.SaveChanges();



            dbOldToNewMapping.Dispose();
            dbMultiUserDB.Dispose();
            Console.WriteLine("Polis Investa Data Migration Is Done");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine();

        }
    }
}
