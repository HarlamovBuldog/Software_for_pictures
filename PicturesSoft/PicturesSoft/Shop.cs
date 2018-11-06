using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesSoft
{
    public class Shop
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Address { get; set; }

        public List<CashBox> CashBoxes { get; set; } = new List<CashBox>();
    }
}
