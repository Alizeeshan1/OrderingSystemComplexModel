namespace OrderingSystem.Models
{
    public class UnitItem
    {
       
        public virtual Item Item { get; set; }
        public int ItemId { get; set; }

        public virtual Unit Unit { get; set; }
        public int UnitId { get; set; }
        
    }
}
