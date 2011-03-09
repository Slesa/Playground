using System.Collections.Generic;
using ActionExecutionContext = Caliburn.Micro.ActionExecutionContext;
using IResult = Caliburn.Micro.IResult;

namespace NightOwl.Core
{
    public class Run
    {
        public static void Coroutine(IResult coroutine, ActionExecutionContext context = null)
        {
            Coroutine(new[] { coroutine }, context);
        }

        public static void Coroutine(IEnumerator<IResult> coroutines, ActionExecutionContext context = null)
        {
            if (context == null)
                Caliburn.Micro.Coroutine.BeginExecute(coroutines);
            else
                Caliburn.Micro.Coroutine.BeginExecute(coroutines, context);
        }

        public static void Coroutine(IEnumerable<IResult> coroutines, ActionExecutionContext context = null)
        {
            Coroutine(coroutines.GetEnumerator(), context);
        }
    }
}