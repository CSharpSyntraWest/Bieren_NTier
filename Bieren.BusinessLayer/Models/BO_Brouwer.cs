
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bieren.BusinessLayer.Models
{
    public class BO_Brouwer 
    {
        public BO_Brouwer()
        {
            Bieren = new List<BO_Bier>();
        }
        
        #region properties
        public int BrouwerNr { get; set;  }
        public string BrNaam { get; set; }
        public string Straat { get; set; }
        public short? PostCode { get; set; }
        public string Gemeente { get; set; }
        public double? Omzet { get; set; }
        public IList<BO_Bier> Bieren { get; set; }
        
        #endregion


    }
}



