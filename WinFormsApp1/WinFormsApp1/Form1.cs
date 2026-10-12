using Npgsql;
using System.Text.RegularExpressions;

namespace WinFormsApp1
{

    public partial class Form1 : Form
    {
        string ConnectString =
        "Host=localhost;" +
        "Port=5432;" +
        "Database=fitnes0;" +
        "Username=postgres;" +
        "Password=123";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using var connection = new NpgsqlConnection(ConnectString);
            connection.Open();
            AddClient(connection);

        }

        private void printButton_Click(object sender, EventArgs e)
        {
            using var connection = new NpgsqlConnection(ConnectString);
            connection.Open();
            
            ClientRepository cli = new ClientRepository();
            List<string> list = cli.ShowClients(connection);

            listBox1.Items.Clear();
            foreach(string items in list)
            {
                listBox1.Items.Add(items);
            }
        }
        void AddClient(NpgsqlConnection connection)
        {
            string sql = """
            INSERT INTO clients
            (first_name, last_name, birth_date, phone, email,
            registration_date, status)
            VALUES
            (@firstName, @lastName, @birthDate, @phone, @email, @regDate, @status);
            """;

            using var command = new NpgsqlCommand(sql, connection);

            if (int.TryParse(textBox6.Text, out int tato))
            {
                MessageBox.Show("Введи имя нормально.");
                return;
            }

            if (int.TryParse(textBox7.Text, out int tato1))
            {
                MessageBox.Show("Введи фамилию нормально.");
                return;
            }

            string rataw = textBox8.Text;

            string pattern = @"^(?:\+7|8)\d{10}$";

            if (!Regex.IsMatch(rataw, pattern))
            {
                MessageBox.Show("Неверный номер!");
                return;
            }

            command.Parameters.AddWithValue("phone", rataw);

            string ratae = textBox9.Text;

            DateTime ratay = dateTimePicker1.Value.Date;

            if (ratay > DateTime.Today)
            {
                MessageBox.Show("Дата рождения не может быть в будущем.");
                return;
            }


            command.Parameters.AddWithValue("firstName", textBox6.Text);
            command.Parameters.AddWithValue("lastName", textBox7.Text);
            command.Parameters.AddWithValue("birthDate", ratay);
            command.Parameters.AddWithValue("email", ratae);
            command.Parameters.AddWithValue("regDate", DateTime.Today);
            command.Parameters.AddWithValue("status", "active");

            int affectedRows = command.ExecuteNonQuery();

            MessageBox.Show("клиент добавлен");
        }
      

        void UpdateClient(NpgsqlConnection connection)
        {

            if (VisibleGroup() == 0)
            {
                if (!int.TryParse(textBox1.Text, out int tato))
                {
                    MessageBox.Show("Введи числовой ID клиента.");
                    return;
                }


                string rata = textBox2.Text;

                string sql = """
                UPDATE clients 
                SET phone = @phone
                WHERE client_id = @clientId
                """;

                using var command = new NpgsqlCommand(sql, connection);

                string pattern = @"^(?:\+7|8)\d{10}$";

                if (!Regex.IsMatch(rata, pattern))
                {
                    label5.Text = "Неверный номер!";
                    return;
                }

                command.Parameters.AddWithValue("phone", rata);
                command.Parameters.AddWithValue("clientId", tato);

                int rows = command.ExecuteNonQuery();
                MessageBox.Show(rows == 0 ? "Клиент не найден." : "Телефон обновлён.");
            }
            else if ((VisibleGroup() == 1))
            {

                if (!int.TryParse(textBox3.Text, out int tato))
                {
                    MessageBox.Show("Введи числовой ID клиента.");
                    return;
                }


                string ratar = textBox4.Text;

                string sql = """
                UPDATE clients 
                SET email = @email
                WHERE client_id = @clientId
                """;

                using var command = new NpgsqlCommand(sql, connection);

                command.Parameters.AddWithValue("email", ratar);
                command.Parameters.AddWithValue("clientID", tato);

                int rows = command.ExecuteNonQuery();
            }
        }

        int VisibleGroup()
        {
            if (comboBox1.SelectedIndex == 0)
            {
                groupBox2.Visible = false;
                groupBox1.Visible = true;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                groupBox1.Visible = false;
                groupBox2.Visible = true;
            }
            return comboBox1.SelectedIndex;
        }

        void DeleteClient(NpgsqlConnection connection)
        {
            try
            {
                string sql = """
                DELETE FROM clients
                WHERE client_id = @clientID;
                """;

                if (!int.TryParse(textBox5.Text, out int clientId))
                {
                    MessageBox.Show("ID должен быть числом.");
                    return;
                }

                using var command = new NpgsqlCommand(sql, connection);

                command.Parameters.AddWithValue("clientID", clientId);

                int affedtedRows = command.ExecuteNonQuery();

                MessageBox.Show(affedtedRows == 0 ? "Клиент не найден." : "Клиент удален");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using var connection = new NpgsqlConnection(ConnectString);
            connection.Open();
            label5.Text = string.Empty;
            UpdateClient(connection);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VisibleGroup();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using var connection = new NpgsqlConnection(ConnectString);
            connection.Open();
            DeleteClient(connection);
        }
    }
}
