using Spectre.Console;
using System.Globalization;

namespace CodingTracker
{
    static class GetUserInput
    {
        static public string GetDateInput()
        {
            var date = AnsiConsole.Ask<string>("Enter date: (format dd.MM.yyyy)");

            while (!DateTime.TryParseExact(date, "dd.MM.yyyy", new CultureInfo("eu-EU"), DateTimeStyles.None, out _))
            {
                AnsiConsole.MarkupLine("[red]Invalid date format. Please enter date in format dd.MM.yyyy[/]");
                date = AnsiConsole.Ask<string>("Enter date: ");
            }

            return date;
        }

        static public string GetTimeInput(string promptMessage)
        {
            var time = AnsiConsole.Ask<string>(promptMessage);

            while (!TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture, out _))
            {
                AnsiConsole.MarkupLine("[red]Invalid time format. Please enter time in format HH:mm[/]");
                time = AnsiConsole.Ask<string>(promptMessage);
            }
            return time;
        }
    }
}
