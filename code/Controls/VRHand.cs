using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Controls
{
	public enum HandType
	{
		Left,
		Right
	}

	public abstract class VRHand : AnimatedEntity
	{

		public abstract HandType HandType { get; }
		public abstract Input.VrHand VrHand { get; }
		

		public override void Spawn()
		{
			var modelPath = GetModelPath();
			SetModel( modelPath );
			base.Spawn();
		}

		private string GetModelPath()
		{
			return HandType switch
			{
				HandType.Left => "models/hands/alyx_hand_left.vmdl_c",
				HandType.Right => "models/hands/alyx_hand_right.vmdl_c",
				_ => throw new NotImplementedException()
			};
		}

		public override void Simulate( IClient cl )
		{
			base.Simulate( cl );
			Transform = VrHand.Transform;
		}

		public override void FrameSimulate( IClient cl )
		{
			base.FrameSimulate( cl );
			Transform = VrHand.Transform;
		}


	}

	public class LeftVRHand : VRHand
	{
		public override HandType HandType => HandType.Left;

		public override Input.VrHand VrHand => Input.VR.LeftHand;
	}

	public class RightVRHand : VRHand
	{
		public override HandType HandType => HandType.Right;

		public override Input.VrHand VrHand => Input.VR.RightHand;
	}
}
