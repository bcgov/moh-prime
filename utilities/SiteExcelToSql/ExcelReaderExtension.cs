using System;
using System.Collections.Generic;
using System.Linq;
using ExcelDataReader;
using Prime.Models;

namespace SiteExcelToSql
{
    public static class ExcelReaderExtension
    {
        public readonly static Guid CreatorUUID = new Guid("00000000-0000-0000-0000-000000000001");

        /// <summary>
        /// To prase a time into a TimeSpan at a given index of reader
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index"></param>
        /// <returns>a timespan object, returns null if parsing failed</returns>
        public static TimeSpan? ParseTimeSpan(this IExcelDataReader reader, int index)
        {
            try
            {
                var time = reader.GetValue(index);
                if (time == null)
                {
                    return null;
                }
                else if (time is DateTime)
                {
                    return ((DateTime)time).TimeOfDay;
                }
                else
                {
                    Console.WriteLine("Invalid time format at cell {0}{1}: \"{2}\"", IntToLetters(index), reader.Depth + 1, time);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing time on cell {0}{1}: {2}", IntToLetters(index), reader.Depth + 1, ex);
                return null;
            }
        }

        /// <summary>
        /// parse business day for a given weekday from the reader cursor
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static BusinessDay ParseBusinessHour(this IExcelDataReader reader, DayOfWeek day)
        {
            var dayBaseIndex = day == DayOfWeek.Sunday ? 7 : (int)day;

            var startTime = reader.ParseTimeSpan(dayBaseIndex * 3);
            var endTime = reader.ParseTimeSpan(dayBaseIndex * 3 + 1);
            var allDay = reader.GetValue(dayBaseIndex * 3 + 2);

            // all day hours
            if (allDay != null && allDay.ToString().Length != 0)
            {
                startTime = new TimeSpan(0, 0, 0);
                endTime = new TimeSpan(24, 0, 0);

                return new BusinessDay()
                {
                    Day = day,
                    StartTime = startTime.Value,
                    EndTime = endTime.Value
                };
            }

            // startTime and endTime are both not-null column in db, both must be valid to return
            if (startTime != null && endTime != null)
            {
                return new BusinessDay()
                {
                    Day = day,
                    StartTime = startTime.Value,
                    EndTime = endTime.Value
                };
            }
            return null;
        }

        /// <summary>
        /// Prase site info including business hours
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Site ParseSite(this IExcelDataReader reader)
        {
            // parse site
            var site = new Site()
            {
                CreatedUserId = CreatorUUID,
                CreatedTimeStamp = DateTimeOffset.UtcNow,
                UpdatedUserId = CreatorUUID,
                UpdatedTimeStamp = DateTimeOffset.UtcNow,
                PEC = reader.GetString((int)SiteInfoIndex.PEC),
                Completed = true, // not null column
                DoingBusinessAs = reader.GetString((int)SiteInfoIndex.SiteName),
                OrganizationId = int.Parse(reader.GetValue((int)SiteInfoIndex.OrgnizationId).ToString()), // not null column
                Status = SiteStatusType.UnderReview, // not null column
                BusinessHours = new List<BusinessDay>(),
                RemoteUsers = new List<RemoteUser>()
            };

            // parse business hours from Monday to Sunday
            foreach (var day in new DayOfWeek[] {DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday,
                DayOfWeek.Saturday, DayOfWeek.Sunday})
            {
                var hour = reader.ParseBusinessHour(day);
                if (hour != null)
                {
                    site.BusinessHours.Add(hour);
                }
            }

            return site;
        }

        public static RemoteUser ParseRemoteUser(this IExcelDataReader reader)
        {
            var user = new RemoteUser
            {
                FirstName = reader.GetString(3),
                LastName = reader.GetString(4)
            };

            return user;
        }

        public static string IntToLetters(int value)
        {
            string result = string.Empty;
            while (value >= 0)
            {
                result = (char)('A' + value % 26 ) + result;
                value /= 26;
                value--;
            }
            return result;
        }
    }
}
