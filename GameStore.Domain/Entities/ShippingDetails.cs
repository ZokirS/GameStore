using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Entities
{
   public class ShippingDetails
    {
        [Required(ErrorMessage ="Type your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Fill your shipping address")]
        [Display(Name ="First address")]
        public string Line1 { get; set; }
        [Display(Name = "Second address")]
        public string Line2 { get; set; }
        [Display(Name = "Third address")]
        public string Line3 { get; set; }

        [Required(ErrorMessage ="Write your City")]
        [Display(Name ="City")]
        public string City { get; set; }

        [Required(ErrorMessage ="Write your Country")]
        [Display(Name ="Country")]
        public string Country { get; set; }
        [Display(Name ="GiftWrap")]
        public bool GiftWrap { get; set; }
    }
}
