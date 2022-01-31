using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CustomLiveryManagerWPF.Helper
{
    public abstract class PropChanged : INotifyPropertyChanged
    {
        private Dictionary<string, object> _propertyDict = new Dictionary<string, object>();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void Notify([CallerMemberName] string? p = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        protected bool Set<T>(T value, [CallerMemberName] string? propertyName = null)
        {
            bool flag = false;
            if (!_propertyDict.ContainsKey(propertyName))
            {
                _propertyDict.Add(propertyName, value);
                flag = true;
            }
            else
            {
                flag = !object.Equals(_propertyDict[propertyName], value);
                _propertyDict[propertyName] = value;
            }

            if (flag)
            {
                NotifyUpdate(propertyName);
            }

            return flag;
        }

        protected void NotifyUpdate(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected T Get<T>([CallerMemberName] string? propertyName = null)
        {
            if (!_propertyDict.ContainsKey(propertyName))
            {
                return default(T);
            }

            return (T)_propertyDict[propertyName];
        }
    }
}
