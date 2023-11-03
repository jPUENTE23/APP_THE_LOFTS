using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace THE_LOFTS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reservaciones : ContentPage
    {
        List<DtoUsuario> lsUsuario = new List<DtoUsuario>();
        public Reservaciones(List<DtoUsuario> Usuario)
        {
            InitializeComponent();
            this.lsUsuario = Usuario; 
            NavigationPage.SetHasNavigationBar(this, false);

        }

        public async void irInicio (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicio(this.lsUsuario));
        }

        public async void irPerfil( object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Perfil(this.lsUsuario));
        }
    }
}