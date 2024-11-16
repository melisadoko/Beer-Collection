using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCollectionBLL.DTO
{
    public class BeerCreatedDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal? Rating { get; set; }
    }
}
