﻿using SIIPROFESORES.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SIIPROFESORES.Splash
{
    class SplashPage : ContentPage
    {
        Image splash;
        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var sub = new AbsoluteLayout();
            splash = new Image
            {
                Source = "itc_logo.jpg",
                WidthRequest = 150,
                HeightRequest = 150

            };
            AbsoluteLayout.SetLayoutFlags(splash, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splash, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            sub.Children.Add(splash);
            this.BackgroundColor = Color.White;
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await splash.ScaleTo(1, 2000);
            await splash.ScaleTo(0.9, 1500, Easing.Linear);
            await splash.ScaleTo(150, 1200, Easing.Linear);
            //Application.Current.MainPage = new MainPage();
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}
