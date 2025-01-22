using Managment_Studio_with_Blazor.Interface;
using Npgsql;
using System.Data;

namespace Managment_Studio_with_Blazor.Data;

public partial class DataBaseService : IDataBaseService
{

    public DataBaseService()
    {
    }


    public async Task CreateDatabaseAsync(string DataBaseName)
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.ConnectionString)) 
            {
                await connection.OpenAsync();

                string Query = $" CREATE DATABASE {DataBaseName};";

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

    public async Task<List<string>> ListDataBaseAsync()
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.ConnectionString))
            {
                await connection.OpenAsync();
                List<string> list = new List<string>();
                DataTable catalogs = await connection.GetSchemaAsync("Databases");

                foreach (DataRow row in catalogs.Rows)
                {
                    list.Add($"{row["Database_Name"]}");
                }
                await connection.CloseAsync();
                return list;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public async Task UpdateDataBaseAsync(string OldDataBaseName, string NewDataBaseName)
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.ConnectionString))
            {
                await connection.OpenAsync();

                string query = $"ALTER DATABASE \"{OldDataBaseName}\" RENAME TO \"{NewDataBaseName}\";";
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
    public async Task DeleteDataBaseAsync(string DataBaseName)
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.ConnectionString))
            {
                await connection.OpenAsync();

                string query = $"DROP DATABASE \"{DataBaseName}\";";
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
