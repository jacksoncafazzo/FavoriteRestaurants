using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace FavoriteRestaurants
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private int _cuisineId;
    private string _description;

    public Restaurant(string Name, string Description, int CuisineId, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _cuisineId = CuisineId;
      _description = Description;
    }
    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool NameEquality = (this.GetName() == newRestaurant.GetName());
        bool IdEquality = (this.GetId() == newRestaurant.GetId());
        bool CuisineIdEquality = (this.GetCuisineId() == newRestaurant.GetCuisineId());
        return (NameEquality && IdEquality && CuisineIdEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public int GetCuisineId()
    {
      return _cuisineId;
    }
    public string GetDescription()
    {
      return _description;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, description, cuisine_id) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantDescription, @CuisineId);", conn);

      SqlParameter NameParameter = new SqlParameter();
      NameParameter.ParameterName = "@RestaurantName";
      NameParameter.Value = this.GetName();
      cmd.Parameters.Add(NameParameter);

      SqlParameter DescriptionParameter = new SqlParameter();
      DescriptionParameter.ParameterName = "@RestaurantDescription";
      DescriptionParameter.Value = this.GetDescription();
      cmd.Parameters.Add(DescriptionParameter);

      SqlParameter CuisineIdParameter = new SqlParameter();
      CuisineIdParameter.ParameterName = "@CuisineId";
      CuisineIdParameter.Value = this.GetCuisineId();
      cmd.Parameters.Add(CuisineIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int RestaurantId = rdr.GetInt32(0);
        string RestaurantName = rdr.GetString(1);
        int newCuisineId = rdr.GetInt32(2);
        string RestaurantDescription = rdr.GetString(3);
        Restaurant newRestaurant = new Restaurant(RestaurantName, RestaurantDescription, RestaurantId, newCuisineId);
        allRestaurants.Add(newRestaurant);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allRestaurants;
    }
    public static List<Restaurant> GetByCuisine(int cuisineId)
    {
      List<Restaurant> restaurantsByCuisine = new List<Restaurant>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE cuisine_id = @CuisineId;", conn);
      SqlParameter RestaurantCuisineIdParameter = new SqlParameter();
      RestaurantCuisineIdParameter.ParameterName = "@CuisineId";
      RestaurantCuisineIdParameter.Value = cuisineId;
      cmd.Parameters.Add(RestaurantCuisineIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int RestaurantId = rdr.GetInt32(0);
        string RestaurantName = rdr.GetString(1);
        int newCuisineId = rdr.GetInt32(2);
        string RestaurantDescription = rdr.GetString(3);
        Restaurant newRestaurant = new Restaurant(RestaurantName, RestaurantDescription, RestaurantId, newCuisineId);
        restaurantsByCuisine.Add(newRestaurant);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return restaurantsByCuisine;
    }
    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);
      SqlParameter RestaurantIdParameter = new SqlParameter();
      RestaurantIdParameter.ParameterName = "@RestaurantId";
      RestaurantIdParameter.Value = id.ToString();
      cmd.Parameters.Add(RestaurantIdParameter);
      rdr = cmd.ExecuteReader();

      int foundRestaurantId = 0;
      string foundRestaurantName = null;
      string foundRestaurantDescription = null;
      int foundRestaurantCuisineId = 0;
      while(rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        foundRestaurantName = rdr.GetString(1);
        foundRestaurantDescription = rdr.GetString(3);
        foundRestaurantCuisineId = rdr.GetInt32(2);
      }
      Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundRestaurantDescription, foundRestaurantCuisineId, foundRestaurantId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundRestaurant;
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines WHERE id = @CuisineId; DELETE FROM restaurants WHERE cuisine_id = @CuisineId;", conn);

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = this.GetId();

      cmd.Parameters.Add(cuisineIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
