using Npgsql;
using System.Linq.Expressions;

namespace Managment_Studio_with_Blazor.Data
{
    public partial class DataBaseService
    {
        public async Task<(List<string>,List<string>,string)> WriteSelectQueryAsync(string query)
        {
            try
            {
                using (var connection = new NpgsqlConnection(Program.ConnectionString))
                {
                    await connection.OpenAsync();

                    //Console.Write("Query input: ");
                    //string query = Console.ReadLine();

                    //while (string.IsNullOrEmpty(query))
                    //{
                    //    Console.WriteLine("Not Entered!!! ");
                    //    query = Console.ReadLine();
                    //}
                    var listColumnName = new List<string>();
                    var listRows = new List<string>();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        var result = await command.ExecuteReaderAsync();
                        if (result.HasRows == false)
                        {
                            return (null, null, "Enumeration yielded no result");
                        }

                        int ColumnCount = result.FieldCount;//      <<<<< ---------
                        var LongestRow = new int[ColumnCount];

                        //while (await result.ReadAsync())
                        //{
                        //    for (int i = 0; i < ColumnCount; i++)
                        //    {
                        //        LongestRow[i] = Math.Max(LongestRow[i], result[i].ToString().Length);
                        //    }
                        //}
                        var Longest = new int[ColumnCount];

                        //Console.WriteLine();
                        for (int i = 0; i < ColumnCount; i++)
                        {
                            //Longest[i] = Math.Max(LongestRow[i], result.GetName(i).Length);
                            listColumnName.Add(result.GetName(i));
                            Console.Write($" {result.GetName(i).PadRight(Longest[i])} |");
                        }

                       // Console.WriteLine();
                        //for (int i = 0; i < ColumnCount; i++)
                        //{

                        //    Console.Write($" {"-".PadRight(Longest[i], '-')} +");
                        //}

                        await result.CloseAsync();
                        result = await command.ExecuteReaderAsync();

                        Console.WriteLine();
                        while (await result.ReadAsync())
                        {
                            for (int i = 0; i < ColumnCount; i++)
                            {
                                listRows.Add($"{result[i]}");
                                //Console.Write($" {result[i].ToString().PadRight(Longest[i])} |");
                            }
                            //Console.WriteLine();
                        }
                    }
                    await connection.CloseAsync();
                    return (listColumnName, listRows, "** Successful **");
                }
            }
            catch (Exception ex)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine(ex.Message + "   <--  Program error!!!!");
                //Console.ForegroundColor = ConsoleColor.Gray;
                return (null,null,ex.Message);
            }
        }

        public async Task<string> WriteChangeQueryAsync(string query)
        {
            try
            {
                using (var connection = new NpgsqlConnection(Program.ConnectionString))
                {
                    await connection.OpenAsync();

                    //Console.Write("Query input: ");
                    //string query = Console.ReadLine();
                    //while (string.IsNullOrEmpty(query))
                    //{
                    //    Console.WriteLine("Not Entered!!! ");
                    //    query = Console.ReadLine();
                    //}
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        var result = await command.ExecuteNonQueryAsync();
                        //Console.WriteLine("\n** Successful **");
                        if(result==-1)
                        {
                            return null;
                        }
                    }
                    await connection.CloseAsync();
                    return "** Successful **";
                }
            }
            catch (Exception ex)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine(ex.Message + "   <--  Program error!!!!");
                //Console.ForegroundColor = ConsoleColor.Gray;
                return ex.Message;
            }
        }
    }
}
