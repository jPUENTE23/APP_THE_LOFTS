using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entity;
using THE_LOFTS.Access;
using BLL;



namespace THE_LOFTS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public readonly string cadena;

        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            cadena = Conection.cadenaString();
        }

        public async void btnIngresar(object sender, EventArgs e)
        {
            //List<DtoUsuario> lstUsuario = BL_Usuario.ValidarUsuario(cadena, txtUsuario.Text, txtPass.Text);

            if (txtUsuario.Text == "Jorge" && txtPass.Text == "jPM230601")
            {
                await Navigation.PushAsync(new Inicio());
                System.Diagnostics.Debug.WriteLine("Navegación a MainPage exitosa.");
            }
            else
            {
                await DisplayAlert("Error", "Credenciales incorrectas. Intente nuevamente.", "Aceptar");
                System.Diagnostics.Debug.WriteLine("Navigation es null.");
            }

        }
    }
}