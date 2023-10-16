﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace THE_LOFTS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public async void irPerfil(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Perfil());
        }

        public async void irHabBasica (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HabBasica());
        }
        public async void irHabPlus(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HabPlus());
        }
        public async void ieHabDeluxe(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HabDeluxe());
        }
        public async void irReservaciones (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reservaciones());
        }
    }
}
