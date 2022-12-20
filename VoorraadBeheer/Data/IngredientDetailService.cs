using VoorraadBeheer.Models;
using VoorraadBeheer.Pages;

namespace VoorraadBeheer.Data
{
    public class IngredientDetailService
    {
        private IngredientConnections _ingredientConnections = new IngredientConnections();

        public async Task<bool> IngredientExists(string ingredient)
        {
            bool exists = await _ingredientConnections.IngredientExists(ingredient);
            return exists;
        }

        public async Task<IEnumerable<IngredientModel>> GetIngredients()
        {
            IEnumerable<IngredientModel> ingredients;
            ingredients = _ingredientConnections.GetIngredients().Result.ToArray();
            return ingredients;
        }

        public async Task<IngredientModel> GetIngredient(string ingredient)
        {
            IngredientModel ingr;
            ingr = await _ingredientConnections.GetIngredient(ingredient);
            return ingr;
        }

        public async Task<IngredientModel> GetIngredientById(int id)
        {
            IngredientModel ingr;
            ingr = await _ingredientConnections.GetIngredientById(id);
            return ingr;
        }

        public async Task<IEnumerable<IngredientModel>> AddIngredient(string ingredient, float amount, int unitId)
        {
            IEnumerable<IngredientModel> ingredients;
            ingredients = _ingredientConnections.AddIngredient(ingredient, amount, unitId).Result.ToArray();
            return ingredients;
        }

        public async Task<bool> UpdateIngredient(int id, string ingredient, float amount, int unitId)
        {
            bool succes = await _ingredientConnections.UpdateIngredient(id, ingredient, amount, unitId);
            return succes;
        }

        public async Task<bool> UpdateIngredientAmount(string ingredient, float amount)
        {
            bool succes = await _ingredientConnections.UpdateIngredientAmount(ingredient, amount);
            return succes;
        }

        public async Task<bool> UpdateIngredientAmountById(int id, float amount)
        {
            bool succes = await _ingredientConnections.UpdateIngredientAmountById(id, amount);
            return succes;
        }

        public async Task<bool> UpdateIngredientAmountAdd(string ingredient, float amount)
        {
            bool succes = await _ingredientConnections.UpdateIngredientAmountAdd(ingredient, amount);
            return succes;
        }

        public async Task<bool> UpdateIngredientAmountAddById(int id, float amount)
        {
            bool succes = await _ingredientConnections.UpdateIngredientAmountAddById(id, amount);
            return succes;
        }

        public async Task<bool> RemoveIngredient(string ingredient)
        {
            bool succes = await _ingredientConnections.RemoveIngredient(ingredient);
            return succes;
        }

        public async Task<bool> RemoveIngredientById(int id)
        {
            bool succes = await _ingredientConnections.RemoveIngredientById(id);
            return succes;
        }
    }
}
