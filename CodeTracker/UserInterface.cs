using Spectre.Console;
using CodingTracker.Controllers;

namespace CodingTracker
{
    class UserInterface
    {

        private readonly TrackingController controller = new();

        public void MainMenu()
        {
            Console.Clear();

            AnsiConsole.Markup("[Yellow]Welcome to Coding Tracker![/]\n");

            var choices = Enum.GetValues(typeof(Enums.MenuChoices)).Cast<Enums.MenuChoices>().ToArray();
            var menu = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.MenuChoices>()
                    .Title("[Yellow]Main Menu[/]")
                    .HighlightStyle("yellow")
                    .PageSize(10)
                    .AddChoices(choices));

            switch (menu)
            {
               case Enums.MenuChoices.Tracking:
                    controller.TrackTime();
                    break;
                case Enums.MenuChoices.ViewLogs:
                    controller.ViewLogs();
                    break;
                case Enums.MenuChoices.DeleteLogs:
                    controller.DeleteLogs();
                    break;
                case Enums.MenuChoices.Exit:
                    Environment.Exit(0);
                    break;
            }

        }

    }
        
}
