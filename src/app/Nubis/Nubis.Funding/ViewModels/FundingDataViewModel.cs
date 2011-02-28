using System;
using Caliburn.Micro;
using Nubis.Core;
using Nubis.Funding.Resources;

namespace Nubis.Funding.ViewModels
{
    public class FundingDataViewModel : Screen, IModule
    {
        public  FundingDataViewModel()
        {
            DisplayName = Strings.FundingDataViewModel_DisplayName;
        }

        public string IconFileName
        {
            get { return "Nubis.Funding;component/Resources/FundingDataView.png"; }
        }

        public string ToolTip
        {
            get { return Strings.FundingDataViewModel_ToolTip; }
        }
    }
}