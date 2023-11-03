﻿using System;
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
    public partial class HabDeluxe : ContentPage
    {
        List<DtoUsuario> lstUsuario = new List<DtoUsuario>();
        public HabDeluxe(List<DtoUsuario> Usuario)
        {
            InitializeComponent();
            this.lstUsuario = Usuario;
            NavigationPage.SetHasNavigationBar(this, false);

        }

        public async void Inicio(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicio(this.lstUsuario));
        }
    }
}