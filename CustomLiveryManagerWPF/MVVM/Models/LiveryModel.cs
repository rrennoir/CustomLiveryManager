using CustomLiveryManagerWPF.Helper;

namespace CustomLiveryManagerWPF.MVVM.Models
{
    public class LiveryModel : PropChanged
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
            set
            {
                _name = value;
                Notify();
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                Notify();
            }
        }
    }
}
