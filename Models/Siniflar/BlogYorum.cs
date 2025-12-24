namespace easy_trip.Models.Siniflar
{
    public class BlogYorum
    {
        public IEnumerable<Blog> Deger1 { get; set; }
        public IEnumerable<Yorumlar> Deger2 { get; set; }
        public IEnumerable<Blog> Deger3 { get; set; }


        //Eklenebilir Bloglar

      //  public IEnumerable<Blog> SonBlogDegerler { get; set; }
        public IEnumerable<Yorumlar> SonYorumDegerler { get; set; }
    }
}
