using System;
using Dapper;
using CodingTracker.Constants;
using System.Data.SQLite;

namespace CodingTracker.Helpers
{
    static class HelpersClass
    {
        public static TimeSpan CalculateDuration(string startTime, string endTime)
        {
            if (DateTime.TryParseExact(startTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime start) &&
                DateTime.TryParseExact(endTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime end))
            {
                if (end < start)
                {
                    end = end.AddDays(1);
                }
                return end - start;
            }
            else
            {
                throw new ArgumentException("Invalid time format. Please use HH:mm format.");
            }
        }

        public static void CreateDataBase()
        {
            string connectionString = AppConstants.ConnectionStringConst;

            const string sql = @"
            CREATE TABLE IF NOT EXISTS CodingTracker (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Date TEXT,
                StartTime TEXT,
                EndTime TEXT,
                Duration INTEGER
            )";

            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            connection.Execute(sql);
        }
    }
}