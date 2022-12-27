using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Controls.Gestures
{
	public enum TurnGestureResult
	{
		TurnLeft,
		TurnRight,
	}

	public class TurnGesture : VRGesture<TurnGestureResult>
	{
		private readonly VRHand _hand;
		private readonly float _deadzone = 0.5f;
		private bool _engaged;

		public TurnGesture(VRHand hand) 
		{
			_hand = hand;
		}

		public override void Simulate()
		{
			var vrHand = _hand.VrHand;

			var xAbs = Math.Abs( vrHand.Joystick.Value.x );

			if ( xAbs > _deadzone && !_engaged )
			{
				_engaged = true;
				Emit(vrHand.Joystick.Value.x > 0 ? TurnGestureResult.TurnRight: TurnGestureResult.TurnLeft);
			}

			if ( xAbs <= _deadzone )
				_engaged = false;
			
		}
	}
}
