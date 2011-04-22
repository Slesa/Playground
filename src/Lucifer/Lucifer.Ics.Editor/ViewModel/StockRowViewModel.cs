using Lucifer.Editor;
using Lucifer.Ics.Model.Entities;
using System.Linq;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class StockRowViewModel : SelectableRowViewModelBase<Stock>
    {
        public StockRowViewModel(Stock stock)
        {
            ElementData = stock;
        }
        public void ExchangeData(Stock stock)
        {
            ElementData = stock;
        }

        public int Id { get { return ElementData.Id; } }
        public string Name { get { return ElementData.Name; } }
        public bool IsMainStock { get { return ElementData.IsMainStock; } }
        public string ItemNames
        {
            get
            {
                var names = ElementData.StockItems
                    .Where(x => x.RecipeableItem != null)
                    .Select(x => x.RecipeableItem.Name)
                    .Take(10);
                return string.Join(", ", names);
            }
        }
        
    }
}