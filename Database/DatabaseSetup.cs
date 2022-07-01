using Microsoft.Data.Sqlite;

namespace Avaliacao3BimLp3.Database;

class DatabaseSetup
{
    private DatabaseConfig databaseConfig; 

    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        this.databaseConfig=databaseConfig;
        CreateProductTable();
    }
    
    public void CreateProductTable()
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Computers(
                id int not null primary key, 
                name varchar(100) not null,
                price varchar(100) not null
            );
        ";
        command.ExecuteNonQuery();

        connection.Close();
    }
}
