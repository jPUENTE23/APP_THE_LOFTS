using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entity;
using BLL;

namespace THE_LOFTS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public List<DtoUsuario> lstUsuario = new List<DtoUsuario>();
        public Inicio(List<DtoUsuario> Usuario)
        {
            InitializeComponent();
            this.lstUsuario = Usuario;
            NavigationPage.SetHasNavigationBar(this, false);
            actRservaciones();




        }

        public async void irPerfil(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Perfil(this.lstUsuario));
        }

        public async void irHabBasica (object sender, EventArgs e)
        {


            await Navigation.PushAsync(new HabBasica(this.lstUsuario));
        }
        public async void irHabPlus(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HabPlus(this.lstUsuario));
        }
        public async void ieHabDeluxe(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HabDeluxe(this.lstUsuario));
        }
        public async void irReservaciones (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reservaciones(this.lstUsuario));
        }

        public async void actRservaciones()
        {
            await BL_RESERVACIONES.actualziarEstatusRservaion();
        }




    }
}
