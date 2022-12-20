using MySqlConnector;
using System.Data;
using VoorraadBeheer.Models;
using VoorraadBeheer.Pages;

namespace VoorraadBeheer.Data
{
    public class IngredientConnections
    {
        private MySqlConnection conn = mySQLSqlHelper.GetConnection();

        private static Mutex mut = new Mutex(); 

        public async Task<bool> IngredientExists(string ingredient)
        {
            bool? result = false;
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("IngredientExists", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_ingredient",
                        DbType = DbType.String,
                        Value = ingredient,
                        Direction = ParameterDirection.Input,
                    });

                    result = cmd.ExecuteScalar() as long? == 0 ? false : true;
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return result.HasValue ? result.Value : false;
        }

        public async Task<IEnumerable<IngredientModel>> GetIngredients()
        {
            List<IngredientModel> list = new List<IngredientModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetIngredients", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new IngredientModel()
                            {
                                Id = reader.GetInt32("Id"),
                                Ingredient = reader.GetString("Ingredient"),
                                Amount = reader.GetFloat("Amount"),
                                Unit = reader.GetString("Unit"),
                                UnitSmall = reader.GetString("UnitSmall"),
                            });
                        }
                    }
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return list;
        }

        public async Task<IngredientModel> GetIngredient(string ingredient)
        {
            List<IngredientModel> list = new List<IngredientModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetIngredient", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_ingredient",
                        DbType = DbType.String,
                        Value = ingredient,
                        Direction = ParameterDirection.Input,
                    });
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new IngredientModel()
                            {
                                Id = reader.GetInt32("Id"),
                                Ingredient = reader.GetString("Ingredient"),
                                Amount = reader.GetFloat("Amount"),
                                Unit = reader.GetString("Unit"),
                                UnitSmall = reader.GetString("UnitSmall"),
                            });
                        }
                    }
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return list.Count > 0 ? list[0] : new IngredientModel();
        }

        public async Task<IngredientModel> GetIngredientById(int id)
        {
            List<IngredientModel> list = new List<IngredientModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetIngredientById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_id",
                        DbType = DbType.Int16,
                        Value = id,
                        Direction = ParameterDirection.Input,
                    });
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new IngredientModel()
                            {
                                Id = reader.GetInt32("Id"),
                                Ingredient = reader.GetString("Ingredient"),
                                Amount = reader.GetFloat("Amount"),
                                Unit = reader.GetString("Unit"),
                                UnitSmall = reader.GetString("UnitSmall"),
                            });
                        }
                    }
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return list.Count > 0 ? list[0] : new IngredientModel();
        }

        public async Task<IEnumerable<IngredientModel>> AddIngredient(string ingredient, float amount, int unitId)
        {
            List<IngredientModel> list = new List<IngredientModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("AddIngredient", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_ingredient",
                        DbType = DbType.String,
                        Value = ingredient,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_amount",
                        DbType = DbType.Double,
                        Value = amount,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_unitId",
                        DbType = DbType.Int16,
                        Value = unitId,
                        Direction = ParameterDirection.Input,
                    });
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new IngredientModel()
                            {
                                Id = reader.GetInt32("Id"),
                                Ingredient = reader.GetString("Ingredient"),
                                Amount = reader.GetFloat("Amount"),
                                Unit = reader.GetString("Unit"),
                                UnitSmall = reader.GetString("UnitSmall"),
                            });
                        }
                    }
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return list;
        }

        public async Task<bool> UpdateIngredient(int id, string ingredient, float amount, int unitId)
        {
            List<IngredientModel> list = new List<IngredientModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("UpdateIngredient", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_id",
                        DbType = DbType.Int16,
                        Value = id,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_ingredient",
                        DbType = DbType.String,
                        Value = ingredient,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_amount",
                        DbType = DbType.Double,
                        Value = amount,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_unitid",
                        DbType = DbType.Int16,
                        Value = unitId,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return true;
        }

        public async Task<bool> UpdateIngredientAmount(string ingredient, float amount)
        {
            List<IngredientModel> list = new List<IngredientModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("UpdateIngredientAmount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_ingredient",
                        DbType = DbType.String,
                        Value = ingredient,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_amount",
                        DbType = DbType.Double,
                        Value = amount,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return true;
        }

        public async Task<bool> UpdateIngredientAmountById(int id, float amount)
        {
            List<IngredientModel> list = new List<IngredientModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("UpdateIngredientAmount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_id",
                        DbType = DbType.Int16,
                        Value = id,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_amount",
                        DbType = DbType.Double,
                        Value = amount,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return true;
        }

        public async Task<bool> UpdateIngredientAmountAdd(string ingredient, float amount)
        {
            List<IngredientModel> list = new List<IngredientModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("UpdateIngredientAmountAdd", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_ingredient",
                        DbType = DbType.String,
                        Value = ingredient,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_amount",
                        DbType = DbType.Double,
                        Value = amount,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return true;
        }

        public async Task<bool> UpdateIngredientAmountAddById(int id, float amount)
        {
            List<IngredientModel> list = new List<IngredientModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("UpdateIngredientAmountAdd", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_id",
                        DbType = DbType.Int16,
                        Value = id,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_amount",
                        DbType = DbType.Double,
                        Value = amount,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return true;
        }

        public async Task<bool> RemoveIngredient(string ingredient)
        {
            List<IngredientModel> list = new List<IngredientModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("RemoveIngredient", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_ingredient",
                        DbType = DbType.String,
                        Value = ingredient,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return true;
        }

        public async Task<bool> RemoveIngredientById(int id)
        {
            List<IngredientModel> list = new List<IngredientModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("RemoveIngredientById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_id",
                        DbType = DbType.Int16,
                        Value = id,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return true;
        }
    }
}
