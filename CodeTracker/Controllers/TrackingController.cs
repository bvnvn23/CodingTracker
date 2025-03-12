using CodingTracker.Constants;
using CodingTracker.Helpers;
using System.Data.SQLite;
using Dapper;

namespace CodingTracker.Controllers
{
    internal class TrackingController
    {
        public void TrackTime()
        {
            var date = GetUserInput.GetDateInput();
            var startingTime = GetUserInput.GetTimeInput("Enter starting time: (format HH:mm)");
            var endingTime = GetUserInput.GetTimeInput("Enter ending time: (format HH:mm)");    
            var duration = HelpersClass.CalculateDuration(startingTime, endingTime);
            
            string connectionString = AppConstants.ConnectionStringConst;

            const string sql = @"INSERT INTO CodingTracker (Date, StartTime, EndTime, Duration) VALUES (@Date, @StartTime, @EndTime, @Duration)";

            using var connection = new SQLiteConnection(connectionString);

            connection.Open();
            connection.Execute(sql, new { Date = date, StartTime = startingTime, EndTime = endingTime, Duration = duration.TotalMinutes});
        }
    }
}