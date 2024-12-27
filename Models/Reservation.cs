// Models/Reservation.cs
namespace AracKiralamaPortal.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Kullanıcı ID
        public int CarId { get; set; } // Araç ID
        public DateTime StartDate { get; set; } // Rezervasyon Başlangıç Tarihi
        public DateTime EndDate { get; set; } // Rezervasyon Bitiş Tarihi

        public Car Car { get; set; } // Navigation Property
    }
}
