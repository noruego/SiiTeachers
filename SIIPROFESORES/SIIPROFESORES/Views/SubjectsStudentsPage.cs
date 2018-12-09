using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SIIPROFESORES.Models;
using SIIPROFESORES.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SIIPROFESORES.Views
{
    class SubjectsStudentsPage : PopupPage
    {
        private ListView lv_subjects;
        private SearchBar sr_kardex;
        private ActivityIndicator ai_kardex;
        private Label lb_seleccionar, lb_periodo, lb_materia;
        private BoxView bv_div;
        private List<Subjects> list_kardex;
        private WsSubjects objWsSubject;
        private StackLayout st_inst;
        private Boolean _isRefreshing = false;
        private Subjects subject;
        private Frame fm;

        public SubjectsStudentsPage(Subjects sub)
        {
            subject = sub;
            objWsSubject = new WsSubjects();
            fm = new Frame
            {
                Margin = 25,
                Padding = 25
            };
            createGUI();
        }
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        private void createGUI()
        {
            //NavigationPage.SetHasNavigationBar(this, false); //Quitar barra de navegacion 
            ai_kardex = new ActivityIndicator()
            {
                Color = Color.Green,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            sr_kardex = new SearchBar()
            {
                HorizontalTextAlignment = TextAlignment.Start,
                Placeholder = "Buscar registro",
                FontSize = 16,
                TextColor = Color.Black
            };
            sr_kardex.TextChanged += (sender, e) => buscarInstitucion(sr_kardex.Text);

            lv_subjects = new ListView();
            lv_subjects.HasUnevenRows = true;
            lv_subjects.ItemTemplate = new DataTemplate(typeof(ResultCellSubjectsStudents));
            lv_subjects.ItemSelected += async (sender, e) =>
            {
                try
                {
                    Subjects sub = (Subjects)e.SelectedItem;
                    var popup = new StudentPageOptions(sub);

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
                catch (Exception ex) { await DisplayAlert("Alert", ex.ToString(), "Aceptar"); }

                //DisplayAlert("Itemselected", Settings.Settings.institucionName + "\n" +
                //Agregar con settings 
                // App.Current.MainPage = new DashBoard();
            };
            lv_subjects.IsPullToRefreshEnabled = true;
            lv_subjects.RefreshCommand = new Command(async()=> {
                IsRefreshing = true;
                list_kardex = await objWsSubject.getSubjects();
                lv_subjects.ItemsSource = list_kardex;
                lv_subjects.EndRefresh();
            });
            bv_div = new BoxView()
            {
                Color = Color.Green,
                HeightRequest = 2,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 300
            };
            lb_seleccionar = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Agregar materia",
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            var tapr = new TapGestureRecognizer();
            tapr.Tapped += async (sender, e) =>
            {
                //var resp = await DisplayAlert("Atención", "¿Desea eliminar materia?", "Aceptar","Cancelar");
                var popup = new GroupsPage();

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


                //App.Current.MainPage = new GroupsPage();
                //asignar ciertas carcateristicas de un tecnologico especifico
            };
            lb_periodo = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Periodo",
                TextColor = Color.Gray,
                FontSize = 15,
                FontFamily = "Roboto"
            };
            lb_materia = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Materia",
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Gray,
                FontSize = 15,
                FontFamily = "Roboto"
            };
            lb_seleccionar.GestureRecognizers.Add(tapr);

            st_inst = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(20),
                Children =
                {
                    sr_kardex,
                   
                    ai_kardex,
                    lv_subjects,
                    bv_div
                }
            };
            fm.Content = st_inst;
            Content = fm;
        }

        private void buscarInstitucion(string institucion)
        {
            if (!string.IsNullOrWhiteSpace(institucion))
            {
                lv_subjects.ItemsSource = list_kardex.Where(x => x.group.matter.name.ToString().Contains(institucion.ToLower()));
            }
            else
            {
                lv_subjects.ItemsSource = list_kardex;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            lv_subjects.IsVisible = false;
            ai_kardex.IsRunning = true;
            list_kardex = await objWsSubject.getSubjectsStudents(subject.group.idGroup.ToString());
            lv_subjects.ItemsSource = list_kardex;
            ai_kardex.IsRunning = false;
            ai_kardex.IsVisible = false;
            lv_subjects.IsVisible = true;

        }
        protected override bool OnBackButtonPressed()
        {
            PopupNavigation.PopAsync();
            return true;
        }
    }
    class ResultCellSubjectsStudents : ViewCell
    {
        public ResultCellSubjectsStudents()
        {
            int width = 180, heigh = 35;

            var lblstudentname = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = 14,
                HeightRequest = heigh,
                WidthRequest = width,
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            lblstudentname.SetBinding(Label.TextProperty, "student.name");
            var lblstudentnoco = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = 14,
                HeightRequest = heigh,
                WidthRequest = 35,
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            lblstudentnoco.SetBinding(Label.TextProperty, "student.nocontrol");
            var lblstudentimage = new Image
            {
                Opacity = 0.75,
                HeightRequest = 50,
                WidthRequest = 50,
            };
            lblstudentimage.SetBinding(Image.SourceProperty, "student.image");
            var stackList = new StackLayout
            {
                Padding = new Thickness(10),
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    lblstudentimage,
                    new StackLayout()
                    {
                         Padding = new Thickness(10),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Center,
                        Orientation= StackOrientation.Vertical,
                        Children=
                        {
                            lblstudentname,
                            lblstudentnoco,
                        }
                    }
                    
                }
            };
            View = stackList;
        }
    }
    
}
