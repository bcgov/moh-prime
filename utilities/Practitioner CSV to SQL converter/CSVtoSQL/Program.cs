// See https://aka.ms/new-console-template for more information
using System.Text;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

class Program
{
    //Note:
    //2025-01-07 : fix issue where the name contains comma and mess up the column count
    //2025-01-07 : fix issue where the name contains single qoute and it messes up the sql creation

    //Improvement - update the insert statement with multiple rows of values like "insert into [table] values (a, b, ..), (d, e, ..), (f, g, ..);
    static void Main(string[] args)
    {
        // update the input file name as needed
        string inputFilename = ".\\pract.20250317.074503.csv";
        // update the output file name as needed
        string outputFilename = ".\\pract.20250317.074503.sql";

        const string insertPrefix = "INSERT INTO public.\"HibcPractitioner\" (\"RecordType\", \"PractitionerId\", \"PractitionerIdRef\"," +
             "\"RecEffDt\", \"FullFirstName\", \"FullMiddleName\", \"FullLastName\", \"DtOfBirth\", \"DtOfDeath\", \"LangPref\", " + 
             "\"Gender\", \"RecInactiveFlag\", \"VersionControlNo\", \"RecCreateUserId\", \"RecCreateTimestamp\",  \"LastUpdUserId\", " +
             "\"LastUpdTimestamp\", \"PractitionerStatus\", \"PractStatusEffDt\", \"EmailAddress\") VALUES";

        const string insertValueStatement =	" (";

        const string insertClose = ");";

        const int bulkInsertAmount = 1000;

        var sqlSB = new StringBuilder();

        try
        {
            using (StreamReader sr = new StreamReader(inputFilename))
            {
                string line;
                var lineCounter = 1;

                while ((line = sr.ReadLine()) != null )
                {
                    if(lineCounter % bulkInsertAmount == 1)
                    {
                        sqlSB.AppendLine(insertPrefix);
                    }

                    sqlSB.Append(insertValueStatement);

                    //Console.WriteLine($"processing line {lineCounter}");

                    if(line.Length > 0) 
                    {
                        var strArray = line.Split(',');

                        //Console.WriteLine($"array length {strArray.Length}");
                        if(strArray.Length == 21)
                        {
                            for(var i = 0; i < strArray.Length; i++)
                            {
                                switch(i){
                                    case 0:
                                    case 1:
                                    case 2:
                                    case 3:
                                    case 4:
                                    case 5:
                                    case 6:
                                    case 7:
                                    case 8:
                                    case 9:
                                    case 10:
                                    case 11:
                                    case 12:
                                    case 13:
                                    case 15:
                                    case 17:
                                    case 18:
                                        sqlSB.Append(toStrValue(strArray[i], true));
                                        break;
                                    case 16:
                                    case 14:
                                    //date timestamp columns
                                        sqlSB.Append(toStrValue(parseDateTime(strArray[i]), true));
                                        break;
                                    case 19:
                                    //last column
                                        sqlSB.Append(toStrValue(strArray[i], false));
                                        break;
                                }
                            }
                        }
                        else if(strArray.Length == 22)
                        {
                            for(var i = 0; i < strArray.Length; i++)
                            {
                                switch(i){
                                    case 0:
                                    case 1:
                                    case 2:
                                    case 3:
                                    case 4:
                                    case 5:
                                    case 8:
                                    case 9:
                                    case 10:
                                    case 11:
                                    case 12:
                                    case 13:
                                    case 14:
                                    case 16:
                                    case 18:
                                    case 19:
                                        sqlSB.Append(toStrValue(strArray[i], true));
                                        break;
                                    case 6:
                                        sqlSB.Append(toStrValue(strArray[6]+strArray[7], true));
                                        break;
                                    case 17:
                                    case 15:
                                    //date timestamp columns
                                        sqlSB.Append(toStrValue(parseDateTime(strArray[i]), true));
                                        break;
                                    case 20:
                                    //last column
                                        sqlSB.Append(toStrValue(strArray[i], false));
                                        break;
                                }
                            }
                        }

                        if(lineCounter % bulkInsertAmount == 0)
                        {
                            sqlSB.AppendLine(insertClose);
                        }
                        else
                        {
                            sqlSB.AppendLine("),");
                        }
                    }

                    lineCounter++;
                }
            }
            //Console.WriteLine(sqlSB.ToString());
        } 
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        Console.WriteLine($"second last character : {sqlSB[sqlSB.Length -3]}");

        if(sqlSB[sqlSB.Length -3] == ','){
            sqlSB[sqlSB.Length -3] = ';';
        }

        // Create the file, or overwrite if the file exists.
        using (FileStream fs = File.Create(outputFilename, 1024))
        {
            
            byte[] info = new UTF8Encoding(true).GetBytes(sqlSB.ToString());
            // Add some information to the file.
            fs.Write(info, 0, info.Length);
        }
    }

    static string toStrValue(string str, bool addComma){
        string? result;
        if (string.IsNullOrEmpty(str))
        {
            result = "null" ;
        }
        else
        {
            result = $"'{str.Trim().Replace("'", "''")}'";
        }

        if(addComma)
        {
                result += ",";
        }
        return result;
    }

    static string parseDateTime(string str)
    {
        if(string.IsNullOrEmpty(str))
        {
            return str;
        }
        else
        {
            return $"{str.Substring(0, 8)} {str.Substring(9, 8)}";
        }
    }
}