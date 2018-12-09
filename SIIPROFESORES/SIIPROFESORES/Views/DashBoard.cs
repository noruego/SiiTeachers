using SIIPROFESORES.Models;
using SIIPROFESORES.WebServices;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SIIPROFESORES.Views
{
    class DashBoard : MasterDetailPage
    {
        private Student student;
        private WsStudent objWsStu;
        private MenuDashBoard menuPage;
        private string sportSelected;
        private Fondo fondo;
        public DashBoard()
        {
            crearGuiAsync();
        }
        public DashBoard(MenuOpcion me)
        {
            
            NavigationTo(me);
            crearGuiAsync();
        }
        private async System.Threading.Tasks.Task crearGuiAsync()
        {
            menuPage = new MenuDashBoard();
            fondo = new Fondo();
            menuPage.OpcionesMenu.ItemSelected += (sender, e) => NavigationTo(e.SelectedItem as MenuOpcion);
            /*ToolbarItems.Add(
                new ToolbarItem
                {
                    Icon = "icon.png",
                    Text = "Selección de disciplinas",
                    Order = ToolbarItemOrder.Primary,
                    Command = new Command(async () =>
                    {
                        sportSelected = await App.Current.MainPage.DisplayActionSheet("Disciplinas", "Cancelar", null,
                            "Ajedrez", "Atletismo", "Básquetbol", "Béisbol",
                            "Fútbol", "Natación", "Tenis",
                            "Vóleibol de Playa", "Vóleibol de Sala");
                        if (sportSelected == null)
                            sportSelected = App.SportTitle;
                        if (!sportSelected.Equals("Cancelar"))
                        {
                            App.SportTitle = sportSelected;
                            switch (App.SportTitle)
                            {
                                case "Ajedrez":
                                    App.SportNumber = 1;
                                    App.tipoCompetencia = 1; //Individual
                                    break;
                                case "Atletismo":
                                    App.SportNumber = 2;
                                    App.tipoCompetencia = 2; //Equipo
                                    break;
                                case "Básquetbol":
                                    App.SportNumber = 3;
                                    App.tipoCompetencia = 2;
                                    break;
                                case "Béisbol":
                                    App.SportNumber = 4;
                                    App.tipoCompetencia = 2;
                                    break;
                                case "Fútbol":
                                    App.SportNumber = 5;
                                    App.tipoCompetencia = 2;
                                    break;
                                case "Natación":
                                    App.SportNumber = 6;
                                    App.tipoCompetencia = 2;
                                    break;
                                case "Tenis":
                                    App.SportNumber = 7;
                                    App.tipoCompetencia = 1;
                                    break;
                                case "Vóleibol de Playa":
                                    App.SportNumber = 8;
                                    App.tipoCompetencia = 2;
                                    break;
                                case "Vóleibol de Sala":
                                    App.SportNumber = 9;
                                    App.tipoCompetencia = 2;
                                    break;
                            }
                        }
                    })
                }
            );*/
            Master = menuPage;
            DataPage data = new DataPage();
            Detail = new NavigationPage(data);
        }
        private void NavigationTo(MenuOpcion item)
        {
            try
            {
                if (!item.TargetType.Name.ToString().Equals("CuentaPage"))
                {
                    Page pagina = (Page)Activator.CreateInstance(item.TargetType);//crear instancia de pagina

                    switch (pagina.GetType().Name)
                    {
                        case "DataPage":
                            Detail = new NavigationPage(pagina);
                            IsPresented = false;
                            break;
                        case "SubjectsPage":
                            Detail = new NavigationPage(pagina);
                            IsPresented = false;
                            break;
                        case "CalificationsPage":
                            Detail = new NavigationPage(pagina);
                            IsPresented = false;
                            break;
                        case "KardexPage":
                            Detail = new NavigationPage(pagina);
                            IsPresented = false;
                            break;
                        case "CuentaPage":
                            //Detail = new NavigationPage(pagina);
                            IsPresented = false;
                            break;
                    }

                }
                else { Navigation.PopModalAsync(); }
                
                

            }
            catch (Exception e) { DisplayAlert("", e.StackTrace, "Aceptar"); }
            
        }

    }
}
