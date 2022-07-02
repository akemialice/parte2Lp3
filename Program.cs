using Microsoft.Data.Sqlite;
using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Repositories;
using Avaliacao3BimLp3.Models;

var databaseConfig = new DatabaseConfig();

var databaseSetup = new DatabaseSetup(databaseConfig);

var productRepository = new productRepository(databaseConfig);

// Routing
var modelName = args[0];
var modelAction = args[1];

    if(modelName == "Product")
    {
        if(modelAction == "List") {
            Console.WriteLine("Product List");
            foreach (var product in productRepository.GetAll()) {
                Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
            }
        }
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        string name = args[3];
        string price = args[4];
        bool active = args[5];
        var product = new Product(id,name,price,active);

        if (productRepository.existsById(id))
        {
            Console.WriteLine($"Produto de id {id} ja existe");
        }
        else
        {
        var product = new Product(id, name, price, active);
        productRepository.Save(product);
        }
    }

    if(modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);
        if(productRepository.ExitsById(id))
        {
            productRepository.Delete(id);
        }
        else
        {
            Console.WriteLine($"O produto de Id {id} não existe.");
        }        
    }

    if (modelAction == "Enable")
    {
        var id = Convert.ToInt32(args[2]);

        if (productRepository.ExistsByID(id))
        {
            productRepository.Enable(id);   
            Console.WriteLine($"Produto de {id} habilitado.");
        }
        else
        {
            Console.WriteLine($"Produto de {id} não foi encontrado.");
        }
    }

    if (modelAction == "Disable")
    {
        var id = Convert.ToInt32(args[2]);

        if (productRepository.ExistsByID(id))
        {
            productRepository.Disable(id);   
            Console.WriteLine($"Produto de {id} desabilitado.");
        }
        else
        {
            Console.WriteLine($"Produto de {id} não foi encontrado.");
        }
    }

    if(modelAction == "PriceBetween")
    {
        var initialPrice = Convert.ToDouble(args[2]);
        var endPrice = Convert.ToDouble(args[3]);
        if(productRepository.GetAllWithPriceBetween(initialPrice, endPrice).Count() == 0)
        {
            Console.WriteLine($"Nenhum produto encontrado dentro de R${initialPrice} e R${endPrice}");
        }
        else
        {
            foreach (var product in productRepository.GetAllWithPriceBetween(initialPrice, endPrice))
            {
                Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}");
            }
        }
    }
    
    if(modelAction == "PriceHigherThan")
    {
        var price = Convert.ToDouble(args[2]);
        if(productRepository.GetAllWithPriceHigherThan(price).Count() == 0)
        {
            Console.WriteLine($"Não encontramos produto com preço maior que: R${price}");
        }
        else
        {
            foreach (var product in productRepository.GetAllWithPriceHigherThan(price))
            {
                Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}");
            }
        }
    }
    
    if(modelAction == "AveragePrice")
    {
        if (!(productRepository.GetAll().Count() == 0))
        {
            Console.WriteLine($"Média dos preços dos produtos: R${productRepository.GetAveragePrice()}");
        }
    }


    if(modelAction == "PriceLowerThan")
    {
        var price = Convert.ToDouble(args[2]);
        
        if(productRepository.GetAllWithPriceLowerThan(price).Count() == 0)
        {
            Console.WriteLine($"Não encontramos produto com preço menor que: R${price}");
        }
        else
        {
            foreach (var product in productRepository.GetAllWithPriceLowerThan(price))
            {
                Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}");
            }
        }
    }

    

