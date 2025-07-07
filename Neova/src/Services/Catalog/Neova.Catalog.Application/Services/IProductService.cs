
namespace Neova.Catalog.Application.Services
{
    public interface IProductService
    {
        //Her işlem; her yapılacak şey fonksiyon mu olmalı?
        void CreateNewProduct();
        void DeleteProduct();
        List<string> GetProducts();
        void UpdateProduct();
    }
}