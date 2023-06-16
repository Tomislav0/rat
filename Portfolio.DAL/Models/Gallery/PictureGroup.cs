﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DAL.Models.Protfolio
{
    public class PictureGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("PrictureGroupId")]
        public virtual ICollection<Picture> Pictures { get; set; }

    }
}
