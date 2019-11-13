using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webApp.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name must have a minimum of 2 and a maximum of 50 characters"), MinLength(2)]
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        [RegularExpression(@"[0-9]{4}\-[0-9]{2}", ErrorMessage = "Date is out of format: YYYY-MM")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Registry is required")]
        [Range(1, 8099999)]
        public int Registry { get; set; }

    }
}