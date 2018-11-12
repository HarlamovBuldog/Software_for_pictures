using System;

namespace PicturesSoft
{
    public class CashBox
    {

        public string Number { get; set; }
        
        public string IpAddress { get; set; }

        public string ShopCode { get; set; }

        public override string ToString()
        {
            return String.Format("Касса №{0}", Number);
        }
    }
}
