using System.IO;

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
