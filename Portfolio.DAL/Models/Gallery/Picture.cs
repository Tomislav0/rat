using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DAL.Models.Protfolio
{
    public class Picture
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        public DateTime? TakenAt { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public Guid? PrictureGroupId { get; set; }
    }
}
