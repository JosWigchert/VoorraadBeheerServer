using VoorraadBeheer.Models;
using VoorraadBeheer.Pages;

namespace VoorraadBeheer.Data
{
    public class ProductDetailService
    {
        private ProductConnections _productConnections = new ProductConnections();

        public async Task<bool> ProductExists(string product)
        {
            bool exists = await _productConnections.ProductExists(product);
            return exists;
        }

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            IEnumerable<ProductModel> products;
            products = _productConnections.GetProducts().Result;
            return products;
        }

        public async Task<ProductModel> GetProduct(string product)
        {
            ProductModel ingr;
            ingr = await _productConnections.GetProduct(product);
            return ingr;
        }

        public async Task<ProductModel> GetProductById(int id)
        {
            ProductModel ingr;
            ingr = await _productConnections.GetProductById(id);
            return ingr;
        }

        public async Task<IEnumerable<ProductModel>> AddProduct(string product, int amount)
        {
            IEnumerable<ProductModel> products;
            products = _productConnections.AddProduct(product, amount).Result;
            return products;
        }

        public async Task<bool> UpdateProduct(int id, string product, int amount)
        {
            bool succes = await _productConnections.UpdateProduct(id, product, amount);
            return succes;
        }

        public async Task<bool> UpdateProductAmount(string product, int amount)
        {
            bool succes = await _productConnections.UpdateProductAmount(product, amount);
            return succes;
        }

        public async Task<bool> UpdateProductAmountById(int id, int amount)
        {
            bool succes = await _productConnections.UpdateProductAmountById(id, amount);
            return succes;
        }

        public async Task<bool> UpdateProductAmountAdd(string product, int amount)
        {
            bool succes = await _productConnections.UpdateProductAmountAdd(product, amount);
            return succes;
        }

        public async Task<bool> UpdateProductAmountAddById(int id, int amount)
        {
            bool succes = await _productConnections.UpdateProductAmountAddById(id, amount);
            return succes;
        }

        public async Task<bool> RemoveProduct(string product)
        {
            bool succes = await _productConnections.RemoveProduct(product);
            return succes;
        }

        public async Task<bool> RemoveProductById(int id)
        {
            bool succes = await _productConnections.RemoveProductById(id);
            return succes;
        }



        public async Task<IEnumerable<ProductIngredientModel>> GetProductIngredients(int productId)
        {
            IEnumerable<ProductIngredientModel> ingredients;
            ingredients = _productConnections.GetProductIngredients(productId).Result;
            return ingredients;
        }

        public async Task<bool> AddProductIngredient(int productId, int ingredientId, float amount)
        {
            bool succes = await _productConnections.AddProductIngredient(productId, ingredientId, amount);
            return succes;
        }

        public async Task<bool> UpdateProductIngredientAmount(int productId, int ingredientId, float amount)
        {
            bool succes = await _productConnections.UpdateProductIngredientAmount(productId, ingredientId, amount);
            return succes;
        }

        public async Task<bool> UpdateProductIngredient(int Id, int ingredientId, float amount)
        {
            bool succes = await _productConnections.UpdateProductIngredient(Id, ingredientId, amount);
            return succes;
        }

        public async Task<bool> RemoveProductIngredient(int productId, int ingredientId)
        {
            bool succes = await _productConnections.RemoveProductIngredient(productId, ingredientId);
            return succes;
        }

        public async Task<bool> ProductIngredientExists(int productId, int ingredientId)
        {
            bool succes = await _productConnections.ProductIngredientExists(productId, ingredientId);
            return succes;
        }
    }
}
