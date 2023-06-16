using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DAL.DTO
{
    public class PictureDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string PictureUrl { get; set; }
        public DateTime? TakenAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid? PrictureGroupId { get; set; }
    }
}
