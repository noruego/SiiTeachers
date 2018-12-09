using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SIIPROFESORES.Models;
using SIIPROFESORES.WebServices;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SIIPROFESORES.Views
{
    class StudentPageOptions : PopupPage
    {
        private Label lbl_nombre_materia, lbl_nombre_materia_, lbl_nombre_maestro_, lbl_nombre__maestro, lbl_email_, lbl_email, lbl_nocontrol, lbl_nocontrol_;
        private StackLayout stk;
        private RelativeLayout rl_data;
        private ActivityIndicator ai_data;
        private Image img;
        private Button btn_envia_correo, btn_calificaciones;
        private Subjects subject;
        private WsSubjects objWsSubject;
        private Frame fm;

        public StudentPageOptions(Subjects sub)
        {
            objWsSubject = new WsSubjects();
            subject = sub;
            fm = new Frame
            {
                Margin =30,
                WidthRequest=700,
                Padding = 25
            };
            createGUIAsync();
        }

        private void createGUIAsync()
        {
            btn_envia_correo = new Button
            {
                Text = "Enviar correo",
                BackgroundColor = Color.FromHex("#80ffe5"),
                BorderColor = Color.FromHex("#b3f0ff"),
                BorderWidth = 3,
                CornerRadius = 40,
                TextColor = Color.Black,
            };
            btn_calificaciones = new Button
            {
                Text = "Actualizar calificaciones", 
                BackgroundColor = Color.FromHex("#80ffe5"),
                BorderColor = Color.FromHex("#b3f0ff"),
                BorderWidth = 3,
                CornerRadius = 40,
                TextColor = Color.Black,
            };
            btn_envia_correo.Clicked += async (sender, e) =>
            {
                try
                {
                    SII.Servivios.ServiciosCorreo.EnviarCorreo(subject.student.email, "Consulta SII", "Estimado alumno: " + subject.student.name);
                }
                catch (Exception ex) { await DisplayAlert("", ex.Message+ex.StackTrace, "asda"); }
            };
            btn_calificaciones.Clicked += async (sender, e) =>
            {
                try
                {
                    var popup = new CalificationsPage(subject);

                    var scaleAnimation = new ScaleAnimation
                    {
                        PositionIn = MoveAnimationOptions.Top,
                        PositionOut = MoveAnimationOptions.Bottom,
                        ScaleIn = 1.2,
                        ScaleOut = 0.8,
                        DurationIn = 400,
                        DurationOut = 800,
                        EasingIn = Easing.BounceIn,
                        EasingOut = Easing.CubicOut,
                        HasBackgroundAnimation = false
                    };

                    popup.Animation = scaleAnimation;

                    await PopupNavigation.PushAsync(popup);
                }
                catch (Exception ex) { await DisplayAlert("", ex.Message + ex.StackTrace, "asda"); }
            };

            ai_data = new ActivityIndicator()
            {
                Color = Color.Aqua,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            lbl_nombre_materia = new Label
            {
                Text = "Materia",
                FontSize = 15,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            lbl_nombre__maestro = new Label
            {
                Text = "Alumno",
                FontSize = 15,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            lbl_email = new Label
            {
                Text = "Correo",
                FontSize = 15,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            lbl_nombre_materia_ = new Label
            {
                FontSize = 15,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            lbl_nocontrol_ = new Label
            {
                Text = "Número de Control",
                FontSize = 15,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            lbl_nombre_maestro_ = new Label
            {
                FontSize = 15,
                TextColor = Color.FromHex("#003399"),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            lbl_email_ = new Label
            {
                FontSize = 15,
                TextColor = Color.FromHex("#003399"),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            lbl_nocontrol = new Label
            {
                FontSize = 15,
                TextColor = Color.FromHex("#003399"),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            stk = new StackLayout
            {
                Orientation = StackOrientation.Vertical,


                HorizontalOptions = LayoutOptions.Center,
                Children =
            {
            lbl_nombre_materia,
            lbl_nombre_materia_,
            btn_calificaciones,
            lbl_nombre__maestro,
            lbl_nombre_maestro_,
            lbl_email,
            lbl_email_,
            btn_envia_correo,
            }
            };
            rl_data = new RelativeLayout();
            rl_data.Children.Add(
                stk,
                Constraint.RelativeToParent((parent) => { return 0; }), //valor para posicion de x apartir del layout
                Constraint.RelativeToParent((parent) => { return parent.Height * .25; }), //valor para posicion de Con un cuarto del tamaño total del padre
                Constraint.RelativeToParent((parent) => { return parent.Width; }), //valor para posicion de x apartir del layout
                Constraint.RelativeToParent((parent) => { return parent.Width; }) //valor para posicion de x apartir del layout
                );
            fm.Content = rl_data;
            Content =fm;
        }
        protected override bool OnBackButtonPressed()
        {
            PopupNavigation.PopAsync();
            return true;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            /*            ai_kardex.IsRunning = true;
                        list_kardex = await objWsSubject.getSubjects();
                        lv_subjects.ItemsSource = list_kardex;
                        ai_kardex.IsRunning = false;
                        ai_kardex.IsVisible = false;
                        lv_subjects.IsVisible = true;
                        */
                    lbl_nombre_materia_.Text = subject.group.matter.name.ToString();
            lbl_nombre_maestro_.Text = subject.student.name+" "+ subject.student.father_lastname + " "+ subject.student.mother_lastname;
            lbl_email_.Text = subject.student.email;

        }

    }
}
