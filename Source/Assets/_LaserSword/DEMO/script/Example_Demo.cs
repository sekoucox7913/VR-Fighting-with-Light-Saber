using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Example_Demo : MonoBehaviour 
{

	[SerializeField()]
	Button show_btn;
	[SerializeField()]
	Button hide_btn;
	[SerializeField()]
	Button character_hit_btn;
	[SerializeField()]
	Button character_idle_btn;

	[Space(20)]

	[SerializeField()]
	Dynamic_Laser LaserSword;
	[SerializeField()]
	Animator character_animator;

	[Space(20)]

	[SerializeField()]
	Camera camera_1;
	[SerializeField()]
	Camera camera_2;




	// Use this for initialization
	void Start () 
	{
		show_btn.onClick.AddListener (ShowLightsaber);
		hide_btn.onClick.AddListener (HideLightsaber);
		character_idle_btn.onClick.AddListener (SetIdle);
		character_hit_btn.onClick.AddListener (SetHit);
	}




	void ShowLightsaber()
	{
		LaserSword._Enable ();
	}

	void HideLightsaber()
	{
		LaserSword._Disable ();
	}

	void SetIdle()
	{
		character_animator.SetTrigger ("idle");
		camera_1.gameObject.SetActive(true);
		camera_2.gameObject.SetActive(false);
	}

	void SetHit()
	{
		character_animator.SetTrigger ("hit");
		camera_1.gameObject.SetActive(false);
		camera_2.gameObject.SetActive(true);
	}

}
