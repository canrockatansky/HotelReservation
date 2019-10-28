using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotels302.Models
{
    public class City:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Hotel> Hotel { get; set; }
    }
}