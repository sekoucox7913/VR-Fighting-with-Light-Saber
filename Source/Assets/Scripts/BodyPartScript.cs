using UnityEngine;
using System.Collections;
	
public enum BodyPart : int
{
	Spine = 0,
	Chest,
	Head,
	LeftArm,
	RightArm,
	LeftLeg,
	RightLeg,
	None
}

public class BodyPartScript : MonoBehaviour 
{
	public BodyPart bodyPart = BodyPart.None;
}

