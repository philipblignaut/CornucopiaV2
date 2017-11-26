using System;

namespace CornucopiaV2
{
	public class TypedFuncArgumentPair<T>
    {
        public Func<T, bool> Func { get; private set; }
        public T FuncArgument { get; private set; }
        public TypedFuncArgumentPair
           (Func<T, bool> func
           , T funcArgument
           )
        {
            Func = func;
            FuncArgument = funcArgument;
        }
    }
}
