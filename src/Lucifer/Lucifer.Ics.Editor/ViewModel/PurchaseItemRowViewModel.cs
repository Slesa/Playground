using System;
using Lucifer.Editor;
using Lucifer.Ics.Model.Entities;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class PurchaseItemRowViewModel : SelectableRowViewModelBase<PurchaseItem>
    {
        public PurchaseItemRowViewModel(PurchaseItem purchaseItem)
        {
            ElementData = purchaseItem;
        }
        public void ExchangeData(PurchaseItem purchaseItem)
        {
            ElementData = purchaseItem;
        }

        public int Id { get { return ElementData.Id; } }
        public string Name { get { return ElementData.Name; } }
        public PurchaseFamily PurchaseFamily
        {
            get { return ElementData.PurchaseFamily; }
            set { ElementData.PurchaseFamily = value; }
        }

        public Unit PurchaseUnit
        {
            get { return ElementData.PurchaseUnit; }
            set { ElementData.PurchaseUnit = value; }
        }

        public Unit RecipeUnit
        {
            get { return ElementData.RecipeUnit; }
            set { ElementData.RecipeUnit = value; }
        }
    }
}