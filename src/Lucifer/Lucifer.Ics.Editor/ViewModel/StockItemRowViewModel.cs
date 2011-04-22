using Lucifer.Editor;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class StockItemRowViewModel : SelectableRowViewModelBase<StockItem>
    {
        public StockItemRowViewModel(StockItem stockItem)
        {
            ElementData = stockItem;
        }
        public void ExchangeData(StockItem stockItem)
        {
            ElementData = stockItem;
        }

        public int Id { get { return ElementData.Id; } }

        public string RecipeableItemType
        {
            get { return ElementData.RecipeableItem.GetRecipeableItemType(); }
        }

        public decimal Quantity
        {
            get { return ElementData.Quantity; }
            set
            {
                if (value == ElementData.Quantity)
                    return;
                ElementData.Quantity = value;
                NotifyOfPropertyChange(() => Quantity);
            }
        }

        public Unit Unit
        {
            get { return ElementData.Unit; }
            set
            {
                if (value == ElementData.Unit)
                    return;
                ElementData.Unit = value;
                NotifyOfPropertyChange(() => Unit);
            }
        }

        public RecipeableItem RecipeableItem
        {
            get { return ElementData.RecipeableItem; }
            set
            {
                if (value == ElementData.RecipeableItem)
                    return;
                ElementData.RecipeableItem = value;
                NotifyOfPropertyChange(() => RecipeableItem);
                NotifyOfPropertyChange(() => RecipeableItemType);
                if (ElementData.RecipeableItem != null)
                    Unit = ElementData.RecipeableItem.RecipeUnit;
            }
        }
    }
}