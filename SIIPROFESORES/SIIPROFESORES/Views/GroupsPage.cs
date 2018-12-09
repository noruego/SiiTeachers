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
    class GroupsPage : PopupPage
    {
        private ListView lv_groups;
        private SearchBar sr_groups;
        private ActivityIndicator ai_groups;
        private Label lb_seleccionar, lb_maestro, lb_materia;
        private BoxView bv_div;
        private List<Group> list_groups;
        private WsGroups objWsGroups;
        private WsSubjects objWsSubjects;
        private StackLayout st_inst;
        private Frame fm;

        public GroupsPage()
        {
            objWsGroups = new WsGroups();
            objWsSubjects = new WsSubjects();
            fm = new Frame {
            Margin =25,
            Padding =25
            };
            
            createGUI();
        }

        private void createGUI()
        {
            //NavigationPage.SetHasNavigationBar(this, false); //Quitar barra de navegacion 
            ai_groups = new ActivityIndicator()
            {
                Color = Color.Green,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            sr_groups = new SearchBar()
            {
                HorizontalTextAlignment = TextAlignment.Start,
                Placeholder = "Buscar registro",
                FontSize = 16,
                TextColor = Color.Black
            };
            sr_groups.TextChanged += (sender, e) => buscarInstitucion(sr_groups.Text);

            lv_groups = new ListView();
            lv_groups.HasUnevenRows = true;
            lv_groups.ItemTemplate = new DataTemplate(typeof(ResultCellGroups));
            lv_groups.ItemSelected += async (sender, e) =>
            {

                Group sub = (Group)e.SelectedItem;
                try
                {
                    if (await objWsSubjects.putSubjects(sub))
                    {
                        
                        DisplayAlert("Correcto", "Materia agregada", "Aceptar");
                        //App.Current.MainPage = new DashBoard();
                    }
                }
                catch (Exception ex) { await DisplayAlert("Alert", ex.ToString(), "Aceptar"); }
                //DisplayAlert("Itemselected", Settings.Settings.institucionName + "\n" +
                //Agregar con settings 
                // App.Current.MainPage = new DashBoard();
            };
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
                Text = "Terminar",
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            var tapr = new TapGestureRecognizer();
            tapr.Tapped += (sender, e) =>
            {
                PopupNavigation.PopAsync();
                //asignar ciertas carcateristicas de un tecnologico especifico
            };
            lb_maestro = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Maestro",
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
                    sr_groups,
                    new StackLayout(){
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        Padding = new Thickness (30),
                        Children ={lb_materia,lb_maestro }
                    },
                    ai_groups,
                    lv_groups,
                    bv_div,
                    lb_seleccionar
                }
            };
            fm.Content = st_inst;
            Content = fm;
        }

        private void buscarInstitucion(string institucion)
        {
            if (!string.IsNullOrWhiteSpace(institucion))
            {
                lv_groups.ItemsSource = list_groups.Where(x => x.matter.name.ToString().Contains(institucion.ToLower()));
            }
            else
            {
                lv_groups.ItemsSource = list_groups;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            lv_groups.IsVisible = false;
            ai_groups.IsRunning = true;
            list_groups = await objWsGroups.getGroups();
            lv_groups.ItemsSource = list_groups;
            ai_groups.IsRunning = false;
            ai_groups.IsVisible = false;
            lv_groups.IsVisible = true;

        }

    }
    class ResultCellGroups : ViewCell
    {
        public ResultCellGroups()
        {
            int width = 180, heigh = 35;

            var lblmateria = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = 14,
                HeightRequest = heigh,
                WidthRequest = width,
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            lblmateria.SetBinding(Label.TextProperty, "matter.name");
            var lblPeriodo = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = 14,
                HeightRequest = heigh,
                WidthRequest = 35,
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            lblPeriodo.SetBinding(Label.TextProperty, "teacher.name");

            var stackList = new StackLayout
            {
                Padding = new Thickness(10),
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    lblmateria,
                    lblPeriodo,
                }
            };
            View = stackList;
        }
    }
}
