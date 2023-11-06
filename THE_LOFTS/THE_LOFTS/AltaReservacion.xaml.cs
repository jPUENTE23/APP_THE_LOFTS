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
    public partial class AltaReservacion : ContentPage
    {
        int precioHab = 300;
        List<DtoUsuario> lstusuario = new List<DtoUsuario>();
        public AltaReservacion(List<DtoUsuario> Usuario)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.lstusuario = Usuario;
        }

        public async void Cerrar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HabBasica(this.lstusuario));
        }

        public async void GuardarRservacion()
        {

            int noNoches = BL_RESERVACIONES.nochesRersevacion(fecInicio.Date, fecFin.Date);
            int total = precioHab * noNoches;

            foreach(DtoUsuario usuario in this.lstusuario)
            {
                List<DtoUsuario> lstUsuario = await BL_Usuario.datosUsuario(usuario.Correo, usuario.Contraseña);
                
                foreach (DtoUsuario user in lstUsuario)
                {
                    DtoReservacion Reservacion = new DtoReservacion
                    {
                        FecInicioResrvacion = fecInicio.Date,
                        FecFinReservacion = fecFin.Date,
                        IdUsuario = user.IdUsuario,
                        CantNoches = noNoches,
                        Total = total
                    };

                    List<string> response = await BL_RESERVACIONES.GuardarReservacion(Reservacion);

                    if (response[0] == "00")
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", response[1], "OK");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", response[1], "OK");
                    }
                }
            }
        }
    }
}