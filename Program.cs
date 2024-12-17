namespace Restoran_Sipariş_Yönetim_Sistemi_Restaurant_Order_Management_System_
{
    class Program
    {
        // Yemek Sınıfı
        class Yemek
        {
            public int Id { get; set; }
            public string Ad { get; set; }
            public decimal Fiyat { get; set; }
        }

        // Sipariş Sınıfı
        class Siparis
        {
            public int Id { get; set; }
            public string MusteriAdi { get; set; }
            public List<Yemek> Yemekler { get; set; }
            public decimal ToplamTutar => Yemekler.Sum(y => y.Fiyat);
            public bool Tamamlandi { get; set; }
        }

        static List<Yemek> menu = new List<Yemek>();
        static List<Siparis> siparisler = new List<Siparis>();
        static int yemekIdSayaci = 1;
        static int siparisIdSayaci = 1;

        static void Main()
        {
            OrnekVerilerEkle(); // Başlangıç için örnek yemekler
            while (true)
            {
                AnaMenu();
            }
        }

        // Ana Menü
        static void AnaMenu()
        {
            Console.WriteLine("\n=== Restoran Sipariş Yönetim Sistemi ===");
            Console.WriteLine("1. Menü Görüntüle ve Sipariş Ver");
            Console.WriteLine("2. Siparişleri Görüntüle");
            Console.WriteLine("3. Siparişi Tamamla");
            Console.WriteLine("4. Menü Yönetimi");
            Console.WriteLine("5. Çıkış");
            Console.Write("Seçiminizi yapın: ");

            string secim = Console.ReadLine();
            switch (secim)
            {
                case "1":
                    SiparisVer();
                    break;
                case "2":
                    SiparisleriListele();
                    break;
                case "3":
                    SiparisTamamla();
                    break;
                case "4":
                    MenuYonetimi();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyin.");
                    break;
            }
        }

        // Menü ve Sipariş Ver
        static void SiparisVer()
        {
            Console.WriteLine("\n=== Menü ===");
            foreach (var yemek in menu)
            {
                Console.WriteLine($"ID: {yemek.Id}, Ad: {yemek.Ad}, Fiyat: {yemek.Fiyat:C}");
            }

            Console.Write("\nMüşteri Adı: ");
            string musteriAdi = Console.ReadLine();

            Console.Write("Sipariş için yemek ID'lerini ',' ile ayırarak girin (örnek: 1,2,3): ");
            string[] yemekIdleri = Console.ReadLine()?.Split(',');

            List<Yemek> secilenYemekler = new List<Yemek>();
            foreach (var id in yemekIdleri)
            {
                int yemekId;
                if (int.TryParse(id.Trim(), out yemekId))
                {
                    var yemek = menu.Find(y => y.Id == yemekId);
                    if (yemek != null)
                    {
                        secilenYemekler.Add(yemek);
                    }
                }
            }

            if (secilenYemekler.Count == 0)
            {
                Console.WriteLine("Geçerli yemekler seçilmedi. Sipariş iptal edildi.");
                return;
            }

            Siparis yeniSiparis = new Siparis
            {
                Id = siparisIdSayaci++,
                MusteriAdi = musteriAdi,
                Yemekler = secilenYemekler,
                Tamamlandi = false
            };

            siparisler.Add(yeniSiparis);
            Console.WriteLine($"Sipariş başarıyla oluşturuldu. Sipariş ID: {yeniSiparis.Id}");
        }

        // Siparişleri Listele
        static void SiparisleriListele()
        {
            Console.WriteLine("\n=== Aktif Siparişler ===");
            foreach (var siparis in siparisler)
            {
                Console.WriteLine($"Sipariş ID: {siparis.Id}, Müşteri: {siparis.MusteriAdi}, Toplam Tutar: {siparis.ToplamTutar:C}, Tamamlandı: {siparis.Tamamlandi}");
                Console.WriteLine("Yemekler:");
                foreach (var yemek in siparis.Yemekler)
                {
                    Console.WriteLine($"- {yemek.Ad} ({yemek.Fiyat:C})");
                }
            }
        }

        // Sipariş Tamamla
        static void SiparisTamamla()
        {
            Console.Write("\nTamamlamak istediğiniz siparişin ID'sini girin: ");
            int id = int.Parse(Console.ReadLine());

            var siparis = siparisler.Find(s => s.Id == id);
            if (siparis == null)
            {
                Console.WriteLine("Sipariş bulunamadı.");
                return;
            }

            siparis.Tamamlandi = true;
            Console.WriteLine("Sipariş başarıyla tamamlandı.");
        }

        // Menü Yönetimi
        static void MenuYonetimi()
        {
            Console.WriteLine("\n=== Menü Yönetimi ===");
            Console.WriteLine("1. Yeni Yemek Ekle");
            Console.WriteLine("2. Mevcut Yemekleri Görüntüle");
            Console.WriteLine("3. Yemek Sil");
            Console.WriteLine("4. Geri Dön");
            Console.Write("Seçiminizi yapın: ");

            string secim = Console.ReadLine();
            switch (secim)
            {
                case "1":
                    YemekEkle();
                    break;
                case "2":
                    YemekleriListele();
                    break;
                case "3":
                    YemekSil();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }
        }

        // Yemek Ekle
        static void YemekEkle()
        {
            Console.Write("Yemek Adı: ");
            string ad = Console.ReadLine();

            Console.Write("Fiyat: ");
            decimal fiyat = decimal.Parse(Console.ReadLine());

            menu.Add(new Yemek { Id = yemekIdSayaci++, Ad = ad, Fiyat = fiyat });
            Console.WriteLine("Yemek başarıyla eklendi.");
        }

        // Yemek Listele
        static void YemekleriListele()
        {
            Console.WriteLine("\n=== Menü ===");
            foreach (var yemek in menu)
            {
                Console.WriteLine($"ID: {yemek.Id}, Ad: {yemek.Ad}, Fiyat: {yemek.Fiyat:C}");
            }
        }

        // Yemek Sil
        static void YemekSil()
        {
            Console.Write("Silmek istediğiniz yemeğin ID'sini girin: ");
            int id = int.Parse(Console.ReadLine());

            var yemek = menu.Find(y => y.Id == id);
            if (yemek == null)
            {
                Console.WriteLine("Yemek bulunamadı.");
                return;
            }

            menu.Remove(yemek);
            Console.WriteLine("Yemek başarıyla silindi.");
        }

        // Örnek Veriler
        static void OrnekVerilerEkle()
        {
            menu.Add(new Yemek { Id = yemekIdSayaci++, Ad = "Kebap", Fiyat = 120 });
            menu.Add(new Yemek { Id = yemekIdSayaci++, Ad = "Lahmacun", Fiyat = 40 });
            menu.Add(new Yemek { Id = yemekIdSayaci++, Ad = "Çorba", Fiyat = 25 });
        }
    }
}
