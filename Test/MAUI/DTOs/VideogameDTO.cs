using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUI.DTOs
{
    public partial class VideogameDTO: ObservableObject
    {
        [ObservableProperty]
        public int idVideoGame;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string description;
        [ObservableProperty]
        public string price;
        [ObservableProperty]
        public string company;
        [ObservableProperty]
        public string gender;

    }
}
