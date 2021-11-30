using System.Text;
using System.Text.Json;


namespace CustomLiveryManager
{
    public class ManagedLivery
    {
        public List<string> Liveries { get; set; }

        public ManagedLivery()
        {
            Liveries = new List<string>();
        }

        public void Add(string name)
        {
            if (!Liveries.Contains(name))
            {
                Liveries.Add(name);
            }
        }

        public void Remove(string name)
        {
            Liveries.Remove(name);
        }

        public void Clear()
        {
            Liveries.Clear();
        }

        public bool Contains(string name)
        {
            return Liveries.Contains(name);
        }
    }

    public class Program
    {
        private const string VERSION = "0.1";
        private const string liveryPath = "\\Assetto Corsa Competizione\\Customs\\Liveries";

        private static readonly string userPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private static bool exit = false;

        private static ManagedLivery liveries = new();

        public static void Main(string[] args)
        {
            Console.WriteLine($"Custom Livery Manager {VERSION}\n");

            LoadJson();

            while (!exit)
            {
                Console.WriteLine("1. Add livery to manager");
                Console.WriteLine("2. Remove livery to manager");
                Console.WriteLine("3. Show managed liveries");
                Console.WriteLine("4. Show all liveries");
                Console.WriteLine("5. Clean unmanaged liveries");
                Console.WriteLine("6. Clear manager");
                Console.WriteLine("7. Exit");

                Console.Write("\nChoose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddLivery();
                        break;

                    case "2":
                        RemoveLivery();
                        break;

                    case "3":
                        ListSavedLiveries();
                        break;

                    case "4":
                        ListLiveries();
                        break;

                    case "5":
                        DeleteLiveries();
                        break;

                    case "6":
                        liveries.Clear();
                        break;

                    case "7":
                        exit = true;
                        break;

                    default:
                        break;
                }
                Console.WriteLine();
            }
            WriteToJsonFile();
        }

        private static void AddLivery()
        {
            Console.WriteLine("Adding livery");
            Console.Write("Livery name: ");
            var liveryName = Console.ReadLine();

            if (liveryName != null && !liveries.Contains(liveryName))
            {
                liveries.Add(liveryName);
            }           
        }

        private static void RemoveLivery()
        {
            Console.WriteLine("Removing livery");
            Console.Write("Livery name: ");
            var liveryName = Console.ReadLine();

            if (liveryName != null)
            {
                liveries.Remove(liveryName);
            }
        }

        private static void DeleteLiveries()
        {
            var liveryFolders = Directory.GetDirectories(userPath + liveryPath);

            Console.Write("Do you want to delete all liveries that doesn't contain a png/dds file ? (y/N) ");
            var choice = Console.ReadLine()?.ToLower();

            if (choice == "y")
            {
                foreach (var livery in liveryFolders)
                {
                    var liveryName = livery.Split('\\').Last();
                    if (!liveries.Contains(liveryName))
                    {
                        var files = Directory.GetFiles(livery);
                        
                        if (files.Length == 2)
                        {
                            Directory.Delete(livery, true);
                            Console.WriteLine($"Livery {liveryName} deleted");
                        }
                    }
                }
            }
            else
            {
                Console.Write("Do you want to delete all liveries that aren't managed ? (y/N) ");
                choice = Console.ReadLine()?.ToLower();

                if (choice == "y")
                {
                    foreach (var livery in liveryFolders)
                    {
                        var liveryName = livery.Split('\\').Last();
                        if (!liveries.Contains(liveryName))
                        {
                            Directory.Delete(livery, true);
                            Console.WriteLine($"Livery {liveryName} deleted");
                        }
                        else
                        {
                            Console.WriteLine($"{liveryName} saved");
                        }
                    }
                }
            }
        }

        private static void ListSavedLiveries()
        {
            Console.WriteLine("Listing managed liveries:\n");

            foreach (var livery in liveries.Liveries)
            {
                Console.WriteLine(livery);
            }
        }

        private static void ListLiveries()
        {
            Console.WriteLine("Listing all liveries:\n");

            var liveryFolders = Directory.GetDirectories(userPath + liveryPath);

            foreach (var liveryFolder in liveryFolders)
            {
                var liveryName = liveryFolder.Split('\\').Last();
                Console.WriteLine(liveryName);
            }
        }

        private static void LoadJson()
        {
            try
            {
                var jsonString = File.ReadAllText(userPath + liveryPath + "\\LiveryManager.json");

                if (jsonString.Length > 0)
                {
                    liveries = JsonSerializer.Deserialize<ManagedLivery>(jsonString);
                }
            }
            catch (FileNotFoundException)
            {
                File.Create(userPath + liveryPath + "\\LiveryManager.json").Close();
            }
        }

        private static void WriteToJsonFile()
        {
            var jsonString = JsonSerializer.Serialize(liveries);
            File.WriteAllText(userPath + liveryPath + "\\LiveryManager.json", jsonString);
        }
    }
}