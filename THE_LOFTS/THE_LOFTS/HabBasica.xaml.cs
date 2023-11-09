﻿using System;
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
    public partial class HabBasica : ContentPage
    {
        string tipoHab = "Basica";
        int precioHab = 300;
        List<DtoUsuario> lsUsuario = new List<DtoUsuario>();
        public HabBasica(List<DtoUsuario> Usuario)
        {
            InitializeComponent();
            this.lsUsuario = Usuario;
            NavigationPage.SetHasNavigationBar(this, false);

        }

        public async void Inicio(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicio(this.lsUsuario));
        }

        public async void Reservar (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AltaReservacion(this.lsUsuario, precioHab, tipoHab));
        }


    }
}