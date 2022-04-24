using Microsoft.Extensions.Logging;
using RapaportWebApplication.Controllers;
using RapaportWebApplication.Interfaces;
using RapaportWebApplication.Models;
using System.Data;
using System.Data.OleDb;
using System.Linq;

namespace RapaportWebApplication.Services
{
    public class DiamondsCsvService : IDiamondsCsvService
    {
        private readonly ILogger<DiamondsCsvService> _logger;
        public DiamondsCsvService(ILogger<DiamondsCsvService> logger)
        {
            _logger = logger;
        }



        //write new data to the diamond list
        public void AddDiamondToCsvFile(DiamondObject diamondObject)
        {
            try
            {
                string path = @"C:\Users\haimha\Downloads\Diamonds\Diamonds.csv";
                List<DiamondObject> diamondCsvList = File.ReadAllLines(path)
                               .Skip(1)
                               .Select(v => DiamondObject.FromCsv(v))
                               .ToList();

                string[] newLine ={ $"{diamondObject.shape},{diamondObject.size},{diamondObject.color}," +
                        $"{diamondObject.clarity},{diamondObject.price},{diamondObject.list_price}" };

                File.AppendAllLines(path, newLine);
                _logger.LogInformation("AddDiamondToCsvFile successed");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;//return to the client error 500
            }

        }        
        
        //get all the diamonds list
        public List<DiamondObject> GetDiamondListFromCsvFile()
        {
            try
            {
                string path = @"C:\Users\haimha\Downloads\Diamonds\Diamonds.csv";

                List<DiamondObject> diamondCsvList = File.ReadAllLines(path)
               .Skip(1)
               .Select(v => DiamondObject.FromCsv(v))
               .ToList();

                return diamondCsvList;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;//return to the client error 500
            }

        }
    }
}
