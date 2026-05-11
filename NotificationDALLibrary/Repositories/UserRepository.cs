using Npgsql;
using NotificationModelLibrary;
using NotificationDALLibrary.Interfaces;

namespace NotificationDALLibrary.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;
        NpgsqlConnection conn;
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;

        public UserRepository(string connectionString = null!)
        {
            _connectionString = connectionString ?? DBHelper.ConnectionString;
        }

        public int Add(User user)
        {
            string sql = $"INSERT INTO users (name, email, phone_number, is_active) VALUES ('{user.Name}', '{user.Email}', '{user.PhoneNumber}', {user.IsActive}) RETURNING id";

            conn = new NpgsqlConnection(_connectionString);
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand(sql, conn);
                int newId = Convert.ToInt32(cmd.ExecuteScalar());
                user.Id = newId;
                return newId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public User? GetByEmail(string email)
        {
            string sql = $"SELECT id, name, email, phone_number, is_active FROM users WHERE email = '{email}' LIMIT 1";

            conn = new NpgsqlConnection(_connectionString);
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                User user = null;
                if (reader.Read())
                {
                    user = new User(reader["name"].ToString(), reader["email"].ToString(), reader["phone_number"].ToString());
                    user.Id = Convert.ToInt32(reader["id"]);
                    user.IsActive = Convert.ToBoolean(reader["is_active"]);
                }
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public User? GetByPhone(string phoneNumber)
        {
            string sql = $"SELECT id, name, email, phone_number, is_active FROM users WHERE phone_number = '{phoneNumber}' LIMIT 1";

            conn = new NpgsqlConnection(_connectionString);
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                User user = null;
                if (reader.Read())
                {
                    user = new User(reader["name"].ToString(), reader["email"].ToString(), reader["phone_number"].ToString());
                    user.Id = Convert.ToInt32(reader["id"]);
                    user.IsActive = Convert.ToBoolean(reader["is_active"]);
                }
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<User> GetAll()
        {
            string sql = "SELECT id, name, email, phone_number, is_active FROM users ORDER BY name";
            var users = new List<User>();

            conn = new NpgsqlConnection(_connectionString);
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User(reader["name"].ToString(), reader["email"].ToString(), reader["phone_number"].ToString());
                    user.Id = Convert.ToInt32(reader["id"]);
                    user.IsActive = Convert.ToBoolean(reader["is_active"]);
                    users.Add(user);
                }
                return users;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
