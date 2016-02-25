using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FavoriteRestaurants
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurantsByCuisine_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfNameIsTheSame()
    {
      //Arrange, Act
      Restaurant Restaurant1 = new Restaurant("Larrys Hoagies","Big sandwiches",1);
      Restaurant Restaurant2 = new Restaurant("Larrys Hoagies","Big sandwiches",1);

      //Assert
      Assert.Equal(Restaurant1.GetName(), Restaurant2.GetName());
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Larrys Hoagies","Big sandwiches",1);

      //Act
      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList[0].GetName(), result[0].GetName());
    }
    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("larrys Hoagies","Big sandwiches",1);

      //Act
      testRestaurant.Save();
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetCuisineId();
      int testId = testRestaurant.GetId();

      //Assert
      Assert.Equal(testId, result);
    }
    [Fact]
    public void Test_Find_FindsRestaurantInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Larrys Hoagies","Big sandwiches",1);
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      //Assert
      Assert.Equal(testRestaurant.GetName(), foundRestaurant.GetName());
    }
    [Fact]
    public void Test_FindByCuisine_FindsRestaurantsInDatabaseByCuisine()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Larrys Hoagies","Big sandwiches",1);
      Restaurant OtherTestRestaurant = new Restaurant("Mitch's Subs","Really big sandwiches",1);
      testRestaurant.Save();
      OtherTestRestaurant.Save();
      string otherReturnString = "";
      //Act
      List<Restaurant> restaurantsByCuisine = Restaurant.GetByCuisine(testRestaurant.GetCuisineId());
      foreach (Restaurant restaurant in restaurantsByCuisine)
      {
        otherReturnString = otherReturnString + restaurant.GetName() + ", ";
      }
      otherReturnString = otherReturnString.Remove(otherReturnString.Length - 2);
      string testString = testRestaurant.GetName() + ", " + OtherTestRestaurant.GetName();
      //Assert
      Assert.Equal(testString, otherReturnString);
    }
    [Fact]
    public void Test_DeleteAll_DeletesAllRestaurantsInDatabase()
    {
      //Arrange
      RestaurantTest testRestaurant = new RestaurantTest();
      testRestaurant.Dispose();

      //Act
      int result = Restaurant.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Find_FindsRestaurantDescripeInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Larrys Hoagies","Big sandwiches",1);
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      //Assert
      Assert.Equal(testRestaurant.GetDescription(), foundRestaurant.GetDescription());
    }
  }
}
