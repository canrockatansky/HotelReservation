using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hotels302.Models
{
    public class Room:BaseEntity
    {

        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }

        [StringLength(10), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Oda Numarası")]
        public string RoomNo { get; set; }
        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        public virtual RoomType RoomType { get; set; }

        [StringLength(10), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Ücret")]
        public string Cost { get; set; }

        [Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Durum")]
        public Status Status { get; set; }

        [StringLength(350), DisplayName("Resim")]
        public string Foto { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}