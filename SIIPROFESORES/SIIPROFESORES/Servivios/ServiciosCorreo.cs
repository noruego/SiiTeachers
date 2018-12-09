
using SIIPROFESORES.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SII.Servivios
{
    public static class ServiciosCorreo
    {
        public static void EnviarCorreo(string direccion,string asunto,string mensaje)
        {
            var correo = DependencyService.Get<ICorreo>();
            correo.CrearCorreo(direccion, asunto, mensaje);
        }
    }
}
