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
    public partial class Perfil : ContentPage
    {
        public List<DtoUsuario> lstUsuario = new List<DtoUsuario>();
        public Perfil(List<DtoUsuario> Usuario)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.lstUsuario = Usuario;
            DatosUsuarios();
        }

        public async void irInicio(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicio(this.lstUsuario));
        }


        public async void Rservaciones(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reservaciones(this.lstUsuario));
        }

        public void DatosUsuarios()
        {
            try
            {
                foreach (DtoUsuario usuario in this.lstUsuario)
                {
                    txt_Usuario.Text = usuario.NomUsuario.ToString();
                    txtNom.Text = usuario.NomUsuario.ToString();
                    txtApellidos.Text = usuario.Apellidos.ToString();
                    txtCorreo.Text = usuario.Correo.ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}