using PaymentMicroServices.Application.Interfaces;

namespace PaymentMicroServices.InfraStructure.Services
{
    public class ServicesManager(Func<IFineServices> IFine) : IServicesManager
    {
        public IFineServices FineServices => IFine.Invoke();
    }
}
