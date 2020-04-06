using PFinalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace PFinalWeb
{
    /// <summary>
    /// Summary description for Coords
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Coords : System.Web.Services.WebService
    {

        [WebMethod]
        public void GetInfoCaso()
        {
            List<string[]> casos = new List<string[]>();
            using (var ctx = new PFinalWebContext())
            {
                var a = (from i in ctx.Casos
                        select i).ToList();
                foreach(var i in a)
                {
                    string[] caso = { 
                        i.IDCaso.ToString(),
                        i.Nombre, 
                        i.Apellido,
                        i.Latitud.ToString(), 
                        i.Longitud.ToString(),
                        i.FechaNacimiento.ToShortDateString()
                    };
                    casos.Add(caso);
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(casos));
        }
    }
}
