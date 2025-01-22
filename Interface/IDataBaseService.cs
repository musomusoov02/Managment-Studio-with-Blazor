using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Managment_Studio_with_Blazor.Interface;

public interface IDataBaseService
{
    public Task CreateColumnAsync(string TableName,string ColumnName,string PostgreSQLDataTypes);
    public Task<List<(string, string)>> ListColumnAsync(string TableName);
    public Task UpdateColumnAsync(string TableName,string s, string ColumnNam0e,string ss);
    public Task DeleteColumnAsync(string TableName, string ColumnName);


    public Task CreateDatabaseAsync(string NameDataBaseName);
    public Task<List<string>> ListDataBaseAsync();
    public Task UpdateDataBaseAsync(string OldDataBaseName,string NewDataBaseName);
    public Task DeleteDataBaseAsync(string DataBaseName);


    public Task<(List<string>, List<string>, int)> GetRowsAsync(string TableName,int PageSize,int Offset);


    public void ChangeDataBase(string DataBaseName);
    public Task<(string, bool)> OpenConnectionStringAsync(string LocalHost, string DbNameDefault, int Port, string User, string Password);


    public Task<(List<string>, List<string>, int)> SearchAsync(string TableName, string ColumnName, string Search, int PageSize, int Offset);

    public Task CreateTableAsync(string TableName);
    public Task<List<string>> ListTableAsync();
    public Task UpdateTableAsync(string OldTableName,string NewTableName);
    public Task DeleteTableAsync(string TableName);


    public  Task<List<string>> ListUsersAsync();

    public Task<string> ValueInputAsync(string TableName, List<string> value);

    public  Task<(List<string>,List<string>,string)> WriteSelectQueryAsync(string query);
    public  Task<string> WriteChangeQueryAsync(string query);
}
