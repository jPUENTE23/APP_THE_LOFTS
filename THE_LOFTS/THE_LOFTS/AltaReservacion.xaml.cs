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
        List<string> lstHab = new List<string>();
        public AltaReservacion(List<DtoUsuario> Usuario, int precioHab, string tipoHab, List<string> lstHabitaciones)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //fecInicio.DateSelected += DatePicker_DateSelected;
            this.lstusuario = Usuario;
            this.tipoHab = tipoHab;
            this.precioHab = precioHab;
            this.lstHab = lstHabitaciones;

            foreach (DtoUsuario user in Usuario)
            {
                txtUsuario.Text = user.NomUsuario + " " + user.Apellidos ;
            }
        }

        public async void Cerrar(object sender, EventArgs e)
        {
            if (this.tipoHab == "Basica")
            {
                await Navigation.PushAsync(new HabBasica(this.lstusuario));
            }
            else if (this.tipoHab == "Plus")
            {
                await Navigation.PushAsync(new HabPlus(this.lstusuario));
            }
            else
            {
                await Navigation.PushAsync(new HabDeluxe(this.lstusuario));
            }
            
        }

        public void calcularTotal()
        {
            int noNoches = BL_RESERVACIONES.nochesRersevacion(fecInicio.Date, fecFin.Date);
            int total = this.precioHab * noNoches;

            txt_Total.Text = "$" + total.ToString();
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (fecInicio.Date > DateTime.Now && fecFin.Date > fecInicio.Date)
            {
                calcularTotal();
            }
 

                
        }

        public async void GuardarRservacion(object sender, EventArgs e)
        {

            if(fecInicio.Date < DateTime.Now)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "La FechaIncioRservacion tiene que ser mayor o igual a la fecha actual", "OK");
            }
            else if (fecFin.Date < fecInicio.Date || fecFin.Date == fecInicio.Date)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "La FechaFinRservacion tiene que ser mayor a FechaIncioRservacion", "OK");
            }
            else
            {
                if(!string.IsNullOrEmpty(txtNoTarjeta.Text) && !string.IsNullOrEmpty(txtFecVec.Text) && !string.IsNullOrEmpty(txtCVV.Text))
                {
                    int noNoches = BL_RESERVACIONES.nochesRersevacion(fecInicio.Date, fecFin.Date);
                    int total = this.precioHab * noNoches;

                    foreach (DtoUsuario usuario in this.lstusuario)
                    {
                        List<DtoUsuario> lstUsuario = await BL_Usuario.datosUsuario(usuario.Correo, usuario.Contraseña);
                        string noHab = await asignarHab();

                        foreach (DtoUsuario user in lstUsuario)
                        {
                            DtoReservacion Reservacion = new DtoReservacion
                            {
                                FecInicioResrvacion = fecInicio.Date,
                                FecFinReservacion = fecFin.Date,
                                IdUsuario = user.IdUsuario,
                                CantNoches = noNoches,
                                Total = total,
                                TipoHab = this.tipoHab,
                                Estatus = 1,
                                noTarjeta = txtNoTarjeta.Text,
                                noHabitacion = noHab
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
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "No se han ingresado los datos para realizar el pago", "OK");
                }
            }
        }

        public async Task<string> asignarHab()
        {
            List<string> lsrRservacionee = await BL_RESERVACIONES.RsservacionesHab(this.tipoHab);

            string nohab = "";

            foreach (string hab in this.lstHab)
            {
                if (!lsrRservacionee.Contains(hab))
                {
                    nohab = hab;
                    break;
                }
            }

            return nohab;
        }

        //private void FecInicio_DateSelected(object sender, DateChangedEventArgs e)
        //{
        //    // Obtiene la fecha seleccionada
        //    DateTime selectedDate = e.NewDate;

        //    // Define el rango de fechas bloqueadas (del 16 al 19)
        //    DateTime fechaInicioBloqueada = new DateTime(selectedDate.Year, selectedDate.Month, 16);
        //    DateTime fechaFinBloqueada = new DateTime(selectedDate.Year, selectedDate.Month, 19);

        //    // Verifica si la fecha seleccionada está dentro del rango bloqueado
        //    if (selectedDate >= fechaInicioBloqueada && selectedDate <= fechaFinBloqueada)
        //    {

        //        // Restaura la fecha anterior o realiza alguna acción adicional si es necesario
        //        fecInicio.Date = e.OldDate;
        //        fecFin.Date = e.OldDate;
        //    }
        //}


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