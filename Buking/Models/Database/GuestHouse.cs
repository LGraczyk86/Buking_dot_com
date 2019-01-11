using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Buking.Models.Database
{
    public class GuestHouse
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Miasto")]
        public string City { get; set; }

        [Required]
        [DisplayName("Ulica")]
        public string Street { get; set; }

        [DisplayName("Nr ulicy")]
        public int StreetNr { get; set; }

        [Required]
        [DisplayName("Region")]
        public string Region { get; set; }

       [DisplayName("Opis")]
        public string Discription { get; set; }

        [Required]
        [DisplayName("Cena")]
        public decimal Price { get; set; }

        //[Required]
        [DisplayName("Dodaj zdjęcie")]
        public string Path { get; set; }

        ////[Required]
        //[DisplayName("Dodaj zdjęcie")]
        //public HttpPostedFileBase Attachment { get; set; }

        [DisplayName("Dodano przez")]
        public virtual IdentityUser CreatedBy { get; set; }

        public ICollection<Booked> Bookeds { get; set; }
    }
}