using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using System.Data;

namespace Managment_Studio_with_Blazor.Data;

public partial  class DataBaseService
{
    public async Task CreateTableAsync(string TableName)
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.ConnectionString))
            {
                await connection.OpenAsync();

                string Query = $@" CREATE TABLE {TableName} ();";

                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = Query;
                    await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }

    public async Task<List<string>> ListTableAsync()
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.ConnectionString))
            {
                await connection.OpenAsync();
                List<string> list = new List<string>();
                DataTable tables = await connection.GetSchemaAsync("Tables");

                foreach (DataRow row in tables.Rows)
                {
                    list.Add($"{row["Table_Name"]}");
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

    public async Task UpdateTableAsync(string OldTableName,string NewTableName)
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.ConnectionString))
            {
                await connection.OpenAsync();

                string query = $"ALTER TABLE \"{OldTableName}\" RENAME TO \"{NewTableName}\";";
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }

    public async Task DeleteTableAsync(string TableName)
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.ConnectionString))
            {
                await connection.OpenAsync();

                string query = $"DROP TABLE \"{TableName}\";";
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }
}
