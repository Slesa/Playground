using Nubis.Maths.Model;

namespace Nubis.Maths.Contracts
{
    public interface ICalculateMissings
    {
        bool CanHandle(AnnuityData annuityData);
        void Calculate(AnnuityData annuityData);
    }
}