using Npgsql;
using System.Data;

namespace Managment_Studio_with_Blazor.Data;

public partial class DataBaseService
{
    public async Task<List<string>> ListUsersAsync()
    {
        try
        {
            using (var connection = new NpgsqlConnection(Program.ConnectionString))
            {
                await connection.OpenAsync();
                var list=new List<string>();
                DataTable users = await connection.GetSchemaAsync("Users");
                foreach (DataRow row in users.Rows)
                {
                    list.Add($"Users: {row["User_Name"]}");
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
}
