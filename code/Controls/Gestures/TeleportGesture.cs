// Copyright © 2009-2022 Level IT
// All rights reserved as Copyright owner.
//
// You may not use this file unless explicitly stated by Level IT.

using Sandbox.Internal;
using Sandbox.Internal.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sandbox.CitizenAnimationHelper;

namespace Sandbox.Controls.Gestures
{
    public partial class TeleportGesture : VRGesture<Vector3>
    {
        private readonly VRHand _hand;
        private readonly VRTeleportDestination _teleportDestination;
        private Vector3? _lastPosition;

        public TeleportGesture(VRHand hand, VRTeleportDestination teleportDestination)
        {
            _hand = hand;
            _teleportDestination = teleportDestination;
        }

        public override void Simulate()
        {
            var threshold = 0.7;
            var isSelecting = _hand.VrHand.Joystick.Value.y > threshold;
            if (!isSelecting)
            {
                _teleportDestination.EnableDrawing = false;

                if (_lastPosition != null)
                {
                    Emit(_lastPosition.Value);
                    _lastPosition = null;
                }

                return;
            }

            _teleportDestination.EnableDrawing = true;

            var worldTrace = Trace.Ray(_hand.Transform.Position, _hand.Position + (_hand.Transform.Rotation.Forward * 1000));
            worldTrace = worldTrace.WorldOnly();
            var result = worldTrace.Run();

            if (result.Hit)
            {
                _teleportDestination.Position = result.EndPosition;
                var yaw = Input.VR.Head.Rotation.Yaw();
                _teleportDestination.Rotation = Rotation.FromYaw(yaw + 90);
                _lastPosition = result.EndPosition;
            } else
            {
                _lastPosition = null;
            }
        }
    }
}
