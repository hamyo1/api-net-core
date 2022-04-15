using RapaportWebApplication.Models;

namespace RapaportWebApplication.Interfaces
{
    public interface IDiamondsCsvService
    {
        void AddDiamondToCsvFile(DiamondObject diamondObject);
        List<DiamondObject> GetDiamondListFromCsvFile();

    }
}
