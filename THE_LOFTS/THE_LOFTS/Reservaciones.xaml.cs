using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace THE_LOFTS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reservaciones : ContentPage
    {
        public Reservaciones()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        public async void irInicio (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicio());
        }

        public async void irPerfil( object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Perfil());
        }
    }
}