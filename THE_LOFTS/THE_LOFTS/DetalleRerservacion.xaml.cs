using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using BLL;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace THE_LOFTS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleRerservacion : ContentPage
    {
        List<DtoUsuario> lsUsuario = new List<DtoUsuario>();
        string IdRservacion;
        public DetalleRerservacion(List<DtoUsuario> Usuario, string idRservacion)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.lsUsuario = Usuario;
            this.IdRservacion = idRservacion;
            txtRseracion.Text = idRservacion;
            MostrarDetalles();
        }

        public async void Cerrar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reservaciones(this.lsUsuario));
        }

        public async void MostrarDetalles()
        {
            List<DtoReservacion> lstRservacion = await BL_RESERVACIONES.datosRservacion(this.IdRservacion);

            foreach (DtoUsuario user in this.lsUsuario)
            {
                foreach (DtoReservacion reserv in lstRservacion)
                {
                    fecRservacion.Text = reserv.FecReservacion.ToString();
                    fecInicio.Text = reserv.FecInicioResrvacion.ToString();
                    fecFin.Text = reserv.FecFinReservacion.ToString();
                    tipHabitacion.Text = reserv.TipoHab;
                    total.Text = "$" + reserv.Total.ToString();
                    usuario.Text = user.NomUsuario + " " + user.Apellidos;

                    if(reserv.Estatus == 0)
                    {
                        btnEliminar.IsEnabled = false;
                    }

                }


            }
        }

        public async void Eliminar(object sender, EventArgs e)
        {
            List<string> response = await BL_RESERVACIONES.eliminarReserv(this.IdRservacion);


            if (response[0] == "00")
            {
                await App.Current.MainPage.DisplayAlert("Alert", response[1], "OK");
                await Navigation.PushAsync(new Reservaciones(this.lsUsuario));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", response[1], "OK");
            }
        }


    }
}