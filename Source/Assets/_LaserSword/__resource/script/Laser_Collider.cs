﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Laser_Collider : MonoBehaviour 
{

	public delegate void CollisionDelegate(Collider coll);
	public event CollisionDelegate OnCollEnter;
	public event CollisionDelegate OnCollStay;
	public event CollisionDelegate OnCollExit;


	void Start ()
	{
		gameObject.layer = LayerMask.NameToLayer("LaserSword");
	}


//	void OnTriggerEnter(Collider coll)
//	{
//		if(OnCollEnter != null)
//			OnCollEnter (coll);
//	}

	void OnCollisionEnter (Collision collision)
	{
		if(OnCollEnter != null)
			OnCollEnter (collision.collider);
	}
	void OnCollisionExit (Collision collision)
	{
		if(OnCollEnter != null)
			OnCollExit (collision.collider);
	}
	void OnCollisionStay (Collision collision)
	{
		if(OnCollEnter != null)
			OnCollStay (collision.collider);
	}

//	void OnTriggerStay(Collider coll)
//	{
//		if(OnCollStay != null)
//			OnCollStay (coll);
//	}
//
//	void OnTriggerExit(Collider coll)
//	{
//		if(OnCollExit != null)
//			OnCollExit (coll);
//	}

	public void Enable_Collider(bool b)
	{
		GetComponent<Collider> ().enabled = b;
	}

}
