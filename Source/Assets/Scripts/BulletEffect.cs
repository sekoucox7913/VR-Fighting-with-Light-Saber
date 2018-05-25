using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour {

	float alpha = 0;
	float startAlpha = 0;
	// Use this for initialization
	void Start () {
		alpha = GetComponent<MeshRenderer> ().material.color.a;
		startAlpha = alpha;
		Debug.Log (" ____________   " + startAlpha);
//		Destroy (gameObject, 3f);
	}
	
	// Update is called once per frame
	void Update () {
//		alpha -= 0.001f;
//		if (alpha <= 0) {
//			alpha = startAlpha;
//		}
//		GetComponent<MeshRenderer> ().material.color.a = alpha;
	}
}
