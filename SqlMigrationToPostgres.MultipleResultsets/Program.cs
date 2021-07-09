using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace SqlMigrationToPostgres.MultipleResultsets
{
    class Program
    {
        static void Main(string[] args)
        {
            GetMultipleResultSetsSqlServer("Data Source=.;Initial Catalog=MigrationDB;Integrated Security=True");
            GetMultipleResultSetsPostgres("Host=localhost; Port=5432;Database=postgres;Username=postgres;Password=123456;");
            Console.ReadKey();
        }

        private static void GetMultipleResultSetsSqlServer(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[GetUserProfile]", conn, tran))
                    {
                        int a = 1;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", a);
                        cmd.Parameters.AddWithValue("@ProfileID", "103ec459-579e-4cb9-9b3c-5ca21c357248");
                        try
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                // Get User details
                                if (reader.Read())
                                {
                                    var userId = reader["UserSqlId"];
                                    Console.WriteLine((int)userId);
                                }

                                // Get Custom Events
                                if (reader.NextResult())
                                {
                                    if (reader.Read())
                                    {
                                        var userId = reader["UserSqlId"];
                                        Console.WriteLine((int)userId);
                                    }
                                }

                                // Get Profile
                                if (reader.NextResult())
                                {
                                    if (reader.Read())
                                    {
                                        var profileId = reader["ProfileId"];
                                        Console.WriteLine(Guid.Parse((string)profileId));
                                    }
                                }
                            }

                            tran.Commit();
                        }
                        catch (SqlException ex)
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        private static void GetMultipleResultSetsPostgres(string connectionString)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (NpgsqlTransaction tran = conn.BeginTransaction())
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("public.getuserprofile", conn, tran))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("par_userid", 1);
                        cmd.Parameters.AddWithValue("par_profileid", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse("103ec459-579e-4cb9-9b3c-5ca21c357248"));
                        try
                        {
                            RefCursorList lstCursors = new RefCursorList();
                            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    lstCursors.RefCursors.Add(reader[0].ToString());
                                }

                                reader.Close();
                            }

                            // Get User details
                            if (lstCursors.HasNextResult)
                            {
                                cmd.CommandText = string.Format(@"FETCH ALL IN ""{0}""", lstCursors.CurrentCursor);
                                cmd.CommandType = CommandType.Text;
                                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                                {                                    
                                    if (reader.Read())
                                    {
                                        var userId = reader["UserSqlId"];
                                        Console.WriteLine((int)userId);
                                    }
                                }
                            }

                            // Get Custom Events
                            if (lstCursors.HasNextResult)
                            {
                                cmd.CommandText = string.Format(@"FETCH ALL IN ""{0}""", lstCursors.CurrentCursor);
                                cmd.CommandType = CommandType.Text;
                                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        var userId = reader["UserSqlId"];
                                        Console.WriteLine((int)userId);
                                    }
                                }
                            }

                            // Get Custom Events
                            if (lstCursors.HasNextResult)
                            {
                                cmd.CommandText = string.Format(@"FETCH ALL IN ""{0}""", lstCursors.CurrentCursor);
                                cmd.CommandType = CommandType.Text;
                                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        var profileId = reader["ProfileId"];
                                        Console.WriteLine(Guid.Parse((string)profileId));
                                    }
                                }
                            }

                            tran.Commit();
                        }
                        catch (NpgsqlException ex)
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }
            }
        }
    }
}