using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
    public class CRandom
    {

        private Random random = new Random((int)DateTime.Now.Ticks);

        public int NextRangeIncl(int from, int to)
        {
            return
                random
                .Next
                (from
                , to + 1
                )
                ;
        }
    }
}