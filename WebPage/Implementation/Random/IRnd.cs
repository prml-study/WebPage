using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPage.Implementation
{
    public interface IRnd
    {
        public double NextDouble();
        public IEnumerable<double> NextDoubles(int size);
    }
}
