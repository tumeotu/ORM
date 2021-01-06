using MyORM.Database;
using MyORM.ORMException;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace MyORM.Extension
{
    static partial class CheckDatabaseExtention
	{

        /// <summary>
        /// Check any DB can valid with config
        /// </summary>
        /// <param name="config"></param>
        /// <returns>IDatabase from config from user. The IDatabase is dynamic and dependence with config </returns
        public static async Task<IDatabase> CheckDatabaseExists(Dictionary<string, string> config)
        {
            string connectionString = "";
            try
            {
                connectionString = ConnectionStringConverter.ConvertToSQL(config);
                SqlConnection connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                connection.Close();
                connection.Dispose();
                return new SQLDatabase(connectionString);
            }
            catch (Exception sql)
            {
                try
                {
                    connectionString = ConnectionStringConverter.ConvertToMySQL(config);
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();
                    connection.Close();
                    connection.Dispose();
                    return new MySQLDatabase(connectionString);
                }
                catch (Exception mysql)
                {
                    try
                    {
                        connectionString = ConnectionStringConverter.ConvertToPostgreSQL(config);
                        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                        connection.Open();
                        connection.Close();
                        connection.Dispose();
                        return new PostgreSQLDatabase(connectionString);
                    }
                    catch (Exception postgree)
                    {
                        throw new DatabaseConfigMalformed("Connection string is invalid. Please check again!");
                    }
                }
            }
        }        
    }
}
