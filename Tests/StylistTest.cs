using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalonNS
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_StylistEmptyAtFirst()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.Equal(0, result);

    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Steven");
      Stylist secondStylist= new Stylist("Steven");

      //Assert
      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
    public void Test_Save_SavesStylistToDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("Household chores");
      testStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //AssertAssert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToStylistObject()
    {
      //Arrange
      Stylist testStylist = new Stylist("Household chores");
      testStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //AssertAssert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsStylistInDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("Household chores");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.Equal(testStylist, foundStylist);
    }

    [Fact]
    public void Test_GetClients_RetrievesAllClientsWithStylist()
    {
      Stylist testStylist = new Stylist("Household chores");
      testStylist.Save();

      Client firstClient = new Client("Mow the lawn", 1, testStylist.GetId());
      firstClient.Save();

      Client secondClient = new Client("Do the dishes", 1, testStylist.GetId());
      secondClient.Save();

      List<Client> testClientList = new List<Client>{firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();

      Assert.Equal(testClientList, resultClientList);
    }

    // public void Update(string newName)
    // {
    //   SqlConnection conn = DB.Connection();
    //   SqlDataReader rdr;
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("UPDATE stylists SET name = @NewName OUTPUT INSERTED.name WHERE id = @StylistId;", conn);
    //
    //   SqlParameter newNameParameter = new SqlParameter();
    //   newNameParameter.ParameterName = "@NewName";
    //   newNameParameter.Value = newName;
    //   cmd.Parameters.Add(newNameParameter);
    //
    //   SqlParameter stylistIdParameter = new SqlParameter();
    //   stylistIdParameter.ParameterName = "@StylistId";
    //   stylistIdParameter.Value = this.GetId();
    //   cmd.Parameters.Add(stylistIdParameter);
    //   rdr = cmd.ExecuteReader();
    //
    //   while(rdr.Read())
    //   {
    //     this._name = rdr.GetString(0);
    //   }
    //
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }
    //
    // public void Delete()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @StylistId; DELETE FROM clients WHERE stylists_id = @StylistId;", conn);
    //
    //   SqlParameter stylistIdParameter = new SqlParameter();
    //   stylistIdParameter.ParameterName = "@StylistId";
    //   stylistIdParameter.Value = this.GetId();
    //
    //   cmd.Parameters.Add(stylistIdParameter);
    //   cmd.ExecuteNonQuery();
    //
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();

    }
  }
}
