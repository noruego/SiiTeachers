using SIIPROFESORES.Models;
using SIIPROFESORES.WebServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SIIPROFESORES.Views
{ 
    class KardexPage : ContentPage
    {
        private ListView lv_kardex;
        private SearchBar sr_kardex;
        private ActivityIndicator ai_kardex;
        private Label lb_nmateria, lb_oportunidad, lb_calificacion, lb_semestre;
        private BoxView bv_div;
        private List<Kardex> list_kardex;
        private WsKardex objWskardex;
        private StackLayout st_inst;
        private ScrollView scv_hor;

        public KardexPage()
        {
            objWskardex = new WsKardex();
            createGUI();
            scv_hor = new ScrollView();
            scv_hor.Orientation = ScrollOrientation.Horizontal;
        }

        private void createGUI()
        {
            NavigationPage.SetHasNavigationBar(this, false); //Quitar barra de navegacion 
            ai_kardex= new ActivityIndicator()
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

            DataTemplate celda = new DataTemplate(typeof(ImageCell));
            celda.SetBinding(TextCell.TextProperty, "institution");
            celda.SetValue(TextCell.TextColorProperty, Color.Gray);
            celda.SetBinding(TextCell.DetailProperty, "short_name");
            celda.SetValue(TextCell.TextColorProperty, Color.Blue);
            celda.SetBinding(ImageCell.ImageSourceProperty, "logo");

            lv_kardex = new ListView();
            lv_kardex.HasUnevenRows = true;
            lv_kardex.ItemTemplate = new DataTemplate(typeof(ResultCell));


           
            /*lv_kardex.ItemSelected += (sender, e) =>
            {

                Institucion objIns = (Institucion)e.SelectedItem;

                Settings.Settings.institucionName = objIns.institution;
                Settings.Settings.institucionShortName = objIns.short_name;
                Settings.Settings.institucionLogo = objIns.logo;

                DisplayAlert("Itemselected", Settings.Settings.institucionName + "\n" +
                    Settings.Settings.institucionShortName + "\n"
                    + Settings.Settings.institucionLogo + "\n", "Aceptar");
                //Agregar con settings 
                App.Current.MainPage = new DashBoard();
            };*/
            bv_div = new BoxView()
            {
                Color = Color.Green,
                HeightRequest = 2,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 300
            };
            lb_nmateria = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Materia",
                FontSize = 12,
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            /* var tapr = new TapGestureRecognizer();
             tapr.Tapped += (sender, e) =>
             {
                 DisplayAlert("error", "selecciono inguno", "aceptar");
                 //asignar ciertas carcateristicas de un tecnologico especifico
             };
             lb_nmateria.GestureRecognizers.Add(tapr);
             */
            lb_oportunidad = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Oportunidad",
                TextColor = Color.Gray,
                FontSize = 12,
                FontFamily = "Roboto"
            };
            lb_calificacion = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Calificacion",
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Gray,
                FontSize = 12,
                FontFamily = "Roboto"
            };
            lb_semestre = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Semestre",
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Gray,
                FontSize = 12,
                FontFamily = "Roboto"
            };

            st_inst = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(20),
                Children =
                {
                    sr_kardex,
                    ai_kardex,
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Padding = new Thickness(20),
                        Children ={ lb_nmateria,lb_oportunidad,lb_calificacion,lb_semestre}
                    },
                    lv_kardex,
                    bv_div,
                    
                }
            };
            
            Content = new ScrollView
            {
                Orientation = ScrollOrientation.Horizontal,
                Content = st_inst
            };
        }

        private void buscarInstitucion(string institucion)
        {
            if (!string.IsNullOrWhiteSpace(institucion))
            {
                lv_kardex.ItemsSource = list_kardex.Where(x => x.matter.name.ToLower().Contains(institucion.ToLower()));
            }
            else
            {
                lv_kardex.ItemsSource = list_kardex;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            lv_kardex.IsVisible = false;
            ai_kardex.IsRunning = true;
            list_kardex = await objWskardex.getKardex();
            lv_kardex.ItemsSource = list_kardex;
            ai_kardex.IsRunning = false;
            ai_kardex.IsVisible = false;
            lv_kardex.IsVisible = true;
             
        }
        protected override bool OnBackButtonPressed()
        {
            DisplayAlert("UPS!", "selecciona una institución", "Aceptar");
            return true;
        }
        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

    }
    class ResultCell : ViewCell
    {
        public ResultCell()
        {
            int width = 150,heigh =35;

            var lblmateria = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = 14,
                HeightRequest =heigh,
                WidthRequest =width,
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            lblmateria.SetBinding(Label.TextProperty, "matter.name");
            var lblOportunity = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = 14,
                HeightRequest = heigh,
                WidthRequest = 100,
                TextColor = Color.Gray,
                FontFamily = "Roboto"
            };
            lblOportunity.SetBinding(Label.TextProperty, "oportunity.description");
            var lblQualification = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = 14,
                HeightRequest = heigh,
                WidthRequest = 80,
                TextColor = Color.Gray,
                FontFamily = "Roboto",
                FontAttributes = FontAttributes.Bold
            };
            lblQualification.SetBinding(Label.TextProperty, "qualification");

            var lblSemester = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = 14,
                HeightRequest = heigh,
               WidthRequest =80,
                TextColor = Color.Gray,
                FontFamily = "Roboto",

            };
            lblSemester.SetBinding(Label.TextProperty, "semester");

            var stackList = new StackLayout
            {
                Padding = new Thickness(10),
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    lblmateria,
                    lblOportunity,
                    lblQualification,
                    lblSemester
                }
            };
        var moreAction = new MenuItem { Text = "More" };
        moreAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
        moreAction.Clicked += async (sender, e) => {
            var mi = ((MenuItem)sender);
            Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);
        };

        var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
        deleteAction.SetBinding (MenuItem.CommandParameterProperty, new Binding ("."));
        deleteAction.Clicked += async (sender, e) => {
            var mi = ((MenuItem)sender);
            Debug.WriteLine("Delete Context Action clicked: " + mi.CommandParameter);
        };
        // add to the ViewCell's ContextActions property
        ContextActions.Add (moreAction);
        ContextActions.Add (deleteAction);
            View = stackList;
        }
       
    }
}
