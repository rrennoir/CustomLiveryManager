using System.Diagnostics;
using System.Text.Json;

namespace CustomLiveryManagerShared
{
    public class ManagedLiveries
    {
        private List<string> _liveries;

        private string _managedPath;

        public ManagedLiveries(string path)
        {
            _managedPath = Path.Combine(path, "LiveryManager.json");
            _liveries = new List<string>();
            LoadLiveriesFromJson();
        }

        ~ManagedLiveries()
        {
            WriteLiveriesToJson();
        }

        public void Add(string name)
        {
            if (!_liveries.Contains(name))
            {
                _liveries.Add(name);
            }
        }

        public void Remove(string name)
        {
            _liveries.Remove(name);
        }

        public void Clear()
        {
            _liveries.Clear();
        }

        public bool Contains(string name)
        {
            return _liveries.Contains(name);
        }

        public List<string> Liveries => _liveries;

        public void LoadLiveriesFromJson()
        {
            try
            {
                var jsonString = File.ReadAllText(_managedPath);

                if (jsonString.Length > 0)
                {
                    var temp = JsonSerializer.Deserialize<List<string>>(jsonString);
                    if (temp is null)
                    {
                        Debug.WriteLine("No livery ?");
                        return;
                    }
                    _liveries = temp;
                }
            }
            catch (FileNotFoundException)
            {
                File.Create(_managedPath).Close();
            }
        }

        public void WriteLiveriesToJson()
        {
            var jsonString = JsonSerializer.Serialize(_liveries);
            File.WriteAllText(_managedPath, jsonString);
        }
    }
}