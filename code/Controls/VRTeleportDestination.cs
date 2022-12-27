// Copyright © 2009-2022 Level IT
// All rights reserved as Copyright owner.
//
// You may not use this file unless explicitly stated by Level IT.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Controls
{
    public class VRTeleportDestination : ModelEntity
    {
        public override void Spawn()
        {
            var modelPath = "models/teleport_destination/teleport_destination.vmdl";
            SetModel(modelPath);
            base.Spawn();
        }
    }
}
