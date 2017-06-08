using SQLite.Net.Attributes;

namespace Maratona.Model
{
    public class GroceryListItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(5)]
        public string Type { get; set; }

        public int Amount { get; set; }
        
        [Ignore]
        public string GroceryNameAmount => string.Format("{0} {1}", Amount, Type);
    }
}
