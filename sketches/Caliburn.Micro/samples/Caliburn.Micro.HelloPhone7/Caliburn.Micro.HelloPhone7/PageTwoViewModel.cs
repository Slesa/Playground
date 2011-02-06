using System;

namespace Caliburn.Micro.HelloPhone7
{
    [SurviveTombstone]
    public class PageTwoViewModel : Conductor<IScreen>.Collection.OneActive
    {
        Func<TabViewModel> createTab;
        PivotFix<IScreen> pivotFix;
    }
}