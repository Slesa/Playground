using Nubis.Maths.Model;

namespace Nubis.Maths.Contracts
{
    public interface ICalculateMissings
    {
        bool CanHandle(FundingData fundingData);
        void Calculate(FundingData fundingData);
    }
}