using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SIIPROFESORES.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace SIIPROFESORES.WebServices
{
    class WsKardex
    {
        public async Task<List<Kardex>> getKardex()
        {
            List<Kardex> listKardex = new List<Kardex>();
            try
            {
                HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Settings.ip);
            var authData = string.Format("{0}:{1}", "root", "root");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            var resp = await httpClient.GetAsync("/SIIWS_PATM/api/wskardex/getkardex/" + Settings.idTeacher + "/" +Settings.token);

            var cadena = resp.Content.ReadAsStringAsync().Result;
            var objJson = JObject.Parse(cadena);

            var arrJson = objJson.SelectToken("kardex").ToList();

           
                foreach (var institucion in arrJson)
                {
                    Kardex kardex = new Kardex();
                    //kardex = JsonConvert.DeserializeObject<Kardex>(institucion.ToString());
                    Object obj = institucion["student"].ToObject<Student>();
                    kardex.student = institucion["student"].ToObject<Student>();
                    kardex.oportunity = institucion["opportunity"].ToObject<Oportunity>();
                    kardex.matter = institucion["matter"].ToObject<Matter>();
                    kardex.qualification = Convert.ToInt32(institucion["qualification"]);
                    kardex.semester = Convert.ToInt32(institucion["semester"]);
                    listKardex.Add(kardex);
                }
            }
            catch (Exception e)
            {

                e.ToString();
            }

            return listKardex;
        }
    }
}
