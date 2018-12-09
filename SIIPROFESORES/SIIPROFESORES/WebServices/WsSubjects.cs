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
    class WsSubjects
    {
        public async Task<List<Subjects>> getSubjects()
        {
            List<Subjects> listSubjects = new List<Subjects>();
            List<string> listString = new List<string>();
            try
            {
                HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Settings.ip);
            var authData = string.Format("{0}:{1}", "root", "root");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            var resp = await httpClient.GetAsync("SIIWS_PATM/api/wslista/getlistaProf/" + Settings.idTeacher + "/" +Settings.token);

            var cadena = resp.Content.ReadAsStringAsync().Result;
            var objJson = JObject.Parse(cadena);

            var arrJson = objJson.SelectToken("lista").ToList();

           
                foreach (var institucion in arrJson)
                {
                    Subjects subject = new Subjects();
                    //kardex = JsonConvert.DeserializeObject<Kardex>(institucion.ToString());
                    Object obj = institucion["student"].ToObject<Student>();
                    subject.student = institucion["student"].ToObject<Student>();
                    subject.period = institucion["periodo"].ToString();
                    subject.group = institucion["group"].ToObject<Group>();
                    subject.calificacion1 = Int32.Parse(institucion["calificacion1"].ToString());
                    subject.calificacion2 = Int32.Parse(institucion["calificacion2"].ToString());
                    subject.calificacion3 = Int32.Parse(institucion["calificacion3"].ToString());
                    subject.calificacion4 = Int32.Parse(institucion["calificacion4"].ToString());
                    subject.promedio = (subject.calificacion1+subject.calificacion2+subject.calificacion3+subject.calificacion4)/4;
                    listString.Add(subject.group.idGroup.ToString());
                    if(listString.Contains(subject.group.idGroup.ToString()))
                    {
                        listSubjects.Add(subject);
                    }
                        
                    
                }
            }
            catch (Exception e)
            {

                e.ToString();
            }

            return listSubjects;
        }
        public async Task<List<Subjects>> getSubjectsStudents(string idgroup)
        {
            List<Subjects> listSubjects = new List<Subjects>();
            List<string> listString = new List<string>();
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(Settings.ip);
                var authData = string.Format("{0}:{1}", "root", "root");
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
                var resp = await httpClient.GetAsync("SIIWS_PATM/api/wslista/getlistaStudents/" + idgroup + "/" + Settings.token);

                var cadena = resp.Content.ReadAsStringAsync().Result;
                var objJson = JObject.Parse(cadena);

                var arrJson = objJson.SelectToken("lista").ToList();


                foreach (var institucion in arrJson)
                {
                    Subjects subject = new Subjects();
                    //kardex = JsonConvert.DeserializeObject<Kardex>(institucion.ToString());
                    Object obj = institucion["student"].ToObject<Student>();
                    subject.student = institucion["student"].ToObject<Student>();
                    subject.period = institucion["periodo"].ToString();
                    subject.group = institucion["group"].ToObject<Group>();
                    subject.calificacion1 = Int32.Parse(institucion["calificacion1"].ToString());
                    subject.calificacion2 = Int32.Parse(institucion["calificacion2"].ToString());
                    subject.calificacion3 = Int32.Parse(institucion["calificacion3"].ToString());
                    subject.calificacion4 = Int32.Parse(institucion["calificacion4"].ToString());
                    subject.promedio = (subject.calificacion1 + subject.calificacion2 + subject.calificacion3 + subject.calificacion4) / 4;
                    listString.Add(subject.group.idGroup.ToString());
                    if (listString.Contains(subject.group.idGroup.ToString()))
                    {
                        listSubjects.Add(subject);
                    }


                }
            }
            catch (Exception e)
            {

                e.ToString();
            }

            return listSubjects;
        }

        public  async Task<Boolean> delete(int idGroup)
        {
            Boolean flag = false;
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(Settings.ip);
                var authData = string.Format("{0}:{1}", "root", "root");
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
                //var resp = await httpClient.GetAsync("SIIWS_PATM/api/wslista/getlista/" + Settings.idStudent + "/" + Settings.token);
                var resp = await httpClient.DeleteAsync("/SIIWS_PATM/api/wslista/deletelista/" + idGroup + "/" + Settings.idTeacher + "/" + Settings.token);

                if (resp.IsSuccessStatusCode)
                    flag = true;
            }
            catch (Exception ex) { ex.ToString(); }
            return flag;

        }

        public async Task<Boolean> putSubjects(Group gr)
        {
            Boolean flag = false;
            List<Subjects> listSubjects = new List<Subjects>();
            try
            {
                
                WsStudent wsStudent = new WsStudent();
                //var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
                Subjects sub = new Subjects();
                sub.student = await wsStudent.getStudent();
                sub.student.idstudent = Int32.Parse(Settings.idTeacher);
                sub.group = gr;
                sub.period = "Agosto-Diciembre";
                var json = JsonConvert.SerializeObject(sub);
                //var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(Settings.ip);
                var authData = string.Format("{0}:{1}", "root", "root");
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

                //var resp = await httpClient.GetAsync("SIIWS_PATM/api/wslista/getlista/" + Settings.idStudent + "/" + Settings.token);
                var resp = await httpClient.PostAsync("SIIWS_PATM/api/wslista/postlista/" + Settings.token, content);
                if (resp.IsSuccessStatusCode)
                    flag = true;
                /*
                var cadena = resp.Content.ReadAsStringAsync().Result;
                var objJson = JObject.Parse(cadena);

                var arrJson = objJson.SelectToken("lista").ToList();


                foreach (var institucion in arrJson)
                {
                    Subjects subject = new Subjects();
                    //kardex = JsonConvert.DeserializeObject<Kardex>(institucion.ToString());
                    Object obj = institucion["student"].ToObject<Student>();
                    subject.student = institucion["student"].ToObject<Student>();
                    subject.period = institucion["periodo"].ToString();
                    subject.group = institucion["group"].ToObject<Group>();
                    listSubjects.Add(subject);
                }*/
            }
            catch (Exception e)
            {

                e.ToString();
            }

            return flag;
        }
        public async Task<Boolean> updateCalif(Subjects sub)
        {
            Boolean flag = false;
            try
            {

               
                var json = JsonConvert.SerializeObject(sub);
                //var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(Settings.ip);
                var authData = string.Format("{0}:{1}", "root", "root");
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

                //var resp = await httpClient.GetAsync("SIIWS_PATM/api/wslista/getlista/" + Settings.idStudent + "/" + Settings.token);
                var resp = await httpClient.PutAsync("SIIWS_PATM/api/wslista/updateCalifi/" + Settings.token, content);
                if (resp.IsSuccessStatusCode)
                    flag = true;
                /*
                var cadena = resp.Content.ReadAsStringAsync().Result;
                var objJson = JObject.Parse(cadena);

                var arrJson = objJson.SelectToken("lista").ToList();


                foreach (var institucion in arrJson)
                {
                    Subjects subject = new Subjects();
                    //kardex = JsonConvert.DeserializeObject<Kardex>(institucion.ToString());
                    Object obj = institucion["student"].ToObject<Student>();
                    subject.student = institucion["student"].ToObject<Student>();
                    subject.period = institucion["periodo"].ToString();
                    subject.group = institucion["group"].ToObject<Group>();
                    listSubjects.Add(subject);
                }*/
            }
            catch (Exception e)
            {

                e.ToString();
            }

            return flag;
        }
    }
}
