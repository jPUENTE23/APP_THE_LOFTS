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
    public partial class HabDeluxe : ContentPage
    {
        string tipoHab = "Deluxe";
        int precioHab = 4500;
        int cantDisponibles = 10;
        List<string> lstHabitacions = new List<string>() { "D101", "D102", "D103", "D104", "D105", "D106", "D106", "D107", "D108", "D109", "D110" };

        List<DtoUsuario> lstUsuario = new List<DtoUsuario>();
        public HabDeluxe(List<DtoUsuario> Usuario)
        {

            InitializeComponent();
            this.lstUsuario = Usuario;
            NavigationPage.SetHasNavigationBar(this, false);
            ValidarHabitaciones();

        }

        public async void Inicio(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicio(this.lstUsuario));
        }

        public async void Reservar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AltaReservacion(this.lstUsuario, precioHab, tipoHab, lstHabitacions));
        }

        public async void ValidarHabitaciones()
        {
            List<DtoReservacion> reservacionees = await BL_RESERVACIONES.Rsservaciones(tipoHab);

            if (reservacionees.Count < cantDisponibles)
            {
                int disp = cantDisponibles - reservacionees.Count;
                txtDisponible.Text = $"Disponibles: {disp}";
            }
            else
            {
                txtDisponible.Text = "No Disponibles";
                btnRservar.IsEnabled = false;

            }
        }
    }
}