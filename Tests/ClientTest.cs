using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalonNS
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }


    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Client.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      //Arrange, Act
      Client firstClient = new Client("Mow the lawn", 1, 1);
      Client secondClient = new Client("Mow the lawn", 1, 1);

      //Assert
      Assert.Equal(firstClient, secondClient);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Client testClient = new Client("Mow the lawn", 1, 1);

      //Act
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsClientInDatabase()
    {
      //Arrange
      Client testClient = new Client("Mow the lawn", 1, 1);
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.GetId());

      //Assert
      Assert.Equal(testClient, foundClient);
    }

    // [Fact]
    // public void Test_Update_UpdatesClientInDatabase()
    // {
    //   //Arrange
    //   string name = "Home stuff";
    //   Client testClient = new Client(name);
    //   testClient.Save();
    //   string newName = "Work stuff";
    //
    //   //Act
    //   testClient.Update(newName);
    //
    //   string result = testClient.GetName();
    //
    //   //Assert
    //   Assert.Equal(newName, result);
    // }
    //
    // [Fact]
    // public void Test_Delete_DeletesClientFromDatabase()
    // {
    //   //Arrange
    //   string name1 = "Home stuff";
    //   Client testClient1 = new Client(name1);
    //   testClient1.Save();
    //
    //   string name2 = "Work stuff";
    //   Client testClient2 = new Client(name2);
    //   testClient2.Save();
    //
    //   Client testClient1 = new Client("Mow the lawn", 1, testClient1.GetId());
    //   testClient1.Save();
    //   Client testClient2 = new Client("Send emails", 1, testClient2.GetId());
    //   testClient2.Save();
    //
    //   //Act
    //   testClient1.Delete();
    //   List<Client> resultClients = Client.GetAll();
    //   List<Client> testClientList = new List<Client> {testClient2};
    //
    //   List<Client> resultClients = Client.GetAll();
    //   List<Client> testClientList = new List<Client> {testClient2};
    //
    //   //Assert
    //   Assert.Equal(testClientList, resultClients);
    //   Assert.Equal(testClientList, resultClients);
    //
    // }

    [Fact]

    public void Dispose()
    {
      Client.DeleteAll();
    }


  }
}
