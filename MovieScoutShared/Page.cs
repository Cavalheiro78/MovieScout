using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScoutShared
{
    public class Page
    {
        public int page { get; set; }
        public IEnumerable<Movie> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
