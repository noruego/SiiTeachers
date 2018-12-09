using SIIPROFESORES.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIPROFESORES.Views
{
    class MenuDataOptions : List<MenuOpcion>
    {
        public MenuDataOptions()
        {
            this.Add(new MenuOpcion()
            {
                Title = "Mis datos",
                IconSource = "data_icon.png",
                TargetType = typeof(DataPage)
            });
            this.Add(new MenuOpcion()
            {
                Title = "Grupos",
                IconSource = "subject_icon.png",
                TargetType = typeof(SubjectsPage),
            });
            this.Add(new MenuOpcion()
            {
                Title = "Cerrar sesión",
                IconSource = "count_icon.png",
                TargetType = typeof(CuentaPage),
            });

        }
    }
}
