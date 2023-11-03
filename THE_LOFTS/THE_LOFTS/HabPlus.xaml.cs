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
    public partial class HabPlus : ContentPage
    {
        public List<DtoUsuario> lstUsuario = new List<DtoUsuario>();
        public HabPlus(List<DtoUsuario> Usuario)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.lstUsuario = Usuario;
        }

        public async void Inicio (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicio(this.lstUsuario));
        }
    }
}