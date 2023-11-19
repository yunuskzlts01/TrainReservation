using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaWepApi.Models
{
    public class ReservationRequest
    {
        public Train Tren { get; set; }
        public int RezervasyonYapilacakKisiSayisi { get; set; }
        public bool KisilerFarkliVagonlaraYerlestirilebilir { get; set; }
    }
}