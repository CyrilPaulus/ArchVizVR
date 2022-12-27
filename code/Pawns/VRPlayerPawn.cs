using Sandbox.Controls;
using Sandbox.Controls.Gestures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sandbox.Input;

namespace Sandbox.Pawns
{
	public partial class VRPlayerPawn : Entity
	{
		[Net, Local] public VRHand LeftHand { get; private set; }
		[Net, Local] public VRHand RightHand { get; private set; }

		private List<IVRGesture> _gestures = new();

		public override void Spawn()
		{
			LeftHand = new LeftVRHand();
			LeftHand.Owner = this;

			RightHand = new RightVRHand();
			RightHand.Owner = this;

			base.Spawn();

			var turnGesture = new TurnGesture( LeftHand );
			turnGesture.OnEmitted += OnTurn;
			_gestures.Add( turnGesture );
		}

		private void OnTurn( object sender, TurnGestureResult e )
		{
			var direction = e == TurnGestureResult.TurnLeft ? -1 : 1;
			Rotation *= new Angles( 0f, -45 * direction, 0f ).ToRotation();
		}

		public override void Simulate( IClient cl )
		{
			base.Simulate( cl );

			LeftHand?.Simulate( cl );
			RightHand?.Simulate( cl );

			foreach ( var gesture in _gestures )
				gesture.Simulate();
		}

		public override void FrameSimulate( IClient cl )
		{
			base.FrameSimulate( cl );

			LeftHand?.FrameSimulate( cl );
			RightHand?.FrameSimulate( cl );
		}
	}
}
