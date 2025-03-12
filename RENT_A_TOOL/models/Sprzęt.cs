using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RENT_A_TOOL.models
{
    internal class Sprzęt
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public int StanMagazynowy { get; set; }
        public byte[]? Zdjecie { get; set; }

    }
}
