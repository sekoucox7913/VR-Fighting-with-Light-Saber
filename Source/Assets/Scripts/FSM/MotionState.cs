using UnityEngine;
using System.Collections;

// State
static public class MotionState
{
	public const string NONE = "none";		// 11.28 CUC
	public const string IDLE = "idle";
	public const string TRACKING = "tracking";
	public const string DEAD = "dead";
	public const string ATTACKING = "attacking";
	public const string HIT = "hit";
	public const string VICTORY = "victory";
}