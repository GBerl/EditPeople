using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace People.Data
{
    public class Peopledb
    {
        private string _connection;
        public Peopledb(string connection)
        {
            _connection = connection;
        }
        public List<Person> GetAllPeople()
        {
            using (SqlConnection conn = new SqlConnection(_connection))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "Select * from People";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Person> people = new List<Person>();
                while (reader.Read())
                {
                    people.Add(new Person
                    {

                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"]

                    });
                }
                return people;
            }
        }
        public void Edit(Person p)
        {
            using (SqlConnection conn = new SqlConnection(_connection))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "update people set firstname=@fname," +
                                   "lastname=@lname,age=@age " +
                                   "where id =@id";
                cmd.Parameters.AddWithValue("@fname", p.FirstName);
                cmd.Parameters.AddWithValue("@lname", p.LastName);
                cmd.Parameters.AddWithValue("@age", p.Age);
                cmd.Parameters.AddWithValue("@id", p.Id);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }
        public void Delete(Person p)
        {
            using (SqlConnection conn = new SqlConnection(_connection))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "delete people " +
                                   "where id =@id and firstname=@fname and " +
                                   "lastname=@lname and age=@age";
                                   
                cmd.Parameters.AddWithValue("@fname", p.FirstName);
                cmd.Parameters.AddWithValue("@lname", p.LastName);
                cmd.Parameters.AddWithValue("@age", p.Age);
                cmd.Parameters.AddWithValue("@id", p.Id);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }
        public void Add(Person p)
        {
            using (SqlConnection conn = new SqlConnection(_connection))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "insert into people(firstname, lastname, age)" +
                                   "values(@fname, @lname, @age)";


                cmd.Parameters.AddWithValue("@fname", p.FirstName);
                cmd.Parameters.AddWithValue("@lname", p.LastName);
                cmd.Parameters.AddWithValue("@age", p.Age);
            
                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }
    }
}
