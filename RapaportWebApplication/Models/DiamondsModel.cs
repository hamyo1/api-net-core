namespace RapaportWebApplication.Models
{
    public class DiamondObject
    {
        public string shape { get; set; }
        
        public decimal? size { get; set; }

        public string color { get; set; }

        public string clarity { get; set; }

        public decimal? price { get; set; }

        public decimal? list_price  { get; set; }


        public static DiamondObject FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            DiamondObject diamondObject = new DiamondObject();
            diamondObject.shape = Convert.ToString(values[0]);
            diamondObject.size = Convert.ToDecimal(values[1]);
            diamondObject.color = Convert.ToString(values[2]);
            diamondObject.clarity = Convert.ToString(values[3]);
            diamondObject.price = Convert.ToDecimal(values[4]);
            diamondObject.list_price = Convert.ToDecimal(values[5]);
            return diamondObject;
        }
    }
}
