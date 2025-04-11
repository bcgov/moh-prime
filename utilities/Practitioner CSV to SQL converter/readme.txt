Process to import Practitioner data into PRIME database
1) get CVS files from Nelson. It should be one per day.
2) copy the file to folder "CSVtoSQL"
3) open the program.cs and update the inputFilename to match the CVS file and change outputFilename accordingly.
4) start a terminal from the folder "CSVtoSQL" and run command "dotnet run"
5) repeat step 3 and 4 to generate the SQL insert scripts for each CSV file
6) finally, run the generated insert scripts to PRIME Production database  