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
    public partial class Perfil : ContentPage
    {
        public List<DtoUsuario> lstUsuario = new List<DtoUsuario>();
        public Perfil(List<DtoUsuario> Usuario)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.lstUsuario = Usuario;

            foreach (DtoUsuario user in Usuario)
            {
                txt_Usuario.Text = user.NomUsuario.ToString();
            }
        }

        public async void irInicio(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicio(this.lstUsuario));
        }


        public async void Rservaciones(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reservaciones(this.lstUsuario));
        }
    }
}