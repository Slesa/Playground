using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace ReallySimpleWpfApp.Framework
{
	public class RandomlyAvailableAttribute : FilterBase, IMessageAware
	{


		public override void Execute(ResultExecutionContext context)
		{
			Inner.Completed += (o, e) =>
			{
				OnCompleted(e);
			};

			Inner.Execute(context);
		}


		System.Threading.Timer _timer;

		IMessage _message;
		public void MakeAwareOf(IMessage message)
		{
			_timer = new System.Threading.Timer(state => { 
				if _message ==
			}); 
			_message = message;
		}

		

		public void Dispose()
		{
			_timer.Dispose();
			_message = null;
		}

		
	}
}
