using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour {
	public bool m_blaster = false;
	public ActorMonster m_actorMonster;
	public Vector3 direction;
	public float force = 150;
	public bool m_back = false;
	// Use this for initialization
	void Start () {
		if (m_blaster) {
			Vector3 targetPos = new Vector3 (ActorPlayer.Instance.transform.position.x, m_actorMonster.transform.position.y, ActorPlayer.Instance.transform.position.z);
			direction = targetPos - m_actorMonster.transform.position;
			GetComponent<Rigidbody> ().AddForce (direction * force);
			Destroy (gameObject, 3.5f);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (m_back) {
			if (GetComponent<Rigidbody> ().velocity.magnitude < 5.0f) {
				Destroy (gameObject);
			}
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (m_blaster) {
			if (collision.collider.GetComponent<ActorPlayer> ()) {
				ActorPlayer.Instance.SetDamage (m_actorMonster._monsterData.m_iAttackPower);
				Destroy (gameObject);
			} else if (collision.collider.GetComponent<PlayerWeapon> ()) {
				BackBlaster ();
			}
		} else if (collision.collider.GetComponent<ActorPlayer> () && m_actorMonster.m_bInAttackAnimation) {
			ActorPlayer.Instance.SetDamage (m_actorMonster._monsterData.m_iAttackPower);
		}
	}

	void OnTriggerEnter (Collider col) {
		if (m_back) {
			if (col.GetComponent<BodyPartScript> ()) {
				ActorMonster mon = col.GetComponentInParent<ActorMonster> ();
				mon.SetDamage (70);
				Destroy (gameObject);
			}
		}
	}

	void ShowBloodEffect(ActorPlayer actor){
		GameObject blood = Instantiate (actor.bloodObj) as GameObject;
		blood.transform.SetParent (actor.bloodParent);
		blood.transform.localPosition = new Vector3 (0.3f, 0.6f, 0.8f);
		blood.transform.localScale = Vector3.one * 0.3f;
		blood.transform.localRotation = Quaternion.Euler (new Vector3 (90, 0, 180));
		Destroy (blood, 1);
	}

	void BackBlaster(){
		m_back = true;
		GetComponent<Collider> ().isTrigger = true;
		GetComponent<Rigidbody> ().AddForce (-direction * force);
		Destroy (gameObject, 3);
	}
}