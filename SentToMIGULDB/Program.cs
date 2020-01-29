using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentToMIGULDB
{
    class Program
    {
        static void Main(string[] args)
        {

            SentClientDataToMIGULDB();
            Sentfn_fundnavDataToMIGULDB();
            SentFundTransactionDataToMIGULDB();
            SentClientMappingToOLDToNewMapping();
            Console.ReadLine();


        }
        static void SentClientDataToMIGULDB()
        {

            Console.WriteLine("PUSH TO MIGULDB");
            Console.WriteLine(DateTime.Now);
            var dbDev = new MIGULDBDevEntities();
            // BuildMyString.com generated code. Please enjoy your string responsibly.

            string sb = "/****** Script for SelectTopNRows command from SSMS  ******/" +
            "SELECT [client_no]" +
            "      ,[salutation]" +
            "      ,[client_name]" +
            "      ,[birth_place]" +
            "      ,[birth_date]" +
            "      ,[gender_code]" +
            "      ,[marital_status_code]" +
            "      ,[occupation_code]" +
            "      ,[category_code]" +
            "      ,[position_code]" +
            "      ,[education_code]" +
            "      ,[client_type_code]" +
            "      ,[religion_code]" +
            "      ,[smoker_status]" +
            "      ,[cigarete_perday]" +
            "      ,[residence_address1]" +
            "      ,[residence_address2]" +
            "      ,[residence_phone_no]" +
            "      ,[residence_fax]" +
            "      ,[residence_city]" +
            "      ,[residence_postal_code]" +
            "      ,[residence_province_code]" +
            "      ,[office_address1]" +
            "      ,[office_address2]" +
            "      ,[office_phone_no]" +
            "      ,[office_fax]" +
            "      ,[office_city]" +
            "      ,[office_province_code]" +
            "      ,[office_postal_code]" +
            "      ,[mail_address1]" +
            "      ,[mail_address2]" +
            "      ,[mail_city]" +
            "      ,[mail_province_code]" +
            "      ,[mail_postal_code]" +
            "      ,[bi_code]" +
            "      ,[bank_account_no]" +
            "      ,[bank_branch]" +
            "      ,[bank_city]" +
            "      ,[account_holder]" +
            "      ,[account_holder_relation_code]" +
            "      ,[mobile_phone]" +
            "      ,[email]" +
            "      ,[id_type_code]" +
            "      ,[no_id]" +
            "      ,[id_issue_date]" +
            "      ,[id_expire_date]" +
            "      ,[height]" +
            "      ,[weight]" +
            "      ,[monthly_cost]" +
            "      ,[client_status]" +
            "      ,[npwp]" +
            "      ,[npwp_name]" +
            "      ,[npwp_address]" +
            "      ,[front_title]" +
            "      ,[back_title]" +
            "      ,[mother_name]" +
            "      ,[mother_birth_date]" +
            "  FROM [MIGULDBDev].[dbo].[cd_client_]";

            var allClient = dbDev.Database.SqlQuery<cd_client>(sb).ToList();
            var dbMIGULDB = new MIGULDBEntities();
            dbMIGULDB.cd_client.RemoveRange(dbMIGULDB.cd_client);
            dbMIGULDB.SaveChanges();
            dbMIGULDB.cd_client.AddRange(allClient);
            dbMIGULDB.SaveChanges();

            Console.WriteLine("PUSH DATA IS DONE");
            Console.WriteLine(DateTime.Now);


        }
        static void SentFundTransactionDataToMIGULDB()
        {

            Console.WriteLine("PUSH ps_fund_transaction_history TO MIGULDB");
            Console.WriteLine(DateTime.Now);
            var dbDev = new MIGULDBDevEntities();
            // BuildMyString.com generated code. Please enjoy your string responsibly.


            var allps_fund_transaction_history = dbDev.Database.SqlQuery<ps_fund_transaction_history>("select * from [MIGULDBDev].[dbo].[ps_fund_transaction_history_]").ToList();
            var dbMIGULDB = new MIGULDBEntities();
            dbMIGULDB.ps_fund_transaction_history.RemoveRange(dbMIGULDB.ps_fund_transaction_history);
            dbMIGULDB.SaveChanges();

            dbMIGULDB.ps_fund_transaction_history.AddRange(allps_fund_transaction_history);
            dbMIGULDB.SaveChanges();

            Console.WriteLine("PUSH ps_fund_transaction_history DATA IS DONE");
            Console.WriteLine(DateTime.Now);


        }
        static void Sentfn_fundnavDataToMIGULDB()
        {

            Console.WriteLine("PUSH fn_fundnav TO MIGULDB");
            Console.WriteLine(DateTime.Now);
            var dbDev = new MIGULDBDevEntities();
            // BuildMyString.com generated code. Please enjoy your string responsibly.


            var allFn_fundnav = dbDev.Database.SqlQuery<fn_fundnav>("select * from fn_fundnav_").ToList();
            var dbMIGULDB = new MIGULDBEntities();
            dbMIGULDB.fn_fundnav.RemoveRange(dbMIGULDB.fn_fundnav);
            dbMIGULDB.SaveChanges();

            dbMIGULDB.fn_fundnav.AddRange(allFn_fundnav);
            dbMIGULDB.SaveChanges();

            Console.WriteLine("PUSH fn_fundnav DATA IS DONE");
            Console.WriteLine(DateTime.Now);


        }

        static void SentClientMappingToOLDToNewMapping()
        {
            Console.WriteLine("PUSH ClientMapping TO MIGULDB");
            Console.WriteLine(DateTime.Now);
            var dbDev = new MIGULDBDevEntities();
            // BuildMyString.com generated code. Please enjoy your string responsibly.


            var allClientMapping = dbDev.Database.SqlQuery<ClientMapping>("select * from [MIGULDBDev].[dbo].[OldClientIdToNewClientId]").ToList();
            var dbOldToNewMapping = new OLDToNEWMappingEntities();
            dbOldToNewMapping.ClientMapping.RemoveRange(dbOldToNewMapping.ClientMapping);
            dbOldToNewMapping.SaveChanges();

            dbOldToNewMapping.ClientMapping.AddRange(allClientMapping);
            dbOldToNewMapping.SaveChanges();

            Console.WriteLine("PUSH ClientMapping DATA IS DONE");
            Console.WriteLine(DateTime.Now);

        }
    }
}
