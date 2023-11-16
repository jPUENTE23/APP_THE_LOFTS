using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BLL;

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
            cargarRservaciones();

        }

        public async void irInicio (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicio(this.lsUsuario));
        }

        public async void irPerfil( object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Perfil(this.lsUsuario));
        }

        public async void cargarRservaciones()
        {

            foreach (DtoUsuario user in this.lsUsuario)
            {
                List<DtoReservacion> lstRservacion = await BL_RESERVACIONES.RsservacionesUsuario(user.IdUsuario);

                listaRservaciones.ItemsSource = lstRservacion;
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var itemSeleccionado = e.SelectedItem as DtoReservacion;

            await Navigation.PushAsync(new DetalleRerservacion(this.lsUsuario, itemSeleccionado.IdReservacion));
        }


    }
}