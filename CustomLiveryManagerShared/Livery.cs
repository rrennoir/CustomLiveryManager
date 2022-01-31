using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLiveryManagerShared
{
    public class Livery
    {
        private string _name = "";
        private bool _isSelected;

        public Livery(string name)
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
