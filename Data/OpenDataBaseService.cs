using Npgsql;

namespace Managment_Studio_with_Blazor.Data;

public partial class DataBaseService
{
    public void ChangeDataBase(string DataBaseName)
    {
        string[] name = Program.ConnectionString.Split(';');
        string result = "";
        for (int i = 0; i < name.Length; i++)
        {
            if (i == 1)
            {
                result += $"Database ={DataBaseName};";
                continue;
            }
            result += (name[i] += ";");
        }
        Program.ConnectionString = result;
    }


    public async Task<(string,bool)> OpenConnectionStringAsync(string LocalHost,string DbNameDefault, int Port,string User,string Password)
    {
        string ResultOpenConnectionString = string.Empty;
        string Default = "postgres";
        try
        {
            string NewConnectionString = $"Host={LocalHost};Database={Default};Port={Port};Username={User};Password={Password};";

            using (var connection = new NpgsqlConnection(NewConnectionString))
            {
                await connection.OpenAsync();

                string QueryDatabaseCheck = $"SELECT 1 FROM pg_database WHERE datname = '{DbNameDefault}';";
                using (var res = new NpgsqlCommand(QueryDatabaseCheck, connection))
                {
                    var result = await res.ExecuteScalarAsync();
                    if (result != null)
                    {
                        await connection.CloseAsync();  
                        ResultOpenConnectionString = $"Host={LocalHost};Database={DbNameDefault};Port={Port};Username={User};Password={Password};";
                        return (ResultOpenConnectionString, true);
                    }
                }
                string Query = $" CREATE DATABASE {DbNameDefault};";
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = Query;
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                    ResultOpenConnectionString = $"Host={LocalHost};Database={DbNameDefault};Port={Port};Username={User};Password={Password};";
                    return(ResultOpenConnectionString, false);
                }
            }
        }
        catch (Exception ex)
        {
            return (null, false);
        }
    }
}
