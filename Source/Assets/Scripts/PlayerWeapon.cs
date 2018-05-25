using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour {

	public Transform controller;
	public LensFlare hit_lens;
	public bool m_bPistal;
	Rigidbody m_rigidBody;

	int count = 0;
	float previousTime;
	Vector3 originalPos;

	// Use this for initialization
	void Start () {
		m_rigidBody = GetComponent<Rigidbody> ();
		originalPos = transform.position;
		previousTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
//		text.text = "Saber Position : " + GetComponent<Rigidbody> ().transform.position.ToString() + '\n' + "Controller Position : " + controller.position.ToString();
		count++;
		if (count >= 10) {
			float dis = Vector3.Distance (transform.position, originalPos);
			float speed = dis / (Time.time - previousTime);
			count = 0;
			previousTime = Time.time;
			originalPos = transform.position;
			if (speed > 0.3f) {
				if (ControllerManager.Instance.m_bLightTurnOn)
					SoundManager.Instance.PlaySound (SoundManager.Instance.moveSounds [Random.Range (0, 2)]);
			}
		}
	}

	void OnCollisionEnter (Collision collision)
	{
//		if (m_rigidBody.velocity.magnitude < 5f)
//			return;
		if (ActorPlayer.Instance.currentHP > 0/* && !m_bPistal*/)
		{
			if (collision.collider.GetComponent<BodyPartScript> ()) {
				ActorMonster mon = collision.collider.GetComponentInParent<ActorMonster> ();
				GenerateEffect (collision.collider.gameObject);
				mon.SetDamage (100);
			}

			MonsterWeapon monWeapon = collision.collider.GetComponent<MonsterWeapon> ();
			if (monWeapon && !monWeapon.m_blaster) {// sword
				GenerateEffect (collision.collider.gameObject);
				SoundManager.Instance.PlaySound (SoundManager.Instance.deflectSound[Random.Range(0,5)]);
				ActorMonster mon = monWeapon.m_actorMonster;
				if (mon && !mon.m_bSolider) {
					mon.GetComponent<Rigidbody> ().isKinematic = false;
					mon.GetComponent<Rigidbody> ().AddForce (mon.transform.forward * (-100000));
				}
			}

			if (monWeapon && monWeapon.m_blaster) {
				GenerateEffect (collision.collider.gameObject, true);
				SoundManager.Instance.PlaySound (SoundManager.Instance.deflectSound[Random.Range(0,5)]);
			}
		}
	}

	void GenerateEffect(GameObject hitObj, bool m_bWave = false){
		GameObject tmp = null;
		if (hitObj.GetComponent<BodyPartScript>())
			tmp = Resources.Load ("Prefabs/body_Hit") as GameObject;
		else if (hitObj.GetComponent<MonsterWeapon>())
			tmp = Resources.Load ("Prefabs/body_Hit") as GameObject;
		GameObject go = Instantiate (tmp, hitObj.transform.position, Quaternion.identity) as GameObject;
		if (!m_bWave)
			go.transform.SetParent (hitObj.transform);
		Destroy (go, 1);
	}

	void DisableFlare(){
		hit_lens.enabled = false;
	}
}