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

                await Navigation.PushAsync(new Inicio());

                //Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Invalid useremail or password", "OK");
            }

            //List<DtoUsuario> lstUsuario = BL_Usuario.ValidarUsuario(cadena, txtUsuario.Text, txtPass.Text);


            //if (lstUsuario.Count > 1)
            //{
            //    await Navigation.PushAsync(new Inicio());
            //    System.Diagnostics.Debug.WriteLine("Navegación a MainPage exitosa.");
            //}
            //else
            //{
            //    await DisplayAlert("Error", "Credenciales incorrectas. Intente nuevamente.", "Aceptar");
            //    System.Diagnostics.Debug.WriteLine("Navigation es null.");
            //}

            //if (txtUsuario.Text == "Jorge" && txtPass.Text == "jPM230601")
            //{
            //    await Navigation.PushAsync(new Inicio());
            //    System.Diagnostics.Debug.WriteLine("Navegación a MainPage exitosa.");
            //}
            //else
            //{
            //    await DisplayAlert("Error", "Credenciales incorrectas. Intente nuevamente.", "Aceptar");
            //    System.Diagnostics.Debug.WriteLine("Navigation es null.");
            //}

        }

        public async void irRegistro(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegUsuarios());
        }
    }
}