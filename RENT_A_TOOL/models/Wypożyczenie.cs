using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RENT_A_TOOL.models
{
    internal class Wypożyczenie
    {
        public int Id { get; set; }

        public int ID_Klienta { get; set; }
        public Użytkownik Klient { get; set; } = null!;

        public int ID_Sprzet { get; set; }
        public Sprzęt Sprzet { get; set; } = null!;

        public DateTime DataWypozyczenia { get; set; }
        public DateTime? DataZwrotu { get; set; }
    }
}
