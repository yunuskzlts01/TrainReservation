using AdaWepApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace AdaWepApi.Controllers
{

    public class TrainController : ApiController
    {
        [HttpPost]
        [Route("api/rezervasyon")]
        public IHttpActionResult RezervasyonYap(ReservationRequest request)
        {
            try
            {
                var tren = request.Tren;
                var rezervasyonKisiSayisi = request.RezervasyonYapilacakKisiSayisi;
                var kisilerFarkliVagonlaraYerlestirilebilir = request.KisilerFarkliVagonlaraYerlestirilebilir;

                // Rezervasyon yapılabilirlik kontrolü burada gerçekleştirilecek

                // Örnek bir kontrol: Eğer herhangi bir vagonun doluluk oranı %70'i geçiyorsa rezervasyon yapılamaz.
                var rezervasyonYapilabilir = tren.Vagonlar.All(vagon => (vagon.DoluKoltukAdet / vagon.Kapasite) * 100 < 70);

                if (!rezervasyonYapilabilir)
                {
                    return BadRequest("Rezervasyon yapılamaz. Vagonların doluluk oranı çok yüksek.");
                }

                // Örnek yerleşim algoritması: Koltukları dolduracak şekilde kişileri vagonlara yerleştir
                var kalanKisiSayisi = rezervasyonKisiSayisi;
                var yerlesimAyrinti = new List<ReservedSeatInfo>();

                foreach (var vagon in tren.Vagonlar)
                {
                    var kisiSayisi = Math.Min(kalanKisiSayisi, vagon.Kapasite - vagon.DoluKoltukAdet);

                    if (kisiSayisi > 0)
                    {
                        yerlesimAyrinti.Add(new ReservedSeatInfo { VagonAdi = vagon.Ad, KisiSayisi = kisiSayisi });
                        kalanKisiSayisi -= kisiSayisi;
                        vagon.DoluKoltukAdet += kisiSayisi;

                        if (kalanKisiSayisi == 0)
                        {
                            break; // Tüm kişiler yerleştirildi
                        }
                    }
                }

                return Ok(new { RezervasyonYapilabilir = true, YerlesimAyrinti = yerlesimAyrinti });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

