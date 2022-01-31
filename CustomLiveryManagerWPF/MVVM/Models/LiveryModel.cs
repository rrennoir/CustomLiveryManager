using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLiveryManagerWPF.MVVM.Models
{
    public class LiveryModel
    {
        private string _name = "";
        private bool _isSelected;
        
        public LiveryModel(string name)
        {
            Name = name;
        }
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => _isSelected = value;
        }
    }
}
