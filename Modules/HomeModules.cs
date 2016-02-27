using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System;

namespace HairSalonNS
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      // stylist
      Get["/"] = _ => {
        return View["index.cshtml", Stylist.GetAll()];
      };

      Post["/"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        return View["index.cshtml", Stylist.GetAll()];
      };

      Get["/stylists/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedStylist = Stylist.Find(parameters.id);
        var StylistClients = SelectedStylist.GetClients();
        List<Stylist>
        model.Add("stylist", SelectedStylist);
        model.Add("client", StylistClients);
        return View["client.cshtml", model];
      };
      // client


      Get["/clients"] = _ => {
        List<Client> AllClients = Client.GetAll();
        return View["client.cshtml", AllClients];
      };

      Post["/clients"] = _ => {
        Client newClient = new Client(Request.Form["client-name"], Request.Form["client-phone"], Request.Form["stylist-id"]);
        newClient.Save();
        return View["client.cshtml"];
      };

    }
  }
}
