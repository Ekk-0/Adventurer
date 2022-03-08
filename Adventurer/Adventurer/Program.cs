using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

// --------------------------------------------------------------------------------------------------------------
class Start
{
    public static string username = "";
    public static Dictionary<string, string> data = new Dictionary<string, string>();
    public static void ReadIt()
    {
        string filepath = @"/Users/eavosloo/Projects/Adventurer/Adventurer/login.txt"; // enter you login.txt file location
        StreamReader sr = new StreamReader(filepath);
        {
            while (!sr.EndOfStream)
            {
                string splitMe = sr.ReadLine();
                string[] bananaSplits = splitMe.Split(new char[] { ':' });

                if (bananaSplits.Length < 2)
                    continue;
                else if (bananaSplits.Length == 2)
                    data.Add(bananaSplits[0].Trim(), bananaSplits[1].Trim());
                else if (bananaSplits.Length > 2)
                    SplitItGood(splitMe, data);
            }
        }
    }

    public static void SplitItGood(string stringInput, Dictionary<string, string> dictInput)
    {
        StringBuilder sb = new StringBuilder();
        List<string> fish = new List<string>();
        bool hasFirstValue = false;

        foreach (char c in stringInput)
        {
            if (c != ':') 
                sb.Append(c);
            else if (c == ':' && !hasFirstValue)
            {
                fish.Add(sb.ToString().Trim());
                sb.Clear();
                hasFirstValue = true;
            }
            else if (c == ':' && hasFirstValue)
            {
                string[] bananaSplit = sb.ToString().Trim().Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                fish.Add(bananaSplit[0].Trim());
                fish.Add(bananaSplit[1].Trim());
                sb.Clear();
            }
        }

        fish.Add(sb.ToString().Trim());

        for (int i = 0; i < fish.Count; i += 2)
        {
            dictInput.Add(fish[i], fish[i + 1]);
        }
    }

    public static void Main()
    {
        ReadIt();
        int key = 1;
        while (key != 0)
        {
            try
            { 
                Console.WriteLine("-------------------------------");
                Console.WriteLine("---  Welcome To Adventurer ---");
                Console.WriteLine("-------------------------------");
                Console.Write("Loading ");
                for ( int i = 0; i < 3; i++)
                {
                    Console.Write(".");
                    System.Threading.Thread.Sleep(500);
                }
                Console.Clear();
                Console.WriteLine("-------------------------------");
                Console.WriteLine("---  Welcome To Adventurer ---");
                Console.WriteLine("-------------------------------");
                Console.WriteLine();
                Console.WriteLine("--- Choose a option: --");
                Console.WriteLine("--     1. Login      --");
                Console.WriteLine("--     2. Register   --");
                Console.WriteLine("-----------------------");
                key = int.Parse(Console.ReadLine());
                if (key == 1)
                {
                    Console.WriteLine("Enter username: ");
                    username = Console.ReadLine();
                    Console.WriteLine("Enter password: ");
                    string password = Console.ReadLine();
               
                    if (Login(username, password) == 1)
                    {
                        Menu.Game(true);
                    }
                }
                else if (key == 2)
                {
                    Register();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Enter the Correct Option!");
            }
        }
        
    }

    public static int Login(string username, string password)
    {
        try
        {
            bool usernameExists = data.ContainsKey(username);
            if (usernameExists)
            {
                if (password == data[username])
                {
                    return 1;
                }
                else
                {
                    Console.WriteLine("Password Incorrect!");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    throw new Exception();
                }
            }
            else
            {
                Console.WriteLine("Username does not Exist!");
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                throw new Exception();
            }

        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static void Register()
    {

        try
        {
            Console.WriteLine("Enter a username: ");
            string username = Console.ReadLine();
            bool usernameExists = data.ContainsKey(username);
            if (usernameExists)
            {
                throw new Exception();
            }
            else
            {
                Console.WriteLine("Enter a password: ");
                string password = Console.ReadLine();
                Console.Clear();
                data.Add(username, password);
                File.WriteAllLines(@"/Users/eavosloo/Projects/Adventurer/Adventurer/login.txt", data.Select(x => x.Key + ": " + x.Value)); // enter you login.txt file location
                File.Create(@"/Users/eavosloo/Projects/Adventurer/Adventurer/Saves/" + username + ".txt").Close();  // enter your saves folder file location

            }
        }
        catch (Exception)
        {
            Console.WriteLine("Username already exists!");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();

        }
    }
}
// --------------------------------------------------------------------------------------------------------------
class Menu
{
    public static void Init()
    {
        int counter = 0;
        foreach (string line in File.ReadLines(@"/Users/eavosloo/Projects/Adventurer/Adventurer/Saves/" + Start.username + ".txt")) // enter your saves folder file location
        {
            switch (counter)
            {
                case 0:
                    Profile.nm = line;
                    break;
                case 1:
                    Profile.ag = int.Parse(line);
                    break;
                case 2:
                    Profile.he = double.Parse(line);
                    break;
                case 3:
                    Profile.we = int.Parse(line);
                    break;
                case 4:
                    Stats.money = int.Parse(line);
                    break;
                case 5:
                    Stats.health = int.Parse(line);
                    break;
                case 6:
                    Weapons.spear = int.Parse(line);
                    break;
                case 7:
                    Weapons.sword = int.Parse(line);
                    break;
                case 8:
                    Weapons.axe = int.Parse(line);
                    break;
                case 9:
                    Weapons.shield = int.Parse(line);
                    break;
                case 10:
                    Camp.headgear = int.Parse(line);
                    break;
                case 11:
                    Camp.chestplate = int.Parse(line);
                    break;
                case 12:
                    Camp.leggings = int.Parse(line);
                    break;
                case 13:
                    Camp.boots = int.Parse(line);
                    break;
            }
            counter++;
        }

    }
    public static void Game(bool running)
    {
        Init();
        while (running == true)
        {
            Console.Clear();
            Console.WriteLine("        Welcome " + Start.username + "!");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("--------      Menu      -------");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("--------   1: Profile   -------");
            Console.WriteLine("--------   2: Weapons   -------");
            Console.WriteLine("--------   3: Missions  -------");
            Console.WriteLine("--------   4: Camp      -------");
            Console.WriteLine("--------   5: Stats     -------");
            Console.WriteLine("--------   6: Save      -------");
            Console.WriteLine("--------   7: Quit      -------");
            Console.WriteLine("-------------------------------");
            Console.Write("Please enter a option: ");
            try
            {
                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Profile.Main1();
                        break;
                    case 2:
                        if (Profile.nm != "")
                        {
                            Weapons.Main2();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Create a Profile First!");
                            break;
                        }

                    case 3:
                        if (Profile.nm != "")
                        {
                            Missions.Main3(true);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Create a Profile First!");
                            break;
                        }
                    case 4:
                        if (Profile.nm != "")
                        {
                            Camp.Main4(true);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Create a Profile First!");
                            break;
                        }
                    case 5:
                        if (Profile.nm != "")
                        {
                            Stats.Main5(true);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Create a Profile First!");
                            break;
                        }
                    case 6:
                        if (Profile.nm != "")
                        {
                            Save.Main6();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Create a Profile First!");
                            break;
                        }
                    case 7:
                        Console.WriteLine("Thanks For Playing!");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        running = false;
                        break;
                    default:
                        throw new Exception();
                            
                }
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Choose a valid Option!");
            }
            catch (Exception)
            {
                Console.WriteLine("Choose a Option from 1 - 6!");
            }
        }
    }
}
// --------------------------------------------------------------------------------------------------------------
class Profile
{

    public static int characterlives = 0;
    public static string nm = "";
    public static int ag = 0;
    public static double he = 0;
    public static int we = 0;
    public static void Main1()
    {
        if (nm == "")
        {
            while (characterlives == 0)
            {
                Console.Clear();
                Console.WriteLine("------ Welcome To Your Profile -----");
                try
                {
                    Console.WriteLine("Enter Your Characters Name: ");
                    string Entry = Console.ReadLine();
                    nm = name(Entry);
                    Console.WriteLine("Welcome " + nm + " To Aventurer!");

                    Console.WriteLine("Enter Your Characters Age: ");
                    int Entry1 = int.Parse(Console.ReadLine());
                    ag = age(Entry1);
                    Console.WriteLine("Your characters Age is: " + ag);

                    Console.WriteLine("Enter Your Characters Height: ");
                    double Entry2 = double.Parse(Console.ReadLine());
                    he = height(Entry2);
                    Console.WriteLine("Your characters Height is: " + he);

                    Console.WriteLine("Enter Your Characters Weight: ");
                    int Entry3 = int.Parse(Console.ReadLine());
                    we = weight(Entry3);
                    Console.WriteLine("Your characters Weight is: " + we);
                    characterlives = 1;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Please Enter the Data Correctly!");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }

            }
        }
        else
        {
            try
            {
                Console.Clear();
                Console.WriteLine("------ Welcome To Your Profile -----");
                Console.WriteLine("Name: " + nm);
                Console.WriteLine("Age: " + ag);
                Console.WriteLine("Height: " + he);
                Console.WriteLine("Weight: " + we);
                Console.WriteLine("Enter 1 To Go Back");
                while (int.Parse(Console.ReadLine()) != 1)
                {
                    Console.Clear();
                    Console.WriteLine("------ Welcome To Your Profile -----");
                    Console.WriteLine("Name: " + nm);
                    Console.WriteLine("Age: " + ag);
                    Console.WriteLine("Height: " + he);
                    Console.WriteLine("Weight: " + we);
                    Console.WriteLine("Enter 1 To Go Back");
                }
            }
            catch(System.FormatException)
            {
                Console.WriteLine("Please Enter 1!");
            }
        }
    }

    static string name(string entry)
    {
        string name = entry;
        return name;
    }

    static int age(int entry)
    {
        int age = entry;
        return age;
    }

    static double height(double entry)
    {
        double height = entry;
        return height;
    }

    static int weight(int entry)
    {
        int weight = entry;
        return weight;
    }
}
// --------------------------------------------------------------------------------------------------------------
class Weapons
{
    public static int spear = 0, sword = 0, axe = 0, shield = 0;
    public static void Main2()
    {
        while(true)
        {
            Console.Clear();
            Console.WriteLine("------ Welcome To Your Armory -----");
            Console.WriteLine("1: Inventory");
            Console.WriteLine("2: Shop");
            Console.WriteLine("3: Back");
            try
            {
                int choice = int.Parse(Console.ReadLine());



                if (choice == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome To your Inventory!");
                    Console.WriteLine("1: Spear");
                    Console.WriteLine("2: Axe");
                    Console.WriteLine("3: Sword");
                    Console.WriteLine("4: Shield");
                    Console.WriteLine("5: Back");
                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            Console.WriteLine("Spear Level: " + spear);
                            System.Threading.Thread.Sleep(1000);                   
                            break;
                        case 2:
                            Console.WriteLine("Axe Level: " + axe);
                            System.Threading.Thread.Sleep(1000);                            
                            break;
                        case 3:
                            Console.WriteLine("Sword Level: " + sword);
                            System.Threading.Thread.Sleep(1000);                            
                            break;
                        case 4:
                            Console.WriteLine("Shield Level: " + shield);
                            System.Threading.Thread.Sleep(1000);
                            break;
                        default:
                            break;
                    }


                }
                else if (choice == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome To your Shop!");
                    Console.WriteLine("1: Upgrade Spear");
                    Console.WriteLine("2: Upgrade Axe");
                    Console.WriteLine("3: Upgrade Sword");
                    Console.WriteLine("4: Upgrade Shield");
                    Console.WriteLine("5: Back");
                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            Spear();
                            break;
                        case 2:
                            Axe();
                            break;
                        case 3:
                            Sword();
                            break;
                        case 4:
                            Shield();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    break;
                }
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Please Enter Correctly");
            }
        }
    }

    static void Spear()
    {
        Console.WriteLine("Upgrade costs 20$ [Y/N]");
        string Opt = Console.ReadLine();
        if (Stats.money >= 20 && (Opt == "Y" || Opt == "y"))
        {
            spear += 1;
        }
        else if (Opt == "N" || Opt == "n")
        {
            spear += 0;
        }
        else
        {
            Console.WriteLine("Insufficient funds");
            System.Threading.Thread.Sleep(1000);
        }

    }

    static void Axe()
    {
        Console.WriteLine("Upgrade costs 30$ [Y/N]");
        string Opt = Console.ReadLine();
        if (Stats.money >= 30 && (Opt == "Y" || Opt == "y"))
        {
            axe += 1;
        }
        else if (Opt == "N" || Opt == "n")
        {
            axe += 0;
        }
        else
        {
            Console.WriteLine("Insufficient funds");
            System.Threading.Thread.Sleep(1000);
        }
    }

    static void Sword()
    {
        Console.WriteLine("Upgrade costs 40$ [Y/N]");
        string Opt = Console.ReadLine();
        if (Stats.money >= 40 && (Opt == "Y" || Opt == "y"))
        {
            sword += 1;
        }
        else if (Opt == "N" || Opt == "n")
        {
            sword += 0;
        }
        else
        {
            Console.WriteLine("Insufficient funds");
            System.Threading.Thread.Sleep(1000);
        }
    }

    static void Shield()
    {
        Console.WriteLine("Upgrade costs 40$ [Y/N]");
        string Opt = Console.ReadLine();
        if (Stats.money >= 40 && (Opt == "Y" || Opt == "y"))
        {
            shield += 1;
        }
        else if (Opt == "N" || Opt == "n")
        {
            shield += 0;
        }
        else
        {
            Console.WriteLine("Insufficient funds");
            System.Threading.Thread.Sleep(1000);
        }
    }
}
// --------------------------------------------------------------------------------------------------------------
class Missions
{
    static int Points = 0;
    static string log =  "";
    public static void Main3(bool x)
    {
        while (x == true)
        {
            Console.Clear();
            Console.WriteLine("Choose Your Mission!");
            Console.WriteLine("1: Doom Island");
            Console.WriteLine("2: Spawn Island");
            Console.WriteLine("3: Ruby Island");
            Console.WriteLine("4: Log");
            Console.WriteLine("5: Back");
            try
            {
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine(Mission_1());
                        System.Threading.Thread.Sleep(2000);
                        break;
                    case 2:
                        Console.WriteLine(Mission_2());
                        System.Threading.Thread.Sleep(2000);
                        break;
                    case 3:
                        Console.WriteLine(Mission_3());
                        System.Threading.Thread.Sleep(2000);
                        break;
                    case 4:
                        Log();
                        Console.Write("Going back in: ");
                        for (int i = 5; i > 0; i--)
                        {
                            Console.Write(i);
                            System.Threading.Thread.Sleep(1000);
                            Console.Write("\b");
                            
                        }
                        break;
                    default:
                        x = false;
                        break;
                }
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Please Enter Correctly!");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }

    static void Log()
    {
        StreamReader sr = new StreamReader(@"/Users/eavosloo/Projects/Adventurer/Adventurer/log.txt"); // enter your log.txt file location
        Console.WriteLine(sr.ReadToEnd());
    }

    static string Mission_1()
    {
        try
        {
            Console.Clear();
            Points = 0;
            Console.WriteLine("Welcome To Doom Island!");
            log = "Doom Island";
            Console.WriteLine("Press Enter To Start");
            string enter = Console.ReadLine();
            if (enter == "")
            {
                Console.WriteLine("Press a button from a-z to get kills");
                for (int i = 0; i < 10; i++)
                {
                    if (Stats.health <= 0)
                    {
                        Console.WriteLine("You Died!");
                        System.Threading.Thread.Sleep(1000);
                        break;
                    }
                    else
                    {
                        string a = Console.ReadLine();
                        switch (a)
                        {
                            case "q":
                                Console.WriteLine("You Killed a Dragon!");
                                Points += 50;
                                Stats.money += 20 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 50 + Stats.armor + Weapons.shield;
                                break;
                            case "w":
                                Console.WriteLine("You Killed a Dog!");
                                Points += 2;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 2 + Stats.armor + Weapons.shield;
                                break;
                            case "e":
                                Console.WriteLine("You Killed a Skeloton!");
                                Points += 15;
                                Stats.money += 15 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 5 + Stats.armor + Weapons.shield;
                                break;
                            case "r":
                                Console.WriteLine("You Killed a Bird!");
                                Points += 3;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "t":
                                Console.WriteLine("You Killed a Horse!");
                                Points += 6;
                                Stats.money += 4 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 2 + Stats.armor + Weapons.shield;
                                break;
                            case "y":
                                Console.WriteLine("You Killed a Person!");
                                Points += 10;
                                Stats.money += 10 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 7 + Stats.armor + Weapons.shield;
                                break;
                            case "u":
                                Console.WriteLine("You Killed a Zombie!");
                                Points += 15;
                                Stats.money += 12 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 9 + Stats.armor + Weapons.shield;
                                break;
                            case "i":
                                Console.WriteLine("You Killed a Elephant!");
                                Points += 20;
                                Stats.money += 3 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 15 + Stats.armor + Weapons.shield;
                                break;
                            case "o":
                                Console.WriteLine("You Killed a Monkey!");
                                Points += 8;
                                Stats.money += 3 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 11 + Stats.armor + Weapons.shield;
                                break;
                            case "p":
                                Console.WriteLine("You Killed a Cat!");
                                Points += 1;
                                Stats.money += 4 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 2 + Stats.armor + Weapons.shield;
                                break;
                            case "a":
                                Console.WriteLine("You Killed a Snake!");
                                Points += 12;
                                Stats.money += 6 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 3 + Stats.armor + Weapons.shield;
                                break;
                            case "s":
                                Console.WriteLine("You Killed a Spider!");
                                Points += 10;
                                Stats.money += 8 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 4 + Stats.armor + Weapons.shield;
                                break;
                            case "d":
                                Console.WriteLine("You Killed a Fish!");
                                Points += 5;
                                Stats.money += 3 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "f":
                                Console.WriteLine("You Killed a Eagel!");
                                Points += 20;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "g":
                                Console.WriteLine("You Killed a Mouse!");
                                Points += 2;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "h":
                                Console.WriteLine("You Killed a Rat!");
                                Points += 3;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "j":
                                Console.WriteLine("You Killed a Eel!");
                                Points += 5;
                                Stats.money += 3 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 2 + Stats.armor + Weapons.shield;
                                break;
                            case "k":
                                Console.WriteLine("You Killed a Giant!");
                                Points += 50;
                                Stats.money += 35 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 30 + Stats.armor + Weapons.shield;
                                break;
                            case "l":
                                Console.WriteLine("You Killed a Mammoth!");
                                Points += 20;
                                Stats.money += 20 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 20 + Stats.armor + Weapons.shield;
                                break;
                            case "z":
                                Console.WriteLine("You Killed a Fly!");
                                Points += 1;
                                Stats.money += 1 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 0 + Stats.armor + Weapons.shield;
                                break;
                            case "x":
                                Console.WriteLine("You Killed a Unicorn!");
                                Points += 50;
                                Stats.money += 12 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 9 + Stats.armor + Weapons.shield;
                                break;
                            case "c":
                                Console.WriteLine("You Killed a Werewolf!");
                                Points += 38;
                                Stats.money += 20 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 25 + Stats.armor + Weapons.shield;
                                break;
                            case "v":
                                Console.WriteLine("You Killed a Fairy!");
                                Points += 46;
                                Stats.money += 5 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 5 + Stats.armor + Weapons.shield;
                                break;
                            case "b":
                                Console.WriteLine("You Killed a Mermaid!");
                                Points += 25;
                                Stats.money += 4 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "n":
                                Console.WriteLine("You Killed a Griffin!");
                                Points += 100;
                                Stats.money += 40 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 35 + Stats.armor + Weapons.shield;
                                break;
                            case "m":
                                Console.WriteLine("You Killed a Yeti!");
                                Points += 60;
                                Stats.money += 35 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 25 + Stats.armor + Weapons.shield;
                                break;
                        }
                    }

                }
            }
        }
        catch (System.FormatException)
        {
            Console.WriteLine("Please Enter Correctly");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
        }
        StreamWriter sw = File.AppendText(@"/Users/eavosloo/Projects/Adventurer/Adventurer/log.txt"); // enter your log.txt file location
        sw.WriteLine("You got " + Points + " Last Time on " + log);
        sw.Close();

        return "You got a total of " + Points + " Points";
    }

    static string Mission_2()
    {
        try
        {
            Console.Clear();
            Points = 0;
            Console.WriteLine("Welcome To Spawn Island!");
            log = "Spawn Island";
            Console.WriteLine("Press Enter To Start");
            string enter = Console.ReadLine();
            if (enter == "")
            {
                Console.WriteLine("Press a button from a-z to get kills");
                for (int i = 0; i < 10; i++)
                {
                    if (Stats.health <= 0)
                    {
                        Console.WriteLine("You Died!");
                        System.Threading.Thread.Sleep(1000);
                        break;
                    }
                    else
                    {
                        string a = Console.ReadLine();
                        switch (a)
                        {
                            case "q":
                                Console.WriteLine("You Killed a Dragon!");
                                Points += 50;
                                Stats.money += 20 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 50 + Stats.armor + Weapons.shield;
                                break;
                            case "w":
                                Console.WriteLine("You Killed a Dog!");
                                Points += 2;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 2 + Stats.armor + Weapons.shield;
                                break;
                            case "e":
                                Console.WriteLine("You Killed a Skeloton!");
                                Points += 15;
                                Stats.money += 15 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 5 + Stats.armor + Weapons.shield;
                                break;
                            case "r":
                                Console.WriteLine("You Killed a Bird!");
                                Points += 3;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "t":
                                Console.WriteLine("You Killed a Horse!");
                                Points += 6;
                                Stats.money += 4 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 2 + Stats.armor + Weapons.shield;
                                break;
                            case "y":
                                Console.WriteLine("You Killed a Person!");
                                Points += 10;
                                Stats.money += 10 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 7 + Stats.armor + Weapons.shield;
                                break;
                            case "u":
                                Console.WriteLine("You Killed a Zombie!");
                                Points += 15;
                                Stats.money += 12 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 9 + Stats.armor + Weapons.shield;
                                break;
                            case "i":
                                Console.WriteLine("You Killed a Elephant!");
                                Points += 20;
                                Stats.money += 3 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 15 + Stats.armor + Weapons.shield;
                                break;
                            case "o":
                                Console.WriteLine("You Killed a Monkey!");
                                Points += 8;
                                Stats.money += 3 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 11 + Stats.armor + Weapons.shield;
                                break;
                            case "p":
                                Console.WriteLine("You Killed a Cat!");
                                Points += 1;
                                Stats.money += 4 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 2 + Stats.armor + Weapons.shield;
                                break;
                            case "a":
                                Console.WriteLine("You Killed a Snake!");
                                Points += 12;
                                Stats.money += 6 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 3 + Stats.armor + Weapons.shield;
                                break;
                            case "s":
                                Console.WriteLine("You Killed a Spider!");
                                Points += 10;
                                Stats.money += 8 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 4 + Stats.armor + Weapons.shield;
                                break;
                            case "d":
                                Console.WriteLine("You Killed a Fish!");
                                Points += 5;
                                Stats.money += 3 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "f":
                                Console.WriteLine("You Killed a Eagel!");
                                Points += 20;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "g":
                                Console.WriteLine("You Killed a Mouse!");
                                Points += 2;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "h":
                                Console.WriteLine("You Killed a Rat!");
                                Points += 3;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "j":
                                Console.WriteLine("You Killed a Eel!");
                                Points += 5;
                                Stats.money += 3 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 2 + Stats.armor + Weapons.shield;
                                break;
                            case "k":
                                Console.WriteLine("You Killed a Giant!");
                                Points += 50;
                                Stats.money += 35 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 30 + Stats.armor + Weapons.shield;
                                break;
                            case "l":
                                Console.WriteLine("You Killed a Mammoth!");
                                Points += 20;
                                Stats.money += 20 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 20 + Stats.armor + Weapons.shield;
                                break;
                            case "z":
                                Console.WriteLine("You Killed a Fly!");
                                Points += 1;
                                Stats.money += 1 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 0 + Stats.armor + Weapons.shield;
                                break;
                            case "x":
                                Console.WriteLine("You Killed a Unicorn!");
                                Points += 50;
                                Stats.money += 12 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 9 + Stats.armor + Weapons.shield;
                                break;
                            case "c":
                                Console.WriteLine("You Killed a Werewolf!");
                                Points += 38;
                                Stats.money += 20 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 25 + Stats.armor + Weapons.shield;
                                break;
                            case "v":
                                Console.WriteLine("You Killed a Fairy!");
                                Points += 46;
                                Stats.money += 5 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 5 + Stats.armor + Weapons.shield;
                                break;
                            case "b":
                                Console.WriteLine("You Killed a Mermaid!");
                                Points += 25;
                                Stats.money += 4 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "n":
                                Console.WriteLine("You Killed a Griffin!");
                                Points += 100;
                                Stats.money += 40 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 35 + Stats.armor + Weapons.shield;
                                break;
                            case "m":
                                Console.WriteLine("You Killed a Yeti!");
                                Points += 60;
                                Stats.money += 35 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 25 + Stats.armor + Weapons.shield;
                                break;
                        }
                    }

                }
            }
        }
        catch (System.FormatException)
        {
            Console.WriteLine("Please Enter Correctly");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
        }
        StreamWriter sw = File.AppendText(@"/Users/eavosloo/Projects/Adventurer/Adventurer/log.txt"); // enter your log.txt file location
        sw.WriteLine("You got " + Points + " Last Time on " + log);
        sw.Close();

        return "You got a total of " + Points + " Points";
    }

    static string Mission_3()
    {
        try
        { 
            Console.Clear();
            Points = 0;
            Console.WriteLine("Welcome To Pony Island!");
            log = "Pony Island";
            Console.WriteLine("Press Enter To Start");
            string enter = Console.ReadLine();
            if (enter == "")
            {
                Console.WriteLine("Press a button from a-z to get kills");
                for (int i = 0; i < 10; i++)
                {
                    if (Stats.health <= 0)
                    {
                        Console.WriteLine("You Died!");
                        System.Threading.Thread.Sleep(1000);
                        break;
                    }
                    else
                    {
                        string a = Console.ReadLine();
                        switch (a)
                        {
                            case "q":
                                Console.WriteLine("You Killed a Dragon!");
                                Points += 50;
                                Stats.money += 20 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 50 + Stats.armor + Weapons.shield;
                                break;
                            case "w":
                                Console.WriteLine("You Killed a Dog!");
                                Points += 2;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 2 + Stats.armor + Weapons.shield;
                                break;
                            case "e":
                                Console.WriteLine("You Killed a Skeloton!");
                                Points += 15;
                                Stats.money += 15 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 5 + Stats.armor + Weapons.shield;
                                break;
                            case "r":
                                Console.WriteLine("You Killed a Bird!");
                                Points += 3;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "t":
                                Console.WriteLine("You Killed a Horse!");
                                Points += 6;
                                Stats.money += 4 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 2 + Stats.armor + Weapons.shield;
                                break;
                            case "y":
                                Console.WriteLine("You Killed a Person!");
                                Points += 10;
                                Stats.money += 10 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 7 + Stats.armor + Weapons.shield;
                                break;
                            case "u":
                                Console.WriteLine("You Killed a Zombie!");
                                Points += 15;
                                Stats.money += 12 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 9 + Stats.armor + Weapons.shield;
                                break;
                            case "i":
                                Console.WriteLine("You Killed a Elephant!");
                                Points += 20;
                                Stats.money += 3 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 15 + Stats.armor + Weapons.shield;
                                break;
                            case "o":
                                Console.WriteLine("You Killed a Monkey!");
                                Points += 8;
                                Stats.money += 3 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 11 + Stats.armor + Weapons.shield;
                                break;
                            case "p":
                                Console.WriteLine("You Killed a Cat!");
                                Points += 1;
                                Stats.money += 4 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 2 + Stats.armor + Weapons.shield;
                                break;
                            case "a":
                                Console.WriteLine("You Killed a Snake!");
                                Points += 12;
                                Stats.money += 6 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 3 + Stats.armor + Weapons.shield;
                                break;
                            case "s":
                                Console.WriteLine("You Killed a Spider!");
                                Points += 10;
                                Stats.money += 8 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 4 + Stats.armor + Weapons.shield;
                                break;
                            case "d":
                                Console.WriteLine("You Killed a Fish!");
                                Points += 5;
                                Stats.money += 3 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "f":
                                Console.WriteLine("You Killed a Eagel!");
                                Points += 20;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "g":
                                Console.WriteLine("You Killed a Mouse!");
                                Points += 2;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "h":
                                Console.WriteLine("You Killed a Rat!");
                                Points += 3;
                                Stats.money += 2 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "j":
                                Console.WriteLine("You Killed a Eel!");
                                Points += 5;
                                Stats.money += 3 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 2 + Stats.armor + Weapons.shield;
                                break;
                            case "k":
                                Console.WriteLine("You Killed a Giant!");
                                Points += 50;
                                Stats.money += 35 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 30 + Stats.armor + Weapons.shield;
                                break;
                            case "l":
                                Console.WriteLine("You Killed a Mammoth!");
                                Points += 20;
                                Stats.money += 20 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 20 + Stats.armor + Weapons.shield;
                                break;
                            case "z":
                                Console.WriteLine("You Killed a Fly!");
                                Points += 1;
                                Stats.money += 1 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 0 + Stats.armor + Weapons.shield;
                                break;
                            case "x":
                                Console.WriteLine("You Killed a Unicorn!");
                                Points += 50;
                                Stats.money += 12 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 9 + Stats.armor + Weapons.shield;
                                break;
                            case "c":
                                Console.WriteLine("You Killed a Werewolf!");
                                Points += 38;
                                Stats.money += 20 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 25 + Stats.armor + Weapons.shield;
                                break;
                            case "v":
                                Console.WriteLine("You Killed a Fairy!");
                                Points += 46;
                                Stats.money += 5 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 5 + Stats.armor + Weapons.shield;
                                break;
                            case "b":
                                Console.WriteLine("You Killed a Mermaid!");
                                Points += 25;
                                Stats.money += 4 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 1 + Stats.armor + Weapons.shield;
                                break;
                            case "n":
                                Console.WriteLine("You Killed a Griffin!");
                                Points += 100;
                                Stats.money += 40 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 35 + Stats.armor + Weapons.shield;
                                break;
                            case "m":
                                Console.WriteLine("You Killed a Yeti!");
                                Points += 60;
                                Stats.money += 35 + Weapons.axe + Weapons.spear + Weapons.sword;
                                Stats.health -= 25 + Stats.armor + Weapons.shield;
                                break;
                        }
                    }

                }
            }
        }
        catch (System.FormatException)
        {
            Console.WriteLine("Please Enter Correctly");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
        }
        StreamWriter sw = File.AppendText(@"/Users/eavosloo/Projects/Adventurer/Adventurer/log.txt"); // enter your log.txt file location
        sw.WriteLine("You got " + Points + " Last Time on " + log);
        sw.Close();

        return "You got a total of " + Points + " Points";
    }
}
// --------------------------------------------------------------------------------------------------------------
class Camp
{
    public static int headgear, chestplate, leggings, boots;
    public static void Main4(bool x)
    {
        while (x == true)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Welcome To your camp!");
                Console.WriteLine("1: Tent");
                Console.WriteLine("2: Fireplace");
                Console.WriteLine("3: Back");
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        Tent();
                        break;
                    case 2:
                        fireplace();
                        break;
                    default:
                        x = false;
                        break;
                }
                Stats.armor = (headgear + chestplate + leggings + boots);
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Enter the Correct Option!");
                System.Threading.Thread.Sleep(2000);
            }
        }
    }

    static void Tent()
    {

        while(true)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("------ Welcome to your tent! -----");
                Console.WriteLine("1: Armor Inventory");
                Console.WriteLine("2: Upgrade Armor");
                Console.WriteLine("3: Back");
                int option = int.Parse(Console.ReadLine());
                if (option == 1)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Your headgear is level: " + headgear);
                        Console.WriteLine("Your chestplate is level: " + chestplate);
                        Console.WriteLine("Your leggings is level: " + leggings);
                        Console.WriteLine("Your boots is level: " + boots);
                        Console.WriteLine("Enter 1 To Go Back");
                        while(int.Parse(Console.ReadLine()) != 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Your headgear is level: " + headgear);
                            Console.WriteLine("Your chestplate is level: " + chestplate);
                            Console.WriteLine("Your leggings is level: " + leggings);
                            Console.WriteLine("Your boots is level: " + boots);
                            Console.WriteLine("Enter 1 To Go Back");
                        }
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("Enter The Correct Option!");
                        System.Threading.Thread.Sleep(2000);
                    }
                    
                }
                else if (option == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Choose a option!");
                    Console.WriteLine("1: Buy headgear level $25");
                    Console.WriteLine("2: Buy chestplate level $35");
                    Console.WriteLine("3: Buy leggings level $20");
                    Console.WriteLine("4: Buy boots level $15");
                    Console.WriteLine("5: Back");
                    int opt = int.Parse(Console.ReadLine());

                    if (opt == 1 && Stats.money >= 25)
                    {
                        headgear += 1;
                        Stats.money -= 25;
                    }
                    else if (opt == 2 && Stats.money >= 35)
                    {
                        chestplate += 1;
                        Stats.money -= 35;

                    }
                    else if (opt == 3 && Stats.money >= 20)
                    {
                        leggings += 1;
                        Stats.money -= 20;
                    }
                    else if (opt == 4 && Stats.money >= 15)
                    {
                        boots += 1;
                        Stats.money -= 15;
                    }
                    else
                    {
                        Console.WriteLine("Insufficient Funds");
                        System.Threading.Thread.Sleep(1000);

                    }
                }
                else
                {
                    break;
                }
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Enter the Correct Option!");
                System.Threading.Thread.Sleep(1000);
            }
        }      
    }

    static void fireplace()
    {
        Console.Clear();
        Console.WriteLine("------ Fireplace ------");
        Console.WriteLine("Reggening Health ");
        while ( Stats.health <= 100)
        {
            Stats.health += 1;
        }
        for (int i = 0; i < 3; i++)
        {
            Console.Write(".");
            System.Threading.Thread.Sleep(500);
        }
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine("Done!");

    }
}
// --------------------------------------------------------------------------------------------------------------
class Stats
{
    public static int money = 0, health = 100, armor = 0;
    public static void Main5(bool x)
    {
        while (x == true)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Welcome To your Stats!");
                Console.WriteLine("1: Health");
                Console.WriteLine("2: Armor");
                Console.WriteLine("3: Money");
                Console.WriteLine("4: Back");
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("Your Health is: " + heal());
                        System.Threading.Thread.Sleep(2000);
                        break;
                    case 2:
                        Console.WriteLine("Armor: " + arm());
                        System.Threading.Thread.Sleep(2000);
                        break;
                    case 3:
                        Console.WriteLine("Money: " + bank() + "$");
                        System.Threading.Thread.Sleep(2000);
                        break;
                    default:
                        x = false;
                        break;
                }
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Enter the Correct Option!");
                System.Threading.Thread.Sleep(2000);
            }
        }
    }

    static int heal()
    {
        return health;
    }

    static int arm()
    {
        return armor;
    }

    static int bank()
    {
        return money;
    }
}
// --------------------------------------------------------------------------------------------------------------
class Save
{
    public static void Main6()
    {
        File.Delete(@"/Users/eavosloo/Projects/Adventurer/Adventurer/Saves/" + Start.username + ".txt"); // enter your saves folder file location
        string[] lines =
        {
            Profile.nm, Profile.ag.ToString(), Profile.he.ToString(), Profile.we.ToString(), Stats.money.ToString(), Stats.health.ToString(), Weapons.spear.ToString(), Weapons.sword.ToString(), Weapons.axe.ToString(), Weapons.shield.ToString(), Camp.headgear.ToString(), Camp.chestplate.ToString(), Camp.leggings.ToString(), Camp.boots.ToString()
        };
        for (int i = 0; i < 14; i++)
        {
            StreamWriter sw = File.AppendText(@"/Users/eavosloo/Projects/Adventurer/Adventurer/Saves/" + Start.username + ".txt"); // enter your saves folder file location
            sw.WriteLine(lines[i]);
            sw.Close();
        }
        Console.Write("Saving ");
        for(int i = 0; i < 3; i++)
                {
            Console.Write(".");
            System.Threading.Thread.Sleep(500);
        }
    }
}
// --------------------------------------------------------------------------------------------------------------