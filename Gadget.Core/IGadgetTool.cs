using System;

namespace Gadget.Core
{
    public interface IGadgetTool<in TInput>
        where TInput : GadgetData
    {
        /// <summary>
        /// process input to output
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Go(TInput input);
    }
}
