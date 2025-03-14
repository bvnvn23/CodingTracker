using CodingTracker.Constants;
using CodingTracker.Helpers;
using CodingTracker.Models;
using Spectre.Console;
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
            connection.Execute(sql, new { Date = date, StartTime = startingTime, EndTime = endingTime, Duration = duration.TotalMinutes });

            AnsiConsole.MarkupLine("[green]Time tracked successfully![/]");
            AnsiConsole.MarkupLine($"[yellow]Duration: {duration.Hours} hours and {duration.Minutes} minutes[/]");
            AnsiConsole.MarkupLine($"[yellow]Press Enter to continue[/]");
            Console.ReadKey();
        }

        public void ViewLogs()
        {
            const string sql = "SELECT * FROM CodingTracker";
            using var connection = new SQLiteConnection(AppConstants.ConnectionStringConst);
            connection.Open();
            connection.Execute(sql);

            var tableData = connection.Query<CodingSessionClass>(sql).ToList();


            if (tableData.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No logs found.[/]");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadKey();
                return;
            }


            var table = new Table()
                .BorderColor(Color.Yellow)
                .AddColumn(new TableColumn("[yellow]Id[/]").Centered())
                .AddColumn(new TableColumn("[yellow]Date[/]").Centered())
                .AddColumn(new TableColumn("[yellow]Start Time[/]").Centered())
                .AddColumn(new TableColumn("[yellow]End Time[/]").Centered())
                .AddColumn(new TableColumn("[yellow]Duration[/]").Centered());

            foreach (var item in tableData)
            {
                table.AddRow(
                    new Markup($"[yellow]{item.Id}[/]"),
                    new Markup($"[yellow]{item.Date}[/]"),
                    new Markup($"[yellow]{item.StartTime}[/]"),
                    new Markup($"[yellow]{item.EndTime}[/]"),
                    new Markup($"[yellow]{item.Duration}[/]")
                );
            }

            AnsiConsole.Render(table);

            var continueButton = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[Yellow]Press Enter to continue[/]")
                    .PageSize(5)
                    .HighlightStyle("yellow")
                    .AddChoices(new[] { "Continue" })
            );

        }

        public void DeleteLogs()
        {
            const string sql = "SELECT * FROM CodingTracker";
            using var connection = new SQLiteConnection(AppConstants.ConnectionStringConst);
            connection.Open();
            connection.Execute(sql);

            var tableData = connection.Query<CodingSessionClass>(sql).ToList();

            var selectionPrompt = new SelectionPrompt<CodingSessionClass>()
                .Title("[Yellow]Select a log to delete:[/]")
                .PageSize(10)
                .HighlightStyle("yellow")
                .UseConverter(item => $"Id: {item.Id}, Date: {item.Date}, Start Time: {item.StartTime}, End Time: {item.EndTime}, Duration: {item.Duration}");

            if (tableData.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No logs found.[/]");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadKey();
                return;
            }

            selectionPrompt.AddChoices(tableData);
            var selectedLog = AnsiConsole.Prompt(selectionPrompt);

         
            var confirm = AnsiConsole.Confirm($"Are you sure you want to delete the log with Id: {selectedLog.Id}?");

            if (confirm)
            {
                const string deleteSql = "DELETE FROM CodingTracker WHERE Id = @Id";
                connection.Execute(deleteSql, new { Id = selectedLog.Id });
                AnsiConsole.MarkupLine("[green]Log deleted successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Deletion cancelled.[/]");
            }
        }
    }
}