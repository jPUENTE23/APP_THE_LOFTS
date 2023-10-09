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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            //btnIngresar.Clicked += (sender,e)=> {
            //    Navigation.PushAsync(new Inicio());
            //};
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public async void btnIngresar(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Jorge" && txtPass.Text == "jPM230601")
            {
                // Navegar a la página principal (MainPage).
                await Navigation.PushAsync(new Inicio());
                System.Diagnostics.Debug.WriteLine("Navegación a MainPage exitosa.");
            }
            else
            {
                // Muestra un mensaje de error en caso de credenciales incorrectas.
                await DisplayAlert("Error", "Credenciales incorrectas. Intente nuevamente.", "Aceptar");
                System.Diagnostics.Debug.WriteLine("Navigation es null.");
            }

        }
    }
}