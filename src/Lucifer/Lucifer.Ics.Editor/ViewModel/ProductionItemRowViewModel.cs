using Lucifer.Editor;
using Lucifer.Ics.Model.Entities;
using System.Linq;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class ProductionItemRowViewModel : SelectableRowViewModelBase<ProductionItem>
    {
        public ProductionItemRowViewModel(ProductionItem productionItem)
        {
            ElementData = productionItem;
        }
        public void ExchangeData(ProductionItem productionItem)
        {
            ElementData = productionItem;
        }

        public int Id { get { return ElementData.Id; } }
        public string Name { get { return ElementData.Name; } }
        public Unit RecipeUnit  { get { return ElementData.RecipeUnit; } }
        public string ItemNames
        {
            get
            {
                var names = ElementData.RecipeItems
                    .Where(x => x.RecipeableItem != null)
                    .Select(x => x.RecipeableItem.Name)
                    .Take(10);
                return string.Join(", ", names);
            }
        }
        
    }
}