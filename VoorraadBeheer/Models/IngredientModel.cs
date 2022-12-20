using System.Text.Json.Serialization;

namespace VoorraadBeheer.Models
{
    public class IngredientModel
    {
        public IngredientModel()
        {
            Id = -1;
        }

        public IngredientModel(IngredientModel ingredientModel)
        {
            Id = ingredientModel.Id;
            Ingredient = ingredientModel.Ingredient;
            Amount = ingredientModel.Amount;
            Unit = ingredientModel.Unit;
            UnitSmall = ingredientModel.UnitSmall;
            IsEditing = ingredientModel.IsEditing;
        }

        public int Id { get; set; }
        public string Ingredient { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }
        public string UnitSmall { get; set; }

        [JsonIgnore]
        public bool IsEditing { get; set; }
    }
}
