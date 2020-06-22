using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Vezb_I
{
    public class Racun
    {
        public int ID { get; set; }
        public DateTime Datum { get; set; } = DateTime.Now;
        public List<ArtKol> ArtikliKolicina { get; set; } = new List<ArtKol>();

        public decimal Total { get => ArtikliKolicina.Sum(ak => ak.Zbir); }
    }
}
