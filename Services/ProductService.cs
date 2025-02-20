using NetCoreMVC.Models;

namespace NetCoreMVC.Services;

public class ProductService : List<ProductModel>{
    public ProductService(){
        this.AddRange(new ProductModel[]{
            new ProductModel{Id = 1, Name = "Doraemon", Price = 1000},
            new ProductModel{Id = 2, Name = "Nobita", Price = 2000},
            new ProductModel{Id = 3, Name = "Xuka", Price = 3000},
            new ProductModel{Id = 4, Name = "Chaien", Price = 4000},
            new ProductModel{Id = 5, Name = "Doremi", Price = 5000}
        });
    }
}