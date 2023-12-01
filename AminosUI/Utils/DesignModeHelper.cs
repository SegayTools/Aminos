using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AminosUI.Utils
{
	internal class DesignModeHelper
	{
		public static void CheckOnlyForDesignMode()
		{
			if (!Avalonia.Controls.Design.IsDesignMode)
				throw new Exception("Only use in DesignMode.");
		}
	}
}
