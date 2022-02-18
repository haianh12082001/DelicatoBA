using System;
using System.ComponentModel.DataAnnotations;

namespace DelicatoBA.Models
{
    public class Subscribe
    {
        public int Id { get; set; }
        [Display(Name = "Email"), StringLength(50)]
        public string Email { get; set; }

        public DateTime CreateDate { get; set; }

        public Subscribe()
        {
            CreateDate = DateTime.Now;
        }
    }
}