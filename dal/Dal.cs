using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using malshinon1.people;
using MySql.Data.MySqlClient;

namespace malshinon1.dal
{
    internal class Dal
    {
        private string ConnStr = "server=localhost;username=root;password=;database=malshinon";
        private MySqlConnection Conn;

        public Dal()
        {
            this.Conn = new MySqlConnection(this.ConnStr);
        }

        public MySqlCommand Command(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            return cmd;
        }

        public void InsertNewPerson(Person person)
        {
            string query = @"INSERT INTO people (first_name, last_name, secret_code, type)
                             VALUES (@first_name, @last_name, @secret_code, @type)";
            try
            {
                this.Conn.Open();
                var cmd = this.Command(query);
                cmd.Parameters.AddWithValue("@first_name", person.firstName);
                cmd.Parameters.AddWithValue("@last_name", person.lastName);
                cmd.Parameters.AddWithValue("@secret_code", person.secretCode);
                cmd.Parameters.AddWithValue("@type", person.type);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding people" + ex.Message);
            }
            finally
            {
                this.Conn.Close();
            }
        }

        public Person GetPersonByName(string name)
        {
            Person person = null;
            string query = "SELECT * FROM people WHERE first_name = @first_name";
            try
            {
                var cmd = this.Command(query);
                cmd.Parameters.AddWithValue("@first_name", name);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    person = new Person
                    (
                        reader.GetInt32("id"),
                        reader.GetString("first_name"),
                        reader.GetString("last_name"),
                        reader.GetString("secret_code"),
                        reader.GetString("type"),
                        reader.GetInt32("num_reports"),
                        reader.GetInt32("num_mentions")
                    );
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving person: " + ex.Message);
            }
            finally
            {
                this.Conn.Close();
            }
            return person;

        }

        public Person GetPersonBySecretCode(string secretCode)
        {
            Person person = null;
            string query = "SELECT * FROM people WHERE  secret_code = @secret_code";
            try
            {
                var cmd = this.Command(query);
                cmd.Parameters.AddWithValue("@secret_code", secretCode);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    person = new Person
                    (
                        reader.GetInt32("id"),
                        reader.GetString("first_name"),
                        reader.GetString("last_name"),
                        reader.GetString("secret_code"),
                        reader.GetString("type"),
                        reader.GetInt32("num_reports"),
                        reader.GetInt32("num_mentions")
                    );
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving person: " + ex.Message);
            }
            finally
            {
                this.Conn.Close();
            }
            return person;


        }
    }
}
