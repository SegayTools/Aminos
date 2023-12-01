using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AminosUI.Utils
{
	internal class DisposeCallbackInvoker : IDisposable
	{
		private readonly Action callback;

		public DisposeCallbackInvoker(Action callback)
		{
			this.callback = callback;
		}

		public void Dispose()
		{
			callback?.Invoke();
		}
	}
}
