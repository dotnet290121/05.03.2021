using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgres230221
{
    internal class PostgresDAO : IDataAccess
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string m_conn;

        public PostgresDAO(string m_conn)
        {
            this.m_conn = m_conn;
        }

        public int Run_sp_GetMax(int x, int y)
        {
            try
            {
                using (var conn = new NpgsqlConnection(m_conn))
                {
                    conn.Open();
                    string sp_name = "a_sp_max";

                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure; // this is default

                    command.Parameters.AddRange(new NpgsqlParameter[]
                    {
                    new NpgsqlParameter("x", x),
                    new NpgsqlParameter("y", y),
                    });

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int random_number = (int)reader["a_sp_max"];
                        // ....
                        return random_number;
                    }
                    throw new ApplicationException("Function not returned value!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine($"Function a_sp_max failed. parameters: x:{x} y:{y}");
                return 0;
            }
        }

        private NpgsqlParameter[] GetParametersFromDataHolder(object dataObject)
        {
            List<NpgsqlParameter> paraResult = new List<NpgsqlParameter>();
            var props_in_dataObject = dataObject.GetType().GetProperties();
            foreach (var prop in props_in_dataObject)
            {
                paraResult.Add(new NpgsqlParameter(prop.Name, prop.GetValue(dataObject)));
            }
            return paraResult.ToArray();
        }

        public List<Dictionary<string, object>> Run_sp(string sp_name, object dataHolder)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            NpgsqlParameter[] para = null;
            try
            {
                using (var conn = new NpgsqlConnection(m_conn))
                {
                    conn.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure; // this is default

                    para = GetParametersFromDataHolder(dataHolder);

                    command.Parameters.AddRange(para);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Dictionary<string, object> one_row = new Dictionary<string, object>();
                        foreach (var item in reader.GetColumnSchema())
                        {
                            object one_column_value = reader[item.ColumnName];
                            one_row[item.ColumnName] = one_column_value;
                        }
                        result.Add(one_row);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                string params_string = "";
                foreach (var item in para)
                {
                    if (params_string != "")
                        params_string += ", ";
                    params_string += $"Name : {item.ParameterName} value: {item.Value}";
                }
                Console.WriteLine($"Function {sp_name} failed. parameters: {params_string}");
            }

            return result;
        }

        public List<T> Run_sp<T>(string sp_name, object dataHolder) where T : new()
        {
            List<T> result = new List<T>();
            NpgsqlParameter[] para = null;
            try
            {
                using (var conn = new NpgsqlConnection(m_conn))
                {
                    conn.Open();

                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure; // this is default

                    para = GetParametersFromDataHolder(dataHolder);

                    command.Parameters.AddRange(para);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        T one_row = new T();
                        Type type_of_t = typeof(T);
                        foreach (var prop in type_of_t.GetProperties())
                        {
                            string column_name = prop.Name;

                            var custom_attr_column_name = 
                                (ColumnAttribute[])prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                            if (custom_attr_column_name.Length > 0)
                                column_name = custom_attr_column_name[0].Name;

                            var value = reader[column_name];

                            prop.SetValue(one_row, value);
                        }
                        
                        result.Add(one_row);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                string params_string = "";
                foreach (var item in para)
                {
                    if (params_string != "")
                        params_string += ", ";
                    params_string += $"Name : {item.ParameterName} value: {item.Value}";
                }
                Console.WriteLine($"Function {sp_name} failed. parameters: {params_string}");
            }

            return result;
        }

        public List<Movie> GetAllMovies()
        {
            List<Movie> movies = new List<Movie>();
            using (var conn = new NpgsqlConnection(m_conn))
            {
                conn.Open();
                string query = "SELECT * FROM movies";

                NpgsqlCommand command = new NpgsqlCommand(query, conn);
                command.CommandType = System.Data.CommandType.Text; // this is default

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    movies.Add(
                        new Movie
                        {
                            Id = (long)reader["id"],
                            Title = (string)reader["title"],
                            ReleaseDate = (DateTime)reader["release_date"],
                            Price = (double)reader["price"],
                            CountryId = (long)reader["country_id"]
                        });
                }
            }
            return movies;
        }

        public bool TestDbConnection()
        {
            my_logger.Debug("Testing db access");
            try
            {
                using (var my_conn = new NpgsqlConnection(m_conn))
                {
                    my_conn.Open();
                    my_logger.Debug("Testing db access. succeed!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                my_logger.Fatal($"Testing db access. Failed!. Error: {ex}");
                return false;
            }
        }
    }
}
