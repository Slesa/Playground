using System;
using System.Linq;

namespace Caliburn.Micro.HelloPhone7
{
    [SurviveTombstone]
    public class PageTwoViewModel : Conductor<IScreen>.Collection.OneActive
    {
        readonly Func<TabViewModel> _createTab;
        readonly PivotFix<IScreen> _pivotFix;

        public PageTwoViewModel(Func<TabViewModel> createTab)
        {
            _createTab = createTab;
            _pivotFix = new PivotFix<IScreen>(this);
        }

        public int NumberOfTabs { get; set; }

        protected override void OnInitialize()
        {
            Enumerable.Range(1, NumberOfTabs).Apply(x=>
                {
                    var tab = _createTab();
                    tab.DisplayName = "Item " + x;
                    Items.Add(tab);
                });

            ActivateItem(Items[0]);
        }

        protected override void OnViewLoaded(object view)
        {
            _pivotFix.OnViewLoaded(view, base.OnViewLoaded);
        }

        protected override void ChangeActiveItem(IScreen newItem, bool closePrevious)
        {
            _pivotFix.ChangeActiveItem(newItem, closePrevious, base.ChangeActiveItem);
        }
    }
}