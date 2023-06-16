using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DAL.BM
{
    public class PictureBM
    {
        public Guid Id { get; set; }
        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        public DateTime? TakenAt { get; set; }
    }
}
