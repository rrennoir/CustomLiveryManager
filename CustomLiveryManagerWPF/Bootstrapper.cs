using Caliburn.Micro;
using CustomLiveryManagerWPF.MVVM.ViewModels;
using System.Windows;

namespace CustomLiveryManagerWPF
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<LiveryManagerViewModel>();
        }
    }
}
