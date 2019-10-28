using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hotels302.Models
{
    public class Hotel:BaseEntity
    {


        [StringLength(200), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Otel İsmi")]
        public string HotelName { get; set; }

        [StringLength(200), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Adres")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Bu Alanı doldurulmalıdır!")]

        [DisplayName("İl")]
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        
        public virtual City City { get; set; }
        
        [Required(ErrorMessage = "Bu Alanı doldurulmalıdır!"), DisplayName("İlçe")]

        public int TownId { get; set; }
        [ForeignKey("TownId")]
        public virtual Town Town { get; set; }
        
        [StringLength(500), DisplayName("Açıklama")]
        public string Explanation { get; set; }
        [Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Oda Sayısı")]
        public int RoomCapacity { get; set; }

        [StringLength(350), DisplayName("Resim")]
        public string Foto { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        
    }
}
//public int HotelId { get; set; }
//[ForeignKey("HotelId")]
//public virtual Hotel Hotel { get; set; }