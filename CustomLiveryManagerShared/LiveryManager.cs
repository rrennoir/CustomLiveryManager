using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLiveryManagerShared
{
    public class LiveryManager
    {
        private const string LIVERY_PATH = @"Assetto Corsa Competizione\Customs\Liveries";
        private static readonly string userPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        private List<string> _liveries;
        private string _liveryFolder;
        private ManagedLiveries _managedLiveries;

        public LiveryManager()
        {
            _liveries = new List<string>();
            _liveryFolder = Path.Combine(userPath, LIVERY_PATH);
            LoadLiveries();
            _managedLiveries = new(_liveryFolder);
        }

        public List<string> Liveries => _liveries;
        public List<string> ManagedLiveries => _managedLiveries.Liveries;

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
                    var liveryPath = Path.Combine(_liveryFolder, livery);
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

            var liveryFolders = Directory.GetDirectories(_liveryFolder);
            foreach (var liveryFolder in liveryFolders)
            {
                _liveries.Add(liveryFolder.Split('\\').Last());
            }
        }

        public void SaveManagedList()
        {
            _managedLiveries.WriteLiveriesToJson();
        }
    }
}
