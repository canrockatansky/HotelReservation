using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotels302.Models
{
    public class RoomType:BaseEntity
    {
        [StringLength(10), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Oda Tipi")]
        public string RoomTypeName { get; set; }

        [StringLength(10), Required(ErrorMessage = "Bu alan doldurulmalıdır!"), DisplayName("Yatak Tipi")]

        public string BedType { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

    }
}