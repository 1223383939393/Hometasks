using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace WinFormsApp1
{
    internal class ClientRepository
    {
        public List<string> ShowClients(NpgsqlConnection connection)
        {
            List<string> clients = new List<string>();
            

            string sql = """
            SELECT client_id, first_name, last_name, phone, email
            FROM clients
            WHERE status = @status AND birth_date < @birthDate;
            """;

            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("status", "active");
            command.Parameters.AddWithValue("birthDate", new DateTime(2005, 10, 7));

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                clients.Add($"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)} {reader.GetString(3)} {reader.GetString(4)}");
            }
            return clients;
        }
        public int UpdatePhone(NpgsqlConnection connection) {

            string sql = """
                UPDATE clients 
                SET phone = @phone
                WHERE client_id = @clientId
                """;
            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("phone", rata);
            command.Parameters.AddWithValue("clientId", tato);

            int rows = command.ExecuteNonQuery();

            return rows;
        }

    }
}
