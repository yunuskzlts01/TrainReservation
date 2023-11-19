using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaWepApi.Models
{
    public class Train
    {
        public string Ad { get; set; }
        public List<Vagon> Vagonlar { get; set; }
    }
}