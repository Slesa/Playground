using System.Collections.Generic;
using Caliburn.Micro;

namespace MediaOwl.Core
{
    /// <summary>
    /// A Helper-Class to execute Coroutines from code and not always have to create an <see cref="IEnumerator&lt;IResult&gt;"/>
    /// </summary>
    public class Run
    {
        /// <summary>
        /// Execute a single coroutine;
        /// </summary>
        /// <param name="coroutine">The <see cref="IResult"/></param>
        /// <param name="context">The <see cref="ActionExecutionContext"/></param>
        public static void Coroutine(IResult coroutine, ActionExecutionContext context = null)
        {
            Coroutine(new[] { coroutine }, context);
        }

        /// <summary>
        /// Execute a enumeration of coroutines.
        /// </summary>
        /// <param name="coroutines">The <see cref="IEnumerator&lt;IResult&gt;"/></param>
        /// <param name="context">The <see cref="ActionExecutionContext"/></param>
        public static void Coroutine(IEnumerator<IResult> coroutines, ActionExecutionContext context = null)
        {
            if (context == null)
                Caliburn.Micro.Coroutine.Execute(coroutines);
            else
                Caliburn.Micro.Coroutine.Execute(coroutines, context);
        }

        /// <summary>
        /// Execute an enumerable of coroutines
        /// </summary>
        /// <param name="coroutines">The <see cref="IEnumerable&lt;IResult&gt;"/></param>
        /// <param name="context">The <see cref="ActionExecutionContext"/></param>
        public static void Coroutine(IEnumerable<IResult> coroutines, ActionExecutionContext context = null)
        {
            Coroutine(coroutines.GetEnumerator(), context);
        }
    }
}