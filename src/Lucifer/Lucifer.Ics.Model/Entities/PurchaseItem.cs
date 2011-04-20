namespace Lucifer.Ics.Model.Entities
{
    public class PurchaseItem : RecipeableItem
    {
        public virtual PurchaseFamily PurchaseFamily { get; set; }
        public virtual Unit PurchaseUnit { get; set; }
    }
}