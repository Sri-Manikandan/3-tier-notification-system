using Npgsql;
using NotificationModelLibrary;
using NotificationDALLibrary.Interfaces;

namespace NotificationDALLibrary.Repositories
{
    public class NotificationRepository : INotificationRepository<Notification>
    {
        private string _connectionString;
        NpgsqlConnection conn;
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;

        public NotificationRepository(string connectionString = null!)
        {
            _connectionString = connectionString ?? DBHelper.ConnectionString;
        }

        public void Add(Notification notification)
        {
            string sql = $"INSERT INTO notifications (message, notification_type, sent_date, user_id) VALUES ('{notification.Message}', '{notification.NotificationType}', '{notification.SentDate}', '{notification.Recipient.Id}') RETURNING id";

            conn = new NpgsqlConnection(_connectionString);
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand(sql, conn);
                notification.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Notification> GetAll()
        {
            string sql = "SELECT n.id AS notif_id, n.message, n.notification_type, n.sent_date, u.id AS user_id, u.name, u.email, u.phone_number, u.is_active FROM notifications n INNER JOIN users u ON n.user_id = u.id ORDER BY n.sent_date DESC";

            var notifications = new List<Notification>();

            conn = new NpgsqlConnection(_connectionString);
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var user = new User(
                        reader.GetString(reader.GetOrdinal("name")),
                        reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : reader.GetString(reader.GetOrdinal("email")),
                        reader.IsDBNull(reader.GetOrdinal("phone_number")) ? string.Empty : reader.GetString(reader.GetOrdinal("phone_number"))
                    );
                    user.Id = reader.GetInt32(reader.GetOrdinal("user_id"));
                    user.IsActive = reader.GetBoolean(reader.GetOrdinal("is_active"));

                    var typeStr = reader.GetString(reader.GetOrdinal("notification_type"));
                    var notifType = Enum.Parse<NotificationType>(typeStr);

                    var notification = new Notification(
                        reader.GetString(reader.GetOrdinal("message")),
                        notifType,
                        user
                    );
                    notification.Id = reader.GetInt32(reader.GetOrdinal("notif_id"));
                    notification.SentDate = reader.GetDateTime(reader.GetOrdinal("sent_date"));

                    notifications.Add(notification);
                }
                return notifications;
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
