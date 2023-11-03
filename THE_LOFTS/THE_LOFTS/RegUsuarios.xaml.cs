using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BLL;
using Entity;

namespace THE_LOFTS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegUsuarios : ContentPage
    {
        public RegUsuarios()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public async void Inicio(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        public async void Registrarse(object sender, EventArgs e)
        {

            DtoUsuario Usuario = new DtoUsuario()
            {
                NomUsuario = txtNombre.Text,
                Apellidos = txtApellidos.Text,
                Correo = txtCorreo.Text,
                Contraseña = txtContraseña.Text
            };

            List<string> resposne = await BL_Usuario.AltaUsuario(Usuario);


            if (resposne[0] == "00")
            {
                await Navigation.PushAsync(new Login());
            }
            else
            {
                await DisplayAlert("Alert", resposne[1], "OK");

            }
            //string key = "AIzaSyDKzEcmyoow7KHD3kWkOiE9JFWwULkGHUo";

            //try
            //{
            //    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(key));
            //    var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(txtCorreo.Text, txtContraseña.Text);
            //    string gettoken = auth.FirebaseToken;

            //    await Application.Current.MainPage.Navigation.PushAsync(new Login());
            //}
            //catch (Exception ex)
            //{
            //    await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            //}
        }
    }
}