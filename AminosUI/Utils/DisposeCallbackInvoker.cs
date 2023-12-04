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
		private readonly bool callbackOnceOnly;

		private bool called = false;

		public DisposeCallbackInvoker(Action callback, bool callbackOnceOnly)
		{
			this.callback = callback;
			this.callbackOnceOnly = callbackOnceOnly;
		}

		public void Dispose()
		{
			if (callbackOnceOnly && called)
				return;

			callback?.Invoke();
			called = true;
		}
	}
}
