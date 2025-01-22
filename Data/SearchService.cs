using Managment_Studio_with_Blazor.Pages;
using Npgsql;
using System.Collections.Generic;

namespace Managment_Studio_with_Blazor.Data;

public partial class DataBaseService
{
    public async Task<(List<string>, List<string>, int)> SearchAsync(string TableName, string ColumnName,string Search, int PageSize, int Offset)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Program.ConnectionString))
            {
                await connection.OpenAsync();

                int TotalRows;
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    string QueryTotalRows = $"SELECT COUNT(*) FROM {TableName} WHERE CAST(\"{ColumnName}\" AS text) ILIKE '{Search}%';";
                    command.CommandText = QueryTotalRows;
                    var TotalRowsNew = await command.ExecuteScalarAsync();
                    TotalRows = Convert.ToInt32(TotalRowsNew);
                }


                var listColumnName = new List<string>();
                var listRowName = new List<string>();

                //bool exit = true;

                //Console.Clear();
                using (NpgsqlCommand res = connection.CreateCommand())
                {
                    string Query = $"SELECT * FROM {TableName} WHERE CAST(\"{ColumnName}\" AS text) ILIKE '{Search}%' LIMIT {PageSize} OFFSET {Offset};";
                    res.CommandText = Query;
                    var result = await res.ExecuteReaderAsync();

                    int ColumnCount = result.FieldCount;//      <<<<< ---------
                    var LongestRow = new int[ColumnCount];

                    while (await result.ReadAsync())
                    {
                        for (int i = 0; i < ColumnCount; i++)
                        {
                            LongestRow[i] = Math.Max(LongestRow[i], result[i].ToString().Length);
                        }
                    }
                    var Longest = new int[ColumnCount];

                    //Console.WriteLine();
                    for (int i = 0; i < ColumnCount; i++)
                    {




                        Longest[i] = Math.Max(LongestRow[i], result.GetName(i).Length);




                        //listColumnName.Add($" {result.GetName(i).PadRight(Longest[i])} ");
                        listColumnName.Add($" {result.GetName(i)} ");


                        Console.Write($" {result.GetName(i).PadRight(Longest[i])} |");
                    }

                    //Console.WriteLine();
                    //for (int i = 0; i < ColumnCount; i++)
                    //{
                    //    Console.Write($" {"-".PadRight(Longest[i], '-')} +");
                    //}

                    await result.CloseAsync();
                    result = await res.ExecuteReaderAsync();

                    Console.WriteLine();
                    while (await result.ReadAsync())
                    {
                        for (int i = 0; i < ColumnCount; i++)
                        {

                            //listRowName.Add($" {result[i].ToString().PadRight(Longest[i])} ");
                            listRowName.Add($" {result[i]} ");
                            Console.Write($" {result[i].ToString().PadRight(Longest[i])} |");
                        }
                        Console.WriteLine();
                    }
                    await result.CloseAsync();
                    return (listColumnName, listRowName, TotalRows);
                }
                await connection.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            return (null, null, 0);
        }

    } 
}
