using MySqlConnector;
using System.Data;
using VoorraadBeheer.Authentication.Cryptography;
using VoorraadBeheer.Models;

namespace VoorraadBeheer.Authentication
{
    public class UserDetailService
    {
        private MySqlConnection conn = mySQLSqlHelper.GetConnection();
        private static Mutex mut = new Mutex();

        public async Task<UserModel> GetUser(string username)
        {
            List<UserModel> list = new List<UserModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_username",
                        DbType = DbType.String,
                        Value = username,
                        Direction = ParameterDirection.Input,
                    });
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new UserModel()
                            {
                                Id = reader.GetInt32("Id"),
                                Username = reader.GetString("Username"),
                                Salt = (byte[])reader.GetValue("Salt"),
                                Hash = (byte[])reader.GetValue("Hash"),
                                Role = reader.GetString("Role"),
                            });
                        }
                    }
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return list.Count > 0 ? list[0] : null;
        }

        public async Task<UserModel> CreateUser(string username, byte[] salt, byte[] hash, Guid token)
        {
            List<UserModel> list = new List<UserModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("AddUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_username",
                        DbType = DbType.String,
                        Value = username,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_salt",
                        DbType = DbType.Binary,
                        Value = salt,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_hash",
                        DbType = DbType.Binary,
                        Value = hash,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_token",
                        DbType = DbType.String,
                        Value = token.ToString(),
                        Direction = ParameterDirection.Input,
                    });

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return list.Count > 0 ? list[0] : new UserModel();
        }

        public async Task<bool> CheckCreateAccountToken(Guid token)
        {
            bool? result = false;
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("CheckCreateAccountToken", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_token",
                        DbType = DbType.String,
                        Value = token.ToString(),
                        Direction = ParameterDirection.Input,
                    });

                    result = cmd.ExecuteScalar() as int? == 0 ? false : true;
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return result.HasValue ? result.Value : false;
        }
    }
}
