using VirtualManager.Shared;

namespace VirtualManager.Client.Services
{
    public interface ITaxService
    {
        Task<IList<Tax>> TaxGetAll();
        Task<Tax> TaxGet(int id);
        Task<Tax> TaxSave(Tax obj);
        Task TaxDelete(int id);
        Task<IList<Tax>> TaxGetExcludedByIds(IList<int> ids);
    }
}
