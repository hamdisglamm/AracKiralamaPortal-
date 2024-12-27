using AracKiralamaPortal.Models; // Reservation sınıfını tanımlayan namespace

namespace AracKiralamaPortal.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int ReservationId { get; set; } // İlgili rezervasyon ID'si
        public decimal Amount { get; set; } // Ödeme tutarı
        public DateTime PaymentDate { get; set; } // Ödeme tarihi
        public string PaymentMethod { get; set; } // Ödeme yöntemi

        public Reservation Reservation { get; set; } // Navigation Property
    }
}
