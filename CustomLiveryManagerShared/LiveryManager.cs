using System.Diagnostics;
using System.Text.Json;

namespace CustomLiveryManagerShared
{
    public class LiveryManager
    {
        private const string LIVERY_PATH = @"Assetto Corsa Competizione\Customs\Liveries";
        private static readonly string userPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private static readonly string LiveryFolder = Path.Combine(userPath, LIVERY_PATH);
        private static readonly string ManagerPath = Path.Combine(LiveryFolder, "LiveryManager.json");

        private List<string> _liveries;
        private List<string> _managedLiveries;

        public LiveryManager()
        {
            _liveries = new();
            LoadLiveries();
            _managedLiveries = new();
            LoadLiveriesFromJson();
        }

        public List<string> Liveries => _liveries;
        public List<string> ManagedLiveries => _managedLiveries;

        public void AddLivery(string name)
        {
            if (_liveries.Contains(name) && !_managedLiveries.Contains(name))
            {
                _managedLiveries.Add(name);
            }
        }

        public void RemoveLivery(string name)
        {
            _managedLiveries.Remove(name);
        }

        public void DeleteUnamangedLiveries(bool emptyOnly)
        {
            foreach (var livery in _liveries)
            {
                if (!_managedLiveries.Contains(livery))
                {
                    var liveryPath = Path.Combine(LiveryFolder, livery);
                    var files = Directory.GetFiles(liveryPath);

                    if (!emptyOnly || (emptyOnly && files.Length <= 2))
                    {
                        Directory.Delete(liveryPath, true);
                        Debug.WriteLine($"Livery {livery} deleted");
                    }
                }
            }
        }

        public void LoadLiveries()
        {
            _liveries.Clear();

            var liveryFolders = Directory.GetDirectories(LiveryFolder);
            foreach (var liveryFolder in liveryFolders)
            {
                _liveries.Add(liveryFolder.Split('\\').Last());
            }
        }

        public void SaveManagedList()
        {
            WriteLiveriesToJson();
        }

        public void LoadLiveriesFromJson()
        {
            try
            {
                var jsonString = File.ReadAllText(ManagerPath);

                if (jsonString.Length > 0)
                {
                    var temp = JsonSerializer.Deserialize<List<string>>(jsonString);
                    if (temp is null)
                    {
                        Debug.WriteLine("No livery ?");
                        return;
                    }
                    _managedLiveries = temp;
                }
            }
            catch (FileNotFoundException)
            {
                File.Create(ManagerPath).Close();
            }
        }

        public void WriteLiveriesToJson()
        {
            var jsonString = JsonSerializer.Serialize(_managedLiveries);
            File.WriteAllText(ManagerPath, jsonString);
        }
    }
}
