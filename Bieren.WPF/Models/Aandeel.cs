using System;
using System.Collections.Generic;
using System.Text;

namespace Bieren.WPF.Models
{
    public class Aandeel
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double ChangesPercentage { get; set; }
        public double Change { get; set; }

        public override string ToString()
        {
            return $"{Symbol} Name:{Name}\n\tPrice: {Price}\n\tChange: {Change}\n\tChangesPercentage: {ChangesPercentage}";
        }
    }
}
