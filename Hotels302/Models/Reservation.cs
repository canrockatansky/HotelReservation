using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hotels302.Models
{
    public class Reservation:BaseEntity
    {

        //public int Id { get; set; }

        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }

        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        public virtual RoomType RoomType { get; set; }

        public int ContactId { get; set; }
        [ForeignKey("ContactId")]
        public virtual Contact Contact { get; set; }

        [DisplayName("Giriş Tarihi")]
        [DataType("datetime-local")]
        [Required(ErrorMessage = "Tarih girilmelidir!")]
        public DateTime EntryDate { get; set; }

        [DisplayName("Çıkış Tarihi")]
        [DataType("datetime-local")]
        [Required(ErrorMessage = "Tarih girilmelidir!")]
        public DateTime ReleaseDate { get; set; }

        [DisplayName("Durum")]
        public Status Status { get; set; }
        //[DisplayName("Oluşturulma Tarihi")]
        //[DataType(DataType.DateTime)]
        //public DateTime CreateDate { get; set; }
        //[DisplayName("Oluşturan Kullanıcı")]
        //public string CreatedBy { get; set; }
        //[DisplayName("Güncelleme Tarihi")]
        //[DataType(DataType.DateTime)]
        //public DateTime UpdateDate { get; set; }
        //[DisplayName("Güncelleyen Kullanıcı")]
        //public string UpdatedBy { get; set; }

    }
}