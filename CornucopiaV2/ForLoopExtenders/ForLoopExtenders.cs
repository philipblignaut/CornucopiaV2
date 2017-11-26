using System;

namespace CornucopiaV2
{
	public static class ForLoopExtenders
    {
        public static void For(this long limit, Action<long> predicate)
        {
            for (long index = 0; index < limit; index++)
            {
                predicate.Invoke(index);
            }
        }
        public static void For(this int limit, Action<int> predicate)
        {
            for (int index = 0; index < limit; index++)
            {
                predicate.Invoke(index);
            }
        }
    }
}
