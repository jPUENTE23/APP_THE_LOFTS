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
    public partial class HabPlus : ContentPage
    {
        string tipoHab = "Plus";
        int precioHab = 1200;
        int cantDisponibles = 10;
        List<string> lstHabitacions = new List<string>() { "P101", "P102", "P103", "P104", "P105", "P106", "P106", "P107", "P108", "P109", "P110" };
        public List<DtoUsuario> lstUsuario = new List<DtoUsuario>();
        public HabPlus(List<DtoUsuario> Usuario)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.lstUsuario = Usuario;
            ValidarHabitaciones();
        }

        public async void Inicio (object sender, EventArgs e)
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
                int disp = cantDisponibles -reservacionees.Count;
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