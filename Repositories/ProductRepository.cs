using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Avaliacao3BimLp3.Repositories;

class ProductRepository
{
     private DatabaseConfig  databaseConfig;

    public ProductRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig=databaseConfig;
    }

public IEnumerable<Product> GetAll(){
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        return connection.Query<Product>("SELECT * FROM Products");
    }


public Product Save(Product product){
    using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
    connection.Open();
    connection.Execute("INSERT INTO Products VALUES(@Id, @Name, @Price, @Active)",product );
    return product;
}
// Deleta um produto na tabela
public void Delete(int id){
    using var connection = new SqliteConnection(databaseConfig.ConnectionString);
    connection.Open();
    connection.Execute("DELETE FROM Products WHERE id = @Id", new {Id = id});
}

public void Enable(int id){
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("UPDATE Products SET active = 1 WHERE id = @Id", new {Id = id});
}

public void Disable(int id){
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("UPDATE Products SET active = 0 WHERE id = @Id", new {Id = id});
}

 public bool existsById (int id){
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var result = connection.ExecuteScalar<bool>("SELECT count(id) FROM Products WHERE id = @Id", new {Id= id});
        return result;
    }

public IEnumerable<Product> GetAllWithPriceBetween(double initialPrice, double endPrice){
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        return connection.Query<Product>("SELECT * FROM Products WHERE price BETWEEN @InitialPrice AND @EndPrice", new {InitialPrice = initialPrice, EndPrice = endPrice});
    }


public IEnumerable<Product> GetAllWithPriceHigherThan(double price) {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        return connection.Query<Product>("SELECT * FROM Products WHERE price > @Price", new {Price = price});
    }

 // Retorna os produtos com preço abaixo de um preço especificado
    public List<Product> GetAllWithPriceLowerThan(double price) {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        return connection.Query<Product>("SELECT * FROM Products WHERE price < @Price", new {Price = price}).ToList();
    }
    
    // Retorna a média dos preços dos produtos
    public double GetAveragePrice(){
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
        return connection.ExecuteScalar<double>("SELECT AVG(price) FROM Products");
    }
}
