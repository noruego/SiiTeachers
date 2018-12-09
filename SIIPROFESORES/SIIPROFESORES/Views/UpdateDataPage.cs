using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SIIPROFESORES.WebServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SIIPROFESORES.Models
{
    class UpdateDataPage : PopupPage
    {
        private Entry txt_pass, txt_user;
        private StackLayout stk, stk1;
        private RelativeLayout rl_data;
        private ActivityIndicator ai_data;
        private WsLogin objWsLogin;
        private Button btnAceptar;
        private Frame fm;
        public UpdateDataPage()
        {
            fm = new Frame
            {
                Margin = 25,
                Padding = 25
            };
            objWsLogin = new WsLogin();
            CreateGUI();
        }

        private void CreateGUI()
        {
            //NavigationPage.SetHasNavigationBar(this, false); //Quitar barra de navegacion 
            btnAceptar = new Button
            {
                Text = "Actualizar datos",
                BackgroundColor = Color.FromHex("#80ffe5"),
                BorderColor = Color.FromHex("#b3f0ff"),
                BorderWidth = 3,
                CornerRadius = 40,
                TextColor = Color.Black,
            };
            btnAceptar.Clicked += async (sender, e) =>
            {
                try
                {
                    if (await objWsLogin.update(txt_pass.Text, txt_user.Text))
                    {
                        DisplayAlert("Atención", "Datos actualizados correctamente", "Aceptar");
                        PopupNavigation.PopAsync();
                    }
                    
                }
                catch (Exception ex) { await DisplayAlert("", ex.StackTrace, "asdasd"); }
            };
            ai_data = new ActivityIndicator()
            {
                Color = Color.Aqua,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            txt_user = new Entry
            {
                Placeholder = "Ingresa nuevo usuario",
                PlaceholderColor = Color.SlateBlue,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,

            };
            txt_pass = new Entry
            {
                Placeholder = "Ingresa nueva contraseña",
                PlaceholderColor = Color.SlateBlue,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                IsPassword =true
            };

            stk1 = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(20),
                Children =
                {
                    
                     new StackLayout()
                    {
                        Children = { txt_user}
                    },new StackLayout()
                    {
                        Children = { txt_pass}
                    },
                    btnAceptar
                }
            };
            rl_data = new RelativeLayout();
            rl_data.Children.Add(
                stk1,
                Constraint.RelativeToParent((parent) => { return 0; }), //valor para posicion de x apartir del layout
                Constraint.RelativeToParent((parent) => { return parent.Height * .4; }), //valor para posicion de Con un cuarto del tamaño total del padre
                Constraint.RelativeToParent((parent) => { return parent.Width; }), //valor para posicion de x apartir del layout
                Constraint.RelativeToParent((parent) => { return parent.Width; }) //valor para posicion de x apartir del layout
                );

            fm.Content = rl_data;
            Content = fm;
        }
    }
}
