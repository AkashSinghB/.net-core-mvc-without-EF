using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCrudeWithoutEF_EntityFramework_.Models
{
    public class BookViewModel
    {
        [Key]
        public int BookId  { get; set; }
        [Required]
        public string Title  { get; set; }
        [Required]
        public string Author { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Should be greater or equal to 1")]
        public int Price { get; set; }
    }
}
