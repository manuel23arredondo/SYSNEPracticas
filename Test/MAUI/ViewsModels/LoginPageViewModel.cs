using MAUI.ViewsModels;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MAUI.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string _username;

        [ObservableProperty]
        public string _password;
    }
}
