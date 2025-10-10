using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class GameEvents
{
   public delegate void OnPushDelegate(Metal pushedMetal, float playerMass, float metalMass);
   public static OnPushDelegate OnPush;

   public delegate void OnHoveringOverMetalDelegate(Metal hoveredMetal, bool isHovering);
   public static OnHoveringOverMetalDelegate OnMouseHoveringOverMetal;
}

