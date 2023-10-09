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
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public async void irInicio(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicio());
        }

    }
}