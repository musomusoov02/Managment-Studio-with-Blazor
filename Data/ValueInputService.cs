using Npgsql;

namespace Managment_Studio_with_Blazor.Data;

public partial class DataBaseService
{
    public async Task<string> ValueInputAsync(string TableName,List<string> value)
    {
        try
        {
            List<(string, string)> listColumn = await ListColumnAsync(TableName);
            int columnCount = listColumn.Select(colu => colu.Item1).Count();
            string ColumnName = string.Empty;

            for (int i = 0; i < listColumn.Count; i++)
                ColumnName += $"\"{listColumn[i].Item1}\",";

            ColumnName = ColumnName.Remove(ColumnName.Length - 1);

            using (var connection = new NpgsqlConnection(Program.ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    string valueString = string.Empty;
                    for (int i = 0; i < columnCount; i++)
                            valueString += $"'{value[i]}',";

                    valueString = valueString.Remove(valueString.Length - 1);

                    string query = $"INSERT INTO \"{TableName}\" ({ColumnName}) VALUES ({valueString});";
                    command.CommandText = query;
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("\n** Add successfully **");
                }
                await connection.CloseAsync();
                return "** Add successfully **";
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
