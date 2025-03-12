using CodingTracker;
using CodingTracker.Helpers;


HelpersClass.CreateDataBase();

var userInterface = new UserInterface();

bool running = true;

while (running)
{
    userInterface.MainMenu();
}
