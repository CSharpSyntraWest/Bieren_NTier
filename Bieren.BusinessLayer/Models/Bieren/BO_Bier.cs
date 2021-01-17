
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bieren.BusinessLayer.Models
{
    public class BO_Bier
    {
        public int BierNr { get; set; }
        public string Naam { get; set; }
        public double? Alcohol { get; set; }

        public BO_Brouwer Brouwer { get; set; }

        public BO_BierSoort BierSoort { get; set; }

    }
}


