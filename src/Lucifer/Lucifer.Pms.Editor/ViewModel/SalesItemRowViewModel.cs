using System;
using Lucifer.Editor;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class SalesItemRowViewModel : SelectableRowViewModelBase<SalesItem>
    {
        public SalesItemRowViewModel(SalesItem salesItem)
        {
            ElementData = salesItem;
        }
        public void ExchangeData(SalesItem salesItem)
        {
            ElementData = salesItem;
        }

        public int Id { get { return ElementData.Id; } }
        public string Name { get { return ElementData.Name; } }
        public SalesFamily SalesFamily
        {
            get { return ElementData.SalesFamily; }
            set { ElementData.SalesFamily = value; }
        }
    }
}