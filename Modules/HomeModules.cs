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
        return View["index.cshtml"];
      };

      Get["stylists"] = _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylist.cshtml", AllStylists];
      };

      Post["/stylists/new"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        return View["stylist.cshtml", Stylist.GetAll()];
      };

      Get["/stylists/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();

        var SelectedStylist = Stylist.Find(parameters.id);
        var StylistClients = SelectedStylist.GetClients();
        List<Stylist> AllStylists = Stylist.GetAll();

        model.Add("stylist", SelectedStylist);
        model.Add("client", StylistClients);
        model.Add("stylists", AllStylists);

        return View["client.cshtml", model];
      };

      Post["/stylists/{id}/new"] = parameters => {
        var newClient = new Client(Request.Form["new-name"], Request.Form["new-phone"], Request.Form["stylist-id"]);
        newClient.Save();

        var model = new Dictionary<string, object>();

        var SelectedStylist = Stylist.Find(parameters.id);
        var StylistClients = SelectedStylist.GetClients();
        List<Stylist> AllStylists = Stylist.GetAll();

        model.Add("stylist", SelectedStylist);
        model.Add("client", StylistClients);
        model.Add("stylists", AllStylists);

        return View["client.cshtml", model];
      };

      Get["/stylists/edit/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        return View["stylist_edit.cshtml", SelectedStylist];
      };

      Patch["/stylists/edit/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        SelectedStylist.Update(Request.Form["stylist-name"]);

        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylist.cshtml", AllStylists];
      };

      Delete["/stylists/delete/{id}"] = parameters => {
        Stylist SelectedStylist = Stylist.Find(parameters.id);
        SelectedStylist.Delete();

        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylist.cshtml", AllStylists];
      };
      // client

      Get["/clients/edit/{id}"] = parameters => {
        Client SelectedClient = Client.Find(parameters.id);
        return View["client_edit.cshtml", SelectedClient];
      };

      Patch["/clients/edit/{id}"] = parameters => {
        Client SelectedClient = Client.Find(parameters.id);
        SelectedClient.Update(Request.Form["client-name"]);

        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist SelectedStylist = Stylist.Find(SelectedClient.GetStylistId());
        List<Client> StylistClient = SelectedStylist.GetClients();
        List<Stylist> AllStylists = Stylist.GetAll();
        model.Add("stylist", SelectedStylist);
        model.Add("client", StylistClient);
        model.Add("stylists", AllStylists);
        return View["client.cshtml", model];
      };

      Delete["/clients/delete/{id}"] = parameters => {
        Client SelectedClient = Client.Find(parameters.id);
        SelectedClient.Delete();

        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist SelectedStylist = Stylist.Find(SelectedClient.GetStylistId());
        List<Client> StylistClient = SelectedStylist.GetClients();
        List<Stylist> AllStylists = Stylist.GetAll();
        model.Add("stylist", SelectedStylist);
        model.Add("client", StylistClient);
        model.Add("stylists", AllStylists);
        return View["client.cshtml", model];
      };

    }
  }
}
