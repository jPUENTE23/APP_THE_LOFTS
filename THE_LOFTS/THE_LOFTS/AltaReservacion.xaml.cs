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
        string tipoHab = "";
        int precioHab = 0;
        List<DtoUsuario> lstusuario = new List<DtoUsuario>();
        public AltaReservacion(List<DtoUsuario> Usuario, int precioHab, string tipoHab)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            fecInicio.DateSelected += FecInicio_DateSelected;
            this.lstusuario = Usuario;
            this.tipoHab = tipoHab;
            this.precioHab = precioHab;
        }

        public async void Cerrar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HabBasica(this.lstusuario));
        }

        public async void GuardarRservacion(object sender, EventArgs e)
        {

            int noNoches = BL_RESERVACIONES.nochesRersevacion(fecInicio.Date, fecFin.Date);
            int total = this.precioHab * noNoches;

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
                        Total = total,
                        TipoHab = this.tipoHab
                    };

                    List<string> response = await BL_RESERVACIONES.GuardarReservacion(Reservacion);

                    if (response[0] == "00")
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", response[1], "OK");
                        await Navigation.PushAsync(new Inicio(this.lstusuario));
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", response[1], "OK");
                    }
                }
            }
        }

        private void FecInicio_DateSelected(object sender, DateChangedEventArgs e)
        {
            // Obtiene la fecha seleccionada
            DateTime selectedDate = e.NewDate;

            // Define el rango de fechas bloqueadas (del 16 al 19)
            DateTime fechaInicioBloqueada = new DateTime(selectedDate.Year, selectedDate.Month, 16);
            DateTime fechaFinBloqueada = new DateTime(selectedDate.Year, selectedDate.Month, 19);

            // Verifica si la fecha seleccionada está dentro del rango bloqueado
            if (selectedDate >= fechaInicioBloqueada && selectedDate <= fechaFinBloqueada)
            {

                // Restaura la fecha anterior o realiza alguna acción adicional si es necesario
                fecInicio.Date = e.OldDate;
                fecFin.Date = e.OldDate;
            }
        }


        //public async void bloquerFechas()
        //{
        //    List<DtoReservacion> lsrReservaciones = await BL_RESERVACIONES.Rsservaciones();
        //    // Obtén las fechas mínima y máxima de tu lista
        //    DateTime minDate = lsrReservaciones.Min(reservacion => reservacion.FecInicioResrvacion);
        //    DateTime maxDate = lsrReservaciones.Max(reservacion => reservacion.FecFinReservacion);

        //    // Establece las fechas en el DatePicker
        //    fecInicio.MinimumDate = minDate;
        //    fecInicio.MaximumDate = maxDate;

        //    fecFin.MinimumDate = minDate;
        //    fecFin.MaximumDate = maxDate;

        //    // Si deseas bloquear todo el rango, puedes establecer la fecha seleccionada inicialmente
        //    fecInicio.Date = minDate;
        //    fecFin.Date = minDate;

        //    fecInicio.DateSelected += (sender, e) =>
        //    {
        //        // Verifica si la fecha seleccionada está dentro del rango prohibido
        //        if (e.NewDate >= minDate && e.NewDate <= maxDate)
        //        {
        //            // Restaura la fecha anterior (puedes mostrar un mensaje de advertencia si deseas)
        //            fecInicio.Date = e.OldDate;
        //        }
        //    };

        //    fecFin.DateSelected += (sender, e) =>
        //    {
        //        // Verifica si la fecha seleccionada está dentro del rango prohibido
        //        if (e.NewDate >= minDate && e.NewDate <= maxDate)
        //        {
        //            // Restaura la fecha anterior (puedes mostrar un mensaje de advertencia si deseas)
        //            fecFin.Date = e.OldDate;
        //        }
        //    };
        //}
    }
}