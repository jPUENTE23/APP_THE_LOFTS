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
using Newtonsoft.Json;
using Firebase.Auth;


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

            string WebAPIkey = "AIzaSyDKzEcmyoow7KHD3kWkOiE9JFWwULkGHUo";


            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(txtUsuario.Text, txtPass.Text);
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);

                List<DtoUsuario> Usuario = await BL_Usuario.datosUsuario(txtUsuario.Text, txtPass.Text);

                await Navigation.PushAsync(new Inicio(Usuario));

            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Invalid useremail or password", "OK");
            }





        }

        public async void irRegistro(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegUsuarios());
        }
    }
}