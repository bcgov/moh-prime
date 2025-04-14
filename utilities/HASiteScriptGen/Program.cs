using System;
using System.Linq;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text;
using DocumentFormat.OpenXml.Math;

namespace PRIME
{
    class Program
    {
        const bool debug = false;
        static void Main(string[] args)
        {

            string fileName = @".\\IHA sites.xlsx";
            string outputFilename = "IHA sites insert.sql";
            
            GenerateInsertScript(fileName, outputFilename);    

        }

        static void GenerateInsertScript(string fileName, string outputFilename)
        {
            var userId = "00000000-0000-0000-0000-000000000014";

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(fileName, false))
            {

                WorkbookPart? workbookPart = spreadsheetDocument.WorkbookPart;
                SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                SharedStringTable sst = sstpart.SharedStringTable;



                Sheet? s = workbookPart.Workbook.Descendants<Sheet>().Where(sht => sht.Name == "Sites").FirstOrDefault();
                WorksheetPart wsPart = (WorksheetPart)workbookPart.GetPartById(s.Id);
                SheetData? sheetData = wsPart.Worksheet.Elements<SheetData>().FirstOrDefault();

                var scriptStr = new StringBuilder();

                scriptStr.AppendLine("do $$");
                scriptStr.AppendLine("declare");
                scriptStr.AppendLine("  address_id integer := null;");
                scriptStr.AppendLine("  site_id integer := null;");
                scriptStr.AppendLine("  vendor_id integer := null;");
                scriptStr.AppendLine("  admin_id integer := null;");
                scriptStr.AppendLine("  ha_care_type_id integer := null;");
                scriptStr.AppendLine("  ha_vendor_id integer := null;");
                scriptStr.AppendLine("  tech_support_id integer := null;");
                scriptStr.AppendLine("  privacy_office_id integer := null;");
                scriptStr.AppendLine("begin");
                scriptStr.AppendLine();

                IEnumerable<Row> rows = sheetData.Elements<Row>();
                for(int i = 1; i< rows.Count(); i++){
                    Row r = rows.ElementAt(i);

                    string authorizedUserId = "-1";
                    string orgId = "-1";
                    string careSetting = "";
                    string careType = "";
                    string vendor = "";
                    string siteName = "";
                    string siteMnemoic = "";
                    string pec = "";
                    string address1 = "";
                    string address2 = "";
                    string city = "";
                    string province = "";
                    string country = "";
                    string postalCode = "";

                    string hours = "";

                    string[]? mondaysHours = null;
                    string[]? tuesdayHours = null;
                    string[]? wednesdayHours = null;
                    string[]? thursdayHours = null;
                    string[]? fridayHours = null;
                    string[]? saturdayHours = null;
                    string[]? sundayHours = null;

                    string adminEmail = "";
                    string techSupportEmail = "";
                    string privacyOfficeEmail = "";
                    
                    //Console.WriteLine($"line:{i}");

                    
                    for(int x = 0; x < r.Elements<Cell>().Count(); x++ ){
                        
                        Cell c = r.Elements<Cell>().ElementAt(x);
                        if(c.InnerText.Length > 0){

                            var value = c.InnerText;

                            value = value.Trim();

                            if(c.DataType != null)
                            {
                                switch(c.DataType.Value){
                                    case CellValues.SharedString:
                                        value = sst.ChildElements[int.Parse(value)].InnerText;
                                        break;
                               }
                            }

                            Console.WriteLine(x + " " + value);

                            switch (x)
                            {
                                case 0:
                                    authorizedUserId = value;    
                                    break;
                                case 1:
                                    orgId = value;
                                    break;
                                case 2:
                                    careSetting = value;
                                    break;
                                case 3:
                                    careType = value;
                                    break;
                                case 4:
                                    vendor = value;
                                    break;
                                case 5:
                                    if(value != "na"){
                                        siteMnemoic = value;
                                    }
                                    break;
                                case 6:
                                    siteName = value;
                                    break;
                                case 7:
                                    if(value != "na"){
                                        pec = value;
                                    }
                                    break;
                                case 8:
                                    address1 = value;
                                    break;
                                case 9:
                                    address2 = value;
                                    break;
                                case 10:
                                    city = value;
                                    break;
                                case 11:
                                    province = value;
                                    break;
                                case 12:
                                    country = value;
                                    break;
                                case 13:
                                    postalCode = value;
                                    break;


                                case 14:
                                    hours = value.ToLower().Trim();
                                    if(hours != "closed"){
                                        mondaysHours = hours.Split("-");
                                    }
                                    break;
                                case 15:
                                    hours = value.ToLower().Trim();
                                    if(hours != "closed"){
                                        tuesdayHours = hours.Split("-");
                                    }
                                    break;
                                case 16:
                                    hours = value.ToLower().Trim();
                                    if(hours != "closed"){
                                        wednesdayHours = hours.Split("-");
                                    }
                                    break;
                                case 17:
                                    hours = value.ToLower().Trim();
                                    if(hours != "closed"){
                                        thursdayHours = hours.Split("-");
                                    }
                                    break;
                                case 18:
                                    hours = value.ToLower().Trim();
                                    if(hours != "closed"){
                                        fridayHours = hours.Split("-");
                                    }
                                    break;
                                case 19:
                                    hours = value.ToLower().Trim();
                                    if(hours != "closed"){
                                        saturdayHours = hours.Split("-");
                                    }
                                    break;
                                case 20:
                                    hours = value.ToLower().Trim();
                                    if(hours != "closed"){
                                        sundayHours = hours.Split("-");
                                    }
                                    break;


                                case 21:
                                    adminEmail = value;
                                    break;
                                case 22:
                                    techSupportEmail = value;
                                    break;
                                case 23:
                                    privacyOfficeEmail = value;
                                    break;

                            }
                        }
                    }

                    if(orgId == "0" && authorizedUserId == "0") break;

                    // construct insert script for the single row
                    string address = address1 + 
                        (address2 == "na" ? "" : " " + address2);

                    string addressInsert = "INSERT INTO \"Address\" (\"CreatedUserId\", \"CreatedTimeStamp\", \"UpdatedUserId\", \"UpdatedTimeStamp\", \"CountryCode\", \"ProvinceCode\"," +
                        $"\"Street\", \"Street2\", \"City\", \"Postal\", \"AddressType\") VALUES('{userId}', CURRENT_TIMESTAMP, '{userId}', CURRENT_TIMESTAMP," +
                        $"'CA', 'BC', '{address}', null, '{city}', '{postalCode}',1) returning \"Id\" into address_id;";

                    scriptStr.AppendLine(addressInsert);
                    //if(debug) Console.WriteLine(addressInsert);

                    string siteInsert = "INSERT INTO \"Site\" (\"CreatedUserId\", \"CreatedTimeStamp\", \"UpdatedUserId\", \"UpdatedTimeStamp\", \"PhysicalAddressId\", \"CareSettingCode\", \"PEC\", " +
                        " \"DoingBusinessAs\", \"Completed\", \"SubmittedDate\", \"ApprovedDate\", \"AdjudicatorId\", \"Flagged\", \"ActiveBeforeRegistration\", \"Mnemonic\") " + 
                        $" VALUES('{userId}', CURRENT_TIMESTAMP, '{userId}', CURRENT_TIMESTAMP, address_id," +
                        $"{careSetting}, '{pec}', '{siteName}', true, null, null, null, false, false, '{siteMnemoic}') returning \"Id\" into site_id;";

                    scriptStr.AppendLine(siteInsert);
                    //if(debug) Console.WriteLine(siteInsert);

                    string siteStatusInsert = " INSERT INTO \"SiteStatus\" (\"CreatedUserId\", \"CreatedTimeStamp\", \"UpdatedUserId\", \"UpdatedTimeStamp\", \"SiteId\", \"StatusType\", \"StatusDate\") " +
                        $" VALUES('{userId}', CURRENT_TIMESTAMP, '{userId}', CURRENT_TIMESTAMP, site_id, 1, CURRENT_TIMESTAMP); ";

                    scriptStr.AppendLine(siteStatusInsert);
                    //if(debug) Console.WriteLine(siteStatusInsert);

                    var mondayInsert = GetHourInsertString(mondaysHours, 1);
                    if(!string.IsNullOrEmpty(mondayInsert)) scriptStr.AppendLine(mondayInsert);
                    //if(debug) Console.WriteLine(mondayInsert);

                    var tuesdayInsert = GetHourInsertString(tuesdayHours, 2);
                    if(!string.IsNullOrEmpty(tuesdayInsert)) scriptStr.AppendLine(tuesdayInsert);
                    //if(debug) Console.WriteLine(tuesdayInsert);

                    var wednesdayInsert = GetHourInsertString(wednesdayHours, 3);
                    if(!string.IsNullOrEmpty(wednesdayInsert))scriptStr.AppendLine(wednesdayInsert);
                    //if(debug) Console.WriteLine(wednesdayInsert);

                    var thursdayInsert = GetHourInsertString(thursdayHours, 4);
                    if(!string.IsNullOrEmpty(thursdayInsert)) scriptStr.AppendLine(thursdayInsert);
                    //if(debug) Console.WriteLine(thursdayInsert);

                    var fridayInsert = GetHourInsertString(fridayHours, 5);
                    if(!string.IsNullOrEmpty(fridayInsert)) scriptStr.AppendLine(fridayInsert);
                    //if(debug) Console.WriteLine(fridayInsert);

                    var saturdayInsert = GetHourInsertString(saturdayHours, 6);
                    if(!string.IsNullOrEmpty(saturdayInsert)) scriptStr.AppendLine(saturdayInsert);
                    //if(debug) Console.WriteLine(saturdayInsert);

                    var sundayInsert = GetHourInsertString(sundayHours, 0);
                    if(!string.IsNullOrEmpty(sundayInsert)) scriptStr.AppendLine(sundayInsert);
                    //if(debug) Console.WriteLine(sundayInsert);


                    var healthAuthorityCareType = $"select hact.\"Id\" into ha_care_type_id from \"HealthAuthorityCareType\" hact where hact.\"HealthAuthorityOrganizationId\" = {orgId} and hact.\"CareType\"" +
                        $" = '{careType}';";
                    
                    var healthAuthorityVendor = $"select hav.\"Id\" into ha_vendor_id	from \"VendorLookup\" vl inner join \"HealthAuthorityVendor\" hav on vl.\"Code\" = hav.\"VendorCode\" where hav.\"HealthAuthorityOrganizationId\" = {orgId} and vl.\"Name\"" +
                        $" = '{vendor}';";

                    var adminSearch = "select hac.\"Id\" into admin_id from \"HealthAuthorityContact\" hac inner join \"Contact\" c on c.\"Id\" = hac.\"ContactId\" " +
                        $"where c.\"Email\" = '{adminEmail}' and hac.\"Discriminator\"  = 'HealthAuthorityPharmanetAdministrator' and hac.\"HealthAuthorityOrganizationId\" = {orgId} ;";

                    var techSupportSearch = "select hac.\"Id\" into tech_support_id from \"HealthAuthorityContact\" hac inner join \"Contact\" c on c.\"Id\" = hac.\"ContactId\" " +
                        " inner join \"HealthAuthorityTechnicalSupportVendor\" hatsv ON hatsv.\"HealthAuthorityTechnicalSupportId\" = hac.\"Id\" " +
                        " inner join \"HealthAuthorityVendor\" hav on hav.\"Id\" = hatsv.\"HealthAuthorityVendorId\" " +
                        " inner join \"VendorLookup\" vl on vl.\"Code\" = hav.\"VendorCode\" " +
                        $"where c.\"Email\" = '{techSupportEmail}' and hac.\"Discriminator\"  = 'HealthAuthorityTechnicalSupport' and hac.\"HealthAuthorityOrganizationId\" = {orgId} and vl.\"Name\" = '{vendor}' ;";

                    var healthAuthoritySiteInsert = "INSERT INTO \"HealthAuthoritySite\" (\"Id\",\"SiteName\", \"SecurityGroupCode\", \"HealthAuthorityOrganizationId\", \"HealthAuthorityVendorId\", \"HealthAuthorityCareTypeId\", \"HealthAuthorityPharmanetAdministratorId\", \"HealthAuthorityTechnicalSupportId\", \"AuthorizedUserId\") VALUES( site_id," +
                        $"'{siteName}', 0, {orgId}, ha_vendor_id, ha_care_type_id, admin_id, tech_support_id, {authorizedUserId} );";

                    scriptStr.AppendLine(healthAuthorityCareType);
                    scriptStr.AppendLine(healthAuthorityVendor);
                    scriptStr.AppendLine(adminSearch);
                    scriptStr.AppendLine(techSupportSearch);
                    scriptStr.AppendLine(healthAuthoritySiteInsert);

                    scriptStr.AppendLine();
                }

                scriptStr.AppendLine("end $$;");

                string script = $@".\\{outputFilename}";

                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(script, 1024))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(scriptStr.ToString());
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

            }
        }

        static string GetHourInsertString(string[]? operationHours, int dayOfWeek){
            var userId = "00000000-0000-0000-0000-000000000014";
            if(operationHours == null){
                return "";
            }else{
                //Console.WriteLine(operationHours[0]);
                var startTime = operationHours[0].Trim();
                if (startTime == "0:00") startTime = "00:00";
                if( startTime.IndexOf(":") < 0 && startTime.Length == 4) startTime = startTime.Substring(0,2) + ":" + startTime.Substring(2,2);

                //Console.WriteLine(operationHours[1]);
                var endTime = operationHours[1].Trim();
                if( endTime.IndexOf(":") < 0 && endTime.Length == 4) endTime = endTime.Substring(0,2) + ":" + endTime.Substring(2,2);

                return "INSERT INTO \"BusinessDay\" (\"CreatedUserId\", \"CreatedTimeStamp\", \"UpdatedUserId\", \"UpdatedTimeStamp\", \"SiteId\", \"Day\", \"StartTime\", \"EndTime\") " +
                    $" VALUES('{userId}', CURRENT_TIMESTAMP, '{userId}', CURRENT_TIMESTAMP, site_id, {dayOfWeek}, '{startTime}', '{endTime}');";
            }
        }
    }
}