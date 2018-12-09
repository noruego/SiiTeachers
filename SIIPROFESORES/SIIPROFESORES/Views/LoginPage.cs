using SIIPROFESORES.Models;
using SIIPROFESORES.WebServices;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace SIIPROFESORES.Views
{
    class LoginPage : ContentPage
    {
        private Entry txtUsuario, txtPassword;
        private ActivityIndicator aiIndicator;
        private Button btnLogin;
        private Image imgLogo;
        private Label lblLogin;
        private CheckBox ckRemember;
        private StackLayout stkLogin;
        private RelativeLayout rlLogin;
        Login login = new Login();

        public LoginPage()
        {
            //InitializeComponent();
            Padding = new Thickness(20);
            BackgroundColor = Color.FromHex("#BBFFFF");
            imgLogo = new Image
            {
                Source = "login_img.png",
                WidthRequest =100,
                HeightRequest =100,
            };
            txtUsuario = new Entry
            {
                Placeholder = "Usuario",
                PlaceholderColor = Color.SlateBlue,
                HorizontalTextAlignment = TextAlignment.Center,
                // HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,

            };
            txtPassword = new Entry
            {
                IsPassword = true,
                Placeholder = "Contraseña",
                PlaceholderColor = Color.SlateBlue,
                HorizontalTextAlignment = TextAlignment.Center,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,

            };
            
            btnLogin = new Button
            {
                Text = "Login",
                BackgroundColor = Color.FromHex("#80ffe5"),
                BorderColor = Color.FromHex("#b3f0ff"),
                BorderWidth = 3,
                CornerRadius = 40,
                TextColor = Color.Black,
            };
            ckRemember = new CheckBox
            {
                DefaultText = "Recordar usuario y contraseña",
                FontSize = 13,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            if (!String.IsNullOrEmpty(Settings.UserName) && !String.IsNullOrEmpty(Settings.password))
            {
                txtPassword.Text = Settings.password;
                txtUsuario.Text = Settings.UserName;
                ckRemember.Checked = true;
            }
            lblLogin = new Label
            {
                Text = "Powered by TecnoHack",
                FontSize = 15,
                TextColor = Color.FromHex("#003399"),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            btnLogin.Clicked += BtnLogin_CLicked;
            aiIndicator = new ActivityIndicator
            {
                HorizontalOptions = LayoutOptions.Center,
                IsVisible=false
            };
            stkLogin = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HeightRequest = 500,
                WidthRequest = 400,
                HorizontalOptions = LayoutOptions.Center,
                Children =
            {
            imgLogo,
            txtUsuario,
            txtPassword,
            aiIndicator,
            ckRemember,
            btnLogin
            }
            };
            rlLogin = new RelativeLayout();
            rlLogin.Children.Add(
                stkLogin,
                Constraint.RelativeToParent((parent) => { return 0; }), //valor para posicion de x apartir del layout
                Constraint.RelativeToParent((parent) => { return parent.Height * .20; }), //valor para posicion de Con un cuarto del tamaño total del padre
                Constraint.RelativeToParent((parent) => { return parent.Width; }), //valor para posicion de x apartir del layout
                Constraint.RelativeToParent((parent) => { return parent.Width; }) //valor para posicion de x apartir del layout
                );
            rlLogin.Children.Add(
                lblLogin,
                Constraint.RelativeToParent((parent) => { return 0; }), //valor para posicion de x apartir del layout
                Constraint.RelativeToView(stkLogin, (parent, view) => { return view.Y + view.Height + 50; }), //valor para posicion de Con un cuarto del tamaño total del padre
                Constraint.RelativeToParent((parent) => { return parent.Width; }), //valor para posicion de x apartir del layout
                Constraint.RelativeToParent((parent) => { return parent.Width; }) //valor para posicion de x apartir del layout
                );
            Content = rlLogin;
        }
        private async void BtnLogin_CLicked(Object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                DisplayAlert("Error", "Debes introducir un usuario", "Aceptar");
                txtUsuario.Focus();
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                DisplayAlert("Error", "Debes introducir un password", "Aceptar");
                txtUsuario.Focus();
            }
            aiIndicator.IsVisible = true;
            aiIndicator.IsRunning = true;
            try
            {
                WsLogin objWSL = new WsLogin();
                String result = await objWSL.conexion(txtUsuario.Text, txtPassword.Text);
                //DisplayAlert("Error", result, "Aceptar");
                if (result.Equals("Acceso denegado"))
                {
                    DisplayAlert("Error", "Acceso denegado", "Aceptar");
                    aiIndicator.IsRunning = false;
                    aiIndicator.IsVisible = false;
                }
                else
                {
                    if (ckRemember.Checked)
                    {
                        Settings.password = txtPassword.Text;
                        Settings.UserName = txtUsuario.Text;
                    }
                    else
                    {
                        Settings.password = null;
                        Settings.UserName = null;
                    }
                    aiIndicator.IsRunning = false;
                    aiIndicator.IsVisible = false;
                    
                    DataPage dt = new DataPage();
                    
                    await Navigation.PushModalAsync(new DashBoard(new MenuOpcion()
                    {
                        
                        TargetType = typeof(DataPage)
                    }));
                }
            }
            catch (Exception ex) { DisplayAlert("Error",ex.StackTrace,"aceptar"); }

        }
    }
}

