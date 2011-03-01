using System;
using Caliburn.Micro;
using Nubis.Core;
using Nubis.Funding.Resources;

namespace Nubis.Funding.ViewModels
{
    public class FundingDataViewModel : Screen, IModule
    {
        public FundingDataViewModel()
        {
            DisplayName = Strings.FundingDataViewModel_DisplayName;
        }

        public string IconFileName
        {
            get { return "Nubis.Funding;component/Resources/Images/FundingData.png"; }
        }

        public string ToolTip
        {
            get { return Strings.FundingDataViewModel_ToolTip; }
        }

        decimal _einDarlehensBetrag;
        public decimal EinDarlehensBetrag
        {
            get { return _einDarlehensBetrag; }
            set
            {
                _einDarlehensBetrag = value;
                NotifyOfPropertyChange(() => EinDarlehensBetrag);
            }
        }

        decimal _einNominalZins;
        public decimal EinNominalZins
        {
            get { return _einNominalZins; }
            set 
            { 
                _einNominalZins = value;
                NotifyOfPropertyChange(() => EinNominalZins);
            }
        }

        decimal _einEffektivZins;
        public decimal EinEffektivZins
        {
            get { return _einEffektivZins; }
            set 
            { 
                _einEffektivZins = value;
                NotifyOfPropertyChange(() => EinEffektivZins);
            }
        }

        int _einEinLaufzeit;
        public int EinLaufzeit
        {
            get { return _einEinLaufzeit; }
            set
            {
                _einEinLaufzeit = value;
                NotifyOfPropertyChange(() => EinLaufzeit);
            }
        }

        int _einFestZinsDauer;
        public int EinFestZinsDauer
        {
            get { return _einFestZinsDauer; }
            set
            {
                _einFestZinsDauer = value;
                NotifyOfPropertyChange(() => EinFestZinsDauer);
            }
        }

        decimal _einMonatsRate;
        public decimal EinMonatsRate
        {
            get { return _einMonatsRate; }
            set
            {
                _einMonatsRate = value;
                NotifyOfPropertyChange(() => EinMonatsRate);
            }
        }

    }
}