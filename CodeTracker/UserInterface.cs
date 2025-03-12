using Spectre.Console;
using CodingTracker.Controllers;

namespace CodingTracker
{
    class UserInterface
    {

        private readonly TrackingController controller = new();

        public void MainMenu()
        {

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
                    ChooseHowToTrackTime();
                    break;
                case Enums.MenuChoices.ViewLogs:
                    //ViewLogs();
                    break;
                case Enums.MenuChoices.DeleteLogs:
                    //DeleteLogs();
                    break;
                case Enums.MenuChoices.Exit:
                    Environment.Exit(0);
                    break;
            }

        }

        public void ChooseHowToTrackTime()
        {
            var choices = Enum.GetValues(typeof(Enums.TrackTimeChoices)).Cast<Enums.TrackTimeChoices>().ToArray();
            var menu = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.TrackTimeChoices>()
                    .Title("[Yellow]Choose how to track data:[/]")
                    .HighlightStyle("yellow")
                    .PageSize(10)
                    .AddChoices(choices));



            switch (menu)
            {
                case Enums.TrackTimeChoices.Timer:
                    break;
                case Enums.TrackTimeChoices.EnterData:
                    controller.TrackTime();
                    break;
                case Enums.TrackTimeChoices.MainMenu:
                    MainMenu();
                    break;
            }
        }
    }
}
