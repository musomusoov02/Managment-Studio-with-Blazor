using Managment_Studio_with_Blazor.Interface;
using Npgsql;
using System.Data;

namespace Managment_Studio_with_Blazor.Data;

public partial class DataBaseService
{

    public async Task CreateColumnAsync(string TableName,string ColumnName,string PostgreSQLDataTypes)
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.ConnectionString))
            {
                await connection.OpenAsync();

                string Query = $"ALTER TABLE {TableName} ADD COLUMN {ColumnName} {PostgreSQLDataTypes};";
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = Query;
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }


   

    public async Task<List<(string, string)>> ListColumnAsync(string TableName)
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.ConnectionString))
            {
                await connection.OpenAsync();
                var list = new List<(string, string)>();

                DataTable schemaColumn = await connection.GetSchemaAsync("Columns", new string[] { null, null, TableName });

                foreach (DataRow row in schemaColumn.Rows)
                {
                    list.Add((row["COLUMN_NAME"].ToString(), row["DATA_TYPE"].ToString()));
                }
                await connection.CloseAsync();
                return list;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return null;
        }
    }

    public async Task UpdateColumnAsync(string TableName,string t, string ColumnName,string ss)
    {
        using (var connection = new NpgsqlConnection(Program.ConnectionString))
        {
            await connection.OpenAsync();
            string NewTypeName = "";
            try
            {
                Console.WriteLine("New Type Name: ");
                NewTypeName = Console.ReadLine();
                //while (string.IsNullOrEmpty(NewTypeName))
                //{
                //    Console.WriteLine("Not Entered!!! ");
                //    NewTypeName = Console.ReadLine();
                //}
                string query = $"ALTER TABLE \"{TableName}\" ALTER COLUMN \"{ColumnName}\" TYPE \"{NewTypeName}\";";

                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("\n** Update Type **");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message + "   <--  Program error!!!!");
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }
        }
    }

    public async Task DeleteColumnAsync(string TableName, string ColumnName)
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.ConnectionString))
            {
                await connection.OpenAsync();

                string query = $"ALTER TABLE \"{TableName}\" DROP COLUMN \"{ColumnName}\";";
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }
}
