using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    // Game configuration data
    const string menuHint = "(Press ENTER to return to main menu)";
    string[] level1Passwords = { "yonit", "blue-white", "israel", "benny1234", "yoyonit" };
    string[] level2Passwords = { "linux", "alan-turing", "i-love-bridges", "bachelor", "technology" };
    string[] level3Passwords = { "schwifty", "pickle-rick", "morty-dorky", "meeseeks", "portal-gun" };

    // Game state
    int level, keys = 0;
    enum Screen { MainMenu, Password, Win }
    Screen currentScreen;
    string password;

	// Use this for initialization
	void Start () {
        ShowMainMenu(1);
    }

    /** 
     * value = 1 - Default main menu
     * value = 2 - Invalid level input
     * value = 3 - Easter egg
     **/
    void ShowMainMenu(int value) {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine(@"       
        +-+-+-+ +-+-+-+-+-+-+
        |T|h|e| |H|a|c|k|e|r|
        +-+-+-+ +-+-+-+-+-+-+
");
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("");
        Terminal.WriteLine("Press 1 for Benny Gantz's iPhone");
        Terminal.WriteLine("Press 2 for the Technion's Server");
        Terminal.WriteLine("Press 3 for Rick Sanchez's PC");
        Terminal.WriteLine("");
        switch (value)
        {
            case 1:
                Terminal.WriteLine("Enter your selection: ");
                break;
            case 2:
                Terminal.WriteLine("Please select a valid level: ");
                break;
            case 3:
                Terminal.WriteLine("Please select a level Mr. Bond! (;");
                break;
            default:
                Debug.LogError("Invalid main menu value");
                break;
        }
        
    }
	
    void OnUserInput(string input)
    {
        if (input == "")
        {
            ShowMainMenu(1);
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }

    }

    void RunMainMenu(string input)
    {
        switch (input)
        {
            case "":
                ShowMainMenu(1);
                break;
            case "1":
                level = 1;
                StartGame();
                break;
            case "2":
                level = 2;
                StartGame();
                break;
            case "3":
                level = 3;
                StartGame();
                break;
            case "007": // Easter egg!
                ShowMainMenu(3);
                break;
            default:
                ShowMainMenu(2);
                break;
        }
    }

    string HintGenerator()
    {
        string hint = password.Anagram();

        while (hint == password)
        {
            hint = password.Anagram();
        }

        return hint;
    }

    void CheckPassword(string input)
    {
        Terminal.ClearScreen();
       
        if (input == password)
        {
            keys += level;
            DisplayWinScreen();
        }
        else
        {
            Terminal.WriteLine("Sorry, wrong password!");
            if ((keys - level) > 0)
            {
                keys -= level;
                if(level == 1)
                {
                    Terminal.WriteLine("You lost a key.");
                }
                else
                {
                    Terminal.WriteLine("You lost " + level + " keys.");
                }
                
            }
            else
            {
                if(keys != 0)
                {
                    if(keys == 1)
                    {
                        Terminal.WriteLine("You lost a key.");
                    }
                    else
                    {
                        Terminal.WriteLine("You lost " + keys + " keys.");
                    }
                    keys = 0;
                }
            }
            if (keys == 1)
            {
                Terminal.WriteLine("You currently have 1 key.");
            }
            else
            {
                Terminal.WriteLine("You currently have " + keys + " keys.");
            }
            Terminal.WriteLine("(hint reminder: " + HintGenerator() + ")");
            Terminal.WriteLine(menuHint);
            Terminal.WriteLine("Try again: ");
        }
    }

    void StartGame()
    {
        Terminal.ClearScreen();
        currentScreen = Screen.Password;
        password = PasswordSelector();
        WelcomeGreetingByLevel();
        Terminal.WriteLine("Enter your password: ");
        Terminal.WriteLine("(hint: " + HintGenerator() + ")");
        Terminal.WriteLine(menuHint);
    }

    string PasswordSelector()
    {
        string pass = "";

        switch (level)
        {
            case 1:
                pass = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                pass = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                pass = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;

        }

        return pass;
    }

    void WelcomeGreetingByLevel()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Welcome to Benny Gants' iPhone!");
                break;
            case 2:
                Terminal.WriteLine("Welcome to Technion's Server!");
                break;
            case 3:
                Terminal.WriteLine("Welcome to Rick Sanchez's PC!");
                break;
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();

        if(level == 1)
        {
            Terminal.WriteLine("WELL DONE! You recieved a key.");
        }
        else
        {
            Terminal.WriteLine("WELL DONE! You recieved " + level + " keys.");
        }

        if (keys == 1)
        {
            Terminal.WriteLine("You have 1 key in total.");
        }
        else
        {
            Terminal.WriteLine("You have " + keys + " keys in total.");
        }
        
        switch (level)
        {
            case 1:
                Terminal.WriteLine(@"
 __
/o \______
\__/-=' ='
");
                break;
            case 2:
                Terminal.WriteLine(@"
 __          __
/o \______  /o \______
\__/-=' ='  \__/-=' ='
");
                break;
            case 3:
                Terminal.WriteLine(@"
 __          __          __
/o \______  /o \______  /o \______
\__/-=' ='  \__/-=' ='  \__/-=' ='
");
                break;
            default:
                Debug.LogError("Invalid level reached");
                break;
        }
        Terminal.WriteLine("");
        Terminal.WriteLine(menuHint);
    }

}
