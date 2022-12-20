using System.Text.Json.Serialization;

namespace VoorraadBeheer.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            Id = -1;
        }

        public int Id { get; set; }
        public string Product { get; set; }
        public int Amount { get; set; }

        [JsonIgnore]
        public bool IsEditing { get; set; }
    }
}
