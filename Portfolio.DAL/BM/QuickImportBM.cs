using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DAL.BM
{
    public class QuickImportBM
    {
        public Guid? PrictureGroupId { get; set; }
        public PictureBM[] pictures { get; set; }
    }
}
