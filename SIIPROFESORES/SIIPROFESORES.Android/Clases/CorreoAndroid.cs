using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SII.Droid.Clases;
using SIIPROFESORES.Droid;
using SIIPROFESORES.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(CorreoAndroid))]
namespace SII.Droid.Clases
{
    public class CorreoAndroid : ICorreo
    {
        void ICorreo.CrearCorreo(string direccion, string asunto, string mensaje)
        {
            var email = new Intent(Intent.ActionSend);

            email.PutExtra(Intent.ExtraEmail, new string[] { direccion });
            email.PutExtra(Intent.ExtraSubject, asunto);
            email.PutExtra(Intent.ExtraText, mensaje);
            email.SetType("message/rfc822");

            MainActivity.Instance.StartActivity(email);
        }
    }

    
}