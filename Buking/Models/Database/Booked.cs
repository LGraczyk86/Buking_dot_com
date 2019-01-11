using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Buking.Models.Database
{
    public class Booked
    {
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Data początku rezerwacji")]
        [DisplayFormat(DataFormatString = @"{0:yyyy-MM-dd}")]
        public DateTime DateStart { get; set; }

        [Required]
        [DisplayName("Data zakończenia rezerwacji")]
        [DisplayFormat(DataFormatString = @"{0:yyyy-MM-dd}")]
        public DateTime DateStop { get; set; }
        
        [ForeignKey("GuestHouse")]
        public Guid? GuestHouse_Id { get; set; }

        [DisplayName("Zarezerwowano przez")]
        public virtual IdentityUser BookedBy { get; set; }

        public virtual GuestHouse GuestHouse { get; set; }

        [DisplayName("Utworzone przez")]
        public virtual IdentityUser CreatedBy { get; set; }
        
    }
}