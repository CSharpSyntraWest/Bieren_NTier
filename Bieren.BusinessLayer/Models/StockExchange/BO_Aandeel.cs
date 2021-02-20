namespace Bieren.BusinessLayer.Models
{
    public class BO_Aandeel
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double ChangesPercentage { get; set; }
        public double Change { get; set; }

    }
}