using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Compiler;
using malshinon1.people;
using malshinon1.reports;
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
                Console.WriteLine("Error adding person" + ex.Message);
            }
            finally
            {
                this.Conn.Close();
            }
        }

        public Person GetPersonByName(string firstName, string lastName)
        {
            Person person = null;
            string query = "SELECT * FROM people WHERE first_name = @first_name AND last_name = @last_name";
            try
            {
                this.Conn.Open();
                var cmd = this.Command(query);
                cmd.Parameters.AddWithValue("@first_name", firstName);
                cmd.Parameters.AddWithValue("@last_name", lastName);
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
                this.Conn.Open();
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

        public void InsertIntelReport(Report report)
        {
            string query = @"INSERT INTO intelreports (reporter_id, target_id, text)
                             VALUES (@reporter_id, @target_id, @text)";
            try
            {
                this.Conn.Open();
                var cmd = this.Command(query);
                cmd.Parameters.AddWithValue("@reporter_id", report.reporterId);
                cmd.Parameters.AddWithValue("@target_id", report.targetId);
                cmd.Parameters.AddWithValue("@text", report.text);
                
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding report" + ex.Message);
            }
            finally
            {
                this.Conn.Close();
            }

        }

        public void UpdateReportCount(string secretCode)
        {
            string query = "UPDATE people SET  num_reports = num_reports + 1 WHERE secret_code = @secret_code";

            try
            {
                this.Conn.Open();
                var cmd = this.Command(query);
                cmd.Parameters.AddWithValue("@secret_code", secretCode);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating ReportCount: " + ex.Message);
            }
            finally
            {
                this.Conn.Close();
            }
        }

        public void UpdateStatus(string firstName, string lastName,string status)
        {
            string query = "UPDATE people SET type = @status WHERE first_name = @first_name AND last_name = @last_name";

            try
            {
                this.Conn.Open();
                var cmd = this.Command(query);
                cmd.Parameters.AddWithValue("@first_name", firstName);
                cmd.Parameters.AddWithValue("@last_name", lastName);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating status: " + ex.Message);
            }
            finally
            {
                this.Conn.Close();
            }
        }


        public void UpdateMentionCount(string secretCode)
        {
            string query = "UPDATE people SET  num_mentions = num_mentions + 1 WHERE secret_code = @secret_code";

            try
            {
                this.Conn.Open();
                var cmd = this.Command(query);
                cmd.Parameters.AddWithValue("@secret_code", secretCode);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating MentionCount: " + ex.Message);
            }
            finally
            {
                this.Conn.Close();
            }
        }

        public (int count, double avgLength) GetReporterStats(int reporterId)
        {
            string query = @"SELECT COUNT(*) AS count, AVG(CHAR_LENGTH(text)) AS avgLength FROM intelreports WHERE reporter_id = @reporter_id";
            int count = 0;
            double avgLength = 0;
            try
            {
                this.Conn.Open();
                var cmd = this.Command(query);
                cmd.Parameters.AddWithValue("@reporter_id", reporterId);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    count = reader.GetInt32("count");
                    avgLength = reader.IsDBNull(reader.GetOrdinal("avgLength")) ? 0.0 : reader.GetDouble("avgLength");

                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving reporter stats: " + ex.Message);
            }
            finally
            {
                this.Conn.Close();
            }
            return (count, avgLength);


        }

        public (int totalMentions, int mentionsLast15Min) GetTargetStats(string secretCode)
        {
            string query = @"SELECT p.num_mentions AS totalMentions, COUNT(i.id) AS mentionsLast15Min
                             FROM people p
                             LEFT JOIN intelreports i 
                             ON i.target_id = p.id 
                             AND i.timestamp >= NOW() - INTERVAL 15 MINUTE
                             WHERE p.secret_code = @secret_code
                             GROUP BY p.num_mentions;";
            int totalMentions = 0;
            int mentionsLast15Min = 0;
            try
            {
                this.Conn.Open();
                var cmd = this.Command(query);
                cmd.Parameters.AddWithValue("@secret_code", secretCode);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    totalMentions = reader.GetInt32("totalMentions");
                    mentionsLast15Min = reader.GetInt32("mentionsLast15Min");

                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving target stats: " + ex.Message);
            }
            finally
            {
                this.Conn.Close();
            }
            return (totalMentions, mentionsLast15Min);
        }

    }
}
