# Convert site excel file to SQL

This is a utility console app for converting paper site excel file to SQL to load into PostgreSQL database

## Prerequisite

Compile the app using `dotnet build` in project folder

## Usage

### Windows

`SiteExcelToSql.exe input_excel_file.xlsx output_file.sql`

### Other platforms

`dotnet SiteExcelToSql.dll input_excel_file.xlsx output_file.sql`


## Note

- Some template value can be adjusted. See the included template.sql. After modifying template.sql file, re-run the app the generate the new SQL output.
- Binaries files are built and generated into `bin\Debug\netcoreapp3.1` folder under the project root.
- Output SQL file is generated into same working directory of the binaries by default.