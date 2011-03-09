namespace Nubis.Maths.Model
{
    public class AnnuityData
    {
        readonly InterestData _interestData;

        public AnnuityData(InterestData interestData)
        {
            _interestData = interestData;
        }

        int _laufzeit;
        public int Laufzeit
        {
            get { return _laufzeit; }
            set { _laufzeit = value;  }
        }

        public decimal TilgungsRate { get; set; }
        public decimal Kreditsumme { get; set; }

        //        public decimal Annuität { get; set; }
    }
}
