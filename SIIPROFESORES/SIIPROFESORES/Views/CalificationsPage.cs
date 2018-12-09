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
    class CalificationsPage : PopupPage
    {
        private Label lbl_materia, lbl_materia_, lbl_student,lbl_student_, lbl_Calificacion1, lbl_Calificacion2, lbl_Calificacion3, lbl_Calificacion4;
        private Entry txt_calificacion1, txt_calificacion2, txt_calificacion3, txt_calificacion4;
        private StackLayout stk;
        private RelativeLayout rl_data;
        private ActivityIndicator ai_data;
        private Image img;
        private Button btn_actualizar;
        private Subjects subject;
        private WsSubjects objWsSubject;
        private Frame fm;

        public CalificationsPage(Subjects sub)
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
            lbl_materia = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Materia",
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            lbl_materia_ = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            lbl_student = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text="Alumno",
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            lbl_student_ = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            lbl_Calificacion1 = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Calificacion1",
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            lbl_Calificacion2 = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Calificacion2",
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            lbl_Calificacion3 = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Calificacion3",
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            lbl_Calificacion4 = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Calificacion4",
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            txt_calificacion1 = new Entry
            {
                PlaceholderColor = Color.SlateBlue,
                HorizontalTextAlignment = TextAlignment.Center,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,

            };
            txt_calificacion2 = new Entry
            {
                PlaceholderColor = Color.SlateBlue,
                HorizontalTextAlignment = TextAlignment.Center,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,

            };
            txt_calificacion3 = new Entry
            {
                PlaceholderColor = Color.SlateBlue,
                HorizontalTextAlignment = TextAlignment.Center,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,

            };
            txt_calificacion4 = new Entry
            {
                PlaceholderColor = Color.SlateBlue,
                HorizontalTextAlignment = TextAlignment.Center,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,
            };
            btn_actualizar = new Button
            {
                Text = "Actualizar",
                BackgroundColor = Color.FromHex("#80ffe5"),
                BorderColor = Color.FromHex("#b3f0ff"),
                BorderWidth = 3,
                CornerRadius = 40,
                TextColor = Color.Black,
            };
            btn_actualizar.Clicked += async (sender, e) =>
            {
                subject.calificacion1 = Int32.Parse(txt_calificacion1.Text);
                subject.calificacion2 = Int32.Parse(txt_calificacion2.Text);
                subject.calificacion3 = Int32.Parse(txt_calificacion3.Text);
                subject.calificacion4 = Int32.Parse(txt_calificacion4.Text);
                try
                {
                        if (await objWsSubject.updateCalif(subject))
                        {
                        DisplayAlert("Atención", "Calificaciones actualizadas", "Aceptar");
                            PopupNavigation.PopAsync();
                        }
                }
                catch (Exception ex) { await DisplayAlert("", ex.Message + ex.StackTrace, "asda"); }
            };
            stk = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(20),
                Children =
                {
                   lbl_materia,
                   lbl_materia_,
                   lbl_student,
                   lbl_student_,
                   lbl_Calificacion1,
                   txt_calificacion1,
                   lbl_Calificacion2,
                   txt_calificacion2,
                   lbl_Calificacion3,
                   txt_calificacion3,
                   lbl_Calificacion4,
                   txt_calificacion4,
                   btn_actualizar
                }
            };
            fm.Content = stk;
            Content =fm;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            txt_calificacion1.Text = subject.calificacion1.ToString();
            txt_calificacion2.Text = subject.calificacion2.ToString();
            txt_calificacion3.Text = subject.calificacion3.ToString();
            txt_calificacion4.Text = subject.calificacion4.ToString();
            lbl_materia_.Text = subject.group.matter.name.ToString();
            lbl_student_.Text = subject.student.name +" "+ subject.student.mother_lastname +" "+ subject.student.mother_lastname;


        }

    }
}
