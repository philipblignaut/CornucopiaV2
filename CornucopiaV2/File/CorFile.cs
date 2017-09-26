using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
    public static class CorFile
    {
        public static void WriteAllText
            (string path
            , string data
            )
        {
            File
                 .WriteAllText
                 (path
                 , data
                 )
            ;
        }
    }
}
