﻿using System;
using System.Collections.Generic;
using System.Text;
using WandelApp.Views;
using Xamarin.Forms;
using System.Resources;

namespace WandelApp.Models
{
    class SplashPage : ContentPage
    {
        Label WelcomeLabel;
        Image SplashPageImage;


        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            WelcomeLabel = new Label
            {
                Text = "Welkom in de wandelapp",
                TextColor = Color.FromHex("DarkGreen"),
                FontSize = 16,
                HorizontalOptions = LayoutOptions.Center,
                Margin = 10
            };
                       
            SplashPageImage = new Image
            {
                Source = ImageSource.FromFile("SplashPageImage.png"),
                WidthRequest = 150,
                HeightRequest = 150
            };
            AbsoluteLayout.SetLayoutFlags(SplashPageImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(SplashPageImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(WelcomeLabel);
            sub.Children.Add(SplashPageImage);

            this.BackgroundColor = Color.FromHex("#339900");
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await SplashPageImage.ScaleTo(1, 2000);
            await SplashPageImage.ScaleTo(0.9, 1500, Easing.Linear);
            Application.Current.MainPage = new NewMainPage();
        }
    }
}
