using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Prime.Services
{
    public class ExportService : BaseService, IExportService
    {
        public ExportService(
            ApiDbContext context,
            ILogger<ExportService> logger)
            : base(context, logger)
        {
            // Set EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        /// <summary>
        /// Export remote users to CSV format
        /// </summary>
        public async Task<byte[]> ExportRemoteUsersToCSVAsync(int siteId)
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                // Write headers
                csvWriter.WriteHeader<RemoteUserExportDto>();
                await csvWriter.NextRecordAsync();

                // Write data
                var exportDtos = await GetRemoteUsers(siteId);
                await csvWriter.WriteRecordsAsync(exportDtos);
                await writer.FlushAsync();

                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Export remote users to Excel format
        /// </summary>
        public async Task<byte[]> ExportRemoteUsersToExcelAsync(int siteId)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Remote Users");

                // Set headers
                var headers = new[] { "First Name", "Last Name", "Email", "College", "License Class", "License Number", "Practitioner Id" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

                // Format header row
                var headerRow = worksheet.Cells[1, 1, 1, headers.Length];
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerRow.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                // Write data
                var exportDtos = await GetRemoteUsers(siteId);
                for (int i = 0; i < exportDtos.Count(); i++)
                {
                    var dto = exportDtos.ElementAt(i);
                    worksheet.Cells[i + 2, 1].Value = dto.FirstName;
                    worksheet.Cells[i + 2, 2].Value = dto.LastName;
                    worksheet.Cells[i + 2, 3].Value = dto.Email;
                    worksheet.Cells[i + 2, 4].Value = dto.College;
                    worksheet.Cells[i + 2, 5].Value = dto.LicenseClass;
                    worksheet.Cells[i + 2, 6].Value = dto.LicenseNumber;
                    worksheet.Cells[i + 2, 7].Value = dto.PractitionerId;
                }

                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();

                return await Task.FromResult(package.GetAsByteArray());
            }
        }

        private async Task<IEnumerable<RemoteUserExportDto>> GetRemoteUsers(int siteId)
        {
            return await _context.RemoteUsers
                .Where(ru => ru.SiteId == siteId)
                .Select(ru => new RemoteUserExportDto
                {
                    FirstName = ru.FirstName,
                    LastName = ru.LastName,
                    Email = ru.Email,
                    College = ru.RemoteUserCertification.College.Name,
                    LicenseClass = ru.RemoteUserCertification.License.Name,
                    LicenseNumber = ru.RemoteUserCertification.LicenseNumber,
                    PractitionerId = ru.RemoteUserCertification.PractitionerId
                })
                .ToListAsync();
        }

        /// <summary>
        /// DTO for exporting remote user data
        /// </summary>
        public class RemoteUserExportDto
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string College { get; set; }
            public string LicenseClass { get; set; }
            public string LicenseNumber { get; set; }
            public string PractitionerId { get; set; }
        }
    }
}
