using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMigrationConsoleTools.DataModels;
namespace DataMigrationConsoleTools
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("|====================================== DATA MIGRATION TOOLS ======================================|");

            Console.WriteLine(new DateTime(1, 1, 1));
            ClearDataInMigrationDB();
            StartDataMigrationPolisInvesta();
            StartDataMigrationPolisIPP();
            StartDataMigrationPolisJiwa();
            StartDataMigrationPolisIlias();

            StartDataMigrationPSFundTransactionHistory();
            StartDataMigrationFnFundnav();

            StartDataMigrationILIASBeneficiary();
            StartDataMigrationJiwaBeneficiary();
            Console.WriteLine("|====================================== DATA MIGRATION DONE ======================================|");

            Console.ReadLine();
        }


        static void ClearDataInMigrationDB()
        {
            Console.WriteLine("Deleting Data Is Starting..");
            Console.WriteLine(DateTime.Now);

            var dbMGULDBDev = new MIGULDBDevEntities();
            var dbMGULDB = new DataModels.PLDataModels.MIGULDBEntitiesNew();

            dbMGULDBDev.cd_client_.RemoveRange(dbMGULDBDev.cd_client_);
            dbMGULDBDev.OldClientIdToNewClientId.RemoveRange(dbMGULDBDev.OldClientIdToNewClientId);
            dbMGULDB.cd_client.RemoveRange(dbMGULDB.cd_client);
            dbMGULDBDev.ps_fund_transaction_history_.RemoveRange(dbMGULDBDev.ps_fund_transaction_history_);
            dbMGULDBDev.fn_fundnav_.RemoveRange(dbMGULDBDev.fn_fundnav_);



            dbMGULDBDev.SaveChanges();
            dbMGULDB.SaveChanges();
            dbMGULDBDev.Dispose();
            dbMGULDB.Dispose();
            Console.WriteLine("Deleting Data Is Done");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine();

        }
        static void StartDataMigrationPolisInvesta()
        {
            Console.WriteLine("Polis Investa Data Migration Is Starting..");
            Console.WriteLine(DateTime.Now);

            var dataMigrationMapper = new DataMigrationMapper();
            var dbMGULDBDev = new MIGULDBDevEntities();
            var dbMultiUserDB = new MultiUserDBEntities();
            var dbMGULDB = new DataModels.PLDataModels.MIGULDBEntitiesNew();


            var tempDate = DateTime.Now.ToString("yyyy-MM-dd");
            var PPdistinctNameAndTTL = dbMultiUserDB.Database.SqlQuery<string>("SELECT  DISTINCT (replace(replace(REPLACE(tt.Nama_PP, ' ', ''), '.', ''), ',', '')+LEFT(CONVERT(VARCHAR,tt.Tgl_PP, 120), 10)) FROM PolisInvesta tt ORDER BY 1").ToList();
            var TTdistinctNameAndTTL = dbMultiUserDB.Database.SqlQuery<string>("SELECT distinct (replace(replace(REPLACE(PolisInvesta.Nama_TT, ' ', ''), '.', ''), ',', '') + LEFT(CONVERT(VARCHAR, PolisInvesta.Tgl_TT, 120), 10)) nameTTL FROM PolisInvesta").ToList();
            var AllClientDistinct = new List<string>();
            AllClientDistinct.AddRange(PPdistinctNameAndTTL);
            AllClientDistinct.AddRange(TTdistinctNameAndTTL);
            AllClientDistinct = AllClientDistinct.Select(x => x.ToLower()).Distinct().OrderBy(x => x).ToList();
            var listCdClient = new List<cd_client_>();
            int counter = 0;
            var lastClient = dbMGULDBDev.cd_client_.OrderByDescending(x => x.client_no).FirstOrDefault();
            counter = lastClient != null ? Convert.ToInt32(lastClient.client_no) : 0;
            var listPolisInvesta = dbMultiUserDB.PolisInvesta.ToList();

            Console.WriteLine("Data Migration Is Started");

            //bersihkan data dari table cd_client
            foreach (var item in AllClientDistinct)
            {
                counter++;
                var cdClient = new cd_client_();
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
                    cdClient.GetResidenceProvinceCode(polisInvesta.Prov_PP ?? "");
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
                    cdClient.GetMailProvinceCode(polisInvesta.Prov_PP ?? "");
                    cdClient.mail_postal_code = polisInvesta.Kode_Pos1;
                    cdClient.bi_code = dataMigrationMapper.GetBankCode(polisInvesta.NamaBank);
                    cdClient.bank_account_no = polisInvesta.NoRek.Replace("-", "").Replace(" ", "");
                    cdClient.bank_branch = polisInvesta.CabangRek;
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
                    cdClient.GetResidenceProvinceCode(polisInvesta.Prov_TT ?? "");
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
                    cdClient.GetMailProvinceCode(polisInvesta.Prov_TT ?? "");
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
            foreach (var item in dbMultiUserDB.PolisInvesta.ToList().Where(x => x.Percent1 > 0 && x.BenNama1 != null))
            {
                allBeneficiaryNameTuple.Add((item.Polis, "BF1", item.BenNama1.Trim()));
            }
            foreach (var item in dbMultiUserDB.PolisInvesta.ToList().Where(x => x.Percent2 > 0 && x.BenNama2 != null))
            {
                allBeneficiaryNameTuple.Add((item.Polis, "BF2", item.BenNama2.Trim()));
            }
            foreach (var item in dbMultiUserDB.PolisInvesta.ToList().Where(x => x.Percent3 > 0 && x.BenNama3 != null))
            {
                allBeneficiaryNameTuple.Add((item.Polis, "BF3", item.BenNama3.Trim()));
            }
            foreach (var item in dbMultiUserDB.PolisInvesta.ToList().Where(x => x.Percent4 > 0 && x.BenNama4 != null))
            {
                allBeneficiaryNameTuple.Add((item.Polis, "BF4", item.BenNama4.Trim()));
            }
            var listClientBene = new List<cd_client_>();
            foreach (var item in allBeneficiaryNameTuple.Where(x => x.name != string.Empty))
            {
                var clientBeneficiary = new cd_client_();
                clientBeneficiary.policy_no = item.noPolis;
                clientBeneficiary.client_name = item.name;
                clientBeneficiary.ppOrttOrbf = item.benType;
                if (listClientBene.Where(x => x.client_name == clientBeneficiary.client_name).FirstOrDefault() == null)
                {
                    counter++;

                    clientBeneficiary.birth_date = Convert.ToDateTime("01/01/01");
                    clientBeneficiary.policy_product = "Investa";
                    clientBeneficiary.client_id_temp = clientBeneficiary.client_name.Replace(" ", "").Replace(",", "").Replace(".", "").ToLower() + clientBeneficiary.birth_date.ToString("yyyy-MM-dd");
                    clientBeneficiary.client_no = counter.ToString().PadLeft(10, '0');
                    clientBeneficiary.salutation = "";
                    clientBeneficiary.birth_place = "";
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
            dbMGULDBDev.cd_client_.AddRange(listCdClient);
            dbMGULDBDev.cd_client_.AddRange(listClientBene);
            dbMGULDBDev.SaveChanges();

            var listOldToNewClientId = new List<OldClientIdToNewClientId>();
            foreach (var item in listCdClient)
            {
                var oldClientIdToNewClientId = new OldClientIdToNewClientId();
                oldClientIdToNewClientId.OldId = item.client_id_temp;
                oldClientIdToNewClientId.NewId = item.client_no;
                oldClientIdToNewClientId.DatabaseName = "Investa";
                oldClientIdToNewClientId.TableName = "PolisInvesta";
                listOldToNewClientId.Add(oldClientIdToNewClientId);
            }

            foreach (var item in listClientBene)
            {
                var oldClientIdToNewClientId = new OldClientIdToNewClientId();
                oldClientIdToNewClientId.OldId = item.client_id_temp;
                oldClientIdToNewClientId.NewId = item.client_no;
                oldClientIdToNewClientId.DatabaseName = "Investa";
                oldClientIdToNewClientId.TableName = "PolisInvesta";
                listOldToNewClientId.Add(oldClientIdToNewClientId);
            }
            dbMGULDBDev.OldClientIdToNewClientId.AddRange(listOldToNewClientId);
            dbMGULDBDev.SaveChanges();
            dbMGULDBDev.Dispose();
            dbMultiUserDB.Dispose();
            dbMGULDB.Dispose();
            Console.WriteLine("Polis Investa Data Migration Is Done");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine();

        }
        static void StartDataMigrationPolisIPP()
        {
            Console.WriteLine("Polis IPP Data Migration Is Starting..");
            Console.WriteLine(DateTime.Now);

            var dataMigrationMapper = new DataMigrationMapper();
            var dbMGULDBDev = new MIGULDBDevEntities();
            var dbMultiUserDB = new MultiUserDBEntities();
            var dbMGULDB = new DataModels.PLDataModels.MIGULDBEntitiesNew();

            var allPolisIPP = dbMultiUserDB.PolisIPP.ToList();
            var distinctNameAndTtlPh = allPolisIPP.Select(x => new
            {
                policyNo = x.No_Polis,
                nameAndTtl = x.Policyholder_Name.Replace(" ", "").Replace(".", "").Replace(",", "") + new DateTime(x.Policyholder_YearLahir.Value, x.Policyholder_MonthLahir.Value, x.Policyholder_DayLahir.Value).ToString("yyyy-MM-dd")
            });
            var distinctNameAndTtlInsured = allPolisIPP.Select(x => new
            {
                policyNo = x.No_Polis,
                nameAndTtl = x.Insured_Name.Replace(" ", "").Replace(".", "").Replace(",", "") + new DateTime(x.Insured_YearLahir.Value, x.Insured_MonthLahir.Value, x.Insured_DayLahir.Value).ToString("yyyy-MM-dd")
            });
            var distinctNameAndTtlBen1 = allPolisIPP.Select(x => new
            {
                policyNo = x.No_Polis,
                nameAndTtl = x.PihakYgDitunjuk_Nama1.Replace(" ", "").Replace(".", "").Replace(",", "") + new DateTime(x.PihakYgDitunjuk_Year1.Value, x.PihakYgDitunjuk_Month1.Value, x.PihakYgDitunjuk_Day1.Value).ToString("yyyy-MM-dd")
            });

            var distinctNameAndTtlBen2 = allPolisIPP.Where(x => !string.IsNullOrWhiteSpace(x.PihakYgDitunjuk_Nama2) && x.PihakYgDitunjuk_Year1 != null && x.PihakYgDitunjuk_Month1 != null && x.PihakYgDitunjuk_Day1 != null).Select(x => new
            {
                policyNo = x.No_Polis,
                nameAndTtl = x.PihakYgDitunjuk_Nama2.Replace(" ", "").Replace(".", "").Replace(",", "") +
                 new DateTime(
                 x.PihakYgDitunjuk_Year2.HasValue && x.PihakYgDitunjuk_Year2.Value != 0 ? x.PihakYgDitunjuk_Year2.Value : 1,
                 x.PihakYgDitunjuk_Month2.HasValue && x.PihakYgDitunjuk_Month2.Value != 0 ? x.PihakYgDitunjuk_Month2.Value : 1,
                 x.PihakYgDitunjuk_Day2.HasValue && x.PihakYgDitunjuk_Day2.Value != 0 ? x.PihakYgDitunjuk_Day2.Value : 1).ToString("yyyy-MM-dd")
            });

            var distinctNameAndTtlBen3 = allPolisIPP.Where(x => !string.IsNullOrWhiteSpace(x.PihakYgDitunjuk_Nama3) && x.PihakYgDitunjuk_Year1 != null && x.PihakYgDitunjuk_Month1 != null && x.PihakYgDitunjuk_Day1 != null).Select(x => new
            {
                policyNo = x.No_Polis,
                nameAndTtl = x.PihakYgDitunjuk_Nama3.Replace(" ", "").Replace(".", "").Replace(",", "") +
                  new DateTime(
                  x.PihakYgDitunjuk_Year3.HasValue && x.PihakYgDitunjuk_Year3.Value != 0 ? x.PihakYgDitunjuk_Year3.Value : 1,
                  x.PihakYgDitunjuk_Month3.HasValue && x.PihakYgDitunjuk_Month3.Value != 0 ? x.PihakYgDitunjuk_Month3.Value : 1,
                  x.PihakYgDitunjuk_Day3.HasValue && x.PihakYgDitunjuk_Day3.Value != 0 ? x.PihakYgDitunjuk_Day3.Value : 1).ToString("yyyy-MM-dd")
            });
            var distinctNameAndTtlBen4 = allPolisIPP.Where(x => !string.IsNullOrWhiteSpace(x.PihakYgDitunjuk_Nama4) && x.PihakYgDitunjuk_Year1 != null && x.PihakYgDitunjuk_Month1 != null && x.PihakYgDitunjuk_Day1 != null).Select(x => new
            {
                policyNo = x.No_Polis,
                nameAndTtl = x.PihakYgDitunjuk_Nama4.Replace(" ", "").Replace(".", "").Replace(",", "") +
                   new DateTime(
                   x.PihakYgDitunjuk_Year4.HasValue && x.PihakYgDitunjuk_Year4.Value != 0 ? x.PihakYgDitunjuk_Year4.Value : 1,
                   x.PihakYgDitunjuk_Month4.HasValue && x.PihakYgDitunjuk_Month4.Value != 0 ? x.PihakYgDitunjuk_Month4.Value : 1,
                   x.PihakYgDitunjuk_Day4.HasValue && x.PihakYgDitunjuk_Day4.Value != 0 ? x.PihakYgDitunjuk_Day4.Value : 1).ToString("yyyy-MM-dd")
            });
            var distinctNameAndTtlBen5 = allPolisIPP.Where(x => !string.IsNullOrWhiteSpace(x.PihakYgDitunjuk_Nama5) && x.PihakYgDitunjuk_Year1 != null && x.PihakYgDitunjuk_Month1 != null && x.PihakYgDitunjuk_Day1 != null).Select(x => new
            {
                policyNo = x.No_Polis,
                nameAndTtl = x.PihakYgDitunjuk_Nama5.Replace(" ", "").Replace(".", "").Replace(",", "") +
                            new DateTime(
                            x.PihakYgDitunjuk_Year5.HasValue && x.PihakYgDitunjuk_Year5.Value != 0 ? x.PihakYgDitunjuk_Year5.Value : 1,
                            x.PihakYgDitunjuk_Month5.HasValue && x.PihakYgDitunjuk_Month5.Value != 0 ? x.PihakYgDitunjuk_Month5.Value : 1,
                            x.PihakYgDitunjuk_Day5.HasValue && x.PihakYgDitunjuk_Day5.Value != 0 ? x.PihakYgDitunjuk_Day5.Value : 1).ToString("yyyy-MM-dd")
            });
            var distinctNameAndTtlBen6 = allPolisIPP.Where(x => !string.IsNullOrWhiteSpace(x.PihakYgDitunjuk_Nama6) && x.PihakYgDitunjuk_Year1 != null && x.PihakYgDitunjuk_Month1 != null && x.PihakYgDitunjuk_Day1 != null).Select(x => new
            {
                policyNo = x.No_Polis,
                nameAndTtl = x.PihakYgDitunjuk_Nama6.Replace(" ", "").Replace(".", "").Replace(",", "") +
                            new DateTime(
                            x.PihakYgDitunjuk_Year6.HasValue && x.PihakYgDitunjuk_Year6.Value != 0 ? x.PihakYgDitunjuk_Year6.Value : 1,
                            x.PihakYgDitunjuk_Month6.HasValue && x.PihakYgDitunjuk_Month6.Value != 0 ? x.PihakYgDitunjuk_Month6.Value : 1,
                            x.PihakYgDitunjuk_Day6.HasValue && x.PihakYgDitunjuk_Day6.Value != 0 ? x.PihakYgDitunjuk_Day6.Value : 1).ToString("yyyy-MM-dd")
            });
            var distinctNameAndTtlBen7 = allPolisIPP.Where(x => !string.IsNullOrWhiteSpace(x.PihakYgDitunjuk_Nama7) && x.PihakYgDitunjuk_Year1 != null && x.PihakYgDitunjuk_Month1 != null && x.PihakYgDitunjuk_Day1 != null).Select(x => new
            {
                policyNo = x.No_Polis,
                nameAndTtl = x.PihakYgDitunjuk_Nama7.Replace(" ", "").Replace(".", "").Replace(",", "") +
                            new DateTime(
                            x.PihakYgDitunjuk_Year7.HasValue && x.PihakYgDitunjuk_Year7.Value != 0 ? x.PihakYgDitunjuk_Year7.Value : 1,
                            x.PihakYgDitunjuk_Month7.HasValue && x.PihakYgDitunjuk_Month7.Value != 0 ? x.PihakYgDitunjuk_Month7.Value : 1,
                            x.PihakYgDitunjuk_Day7.HasValue && x.PihakYgDitunjuk_Day7.Value != 0 ? x.PihakYgDitunjuk_Day7.Value : 1).ToString("yyyy-MM-dd")
            });

            var allClientNameAndTtl = new List<string>();
            allClientNameAndTtl.AddRange(distinctNameAndTtlPh.Select(x => x.nameAndTtl.ToLower()));
            allClientNameAndTtl.AddRange(distinctNameAndTtlInsured.Select(x => x.nameAndTtl.ToLower()));
            allClientNameAndTtl.AddRange(distinctNameAndTtlBen1.Select(x => x.nameAndTtl.ToLower()));
            allClientNameAndTtl.AddRange(distinctNameAndTtlBen2.Select(x => x.nameAndTtl.ToLower()));
            allClientNameAndTtl.AddRange(distinctNameAndTtlBen3.Select(x => x.nameAndTtl.ToLower()));
            allClientNameAndTtl.AddRange(distinctNameAndTtlBen4.Select(x => x.nameAndTtl.ToLower()));
            allClientNameAndTtl.AddRange(distinctNameAndTtlBen5.Select(x => x.nameAndTtl.ToLower()));
            allClientNameAndTtl.AddRange(distinctNameAndTtlBen6.Select(x => x.nameAndTtl.ToLower()));
            allClientNameAndTtl.AddRange(distinctNameAndTtlBen7.Select(x => x.nameAndTtl.ToLower()));
            allClientNameAndTtl = allClientNameAndTtl.Distinct().OrderBy(X => X).ToList();


            var listTupleAllIPPPh = new List<(PolisIPP polisIPP, DateTime tanggalLahir)>();
            foreach (var item in allPolisIPP)
            {
                listTupleAllIPPPh.Add((item, new DateTime(item.Policyholder_YearLahir.Value, item.Policyholder_MonthLahir.Value, item.Policyholder_DayLahir.Value)));
            }
            var listClientToInsert = new List<cd_client_>();
            var counter = 0;
            var lastClientId = dbMGULDBDev.cd_client_.OrderByDescending(x => x.client_no).FirstOrDefault();
            counter = lastClientId != null ? Convert.ToInt32(lastClientId.client_no) : 0;


            var existClients = dbMGULDBDev.cd_client_.ToList();
            foreach (var item in allClientNameAndTtl)
            {
                var selectedClient = listTupleAllIPPPh.Where(x =>
                (x.polisIPP.Policyholder_Name.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + x.tanggalLahir.ToString("yyyy-MM-dd")) == item);
                if (selectedClient.Count() > 1)
                {
                    var lebihdariDua = true;
                }


                if (selectedClient.Count() > 0)
                {

                    var polisIPP = selectedClient.FirstOrDefault().polisIPP;
                    var tangglLahir = selectedClient.FirstOrDefault().tanggalLahir;
                    counter++;

                    var existClient = existClients.Where(x => x.client_id_temp == item).FirstOrDefault();

                    if (existClient == null)
                    {
                        var client = new cd_client_();
                        client.policy_no = selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        client.client_name = selectedClient.FirstOrDefault().polisIPP.Policyholder_Name;
                        client.policy_product = "IPP";
                        client.ppOrttOrbf = "PP";
                        client.client_id_temp = item;
                        client.client_no = counter.ToString().PadLeft(10, '0');

                        client.GetSalutation(polisIPP.Policyholder_Status, polisIPP.Policyholder_Sex);
                        client.GetMaritalStatusCode(polisIPP.Policyholder_Status, polisIPP.Policyholder_Sex);
                        client.birth_place = "";
                        client.birth_date = tangglLahir;
                        client.smoker_status = "N";
                        client.residence_address1 = polisIPP.Policyholder_Korespondensi;
                        client.account_holder = "";
                        client.account_holder_relation_code = "";
                        client.mobile_phone = polisIPP.Policyholder_HP;
                        client.email = (polisIPP.Policyholder_Email ?? "").ToLower();
                        client.mother_name = "";
                        client.client_name = polisIPP.Policyholder_Name;
                        client.gender_code = polisIPP.Policyholder_Sex.ToUpper();
                        client.GetMaritalStatusCode(polisIPP.Policyholder_Status, polisIPP.Policyholder_Sex);
                        client.GetOccupation(polisIPP.Policyholder_Pekerjaan);
                        client.GetOccupationCategory(polisIPP.Policyholder_Pekerjaan);
                        client.position_code = null;
                        client.client_type_code = null;
                        client.religion_code = null;
                        client.cigarete_perday = null;
                        client.residence_address2 = polisIPP.Policyholder_Kel;
                        client.residence_phone_no = polisIPP.Policyholder_Telp;
                        client.residence_fax = null;
                        client.GetResidenceCity(polisIPP.Policyholder_City);
                        client.residence_postal_code = polisIPP.Policyholder_KodePos;
                        client.GetResidenceProvinceCode(polisIPP.Policyholder_City ?? "");
                        client.office_address1 = polisIPP.Policyholder_AlamatPerusahaan;
                        client.office_address2 = null;
                        client.office_phone_no = null;
                        client.office_fax = null;
                        client.office_city = null;
                        client.office_province_code = null;
                        client.office_postal_code = null;
                        client.mail_address1 = polisIPP.Policyholder_Korespondensi;
                        client.mail_address2 = polisIPP.Policyholder_Kel;
                        client.GetMailCity(polisIPP.Policyholder_City);
                        client.mail_province_code = client.residence_province_code;
                        client.mail_postal_code = polisIPP.Policyholder_KodePos;
                        client.bi_code = dataMigrationMapper.GetBankCode(polisIPP.Policyholder_NamaBank);
                        client.bank_account_no = polisIPP.Policyholder_NoRekPemilik.Replace("-", "").Replace(" ", "");
                        client.bank_branch = polisIPP.Policyholder_CabangBank;
                        client.bank_city = null;
                        client.id_type_code = dataMigrationMapper.GetIdTypeCode(polisIPP.Policyholder_IDtype);
                        client.no_id = polisIPP.Policyholder_NoID.Replace(" ", "").Replace("-", "").Trim();
                        client.id_issue_date = null;
                        client.id_expire_date = null;
                        client.height = null;
                        client.weight = null;
                        client.monthly_cost = null;
                        client.client_status = null;
                        client.npwp = polisIPP.Policyholder_NPWP.Replace(" ", "").Replace("-", "").Trim();
                        client.npwp_name = null;
                        client.npwp_address = null;
                        client.front_title = null;
                        client.back_title = null;
                        client.mother_birth_date = null;
                        listClientToInsert.Add(client);
                    }
                    else
                    {
                        existClient.policy_no = existClient.policy_no + "-" + selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        existClient.ppOrttOrbf = existClient.ppOrttOrbf + "-PP";
                        existClient.policy_product = existClient.policy_product + "-IPP";
                        dbMGULDBDev.Entry(existClient).State = System.Data.Entity.EntityState.Modified;

                        var oldClientIdToNewClientId = new OldClientIdToNewClientId();
                        oldClientIdToNewClientId.OldId = existClient.client_id_temp;
                        oldClientIdToNewClientId.NewId = existClient.client_no;
                        oldClientIdToNewClientId.DatabaseName = "IPP";
                        oldClientIdToNewClientId.TableName = "PolisIPP";
                        dbMGULDBDev.OldClientIdToNewClientId.Add(oldClientIdToNewClientId);
                    }

                }
            }
            dbMGULDBDev.cd_client_.AddRange(listClientToInsert);


            dbMGULDBDev.SaveChanges();
            var listOldToNewClientId = new List<OldClientIdToNewClientId>();
            foreach (var item in listClientToInsert)
            {
                var oldClientIdToNewClientId = new OldClientIdToNewClientId();
                oldClientIdToNewClientId.OldId = item.client_id_temp;
                oldClientIdToNewClientId.NewId = item.client_no;
                oldClientIdToNewClientId.DatabaseName = "IPP";
                oldClientIdToNewClientId.TableName = "PolisIPP";
                listOldToNewClientId.Add(oldClientIdToNewClientId);
            }
            dbMGULDBDev.OldClientIdToNewClientId.AddRange(listOldToNewClientId);
            dbMGULDBDev.SaveChanges();


            //Insert Insured

            listClientToInsert.Clear();
            var listTupleAllIPPInsured = new List<(PolisIPP polisIPP, DateTime tanggalLahir)>();
            foreach (var item in allPolisIPP)
            {
                listTupleAllIPPInsured.Add((item, new DateTime(item.Insured_YearLahir.Value, item.Insured_MonthLahir.Value, item.Insured_DayLahir.Value)));
            }
            existClients = dbMGULDBDev.cd_client_.ToList();
            foreach (var item in allClientNameAndTtl)
            {
                var selectedClient = listTupleAllIPPInsured.Where(x =>
                (x.polisIPP.Insured_Name.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + x.tanggalLahir.ToString("yyyy-MM-dd")) == item);
                if (selectedClient.Count() > 1)
                {
                    var lebihdariDua = true;
                }


                if (selectedClient.Count() > 0)
                {

                    var polisIPP = selectedClient.FirstOrDefault().polisIPP;
                    var tangglLahir = selectedClient.FirstOrDefault().tanggalLahir;
                    counter++;

                    var existClient = existClients.Where(x => x.client_id_temp == item).FirstOrDefault();

                    if (existClient == null)
                    {
                        var client = new cd_client_();
                        client.policy_no = selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        client.client_name = selectedClient.FirstOrDefault().polisIPP.Insured_Name;
                        client.policy_product = "IPP";
                        client.ppOrttOrbf = "TT";
                        client.client_id_temp = item;
                        client.client_no = counter.ToString().PadLeft(10, '0');

                        client.GetSalutation(polisIPP.Insured_Status, polisIPP.Insured_Sex);
                        client.GetMaritalStatusCode(polisIPP.Insured_Status, polisIPP.Insured_Sex);
                        client.birth_place = "";
                        client.birth_date = tangglLahir;
                        client.smoker_status = "N";
                        client.residence_address1 = polisIPP.Insured_Korespondensi;
                        client.account_holder = "";
                        client.account_holder_relation_code = "";
                        client.mobile_phone = polisIPP.Insured_HP;
                        client.email = (polisIPP.Insured_Email ?? "").ToLower();
                        client.mother_name = "";
                        client.client_name = polisIPP.Insured_Name;
                        client.gender_code = polisIPP.Insured_Sex.ToUpper();
                        client.GetMaritalStatusCode(polisIPP.Insured_Status, polisIPP.Insured_Sex);
                        client.GetOccupation(polisIPP.Insured_Pekerjaan);
                        client.GetOccupationCategory(polisIPP.Insured_Pekerjaan);
                        client.position_code = null;
                        client.client_type_code = null;
                        client.religion_code = null;
                        client.cigarete_perday = null;
                        client.residence_address2 = polisIPP.Insured_Kel;
                        client.residence_phone_no = polisIPP.Insured_Telp;
                        client.residence_fax = null;
                        client.GetResidenceCity(polisIPP.Insured_City);
                        client.residence_postal_code = polisIPP.Insured_KodePos;
                        client.GetResidenceProvinceCode(polisIPP.Insured_City ?? "");
                        client.office_address1 = polisIPP.Insured_AlamatPerusahaan;
                        client.office_address2 = null;
                        client.office_phone_no = null;
                        client.office_fax = null;
                        client.office_city = null;
                        client.office_province_code = null;
                        client.office_postal_code = null;
                        client.mail_address1 = polisIPP.Insured_Korespondensi;
                        client.mail_address2 = polisIPP.Insured_Kel;
                        client.GetMailCity(polisIPP.Insured_City);
                        client.mail_province_code = client.residence_province_code;
                        client.mail_postal_code = polisIPP.Insured_KodePos;
                        //client.bi_code = dataMigrationMapper.GetBankCode(null);
                        client.bank_account_no = null;
                        client.bank_branch = null;
                        client.bank_city = null;
                        client.id_type_code = dataMigrationMapper.GetIdTypeCode(polisIPP.Insured_IDType);
                        client.no_id = polisIPP.Insured_NoID.Replace(" ", "").Replace("-", "").Trim();
                        client.id_issue_date = null;
                        client.id_expire_date = null;
                        client.height = null;
                        client.weight = null;
                        client.monthly_cost = null;
                        client.client_status = null;
                        client.npwp = polisIPP.Insured_NPWP.Replace(" ", "").Replace("-", "").Trim();
                        client.npwp_name = null;
                        client.npwp_address = null;
                        client.front_title = null;
                        client.back_title = null;
                        client.mother_birth_date = null;
                        listClientToInsert.Add(client);
                    }
                    else
                    {
                        existClient.policy_no = existClient.policy_no + "-" + selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        existClient.ppOrttOrbf = existClient.ppOrttOrbf + "-TT";
                        existClient.policy_product = existClient.policy_product + "-IPP";
                        dbMGULDBDev.Entry(existClient).State = System.Data.Entity.EntityState.Modified;

                        OldClientIdToNewClientId oldClientIdToNewClientId;
                        oldClientIdToNewClientId = dbMGULDBDev.OldClientIdToNewClientId.Where(x => x.OldId == existClient.client_id_temp && x.TableName == "PolisIPP").FirstOrDefault();

                        if (oldClientIdToNewClientId == null)
                        {
                            oldClientIdToNewClientId = new OldClientIdToNewClientId();
                            oldClientIdToNewClientId.OldId = existClient.client_id_temp;
                            oldClientIdToNewClientId.NewId = existClient.client_no;
                            oldClientIdToNewClientId.DatabaseName = "IPP";
                            oldClientIdToNewClientId.TableName = "PolisIPP";
                            dbMGULDBDev.OldClientIdToNewClientId.Add(oldClientIdToNewClientId);
                        }

                    }

                }
            }
            dbMGULDBDev.cd_client_.AddRange(listClientToInsert);

            listOldToNewClientId.Clear();
            foreach (var item in listClientToInsert)
            {
                var oldClientIdToNewClientId = new OldClientIdToNewClientId();
                oldClientIdToNewClientId.OldId = item.client_id_temp;
                oldClientIdToNewClientId.NewId = item.client_no;
                oldClientIdToNewClientId.DatabaseName = "IPP";
                oldClientIdToNewClientId.TableName = "PolisIPP";
                listOldToNewClientId.Add(oldClientIdToNewClientId);
            }
            dbMGULDBDev.OldClientIdToNewClientId.AddRange(listOldToNewClientId);
            dbMGULDBDev.SaveChanges();


            //Insert Ben1

            listClientToInsert.Clear();
            var listTupleAllIPPBen1 = new List<(PolisIPP polisIPP, DateTime tanggalLahir)>();
            foreach (var item in allPolisIPP)
            {
                listTupleAllIPPBen1.Add((item, new DateTime(item.PihakYgDitunjuk_Year1.Value, item.PihakYgDitunjuk_Month1.Value, item.PihakYgDitunjuk_Day1.Value)));
            }
            existClients = dbMGULDBDev.cd_client_.ToList();
            foreach (var item in allClientNameAndTtl)
            {
                var selectedClient = listTupleAllIPPBen1.Where(x =>
                (x.polisIPP.PihakYgDitunjuk_Nama1.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + x.tanggalLahir.ToString("yyyy-MM-dd")) == item);
                if (selectedClient.Count() > 1)
                {
                    var lebihdariDua = true;
                }


                if (selectedClient.Count() > 0)
                {

                    var polisIPP = selectedClient.FirstOrDefault().polisIPP;
                    var tangglLahir = selectedClient.FirstOrDefault().tanggalLahir;
                    counter++;

                    var existClient = existClients.Where(x => x.client_id_temp == item).FirstOrDefault();

                    if (existClient == null)
                    {
                        var client = new cd_client_();
                        client.policy_no = selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        client.client_name = selectedClient.FirstOrDefault().polisIPP.PihakYgDitunjuk_Nama1;
                        client.policy_product = "IPP";
                        client.ppOrttOrbf = "TT";
                        client.client_id_temp = item;
                        client.client_no = counter.ToString().PadLeft(10, '0');
                        client.salutation = "";
                        //client.GetSalutation(null, polisIPP.PihakYgDitunjuk_Sex1);
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex1);
                        client.birth_place = "";
                        client.birth_date = tangglLahir;
                        client.smoker_status = "N";
                        client.residence_address1 = "";
                        client.account_holder = "";
                        client.account_holder_relation_code = "";
                        client.mobile_phone = "";
                        client.email = "";
                        client.mother_name = "";
                        client.client_name = polisIPP.PihakYgDitunjuk_Nama1;
                        client.gender_code = polisIPP.PihakYgDitunjuk_Sex1.ToUpper();
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex1);
                        //client.GetOccupation(null);
                        //client.GetOccupationCategory(null);
                        client.position_code = null;
                        client.client_type_code = null;
                        client.religion_code = null;
                        client.cigarete_perday = null;
                        client.residence_address2 = null;
                        client.residence_phone_no = null;
                        client.residence_fax = null;
                        //client.GetResidenceCity(null);
                        client.residence_postal_code = null;
                        //client.GetResidenceProvinceCode(null);
                        client.office_address1 = null;
                        client.office_address2 = null;
                        client.office_phone_no = null;
                        client.office_fax = null;
                        client.office_city = null;
                        client.office_province_code = null;
                        client.office_postal_code = null;
                        client.mail_address1 = null;
                        client.mail_address2 = null;
                        //client.GetMailCity(null);
                        client.mail_province_code = null;
                        client.mail_postal_code = null;
                        client.bi_code = null;
                        client.bank_account_no = null;
                        client.bank_branch = null;
                        client.bank_city = null;
                        client.id_type_code = null;
                        client.no_id = null;
                        client.id_issue_date = null;
                        client.id_expire_date = null;
                        client.height = null;
                        client.weight = null;
                        client.monthly_cost = null;
                        client.client_status = null;
                        client.npwp = null;
                        client.npwp_name = null;
                        client.npwp_address = null;
                        client.front_title = null;
                        client.back_title = null;
                        client.mother_birth_date = null;
                        listClientToInsert.Add(client);
                    }
                    else
                    {
                        existClient.policy_no = existClient.policy_no + "-" + selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        existClient.ppOrttOrbf = existClient.ppOrttOrbf + "-BE1";
                        existClient.policy_product = existClient.policy_product + "-IPP";
                        dbMGULDBDev.Entry(existClient).State = System.Data.Entity.EntityState.Modified;

                        OldClientIdToNewClientId oldClientIdToNewClientId;
                        oldClientIdToNewClientId = dbMGULDBDev.OldClientIdToNewClientId.Where(x => x.OldId == existClient.client_id_temp && x.TableName == "PolisIPP").FirstOrDefault();

                        if (oldClientIdToNewClientId == null)
                        {
                            oldClientIdToNewClientId = new OldClientIdToNewClientId();
                            oldClientIdToNewClientId.OldId = existClient.client_id_temp;
                            oldClientIdToNewClientId.NewId = existClient.client_no;
                            oldClientIdToNewClientId.DatabaseName = "IPP";
                            oldClientIdToNewClientId.TableName = "PolisIPP";
                            dbMGULDBDev.OldClientIdToNewClientId.Add(oldClientIdToNewClientId);
                        }
                    }

                }
            }
            dbMGULDBDev.cd_client_.AddRange(listClientToInsert);

            dbMGULDBDev.cd_client_.AddRange(listClientToInsert);

            listOldToNewClientId.Clear();
            foreach (var item in listClientToInsert)
            {
                var oldClientIdToNewClientId = new OldClientIdToNewClientId();
                oldClientIdToNewClientId.OldId = item.client_id_temp;
                oldClientIdToNewClientId.NewId = item.client_no;
                oldClientIdToNewClientId.DatabaseName = "IPP";
                oldClientIdToNewClientId.TableName = "PolisIPP";
                listOldToNewClientId.Add(oldClientIdToNewClientId);
            }
            dbMGULDBDev.OldClientIdToNewClientId.AddRange(listOldToNewClientId);

            dbMGULDBDev.SaveChanges();

            ////Insert Ben2

            listClientToInsert.Clear();
            var listTupleAllIPPBen2 = new List<(PolisIPP polisIPP, DateTime tanggalLahir)>();
            foreach (var item in allPolisIPP.Where(x => x.PihakYgDitunjuk_Nama2 != string.Empty && x.PihakYgDitunjuk_Benefit2 > 0))
            {
                var dob = new DateTime(item.PihakYgDitunjuk_Year2.Value, item.PihakYgDitunjuk_Month2.Value, item.PihakYgDitunjuk_Day2.Value);

                listTupleAllIPPBen2.Add((item, dob));
            }
            existClients = dbMGULDBDev.cd_client_.ToList();
            foreach (var item in allClientNameAndTtl)
            {
                var selectedClient = listTupleAllIPPBen2.Where(x =>
                (x.polisIPP.PihakYgDitunjuk_Nama2.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + x.tanggalLahir.ToString("yyyy-MM-dd")) == item);
                if (selectedClient.Count() > 2)
                {
                    var lebihdariDua = true;
                }


                if (selectedClient.Count() > 0)
                {

                    var polisIPP = selectedClient.FirstOrDefault().polisIPP;
                    var tangglLahir = selectedClient.FirstOrDefault().tanggalLahir;
                    counter++;

                    var existClient = existClients.Where(x => x.client_id_temp == item).FirstOrDefault();

                    if (existClient == null)
                    {
                        var client = new cd_client_();
                        client.policy_no = selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        client.client_name = selectedClient.FirstOrDefault().polisIPP.PihakYgDitunjuk_Nama2;
                        client.policy_product = "IPP";
                        client.ppOrttOrbf = "TT";
                        client.client_id_temp = item;
                        client.client_no = counter.ToString().PadLeft(10, '0');
                        client.salutation = "";
                        //client.GetSalutation(null, polisIPP.PihakYgDitunjuk_Sex2);
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex2);
                        client.birth_place = "";
                        client.birth_date = tangglLahir;
                        client.smoker_status = "N";
                        client.residence_address1 = "";
                        client.account_holder = "";
                        client.account_holder_relation_code = "";
                        client.mobile_phone = "";
                        client.email = "";
                        client.mother_name = "";
                        client.client_name = polisIPP.PihakYgDitunjuk_Nama2;
                        client.gender_code = polisIPP.PihakYgDitunjuk_Sex2.ToUpper();
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex2);
                        //client.GetOccupation(null);
                        //client.GetOccupationCategory(null);
                        client.position_code = null;
                        client.client_type_code = null;
                        client.religion_code = null;
                        client.cigarete_perday = null;
                        client.residence_address2 = null;
                        client.residence_phone_no = null;
                        client.residence_fax = null;
                        //client.GetResidenceCity(null);
                        client.residence_postal_code = null;
                        //client.GetResidenceProvinceCode(null);
                        client.office_address2 = null;
                        client.office_address2 = null;
                        client.office_phone_no = null;
                        client.office_fax = null;
                        client.office_city = null;
                        client.office_province_code = null;
                        client.office_postal_code = null;
                        client.mail_address2 = null;
                        client.mail_address2 = null;
                        //client.GetMailCity(null);
                        client.mail_province_code = null;
                        client.mail_postal_code = null;
                        client.bi_code = null;
                        client.bank_account_no = null;
                        client.bank_branch = null;
                        client.bank_city = null;
                        client.id_type_code = null;
                        client.no_id = null;
                        client.id_issue_date = null;
                        client.id_expire_date = null;
                        client.height = null;
                        client.weight = null;
                        client.monthly_cost = null;
                        client.client_status = null;
                        client.npwp = null;
                        client.npwp_name = null;
                        client.npwp_address = null;
                        client.front_title = null;
                        client.back_title = null;
                        client.mother_birth_date = null;
                        listClientToInsert.Add(client);
                    }
                    else
                    {
                        existClient.policy_no = existClient.policy_no + "-" + selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        existClient.ppOrttOrbf = existClient.ppOrttOrbf + "-BE2";
                        existClient.policy_product = existClient.policy_product + "-IPP";
                        dbMGULDBDev.Entry(existClient).State = System.Data.Entity.EntityState.Modified;

                        OldClientIdToNewClientId oldClientIdToNewClientId;
                        oldClientIdToNewClientId = dbMGULDBDev.OldClientIdToNewClientId.Where(x => x.OldId == existClient.client_id_temp && x.TableName == "PolisIPP").FirstOrDefault();

                        if (oldClientIdToNewClientId == null)
                        {
                            oldClientIdToNewClientId = new OldClientIdToNewClientId();
                            oldClientIdToNewClientId.OldId = existClient.client_id_temp;
                            oldClientIdToNewClientId.NewId = existClient.client_no;
                            oldClientIdToNewClientId.DatabaseName = "IPP";
                            oldClientIdToNewClientId.TableName = "PolisIPP";
                            dbMGULDBDev.OldClientIdToNewClientId.Add(oldClientIdToNewClientId);
                        }
                    }

                }
            }
            dbMGULDBDev.cd_client_.AddRange(listClientToInsert);

            listOldToNewClientId.Clear();
            foreach (var item in listClientToInsert)
            {
                var oldClientIdToNewClientId = new OldClientIdToNewClientId();
                oldClientIdToNewClientId.OldId = item.client_id_temp;
                oldClientIdToNewClientId.NewId = item.client_no;
                oldClientIdToNewClientId.DatabaseName = "IPP";
                oldClientIdToNewClientId.TableName = "PolisIPP";
                listOldToNewClientId.Add(oldClientIdToNewClientId);
            }
            dbMGULDBDev.OldClientIdToNewClientId.AddRange(listOldToNewClientId);
            dbMGULDBDev.SaveChanges();

            ////Insert Ben3

            listClientToInsert.Clear();
            var listTupleAllIPPBen3 = new List<(PolisIPP polisIPP, DateTime tanggalLahir)>();
            foreach (var item in allPolisIPP.Where(x => x.PihakYgDitunjuk_Nama3 != string.Empty && x.PihakYgDitunjuk_Benefit3 > 0))
            {
                var dob = new DateTime(item.PihakYgDitunjuk_Year3.Value, item.PihakYgDitunjuk_Month3.Value, item.PihakYgDitunjuk_Day3.Value);

                listTupleAllIPPBen3.Add((item, dob));
            }
            existClients = dbMGULDBDev.cd_client_.ToList();
            foreach (var item in allClientNameAndTtl)
            {
                var getAgung = listTupleAllIPPBen3.Where(x => x.polisIPP.PihakYgDitunjuk_Nama3.Replace(" ", "").Replace(".", "").Replace(",", "").ToLower() == "alvinagungperdana");
                if (getAgung != null)
                {

                }
                if (item.Contains("alvinagungperdana"))
                {

                }
                var polis = listTupleAllIPPBen3.Where(x => x.polisIPP.No_SPAJ == "30000256").ToList();
                var alvin = listTupleAllIPPBen3.Where(x => x.polisIPP.PihakYgDitunjuk_Nama3.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") == "alvinagungperdana").ToList().FirstOrDefault();

                var selectedClient = listTupleAllIPPBen3.Where(x =>
                (x.polisIPP.PihakYgDitunjuk_Nama3.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + x.tanggalLahir.ToString("yyyy-MM-dd")) == item).ToList();
                if (selectedClient.Count() > 3)
                {
                    var lebihdariDua = true;
                }

                if (selectedClient.Count() > 0)
                {

                    var polisIPP = selectedClient.FirstOrDefault().polisIPP;
                    var tangglLahir = selectedClient.FirstOrDefault().tanggalLahir;
                    counter++;

                    var existClient = existClients.Where(x => x.client_id_temp == item).FirstOrDefault();

                    if (existClient == null)
                    {
                        var client = new cd_client_();
                        client.policy_no = selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        client.client_name = selectedClient.FirstOrDefault().polisIPP.PihakYgDitunjuk_Nama3;
                        client.policy_product = "IPP";
                        client.ppOrttOrbf = "TT";
                        client.client_id_temp = item;
                        client.client_no = counter.ToString().PadLeft(10, '0');
                        client.salutation = "";
                        //client.GetSalutation(null, polisIPP.PihakYgDitunjuk_Sex3);
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex3);
                        client.birth_place = "";
                        client.birth_date = tangglLahir;
                        client.smoker_status = "N";
                        client.residence_address1 = "";
                        client.account_holder = "";
                        client.account_holder_relation_code = "";
                        client.mobile_phone = "";
                        client.email = "";
                        client.mother_name = "";
                        client.client_name = polisIPP.PihakYgDitunjuk_Nama3;
                        client.gender_code = polisIPP.PihakYgDitunjuk_Sex3.ToUpper();
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex3);
                        //client.GetOccupation(null);
                        //client.GetOccupationCategory(null);
                        client.position_code = null;
                        client.client_type_code = null;
                        client.religion_code = null;
                        client.cigarete_perday = null;
                        client.residence_address2 = null;
                        client.residence_phone_no = null;
                        client.residence_fax = null;
                        //client.GetResidenceCity(null);
                        client.residence_postal_code = null;
                        //client.GetResidenceProvinceCode(null);
                        client.office_address1 = null;
                        client.office_address2 = null;
                        client.office_phone_no = null;
                        client.office_fax = null;
                        client.office_city = null;
                        client.office_province_code = null;
                        client.office_postal_code = null;
                        client.mail_address1 = null;
                        client.mail_address2 = null;
                        //client.GetMailCity(null);
                        client.mail_province_code = null;
                        client.mail_postal_code = null;
                        client.bi_code = null;
                        client.bank_account_no = null;
                        client.bank_branch = null;
                        client.bank_city = null;
                        client.id_type_code = null;
                        client.no_id = null;
                        client.id_issue_date = null;
                        client.id_expire_date = null;
                        client.height = null;
                        client.weight = null;
                        client.monthly_cost = null;
                        client.client_status = null;
                        client.npwp = null;
                        client.npwp_name = null;
                        client.npwp_address = null;
                        client.front_title = null;
                        client.back_title = null;
                        client.mother_birth_date = null;
                        listClientToInsert.Add(client);
                    }
                    else
                    {
                        existClient.policy_no = existClient.policy_no + "-" + selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        existClient.ppOrttOrbf = existClient.ppOrttOrbf + "-BE3";
                        existClient.policy_product = existClient.policy_product + "-IPP";
                        dbMGULDBDev.Entry(existClient).State = System.Data.Entity.EntityState.Modified;

                        OldClientIdToNewClientId oldClientIdToNewClientId;
                        oldClientIdToNewClientId = dbMGULDBDev.OldClientIdToNewClientId.Where(x => x.OldId == existClient.client_id_temp && x.TableName == "PolisIPP").FirstOrDefault();

                        if (oldClientIdToNewClientId == null)
                        {
                            oldClientIdToNewClientId = new OldClientIdToNewClientId();
                            oldClientIdToNewClientId.OldId = existClient.client_id_temp;
                            oldClientIdToNewClientId.NewId = existClient.client_no;
                            oldClientIdToNewClientId.DatabaseName = "IPP";
                            oldClientIdToNewClientId.TableName = "PolisIPP";
                            dbMGULDBDev.OldClientIdToNewClientId.Add(oldClientIdToNewClientId);
                        }
                    }

                }
            }
            dbMGULDBDev.cd_client_.AddRange(listClientToInsert);

            listOldToNewClientId.Clear();
            foreach (var item in listClientToInsert)
            {
                var oldClientIdToNewClientId = new OldClientIdToNewClientId();
                oldClientIdToNewClientId.OldId = item.client_id_temp;
                oldClientIdToNewClientId.NewId = item.client_no;
                oldClientIdToNewClientId.DatabaseName = "IPP";
                oldClientIdToNewClientId.TableName = "PolisIPP";
                listOldToNewClientId.Add(oldClientIdToNewClientId);
            }
            dbMGULDBDev.OldClientIdToNewClientId.AddRange(listOldToNewClientId);
            dbMGULDBDev.SaveChanges();

            ////Insert Ben4

            listClientToInsert.Clear();
            var listTupleAllIPPBen4 = new List<(PolisIPP polisIPP, DateTime tanggalLahir)>();
            foreach (var item in allPolisIPP.Where(x => x.PihakYgDitunjuk_Nama4 != string.Empty && x.PihakYgDitunjuk_Benefit4 > 0))
            {

                var dob = new DateTime(item.PihakYgDitunjuk_Year4.Value, item.PihakYgDitunjuk_Month4.Value, item.PihakYgDitunjuk_Day4.Value);
                listTupleAllIPPBen4.Add((item, dob));
            }
            existClients = dbMGULDBDev.cd_client_.ToList();
            foreach (var item in allClientNameAndTtl)
            {
                var selectedClient = listTupleAllIPPBen4.Where(x =>
                (x.polisIPP.PihakYgDitunjuk_Nama4.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + x.tanggalLahir.ToString("yyyy-MM-dd")) == item);
                if (selectedClient.Count() > 4)
                {
                    var lebihdariDua = true;
                }


                if (selectedClient.Count() > 0)
                {

                    var polisIPP = selectedClient.FirstOrDefault().polisIPP;
                    var tangglLahir = selectedClient.FirstOrDefault().tanggalLahir;
                    counter++;

                    var existClient = existClients.Where(x => x.client_id_temp == item).FirstOrDefault();

                    if (existClient == null)
                    {
                        var client = new cd_client_();
                        client.policy_no = selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        client.client_name = selectedClient.FirstOrDefault().polisIPP.PihakYgDitunjuk_Nama4;
                        client.policy_product = "IPP";
                        client.ppOrttOrbf = "TT";
                        client.client_id_temp = item;
                        client.client_no = counter.ToString().PadLeft(10, '0');
                        client.salutation = "";
                        //client.GetSalutation(null, polisIPP.PihakYgDitunjuk_Sex4);
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex4);
                        client.birth_place = "";
                        client.birth_date = tangglLahir;
                        client.smoker_status = "N";
                        client.residence_address1 = "";
                        client.account_holder = "";
                        client.account_holder_relation_code = "";
                        client.mobile_phone = "";
                        client.email = "";
                        client.mother_name = "";
                        client.client_name = polisIPP.PihakYgDitunjuk_Nama4;
                        client.gender_code = polisIPP.PihakYgDitunjuk_Sex4.ToUpper();
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex4);
                        //client.GetOccupation(null);
                        //client.GetOccupationCategory(null);
                        client.position_code = null;
                        client.client_type_code = null;
                        client.religion_code = null;
                        client.cigarete_perday = null;
                        client.residence_address2 = null;
                        client.residence_phone_no = null;
                        client.residence_fax = null;
                        //client.GetResidenceCity(null);
                        client.residence_postal_code = null;
                        //client.GetResidenceProvinceCode(null);
                        client.office_address1 = null;
                        client.office_address2 = null;
                        client.office_phone_no = null;
                        client.office_fax = null;
                        client.office_city = null;
                        client.office_province_code = null;
                        client.office_postal_code = null;
                        client.mail_address1 = null;
                        client.mail_address2 = null;
                        //client.GetMailCity(null);
                        client.mail_province_code = null;
                        client.mail_postal_code = null;
                        client.bi_code = null;
                        client.bank_account_no = null;
                        client.bank_branch = null;
                        client.bank_city = null;
                        client.id_type_code = null;
                        client.no_id = null;
                        client.id_issue_date = null;
                        client.id_expire_date = null;
                        client.height = null;
                        client.weight = null;
                        client.monthly_cost = null;
                        client.client_status = null;
                        client.npwp = null;
                        client.npwp_name = null;
                        client.npwp_address = null;
                        client.front_title = null;
                        client.back_title = null;
                        client.mother_birth_date = null;
                        listClientToInsert.Add(client);
                    }
                    else
                    {
                        existClient.policy_no = existClient.policy_no + "-" + selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        existClient.ppOrttOrbf = existClient.ppOrttOrbf + "-BE4";
                        existClient.policy_product = existClient.policy_product + "-IPP";
                        dbMGULDBDev.Entry(existClient).State = System.Data.Entity.EntityState.Modified;

                        OldClientIdToNewClientId oldClientIdToNewClientId;
                        oldClientIdToNewClientId = dbMGULDBDev.OldClientIdToNewClientId.Where(x => x.OldId == existClient.client_id_temp && x.TableName == "PolisIPP").FirstOrDefault();

                        if (oldClientIdToNewClientId == null)
                        {
                            oldClientIdToNewClientId = new OldClientIdToNewClientId();
                            oldClientIdToNewClientId.OldId = existClient.client_id_temp;
                            oldClientIdToNewClientId.NewId = existClient.client_no;
                            oldClientIdToNewClientId.DatabaseName = "IPP";
                            oldClientIdToNewClientId.TableName = "PolisIPP";
                            dbMGULDBDev.OldClientIdToNewClientId.Add(oldClientIdToNewClientId);
                        }
                    }

                }
            }
            dbMGULDBDev.cd_client_.AddRange(listClientToInsert);

            listOldToNewClientId.Clear();
            foreach (var item in listClientToInsert)
            {
                var oldClientIdToNewClientId = new OldClientIdToNewClientId();
                oldClientIdToNewClientId.OldId = item.client_id_temp;
                oldClientIdToNewClientId.NewId = item.client_no;
                oldClientIdToNewClientId.DatabaseName = "IPP";
                oldClientIdToNewClientId.TableName = "PolisIPP";
                listOldToNewClientId.Add(oldClientIdToNewClientId);
            }
            dbMGULDBDev.OldClientIdToNewClientId.AddRange(listOldToNewClientId);
            dbMGULDBDev.SaveChanges();

            ////Insert Ben5

            listClientToInsert.Clear();
            var listTupleAllIPPBen5 = new List<(PolisIPP polisIPP, DateTime tanggalLahir)>();
            foreach (var item in allPolisIPP.Where(x => x.PihakYgDitunjuk_Nama5 != string.Empty && x.PihakYgDitunjuk_Benefit5 > 0))
            {

                var dob = new DateTime(item.PihakYgDitunjuk_Year5.Value, item.PihakYgDitunjuk_Month5.Value, item.PihakYgDitunjuk_Day5.Value);
                listTupleAllIPPBen5.Add((item, dob));
            }
            existClients = dbMGULDBDev.cd_client_.ToList();
            foreach (var item in allClientNameAndTtl)
            {
                var selectedClient = listTupleAllIPPBen5.Where(x =>
                (x.polisIPP.PihakYgDitunjuk_Nama5.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + x.tanggalLahir.ToString("yyyy-MM-dd")) == item);
                if (selectedClient.Count() > 5)
                {
                    var lebihdariDua = true;
                }


                if (selectedClient.Count() > 0)
                {

                    var polisIPP = selectedClient.FirstOrDefault().polisIPP;
                    var tangglLahir = selectedClient.FirstOrDefault().tanggalLahir;
                    counter++;

                    var existClient = existClients.Where(x => x.client_id_temp == item).FirstOrDefault();

                    if (existClient == null)
                    {
                        var client = new cd_client_();
                        client.policy_no = selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        client.client_name = selectedClient.FirstOrDefault().polisIPP.PihakYgDitunjuk_Nama5;
                        client.policy_product = "IPP";
                        client.ppOrttOrbf = "TT";
                        client.client_id_temp = item;
                        client.client_no = counter.ToString().PadLeft(10, '0');
                        client.salutation = "";
                        //client.GetSalutation(null, polisIPP.PihakYgDitunjuk_Sex5);
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex5);
                        client.birth_place = "";
                        client.birth_date = tangglLahir;
                        client.smoker_status = "N";
                        client.residence_address1 = "";
                        client.account_holder = "";
                        client.account_holder_relation_code = "";
                        client.mobile_phone = "";
                        client.email = "";
                        client.mother_name = "";
                        client.client_name = polisIPP.PihakYgDitunjuk_Nama5;
                        client.gender_code = polisIPP.PihakYgDitunjuk_Sex5.ToUpper();
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex5);
                        //client.GetOccupation(null);
                        //client.GetOccupationCategory(null);
                        client.position_code = null;
                        client.client_type_code = null;
                        client.religion_code = null;
                        client.cigarete_perday = null;
                        client.residence_address2 = null;
                        client.residence_phone_no = null;
                        client.residence_fax = null;
                        //client.GetResidenceCity(null);
                        client.residence_postal_code = null;
                        //client.GetResidenceProvinceCode(null);
                        client.office_address1 = null;
                        client.office_address2 = null;
                        client.office_phone_no = null;
                        client.office_fax = null;
                        client.office_city = null;
                        client.office_province_code = null;
                        client.office_postal_code = null;
                        client.mail_address1 = null;
                        client.mail_address2 = null;
                        //client.GetMailCity(null);
                        client.mail_province_code = null;
                        client.mail_postal_code = null;
                        client.bi_code = null;
                        client.bank_account_no = null;
                        client.bank_branch = null;
                        client.bank_city = null;
                        client.id_type_code = null;
                        client.no_id = null;
                        client.id_issue_date = null;
                        client.id_expire_date = null;
                        client.height = null;
                        client.weight = null;
                        client.monthly_cost = null;
                        client.client_status = null;
                        client.npwp = null;
                        client.npwp_name = null;
                        client.npwp_address = null;
                        client.front_title = null;
                        client.back_title = null;
                        client.mother_birth_date = null;
                        listClientToInsert.Add(client);
                    }
                    else
                    {
                        existClient.policy_no = existClient.policy_no + "-" + selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        existClient.ppOrttOrbf = existClient.ppOrttOrbf + "-BE5";
                        existClient.policy_product = existClient.policy_product + "-IPP";
                        dbMGULDBDev.Entry(existClient).State = System.Data.Entity.EntityState.Modified;


                        OldClientIdToNewClientId oldClientIdToNewClientId;
                        oldClientIdToNewClientId = dbMGULDBDev.OldClientIdToNewClientId.Where(x => x.OldId == existClient.client_id_temp && x.TableName == "PolisIPP").FirstOrDefault();

                        if (oldClientIdToNewClientId == null)
                        {
                            oldClientIdToNewClientId = new OldClientIdToNewClientId();
                            oldClientIdToNewClientId.OldId = existClient.client_id_temp;
                            oldClientIdToNewClientId.NewId = existClient.client_no;
                            oldClientIdToNewClientId.DatabaseName = "IPP";
                            oldClientIdToNewClientId.TableName = "PolisIPP";
                            dbMGULDBDev.OldClientIdToNewClientId.Add(oldClientIdToNewClientId);
                        }
                    }

                }
            }
            dbMGULDBDev.cd_client_.AddRange(listClientToInsert);
            listOldToNewClientId.Clear();
            foreach (var item in listClientToInsert)
            {
                var oldClientIdToNewClientId = new OldClientIdToNewClientId();
                oldClientIdToNewClientId.OldId = item.client_id_temp;
                oldClientIdToNewClientId.NewId = item.client_no;
                oldClientIdToNewClientId.DatabaseName = "IPP";
                oldClientIdToNewClientId.TableName = "PolisIPP";
                listOldToNewClientId.Add(oldClientIdToNewClientId);
            }
            dbMGULDBDev.OldClientIdToNewClientId.AddRange(listOldToNewClientId);
            dbMGULDBDev.SaveChanges();


            ////Insert Ben6

            listClientToInsert.Clear();
            var listTupleAllIPPBen6 = new List<(PolisIPP polisIPP, DateTime tanggalLahir)>();
            foreach (var item in allPolisIPP.Where(x => x.PihakYgDitunjuk_Nama6 != string.Empty && x.PihakYgDitunjuk_Benefit6 > 0))
            {

                var dob = new DateTime(item.PihakYgDitunjuk_Year6.Value, item.PihakYgDitunjuk_Month6.Value, item.PihakYgDitunjuk_Day6.Value);
                listTupleAllIPPBen6.Add((item, dob));
            }
            existClients = dbMGULDBDev.cd_client_.ToList();
            foreach (var item in allClientNameAndTtl)
            {
                var selectedClient = listTupleAllIPPBen6.Where(x =>
                (x.polisIPP.PihakYgDitunjuk_Nama6.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + x.tanggalLahir.ToString("yyyy-MM-dd")) == item);
                if (selectedClient.Count() > 6)
                {
                    var lebihdariDua = true;
                }


                if (selectedClient.Count() > 0)
                {

                    var polisIPP = selectedClient.FirstOrDefault().polisIPP;
                    var tangglLahir = selectedClient.FirstOrDefault().tanggalLahir;
                    counter++;

                    var existClient = existClients.Where(x => x.client_id_temp == item).FirstOrDefault();

                    if (existClient == null)
                    {
                        var client = new cd_client_();
                        client.policy_no = selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        client.client_name = selectedClient.FirstOrDefault().polisIPP.PihakYgDitunjuk_Nama6;
                        client.policy_product = "IPP";
                        client.ppOrttOrbf = "TT";
                        client.client_id_temp = item;
                        client.client_no = counter.ToString().PadLeft(10, '0');
                        client.salutation = "";
                        //client.GetSalutation(null, polisIPP.PihakYgDitunjuk_Sex6);
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex6);
                        client.birth_place = "";
                        client.birth_date = tangglLahir;
                        client.smoker_status = "N";
                        client.residence_address1 = "";
                        client.account_holder = "";
                        client.account_holder_relation_code = "";
                        client.mobile_phone = "";
                        client.email = "";
                        client.mother_name = "";
                        client.client_name = polisIPP.PihakYgDitunjuk_Nama6;
                        client.gender_code = polisIPP.PihakYgDitunjuk_Sex6.ToUpper();
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex6);
                        //client.GetOccupation(null);
                        //client.GetOccupationCategory(null);
                        client.position_code = null;
                        client.client_type_code = null;
                        client.religion_code = null;
                        client.cigarete_perday = null;
                        client.residence_address2 = null;
                        client.residence_phone_no = null;
                        client.residence_fax = null;
                        //client.GetResidenceCity(null);
                        client.residence_postal_code = null;
                        //client.GetResidenceProvinceCode(null);
                        client.office_address1 = null;
                        client.office_address2 = null;
                        client.office_phone_no = null;
                        client.office_fax = null;
                        client.office_city = null;
                        client.office_province_code = null;
                        client.office_postal_code = null;
                        client.mail_address1 = null;
                        client.mail_address2 = null;
                        //client.GetMailCity(null);
                        client.mail_province_code = null;
                        client.mail_postal_code = null;
                        client.bi_code = null;
                        client.bank_account_no = null;
                        client.bank_branch = null;
                        client.bank_city = null;
                        client.id_type_code = null;
                        client.no_id = null;
                        client.id_issue_date = null;
                        client.id_expire_date = null;
                        client.height = null;
                        client.weight = null;
                        client.monthly_cost = null;
                        client.client_status = null;
                        client.npwp = null;
                        client.npwp_name = null;
                        client.npwp_address = null;
                        client.front_title = null;
                        client.back_title = null;
                        client.mother_birth_date = null;
                        listClientToInsert.Add(client);
                    }
                    else
                    {
                        existClient.policy_no = existClient.policy_no + "-" + selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        existClient.ppOrttOrbf = existClient.ppOrttOrbf + "-BE6";
                        existClient.policy_product = existClient.policy_product + "-IPP";
                        dbMGULDBDev.Entry(existClient).State = System.Data.Entity.EntityState.Modified;


                        OldClientIdToNewClientId oldClientIdToNewClientId;
                        oldClientIdToNewClientId = dbMGULDBDev.OldClientIdToNewClientId.Where(x => x.OldId == existClient.client_id_temp && x.TableName == "PolisIPP").FirstOrDefault();

                        if (oldClientIdToNewClientId == null)
                        {
                            oldClientIdToNewClientId = new OldClientIdToNewClientId();
                            oldClientIdToNewClientId.OldId = existClient.client_id_temp;
                            oldClientIdToNewClientId.NewId = existClient.client_no;
                            oldClientIdToNewClientId.DatabaseName = "IPP";
                            oldClientIdToNewClientId.TableName = "PolisIPP";
                            dbMGULDBDev.OldClientIdToNewClientId.Add(oldClientIdToNewClientId);
                        }
                    }

                }
            }
            dbMGULDBDev.cd_client_.AddRange(listClientToInsert);
            listOldToNewClientId.Clear();
            foreach (var item in listClientToInsert)
            {
                var oldClientIdToNewClientId = new OldClientIdToNewClientId();
                oldClientIdToNewClientId.OldId = item.client_id_temp;
                oldClientIdToNewClientId.NewId = item.client_no;
                oldClientIdToNewClientId.DatabaseName = "IPP";
                oldClientIdToNewClientId.TableName = "PolisIPP";
                listOldToNewClientId.Add(oldClientIdToNewClientId);
            }
            dbMGULDBDev.OldClientIdToNewClientId.AddRange(listOldToNewClientId);
            dbMGULDBDev.SaveChanges();


            ////Insert Ben7

            listClientToInsert.Clear();
            var listTupleAllIPPBen7 = new List<(PolisIPP polisIPP, DateTime tanggalLahir)>();
            foreach (var item in allPolisIPP.Where(x => x.PihakYgDitunjuk_Nama7 != string.Empty && x.PihakYgDitunjuk_Benefit7 > 0))
            {

                var dob = new DateTime(item.PihakYgDitunjuk_Year7.Value, item.PihakYgDitunjuk_Month7.Value, item.PihakYgDitunjuk_Day7.Value);
                listTupleAllIPPBen7.Add((item, dob));
            }
            existClients = dbMGULDBDev.cd_client_.ToList();
            foreach (var item in allClientNameAndTtl)
            {
                var selectedClient = listTupleAllIPPBen7.Where(x =>
                (x.polisIPP.PihakYgDitunjuk_Nama7.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + x.tanggalLahir.ToString("yyyy-MM-dd")) == item);
                if (selectedClient.Count() > 7)
                {
                    var lebihdariDua = true;
                }


                if (selectedClient.Count() > 0)
                {

                    var polisIPP = selectedClient.FirstOrDefault().polisIPP;
                    var tangglLahir = selectedClient.FirstOrDefault().tanggalLahir;
                    counter++;

                    var existClient = existClients.Where(x => x.client_id_temp == item).FirstOrDefault();

                    if (existClient == null)
                    {
                        var client = new cd_client_();
                        client.policy_no = selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        client.client_name = selectedClient.FirstOrDefault().polisIPP.PihakYgDitunjuk_Nama7;
                        client.policy_product = "IPP";
                        client.ppOrttOrbf = "TT";
                        client.client_id_temp = item;
                        client.client_no = counter.ToString().PadLeft(10, '0');
                        client.salutation = "";
                        //client.GetSalutation(null, polisIPP.PihakYgDitunjuk_Sex7);
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex7);
                        client.birth_place = "";
                        client.birth_date = tangglLahir;
                        client.smoker_status = "N";
                        client.residence_address1 = "";
                        client.account_holder = "";
                        client.account_holder_relation_code = "";
                        client.mobile_phone = "";
                        client.email = "";
                        client.mother_name = "";
                        client.client_name = polisIPP.PihakYgDitunjuk_Nama7;
                        client.gender_code = polisIPP.PihakYgDitunjuk_Sex7.ToUpper();
                        //client.GetMaritalStatusCode(null, polisIPP.PihakYgDitunjuk_Sex7);
                        //client.GetOccupation(null);
                        //client.GetOccupationCategory(null);
                        client.position_code = null;
                        client.client_type_code = null;
                        client.religion_code = null;
                        client.cigarete_perday = null;
                        client.residence_address2 = null;
                        client.residence_phone_no = null;
                        client.residence_fax = null;
                        //client.GetResidenceCity(null);
                        client.residence_postal_code = null;
                        //client.GetResidenceProvinceCode(null);
                        client.office_address1 = null;
                        client.office_address2 = null;
                        client.office_phone_no = null;
                        client.office_fax = null;
                        client.office_city = null;
                        client.office_province_code = null;
                        client.office_postal_code = null;
                        client.mail_address1 = null;
                        client.mail_address2 = null;
                        //client.GetMailCity(null);
                        client.mail_province_code = null;
                        client.mail_postal_code = null;
                        client.bi_code = null;
                        client.bank_account_no = null;
                        client.bank_branch = null;
                        client.bank_city = null;
                        client.id_type_code = null;
                        client.no_id = null;
                        client.id_issue_date = null;
                        client.id_expire_date = null;
                        client.height = null;
                        client.weight = null;
                        client.monthly_cost = null;
                        client.client_status = null;
                        client.npwp = null;
                        client.npwp_name = null;
                        client.npwp_address = null;
                        client.front_title = null;
                        client.back_title = null;
                        client.mother_birth_date = null;
                        listClientToInsert.Add(client);
                    }
                    else
                    {
                        existClient.policy_no = existClient.policy_no + "-" + selectedClient.FirstOrDefault().polisIPP.No_Polis;
                        existClient.ppOrttOrbf = existClient.ppOrttOrbf + "-BE7";
                        existClient.policy_product = existClient.policy_product + "-IPP";
                        dbMGULDBDev.Entry(existClient).State = System.Data.Entity.EntityState.Modified;

                        OldClientIdToNewClientId oldClientIdToNewClientId;
                        oldClientIdToNewClientId = dbMGULDBDev.OldClientIdToNewClientId.Where(x => x.OldId == existClient.client_id_temp && x.TableName == "PolisIPP").FirstOrDefault();

                        if (oldClientIdToNewClientId == null)
                        {
                            oldClientIdToNewClientId = new OldClientIdToNewClientId();
                            oldClientIdToNewClientId.OldId = existClient.client_id_temp;
                            oldClientIdToNewClientId.NewId = existClient.client_no;
                            oldClientIdToNewClientId.DatabaseName = "IPP";
                            oldClientIdToNewClientId.TableName = "PolisIPP";
                            dbMGULDBDev.OldClientIdToNewClientId.Add(oldClientIdToNewClientId);
                        }
                    }

                }
            }
            dbMGULDBDev.cd_client_.AddRange(listClientToInsert);
            listOldToNewClientId.Clear();
            foreach (var item in listClientToInsert)
            {
                var oldClientIdToNewClientId = new OldClientIdToNewClientId();
                oldClientIdToNewClientId.OldId = item.client_id_temp;
                oldClientIdToNewClientId.NewId = item.client_no;
                oldClientIdToNewClientId.DatabaseName = "IPP";
                oldClientIdToNewClientId.TableName = "PolisIPP";
                listOldToNewClientId.Add(oldClientIdToNewClientId);
            }
            dbMGULDBDev.OldClientIdToNewClientId.AddRange(listOldToNewClientId);
            dbMGULDBDev.SaveChanges();

            Console.WriteLine("Polis IPP Data Migration Is Done");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine();

        }
        static void StartDataMigrationPolisJiwa()
        {
            Console.WriteLine("Client Jiwa Data Migration Is Starting..");
            Console.WriteLine(DateTime.Now);

            var dataMigrationMapper = new DataMigrationMapper();
            var dbMGULDBDev = new MIGULDBDevEntities();
            var dbMultiUserDB = new MultiUserDBEntities();
            var dbMGULDB = new DataModels.PLDataModels.MIGULDBEntitiesNew();
            var dbISLPortal = new ISL_PORTALEntities();

            var listClientToInsert = new List<cd_client_>();
            var listOldIdToNewId = new List<OldClientIdToNewClientId>();
            var counter = 0;
            var lastClientId = dbMGULDBDev.cd_client_.OrderByDescending(x => x.client_no).FirstOrDefault();
            counter = lastClientId != null ? Convert.ToInt32(lastClientId.client_no) : 0;
            var allClients = dbMGULDBDev.cd_client_.ToList();
            var islPortalClients = dbISLPortal.ISL_APP_CUSTOMERS.ToList();
            var totalOfClientsInISLPortal = 0;
            foreach (var item in islPortalClients)
            {
                var nameTTL = item.C_CUSTOMER_NAME.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + item.D_DOB.ToString("yyyy-MM-dd");
                var clientIfExist = allClients.Where(x => x.client_id_temp == nameTTL).FirstOrDefault();
                var client = new cd_client_();
                if (clientIfExist != null)
                {
                    var newOldIdToNewId = new OldClientIdToNewClientId();
                    newOldIdToNewId.NewId = clientIfExist.client_no;
                    newOldIdToNewId.OldId = item.N_CUSTOMER_ID.ToString();
                    newOldIdToNewId.DatabaseName = "ISLPortal";
                    newOldIdToNewId.TableName = "ISL_APP_CUSTOMERS";
                    listOldIdToNewId.Add(newOldIdToNewId);
                }
                else
                {
                    counter++;
                    client.client_id_temp = nameTTL + counter.ToString();
                    client.client_no = counter.ToString().PadLeft(10, '0');
                    client.policy_no = "";
                    client.policy_product = "Jiwa";
                    client.ppOrttOrbf = "";
                    client.salutation = "";
                    client.birth_place = item.C_PLACE_OF_BIRTH;
                    client.birth_date = item.D_DOB;
                    client.smoker_status = "N";
                    client.residence_address1 = item.C_ADDRESS_1;
                    client.residence_address2 = item.C_ADDRESS_2;
                    var accountHolder = dbISLPortal.ISL_APP_CUSTOMER_BANKS.Where(x => x.N_CUSTOMER_ID == item.N_CUSTOMER_ID).FirstOrDefault();
                    client.account_holder = accountHolder != null ? accountHolder.C_BANK_ACCOUNT_NAME : "";
                    client.account_holder_relation_code = "";
                    client.mobile_phone = item.C_HP ?? "";
                    client.email = (item.C_EMAIL_ADDRESS ?? "").ToLower();
                    client.mother_name = "";
                    client.client_name = item.C_CUSTOMER_NAME;
                    client.gender_code = item.C_SEX;
                    //client.GetMaritalStatusCode(polisInvesta.Marital_PP, polisInvesta.Jk_PP);
                    client.GetOccupation(item.C_OCCUPATION);
                    client.GetOccupationCategory(item.C_OCCUPATION);
                    client.position_code = null;
                    client.client_type_code = null;
                    client.religion_code = null;
                    client.cigarete_perday = null;
                    client.residence_phone_no = null;
                    client.residence_fax = null;
                    client.GetResidenceCity(item.C_CITY);
                    client.residence_postal_code = item.N_POSTAL_CODE;
                    client.GetResidenceProvinceCode(item.C_PROVINCE ?? "");
                    client.office_address1 = null;
                    client.office_address2 = null;
                    client.office_phone_no = null;
                    client.office_fax = null;
                    client.office_city = null;
                    client.office_province_code = null;
                    client.office_postal_code = null;
                    client.mail_address1 = item.C_ADDRESS_1;
                    client.mail_address2 = item.C_ADDRESS_2;
                    client.GetMailCity(item.C_CITY);
                    client.GetMailProvinceCode(item.C_PROVINCE ?? "");
                    client.mail_postal_code = item.N_POSTAL_CODE;
                    ISL_APP_BANKS bank;
                    var ahBankId = (accountHolder != null ? accountHolder.N_BANK_ID : 0);
                    bank = dbISLPortal.ISL_APP_BANKS.Where(x => x.N_BANK_ID == ahBankId).FirstOrDefault();

                    client.bi_code = dataMigrationMapper.GetBankCode(bank != null ? bank.C_BANK_NAME : "");
                    client.bank_account_no = accountHolder != null ? accountHolder.C_BANK_ACCOUNT_NO.Replace("-", "").Replace(" ", "") : null;
                    client.bank_branch = accountHolder != null ? accountHolder.C_BANK_BRANCH : null;
                    client.bank_city = accountHolder != null ? accountHolder.C_BANK_CITY : null;
                    //client.id_type_code = dataMigrationMapper.GetIdTypeCode(item.N_CUSTOMER_ID);
                    client.no_id = item.C_ID_CARD_NUMBER.Replace(" ", "").Replace("-", "").Trim(); ;
                    client.id_issue_date = null;
                    client.id_expire_date = null;
                    client.height = null;
                    client.weight = null;
                    client.monthly_cost = null;
                    client.client_status = null;
                    //client.npwp = item..NPWP_PP.Replace(" ", "").Replace("-", "").Trim();
                    client.npwp_name = null;
                    client.npwp_address = null;
                    client.front_title = null;
                    client.back_title = null;
                    client.mother_birth_date = null;
                    listClientToInsert.Add(client);

                    var newOldIdToNewId = new OldClientIdToNewClientId();
                    newOldIdToNewId.NewId = client.client_no;
                    newOldIdToNewId.OldId = item.N_CUSTOMER_ID.ToString();
                    newOldIdToNewId.DatabaseName = "ISLPortal";
                    newOldIdToNewId.TableName = "ISL_APP_CUSTOMERS";
                    listOldIdToNewId.Add(newOldIdToNewId);
                }
            }
            dbMGULDBDev.cd_client_.AddRange(listClientToInsert);
            dbMGULDBDev.SaveChanges();
            dbMGULDBDev.OldClientIdToNewClientId.AddRange(listOldIdToNewId);
            dbMGULDBDev.SaveChanges();
            Console.WriteLine("Client Jiwa Data Migration Is Done");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine();


        }
        static void StartDataMigrationPolisIlias()
        {

            Console.WriteLine("Client Ilias Data Migration Is Starting..");
            Console.WriteLine(DateTime.Now);
            var dbIlias = new ILIASEntities();
            var allClientIlias = dbIlias.CLIENT.ToList();
            var allClientCore = new MIGULDBDevEntities().cd_client_.ToList();
            var dbMigUlDev = new MIGULDBDevEntities();
            var allClientsIliasWithAppRel = new List<(CLIENT client, APPREL apprel)>();
            var allBankAccount = dbIlias.Database.SqlQuery<View_BankAccount>("SELECT cast( row_number() OVER (PARTITION BY getdate() order by getdate()) as int) AS ROW_ID, ba.* FROM BANK_ACCOUNT ba").ToList();
            foreach (var item in allClientIlias)
            {
                var apprel = dbIlias.APPREL.Where(x => x.CLIENT_ID == item.CLIENT_ID && x.RELA_CODE == "OWN").FirstOrDefault();
                if (apprel == null)
                {
                    apprel = dbIlias.APPREL.Where(x => x.CLIENT_ID == item.CLIENT_ID).FirstOrDefault();
                }
                allClientsIliasWithAppRel.Add((item, apprel));
            }

            var excludedClient = new List<(CLIENT client, APPREL apprel)>();
            var includedClient = new List<(CLIENT client, APPREL apprel)>();
            foreach (var item in allClientsIliasWithAppRel)
            {
                var nameDob = item.client.NAME.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + item.client.DOB.Value.ToString("yyyy-MM-dd");
                var idNo = item.apprel != null ? item.apprel.PERSONID.Replace(" ", "").Replace("-", "").Trim() : null;
                var foundedClient = allClientCore.Where(x => x.client_id_temp == nameDob).FirstOrDefault();
                if (foundedClient != null)
                {
                    excludedClient.Add(item);
                }
                else
                {
                    includedClient.Add(item);
                }
            }

            //Update Data Client Bila Ditemukan data pelengkap
            foreach (var item in excludedClient)
            {
                var nameDob = item.client.NAME.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + item.client.DOB.Value.ToString("yyyy-MM-dd");
                var idNo = item.apprel != null ? item.apprel.PERSONID.Replace(" ", "").Replace("-", "").Trim() : null;

                var client = allClientCore.Where(x => x.client_id_temp == nameDob).FirstOrDefault();
                client = dbMigUlDev.cd_client_.Find(client.client_no);
                if (string.IsNullOrWhiteSpace(client.mother_name))
                {
                    client.mother_name = item.client.MOTHERNAME != null ? item.client.MOTHERNAME : "";
                }
                if (string.IsNullOrWhiteSpace(client.birth_place))
                {
                    client.mother_name = item.client.POB != null ? item.client.POB : "";
                }
                if (item.apprel != null && string.IsNullOrWhiteSpace(client.no_id))
                {
                    client.no_id = item.apprel.PERSONID;
                }
                if (!client.policy_product.Contains("Ilias"))
                {
                    client.policy_product = client.policy_product + "-" + "Ilias";
                }





                dbMigUlDev.Entry(client).State = System.Data.Entity.EntityState.Modified;
                dbMigUlDev.SaveChanges();




            }

            //Generate Mapping OldClientIdToNewClientId

            foreach (var item in excludedClient)
            {
                var nameDob = item.client.NAME.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + item.client.DOB.Value.ToString("yyyy-MM-dd");

                var client = allClientCore.Where(x => x.client_id_temp == nameDob).FirstOrDefault();

                OldClientIdToNewClientId oldClientIdToNewClientId;
                oldClientIdToNewClientId = dbMigUlDev.OldClientIdToNewClientId.Where(x => x.NewId == client.no_id && x.OldId == item.client.CLIENT_ID.ToString()).FirstOrDefault();
                if (oldClientIdToNewClientId == null)
                {
                    oldClientIdToNewClientId = new OldClientIdToNewClientId();
                    oldClientIdToNewClientId.OldId = item.client.CLIENT_ID.ToString();
                    oldClientIdToNewClientId.NewId = client.client_no;
                    oldClientIdToNewClientId.DatabaseName = "ILIAS";
                    oldClientIdToNewClientId.TableName = "Client";

                    dbMigUlDev.OldClientIdToNewClientId.Add(oldClientIdToNewClientId);
                }
                dbMigUlDev.SaveChanges();
            }

            //Insert Data To Client From Ilias Client
            var listClientToInsert = new List<cd_client_>();
            var counter = 0;
            var lastClientId = dbMigUlDev.cd_client_.OrderByDescending(x => x.client_no).FirstOrDefault();
            counter = lastClientId != null ? Convert.ToInt32(lastClientId.client_no) : 0;
            var migrationMapper = new DataMigrationMapper();
            var listOldIdToNewId = new List<OldClientIdToNewClientId>();
            var listAllAddress = dbIlias.ADDRESS.ToList();
            foreach (var item in includedClient)
            {
                counter++;
                var nameDob = item.client.NAME.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + item.client.DOB.Value.ToString("yyyy-MM-dd");
                var idNo = item.apprel != null ? item.apprel.PERSONID.Replace(" ", "").Replace("-", "").Trim() : null;

                var client = new cd_client_();
                client.client_id_temp = nameDob;
                client.client_no = counter.ToString().PadLeft(10, '0');
                client.policy_no = item.apprel != null ? item.apprel.APPLI_NUM ?? "" : "";
                client.policy_product = "Ilias";
                client.ppOrttOrbf = "";
                client.salutation = migrationMapper.GetSalutaion(item.client.MARITAL_ST, item.client.SEX);
                client.birth_place = item.client.POB ?? "";
                client.birth_date = item.client.DOB.HasValue ? item.client.DOB.Value : new DateTime(1, 1, 1);
                if (item.apprel != null)
                {
                    ADDRESS address;
                    ADDRESS businessAddress;
                    if (item.apprel.RELA_CODE == "OWN")
                    {
                        address = listAllAddress.Where(x => x.ADDR_ID == item.apprel.APPLICATION.ADDR_ID).FirstOrDefault();
                        businessAddress = listAllAddress.Where(x => x.ADDR_ID == item.apprel.B_ADDR).FirstOrDefault();

                    }
                    else
                    {
                        address = listAllAddress.Where(x => x.ADDR_ID == item.apprel.H_ADDR).FirstOrDefault();
                        businessAddress = listAllAddress.Where(x => x.ADDR_ID == item.apprel.B_ADDR).FirstOrDefault();
                    }
                    if (address == null)
                    {

                        client.residence_address1 = "";
                        client.residence_address2 = null;
                        client.email = "";



                        client.mobile_phone = "";
                        client.smoker_status = "N";
                    }
                    else
                    {
                        client.residence_address1 = address != null ? address.ADDR1 ?? "" : "";
                        client.residence_address2 = address.ADDR2;
                        client.email = address.EMAIL ?? "";

                        client.GetResidenceCity(address.CITY ?? "");

                        client.residence_postal_code = address.POSTCODE;
                        client.GetResidenceProvinceCode(address != null ? address.PROVINCE ?? "" : "");

                        //client.mobile_phone = address.HPNO ?? "";
                        if (address.HPNO.Count() <= 15)
                        {
                            client.mobile_phone = address.HPNO ?? "";
                        }
                        else
                        {
                            client.mobile_phone = "";
                        }
                        client.smoker_status = item.apprel.APPLICATION != null ? item.apprel.APPLICATION.SMOKER == "S" ? "Y" : "N" : "N";
                        client.residence_fax = address.FAXNO;
                    }


                    client.mail_address1 = client.residence_address1;
                    client.mail_address2 = client.residence_address2;
                    client.mail_city = client.residence_city;
                    client.mail_province_code = client.residence_province_code;
                    client.mail_postal_code = client.residence_postal_code;
                    client.GetOccupation(item.apprel != null ? item.apprel.OCU1 : "");
                    client.GetOccupationCategory(item.apprel.OCU2);

                    var applinum = (item.apprel != null ? item.apprel.APPLI_NUM : "wertyuqwertywertyuytrewertyu");
                    var bankAccount = allBankAccount.Where(x => x.APPLI_NUM == applinum).FirstOrDefault();
                    client.account_holder = bankAccount != null ? bankAccount.NAME ?? "" : "";
                    client.bi_code = migrationMapper.GetBankCode(bankAccount != null ? bankAccount.NAME : "");
                    client.bank_account_no = bankAccount != null ? bankAccount.ACCNUMBER ?? "" : "";
                    client.bank_branch = bankAccount.BRANCH;
                    client.bank_city = bankAccount.CITY;

                    if (businessAddress != null)
                    {
                        client.office_address1 = businessAddress.ADDR1;
                        client.office_address2 = businessAddress.ADDR2;
                        client.office_city = businessAddress.CITY;
                        client.office_fax = businessAddress.FAXNO;
                        client.office_phone_no = businessAddress.TELP;
                        client.office_postal_code = businessAddress.POSTCODE;
                        client.office_province_code = businessAddress.PROVINCE;
                    }


                }
                else
                {
                    client.residence_address1 = "";
                    client.email = "";

                    client.account_holder = "";

                    client.mobile_phone = "";
                    client.smoker_status = "N";
                }

                client.account_holder_relation_code = "";
                client.mother_name = item.client.MOTHERNAME ?? "";
                client.client_name = item.client.NAME;
                client.gender_code = item.client.SEX;
                //client.GetMaritalStatusCode(polisInvesta.Marital_PP, polisInvesta.Jk_PP);

                client.position_code = null;
                client.client_type_code = null;
                client.religion_code = null;
                client.cigarete_perday = null;
                client.residence_phone_no = null;
                client.residence_fax = null;
                client.office_address1 = null;
                client.office_address2 = null;
                client.office_phone_no = null;
                client.office_fax = null;
                client.office_city = null;
                client.office_province_code = null;
                client.office_postal_code = null;



                client.bank_branch = null;
                client.bank_city = null;
                //client.id_type_code = dataMigrationMapper.GetIdTypeCode(item.N_CUSTOMER_ID);
                client.no_id = item.client.KTP != null ? item.client.KTP.Replace(" ", "").Replace("-", "").Trim() : null;
                client.id_issue_date = null;
                client.id_expire_date = null;
                client.height = null;
                client.weight = null;
                client.monthly_cost = null;
                client.client_status = null;
                //client.npwp = item.apprel..NPWP_PP.Replace(" ", "").Replace("-", "").Trim();
                client.npwp_name = null;
                client.npwp_address = null;
                client.front_title = null;
                client.back_title = null;
                client.mother_birth_date = null;

                //if (listClientToInsert.Where(x => x.client_id_temp == client.client_id_temp).FirstOrDefault() == null)
                //{
                listClientToInsert.Add(client);

                OldClientIdToNewClientId oldClientIdToNewClientId = new OldClientIdToNewClientId();

                oldClientIdToNewClientId.OldId = item.client.CLIENT_ID.ToString();
                oldClientIdToNewClientId.NewId = client.client_no;
                oldClientIdToNewClientId.DatabaseName = "ILIAS";
                oldClientIdToNewClientId.TableName = "Client";
                listOldIdToNewId.Add(oldClientIdToNewClientId);
                //}
            }
            dbMigUlDev.cd_client_.AddRange(listClientToInsert);
            dbMigUlDev.SaveChanges();
            dbMigUlDev.OldClientIdToNewClientId.AddRange(listOldIdToNewId);
            dbMigUlDev.SaveChanges();

            Console.WriteLine("Client Ilias Data Migration Is Done");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine();

        }

        static void StartDataMigrationPSFundTransactionHistory()
        {
            Console.WriteLine("FundTransactionHistory Data Migration Is Starting..");
            Console.WriteLine(DateTime.Now);
            var dbIlias = new ILIASEntities();
            var dbMigUlDev = new MIGULDBDevEntities();
            var deleteFromps_fund_transaction_history_ = dbMigUlDev.Database.ExecuteSqlCommand("delete  [MIGULDBDev].[dbo].[ps_fund_transaction_history_]");

            var result = dbMigUlDev.spMigrateFundTransactionFromIlias().ToList();
            if (result.FirstOrDefault() != null && result.FirstOrDefault() == 1)
            {
                Console.WriteLine("spMigrateFundTransactionFromIlias Data Migration Is Done");
            }
            else
            {
                Console.WriteLine("spMigrateFundTransactionFromIlias Data Migration Is FAILED !!!");

            }
            var resultSpMigrationMultiuser = dbMigUlDev.spMigrateFuntTransactionMultiUser().ToList();

            if (resultSpMigrationMultiuser.FirstOrDefault() != null && resultSpMigrationMultiuser.FirstOrDefault() == 1)
            {
                Console.WriteLine("spMigrateFuntTransactionMultiUser Data Migration Is Done");
            }
            else
            {
                Console.WriteLine("spMigrateFuntTransactionMultiUser Data Migration Is FAILED !!!");

            }
            Console.WriteLine("FundTransactionHistory Data Migration Is Done");

            Console.WriteLine(DateTime.Now);
            Console.WriteLine();
        }

        static void StartDataMigrationFnFundnav()
        {
            Console.WriteLine("FundNAV Data Migration Is Starting..");
            Console.WriteLine(DateTime.Now);
            var dbIlias = new ILIASEntities();
            var dbMigUlDev = new MIGULDBDevEntities();
            var deleteFundNAV = dbMigUlDev.Database.ExecuteSqlCommand("delete  [MIGULDBDev].[dbo].[fn_fundnav_]");


            var result = dbMigUlDev.spMigrateFnFundnav().ToList();
            if (result.FirstOrDefault() != null && result.FirstOrDefault() == 1)
            {
                Console.WriteLine("FundNAV Data Migration Is Done");
            }
            else
            {
                Console.WriteLine("FundNAV Data Migration Is FAILED !!!");

            }


            Console.WriteLine(DateTime.Now);
            Console.WriteLine();
        }


        static void StartDataMigrationILIASBeneficiary()
        {

            Console.WriteLine("ILIASBeneficiary Data Migration Is Starting..");
            Console.WriteLine(DateTime.Now);
            var dbIlias = new ILIASEntities();
            var dbMigUlDev = new MIGULDBDevEntities();
            var dbClientMapping = new OLDToNEWMappingEntities();
            var allMapping = dbClientMapping.ClientMapping.ToList();

            var allBeneficiary = dbIlias.BENEFICIARY.ToList();
            //var allBeneficiaryToInsert = new List<BENEFICIARY>();
            //var allNewClients = dbMigUlDev.cd_client_.ToList();
            var listOldIdToNewId = new List<OldClientIdToNewClientId>();
            //foreach (var item in allBeneficiary)
            //{
            //    var allClientInOnePolice = new List<CLIENT>();
            //    foreach (var itemAppRel in item.APPLICATION.APPREL)
            //    {
            //        allClientInOnePolice.Add(itemAppRel.CLIENT);
            //    }

            //    var client = allClientInOnePolice.Where(x => x.NAME == item.NAME).FirstOrDefault();
            //    if (client == null)
            //    {
            //        allBeneficiaryToInsert.Add(item);
            //    }
            //    else
            //    {
            //        OldClientIdToNewClientId oldClientIdToNewClientId = new OldClientIdToNewClientId();
            //        var clientMapping = allMapping.Where(x => x.DatabaseName == "ILIAS" && x.OldId == client.CLIENT_ID.ToString()).FirstOrDefault();
            //        if (clientMapping != null)
            //        {
            //            oldClientIdToNewClientId.OldId = clientMapping.OldId;
            //            oldClientIdToNewClientId.NewId = clientMapping.NewId;
            //            oldClientIdToNewClientId.DatabaseName = "ILIAS";
            //            oldClientIdToNewClientId.TableName = "Beneficiary";
            //            listOldIdToNewId.Add(oldClientIdToNewClientId);

            //        }
            //        else
            //        {
            //            oldClientIdToNewClientId.OldId = item.BEN_ID.ToString();
            //            oldClientIdToNewClientId.NewId = client;
            //            oldClientIdToNewClientId.DatabaseName = "ILIAS";
            //            oldClientIdToNewClientId.TableName = "Beneficiary";
            //            listOldIdToNewId.Add(oldClientIdToNewClientId);
            //        }
            //    }

            //}
            var counter = 0l;
            var lastClientId = dbMigUlDev.cd_client_.ToList().Select(x => new { id = Convert.ToInt64(x.client_no) }).OrderByDescending(x => x.id).FirstOrDefault();
            counter = lastClientId != null ? Convert.ToInt64(lastClientId.id) : 0;

            var listClientToInsert = new List<cd_client_>();

            foreach (var item in allBeneficiary)
            {
                counter++;
                var dob = new DateTime(2001, 1, 1);
                var idTemp = item.NAME.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + dob.ToString("yyyy-MM-dd");
                var client = new cd_client_();
                client.client_name = item.NAME;
                client.account_holder = "";
                client.account_holder_relation_code = "";
                client.birth_date = dob;
                client.birth_place = "";
                client.client_id_temp = idTemp;
                client.client_no = counter.ToString().PadLeft(10, '0');
                client.email = "";
                client.mobile_phone = "";
                client.mother_name = "";
                client.policy_no = "";
                client.policy_product = "";
                client.ppOrttOrbf = "BF";
                client.residence_address1 = "";
                client.salutation = "";
                client.smoker_status = "";
                listClientToInsert.Add(client);


                OldClientIdToNewClientId oldClientIdToNewClientId = new OldClientIdToNewClientId();

                oldClientIdToNewClientId.OldId = item.BEN_ID.ToString();
                oldClientIdToNewClientId.NewId = client.client_no;
                oldClientIdToNewClientId.DatabaseName = "ILIAS";
                oldClientIdToNewClientId.TableName = "Beneficiary";
                listOldIdToNewId.Add(oldClientIdToNewClientId);
            }
            dbMigUlDev.cd_client_.AddRange(listClientToInsert);
            dbMigUlDev.OldClientIdToNewClientId.AddRange(listOldIdToNewId);
            dbMigUlDev.SaveChanges();

            Console.WriteLine("ILIASBeneficiary Data Migration Is DONE");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine();
        }

        static void StartDataMigrationJiwaBeneficiary()
        {
            Console.WriteLine("JiwaBeneficiary Data Migration Is Starting..");
            Console.WriteLine(DateTime.Now);
            var dbIlias = new ILIASEntities();
            var dbMigUlDev = new MIGULDBDevEntities();
            var dbClientMapping = new OLDToNEWMappingEntities();
            var dbIslPortal = new ISL_PORTALEntities();
            var allBeneficiaryFromISLPortal = new List<BeneficiaryJiwa>();
            string sb = "SELECT C_BENEF_NAME_1 name" +
"	,C_BENEF_SEX_1 sex" +
"	,D_BENEF_DOB_1 dob" +
" FROM [ISL_PORTAL].[dbo].[ISL_OPS_POLICIES]" +
" UNION" +
" SELECT C_BENEF_NAME_2 name" +
"	,C_BENEF_SEX_2 sex" +
"	,D_BENEF_DOB_2 dob" +
" FROM [ISL_PORTAL].[dbo].[ISL_OPS_POLICIES]" +
"  UNION" +
" SELECT C_BENEF_NAME_3 name" +
"	,C_BENEF_SEX_3 sex" +
"	,D_BENEF_DOB_3 dob" +
" FROM [ISL_PORTAL].[dbo].[ISL_OPS_POLICIES]" +
" UNION" +
" SELECT C_BENEF_NAME_4 name" +
"	,C_BENEF_SEX_4 sex" +
"	,D_BENEF_DOB_4 dob" +
" FROM [ISL_PORTAL].[dbo].[ISL_OPS_POLICIES]";
            var beneficiaryFromIslPortal = dbIslPortal.Database.SqlQuery<BeneficiaryJiwa>(sb).ToList();


            var counter = 0l;
            var lastClientId = dbMigUlDev.cd_client_.ToList().Select(x => new { id = Convert.ToInt64(x.client_no) }).OrderByDescending(x => x.id).FirstOrDefault();
            counter = lastClientId != null ? Convert.ToInt64(lastClientId.id) : 0;

            var listClientToInsert = new List<cd_client_>();
            var listOldIdToNewId = new List<OldClientIdToNewClientId>();
            foreach (var item in beneficiaryFromIslPortal.Where(x => x.name != null))
            {
                counter++;
                var dob = item.dob.HasValue ? item.dob : new DateTime(2001, 1, 1);
                var idTemp = item.name.ToLower().Replace(" ", "").Replace(".", "").Replace(",", "") + dob.Value.ToString("yyyy-MM-dd");
                var client = new cd_client_();
                client.client_name = item.name;
                client.gender_code = item.sex;
                client.account_holder = "";
                client.account_holder_relation_code = "";
                client.birth_date = dob.Value;
                client.birth_place = "";
                client.client_id_temp = idTemp;
                client.client_no = counter.ToString().PadLeft(10, '0');
                client.email = "";
                client.mobile_phone = "";
                client.mother_name = "";
                client.policy_no = "";
                client.policy_product = "";
                client.ppOrttOrbf = "BF";
                client.residence_address1 = "";
                client.salutation = "";
                client.smoker_status = "";
                listClientToInsert.Add(client);


                OldClientIdToNewClientId oldClientIdToNewClientId = new OldClientIdToNewClientId();

                oldClientIdToNewClientId.OldId = client.client_id_temp.ToString();
                oldClientIdToNewClientId.NewId = client.client_no;
                oldClientIdToNewClientId.DatabaseName = "JIWA";
                oldClientIdToNewClientId.TableName = "ISL_OPS_POLICIES";
                listOldIdToNewId.Add(oldClientIdToNewClientId);
            }
            dbMigUlDev.cd_client_.AddRange(listClientToInsert);
            dbMigUlDev.OldClientIdToNewClientId.AddRange(listOldIdToNewId);
            dbMigUlDev.SaveChanges();
            Console.WriteLine("JiwaBeneficiary Data Migration Is DONE");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine();
        }

        static void Test()
        {
            var date = new DateTime();
            string test = null;
            Console.WriteLine(test);
            Console.WriteLine(DateTime.TryParseExact("2020-" + test + "-31", "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out date));
            Console.WriteLine(date.Day + " " + date.Month + " " + date.Year);

        }
    }

    class BeneficiaryJiwa
    {
        public string name { get; set; }
        public string sex { get; set; }
        public DateTime? dob { get; set; }
    }
}