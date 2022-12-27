using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Controls
{

	public interface IVRGesture
	{
		public void Simulate();
	}

	public abstract class VRGesture<TResult> : IVRGesture
	{

		public event EventHandler<TResult> OnEmitted;

		public abstract void Simulate();

		protected void Emit(TResult value)
		{
			OnEmitted?.Invoke(this, value);
		}

	}
}
