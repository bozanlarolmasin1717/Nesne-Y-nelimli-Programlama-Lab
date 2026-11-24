using System;
using System.Collections.Generic;

namespace AkilliEvSimulatoru
{
    // 1. İster: Cihaz türlerini temsil eden enum
    public enum CihazTuru
    {
        Lamba = 1,
        Klima = 2,
        GuvenlikSistemi = 3
    }

    // 2. İster: Cihaz sınıfı
    public class Cihaz
    {
        // 3. İster: Kimlik, Ad, Tur ve AcikMi bilgileri
        public int Kimlik { get; private set; }
        public string Ad { get; private set; }
        public CihazTuru Tur { get; private set; }
        public bool AcikMi { get; private set; }

        // 4. İster: Constructor üzerinden değer atama ve başlangıçta kapalı olma
        public Cihaz(int kimlik, string ad, CihazTuru tur)
        {
            Kimlik = kimlik;
            Ad = ad;
            Tur = tur;
            AcikMi = false;
        }

        // 5. İster: Cihazı açma ve kapatma metotları
        public void Ac()
        {
            if (AcikMi)
            {
                Console.WriteLine($"'{Ad}' zaten açık durumda.");
            }
            else
            {
                AcikMi = true;
                Console.WriteLine($"'{Ad}' açıldı.");
            }
        }

        public void Kapat()
        {
            if (!AcikMi)
            {
                Console.WriteLine($"'{Ad}' zaten kapalı durumda.");
            }
            else
            {
                AcikMi = false;
                Console.WriteLine($"'{Ad}' kapatıldı.");
            }
        }

        // 6. İster: Cihaz bilgilerini ekrana yazdırma metodu
        public void BilgileriGoster()
        {
            string durum = AcikMi ? "Açık" : "Kapalı";
            Console.WriteLine($"Kimlik: {Kimlik} | Ad: {Ad} | Tür: {Tur} | Durum: {durum}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 7. İster: Cihazları List<Cihaz> içinde saklama ve başlangıç cihazları
            List<Cihaz> cihazlar = new List<Cihaz>
            {
                new Cihaz(1, "Salon Lambası", CihazTuru.Lamba),
                new Cihaz(2, "Salon Kliması", CihazTuru.Klima),
                new Cihaz(3, "Ev Güvenlik Sistemi", CihazTuru.GuvenlikSistemi)
            };

            bool devam = true;

            // 8. İster: Basit menü yapısı
            while (devam)
            {
                Console.WriteLine("\n=== AKILLI EV SİMÜLATÖRÜ ===");
                Console.WriteLine("1 - Cihazları Listele");
                Console.WriteLine("2 - Yeni Cihaz Ekle");
                Console.WriteLine("3 - Cihaz Aç");
                Console.WriteLine("4 - Cihaz Kapat");
                Console.WriteLine("0 - Çıkış");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();
                Console.WriteLine();

                switch (secim)
                {
                    case "1":
                        CihazlariListele(cihazlar);
                        break;
                    case "2":
                        YeniCihazEkle(cihazlar);
                        break;
                    case "3":
                        CihazDurumDegistir(cihazlar, acilsinMi: true);
                        break;
                    case "4":
                        CihazDurumDegistir(cihazlar, acilsinMi: false);
                        break;
                    case "0":
                        devam = false;
                        Console.WriteLine("Programdan çıkılıyor...");
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim! Lütfen menüdeki numaralardan birini giriniz.");
                        break;
                }
            }
        }

        static void CihazlariListele(List<Cihaz> cihazlar)
        {
            if (cihazlar.Count == 0)
            {
                Console.WriteLine("Sistemde kayıtlı cihaz bulunmamaktadır.");
                return;
            }

            Console.WriteLine("--- Evdeki Cihazlar ---");
            foreach (Cihaz cihaz in cihazlar)
            {
                cihaz.BilgileriGoster();
            }
        }

        static void YeniCihazEkle(List<Cihaz> cihazlar)
        {
            Console.Write("Cihaz Kimliği (Sayı): ");
            if (!int.TryParse(Console.ReadLine(), out int kimlik))
            {
                Console.WriteLine("Hata: Geçerli bir sayısal kimlik girmelisiniz.");
                return;
            }

            // 9. İster: Aynı kimliğe sahip cihaz eklenmesini engelleme
            if (CihazBul(cihazlar, kimlik) != null)
            {
                Console.WriteLine($"Hata: {kimlik} kimliğine sahip bir cihaz zaten sistemde mevcut!");
                return;
            }

            Console.Write("Cihaz Adı: ");
            string ad = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ad))
            {
                Console.WriteLine("Hata: Cihaz adı boş bırakılamaz.");
                return;
            }

            Console.WriteLine("Cihaz Türü Seçiniz:");
            Console.WriteLine("1 - Lamba");
            Console.WriteLine("2 - Klima");
            Console.WriteLine("3 - Güvenlik Sistemi");
            Console.Write("Tür seçiminiz: ");

            if (!int.TryParse(Console.ReadLine(), out int turSecim) || !Enum.IsDefined(typeof(CihazTuru), turSecim))
            {
                Console.WriteLine("Hata: Geçersiz cihaz türü seçtiniz.");
                return;
            }

            CihazTuru tur = (CihazTuru)turSecim;
            Cihaz yeniCihaz = new Cihaz(kimlik, ad.Trim(), tur);
            cihazlar.Add(yeniCihaz);

            Console.WriteLine($"'{yeniCihaz.Ad}' sisteme başarıyla eklendi (Durum: Kapalı).");
        }

        static void CihazDurumDegistir(List<Cihaz> cihazlar, bool acilsinMi)
        {
            Console.Write(acilsinMi ? "Açmak istediğiniz cihazın kimliği: " : "Kapatmak istediğiniz cihazın kimliği: ");
            if (!int.TryParse(Console.ReadLine(), out int kimlik))
            {
                Console.WriteLine("Hata: Geçerli bir sayısal kimlik girmelisiniz.");
                return;
            }

            Cihaz bulunanCihaz = CihazBul(cihazlar, kimlik);

            // 10. İster: Olmayan bir cihaz üzerinde işlem yapılmaya çalışıldığında uyarı gösterme
            if (bulunanCihaz == null)
            {
                Console.WriteLine($"Uyarı: {kimlik} kimliğine sahip bir cihaz bulunamadı!");
                return;
            }

            if (acilsinMi)
            {
                bulunanCihaz.Ac();
            }
            else
            {
                bulunanCihaz.Kapat();
            }
        }

        static Cihaz CihazBul(List<Cihaz> cihazlar, int kimlik)
        {
            foreach (Cihaz cihaz in cihazlar)
            {
                if (cihaz.Kimlik == kimlik)
                {
                    return cihaz;
                }
            }
            return null;
        }
    }
}