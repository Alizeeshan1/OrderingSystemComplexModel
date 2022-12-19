namespace OrderingSystem.Models
{
    public class OrderedItem
    {

        public int ItemId { get; set; }
        public int UnitId { get; set; }
        public virtual Order Order { get; set; }
        public int? OrderId_FK { get; set; }

        public virtual Item Item { get; set; }
        public int? ItemId_Fk { get; set; }


        public virtual Unit Unit { get; set; }
        public int? UnitId_Fk { get; set; }


        public int Quantity { get; set; }
        public decimal Sub_Total { get; set; }
    }
}
