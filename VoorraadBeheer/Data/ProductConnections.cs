using MySqlConnector;
using System.Data;
using VoorraadBeheer.Models;
using VoorraadBeheer.Pages;

namespace VoorraadBeheer.Data
{
    public class ProductConnections
    {
        private MySqlConnection conn = mySQLSqlHelper.GetConnection();

        private static Mutex mut = new Mutex();

        public ProductConnections()
        {
            
        }

        public async Task<bool> ProductExists(string product)
        {
            bool? result = false;
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("ProductExists", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_product",
                        DbType = DbType.String,
                        Value = product,
                        Direction = ParameterDirection.Input,
                    });
                    result = cmd.ExecuteScalar() as long? == 0 ? false : true;
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return result.HasValue ? result.Value : false;
        }

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            List<ProductModel> list = new List<ProductModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetProducts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ProductModel()
                            {
                                Id = reader.GetInt32("Id"),
                                Product = reader.GetString("Product"),
                                Amount = reader.GetInt32("Amount"),
                            });
                        }
                    }
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return list;
        }

        public async Task<ProductModel> GetProduct(string product)
        {
            List<ProductModel> list = new List<ProductModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetProduct", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_product",
                        DbType = DbType.String,
                        Value = product,
                        Direction = ParameterDirection.Input,
                    });
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ProductModel()
                            {
                                Id = reader.GetInt32("Id"),
                                Product = reader.GetString("Product"),
                                Amount = reader.GetInt32("Amount"),
                            });
                        }
                    }
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return list.Count > 0 ? list[0] : new ProductModel();
        }

        public async Task<ProductModel> GetProductById(int id)
        {
            List<ProductModel> list = new List<ProductModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetProductById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_id",
                        DbType = DbType.Int32,
                        Value = id,
                        Direction = ParameterDirection.Input,
                    });
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ProductModel()
                            {
                                Id = reader.GetInt32("Id"),
                                Product = reader.GetString("Product"),
                                Amount = reader.GetInt32("Amount"),
                            });
                        }
                    }
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return list.Count > 0 ? list[0] : new ProductModel();
        }

        public async Task<IEnumerable<ProductModel>> AddProduct(string product, int amount)
        {
            List<ProductModel> list = new List<ProductModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("AddProduct", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_product",
                        DbType = DbType.String,
                        Value = product,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_amount",
                        DbType = DbType.Int32,
                        Value = amount,
                        Direction = ParameterDirection.Input,
                    });
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ProductModel()
                            {
                                Id = reader.GetInt32("Id"),
                                Product = reader.GetString("Product"),
                                Amount = reader.GetInt32("Amount"),
                            });
                        }
                    }
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return list;
        }

        public async Task<bool> UpdateProduct(int id, string product, int amount)
        {
            bool result = false;

            mut.WaitOne();
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand("UpdateProduct", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_id",
                    DbType = DbType.Int32,
                    Value = id,
                    Direction = ParameterDirection.Input,
                });
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_product",
                    DbType = DbType.String,
                    Value = product,
                    Direction = ParameterDirection.Input,
                });
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_amount",
                    DbType = DbType.Int32,
                    Value = amount,
                    Direction = ParameterDirection.Input,
                });
                result = cmd.ExecuteNonQuery() > 0;
            }
            conn.Close();
            mut.ReleaseMutex();

            return result;
        }

        public async Task<bool> UpdateProductAmount(string product, int amount)
        {
            bool result = false;
             
            mut.WaitOne();
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand("UpdateProductAmount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_product",
                    DbType = DbType.String,
                    Value = product,
                    Direction = ParameterDirection.Input,
                });
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_amount",
                    DbType = DbType.Int32,
                    Value = amount,
                    Direction = ParameterDirection.Input,
                });
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            mut.ReleaseMutex();

            return result;
        }

        public async Task<bool> UpdateProductAmountById(int id, int amount)
        {
            bool result = false;
             
            mut.WaitOne();
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand("UpdateProductAmount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_id",
                    DbType = DbType.Int32,
                    Value = id,
                    Direction = ParameterDirection.Input,
                });
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_amount",
                    DbType = DbType.Int32,
                    Value = amount,
                    Direction = ParameterDirection.Input,
                });
                result = cmd.ExecuteNonQuery() > 0;
            }
            conn.Close();
            mut.ReleaseMutex();

            return result;
        }

        public async Task<bool> UpdateProductAmountAdd(string product, int amount)
        {
            bool result = false;
             
            mut.WaitOne();
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand("UpdateProductAmountAdd", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_product",
                    DbType = DbType.String,
                    Value = product,
                    Direction = ParameterDirection.Input,
                });
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_amount",
                    DbType = DbType.Int32,
                    Value = amount,
                    Direction = ParameterDirection.Input,
                });
                result = cmd.ExecuteNonQuery() > 0;
            }
            conn.Close();
            mut.ReleaseMutex();

            return result;
        }

        public async Task<bool> UpdateProductAmountAddById(int id, int amount)
        {
            bool result = false;
             
            mut.WaitOne();
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand("UpdateProductAmountAdd", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_id",
                    DbType = DbType.Int32,
                    Value = id,
                    Direction = ParameterDirection.Input,
                });
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_amount",
                    DbType = DbType.Int32,
                    Value = amount,
                    Direction = ParameterDirection.Input,
                });
                result = cmd.ExecuteNonQuery() > 0;
            }
            conn.Close();
            mut.ReleaseMutex();

            return result;
        }

        public async Task<bool> RemoveProduct(string product)
        {
            bool result = false;

            mut.WaitOne();
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand("RemoveProduct", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_product",
                    DbType = DbType.String,
                    Value = product,
                    Direction = ParameterDirection.Input,
                });
                result = cmd.ExecuteNonQuery() > 0;
            }
            conn.Close();
            mut.ReleaseMutex();

            return result;
        }

        public async Task<bool> RemoveProductById(int id)
        {
            bool result = false;

            mut.WaitOne();
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand("RemoveProductById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_id",
                    DbType = DbType.Int32,
                    Value = id,
                    Direction = ParameterDirection.Input,
                });
                result = cmd.ExecuteNonQuery() > 0;
            }
            conn.Close();
            mut.ReleaseMutex();

            return result;
        }






        public async Task<IEnumerable<ProductIngredientModel>> GetProductIngredients(int productId)
        {
            List<ProductIngredientModel> list = new List<ProductIngredientModel>();
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetProductIngredients", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_productid",
                        DbType = DbType.Int64,
                        Value = productId,
                        Direction = ParameterDirection.Input,
                    });
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ProductIngredientModel()
                            {
                                Id = reader.GetInt32("Id"),
                                IngredientModel = new IngredientModel()
                                {
                                    Id = reader.GetInt32("IngredientId"),
                                    Ingredient = reader.GetString("Ingredient"),
                                    Amount = reader.GetFloat("Amount"),
                                    Unit = reader.GetString("Unit"),
                                    UnitSmall = reader.GetString("UnitSmall"),
                                },
                            });
                        }
                    }
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return list;
        }

        public async Task<bool> AddProductIngredient(int productId, int ingredientId, float amount)
        {
            bool result = false;

            mut.WaitOne();
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand("AddProductIngredient", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_productid",
                    DbType = DbType.Int32,
                    Value = productId,
                    Direction = ParameterDirection.Input,
                });
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_ingredientid",
                    DbType = DbType.Int32,
                    Value = ingredientId,
                    Direction = ParameterDirection.Input,
                });
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_amount",
                    DbType = DbType.Double,
                    Value = amount,
                    Direction = ParameterDirection.Input,
                });
                result = cmd.ExecuteNonQuery() > 0;
            }
            conn.Close();
            mut.ReleaseMutex();

            return result;
        }

        public async Task<bool> UpdateProductIngredientAmount(int productId, int ingredientId, float amount)
        {
            bool result = false;

            mut.WaitOne();
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand("UpdateProductIngredientAmount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_productid",
                    DbType = DbType.Int32,
                    Value = productId,
                    Direction = ParameterDirection.Input,
                });
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_ingredientid",
                    DbType = DbType.Int32,
                    Value = ingredientId,
                    Direction = ParameterDirection.Input,
                });
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_amount",
                    DbType = DbType.Double,
                    Value = amount,
                    Direction = ParameterDirection.Input,
                });
                result = cmd.ExecuteNonQuery() > 0;
            }
            conn.Close();
            mut.ReleaseMutex();

            return result;
        }

        public async Task<bool> UpdateProductIngredient(int Id, int ingredientId, float amount)
        {
            bool result = false;

            mut.WaitOne();
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand("UpdateProductIngredient", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_id",
                    DbType = DbType.Int32,
                    Value = Id,
                    Direction = ParameterDirection.Input,
                });
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_ingredientid",
                    DbType = DbType.Int32,
                    Value = ingredientId,
                    Direction = ParameterDirection.Input,
                });
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_amount",
                    DbType = DbType.Double,
                    Value = amount,
                    Direction = ParameterDirection.Input,
                });
                result = cmd.ExecuteNonQuery() > 0;
            }
            conn.Close();
            mut.ReleaseMutex();

            return result;
        }

        public async Task<bool> RemoveProductIngredient(int productId, int ingredientId)
        {
            bool result = false;

            mut.WaitOne();
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand("RemoveProductIngredient", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_productid",
                    DbType = DbType.Int32,
                    Value = productId,
                    Direction = ParameterDirection.Input,
                });
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "param_ingredientid",
                    DbType = DbType.Int32,
                    Value = ingredientId,
                    Direction = ParameterDirection.Input,
                });

                result = cmd.ExecuteNonQuery() > 0;
            }
            conn.Close();
            mut.ReleaseMutex();

            return result;
        }

        public async Task<bool> ProductIngredientExists(int productId, int ingredientId)
        {

            bool? result = false;
            {
                mut.WaitOne();
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("ProductIngredientExists", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_productid",
                        DbType = DbType.Int32,
                        Value = productId,
                        Direction = ParameterDirection.Input,
                    });
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "param_ingredientid",
                        DbType = DbType.Int32,
                        Value = ingredientId,
                        Direction = ParameterDirection.Input,
                    });
                    result = cmd.ExecuteScalar() as long? == 0 ? false : true;
                }
                conn.Close();
                mut.ReleaseMutex();
            }
            return result.HasValue ? result.Value : false;
        }
    }
}
