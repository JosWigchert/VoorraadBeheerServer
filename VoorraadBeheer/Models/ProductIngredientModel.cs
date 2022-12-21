using System.Text.Json.Serialization;

namespace VoorraadBeheer.Models
{
    public class ProductIngredientModel
    {
        public ProductIngredientModel()
        {
            Id = -1;
            IngredientModel = new IngredientModel();
        }

        public ProductIngredientModel(ProductIngredientModel model)
        {
            Id = model.Id;
            IngredientModel = new IngredientModel(model.IngredientModel);
            IsEditing = model.IsEditing;
        }

        public int Id { get; set; }
        public IngredientModel IngredientModel { get; set; }

        [JsonIgnore]
        public bool IsEditing { get; set; }
    }
}
