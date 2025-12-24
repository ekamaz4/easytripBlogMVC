using System.ComponentModel.DataAnnotations;

namespace easy_trip.Models.Siniflar
{
    public class Yorumlar
    {
        [Key]
        public int ID{ get; set; }
        public string KullaniciAdi { get; set; }
        public string MailAdres { get; set; }
        public string Yorum { get; set; }
        public int Blogid { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
