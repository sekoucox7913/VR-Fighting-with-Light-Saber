using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActorPlayer : MonoBehaviour {

	public static ActorPlayer Instance = null;
	public Camera m_MainCamera;
	public OVRScreenFade m_OVRScreenFade;
	public Transform bloodParent;
	public GameObject bloodObj;
	public GameObject shootPosObj;
	public Text m_currentHPTxt;
	public int currentHP = 100;
	public int m_DeadMonsterCount;
	// Use this for initialization
	void Start () {
		if (Instance == null)
			Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_currentHPTxt != null)
			m_currentHPTxt.text = currentHP.ToString ();
	}

	public void Init(){
		m_DeadMonsterCount = 0;
	}

	public void SetDamage(int damage) {
//		SoundManager.Instance.PlaySound (SoundManager.Instance.hitSound);
		SoundManager.Instance.PlaySound (SoundManager.Instance.playerHitSounds [Random.Range (0, 2)]);
		currentHP -= damage;
		if (currentHP <= 0) {
			currentHP = 0;
			Dead ();
		}
		ShowBloodEffect ();
	}

	public void ShowBloodEffect() {
		GameObject blood = Instantiate (bloodObj) as GameObject;
		blood.transform.SetParent (bloodParent);
		blood.transform.localPosition = new Vector3 (0.3f, 0.6f, 0.8f);
		blood.transform.localScale = Vector3.one * 0.3f;
		blood.transform.localRotation = Quaternion.Euler (new Vector3 (90, 0, 180));
		Destroy (blood, 1);
	}

	public void Dead(){
		BattleManager.Instance.EndGame();
	}

	public void ShootBullet(){
		ShootEffect ();
		SoundManager.Instance.PlaySound (SoundManager.Instance.playShootSound);
		var layerMask = 1 << 8;
		layerMask = ~layerMask;
		Ray ray = new Ray(shootPosObj.transform.position, -shootPosObj.transform.forward);
		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask)) {
			GameObject hitObj = hit.collider.gameObject;
			if (hitObj.GetComponent<BodyPartScript> ()) {
				ActorMonster mon = hitObj.GetComponentInParent<ActorMonster> ();
				GenerateEffect (hitObj);
				mon.SetDamage (100);
			}
		}
	}

	void GenerateEffect(GameObject hitObj, bool m_bWave = false){
		GameObject tmp = null;
		if (hitObj.GetComponent<BodyPartScript>())
			tmp = Resources.Load ("Prefabs/body_Hit") as GameObject;
		else if (hitObj.GetComponent<MonsterWeapon>())
			tmp = Resources.Load ("Prefabs/wave_Hit") as GameObject;
		GameObject go = Instantiate (tmp, hitObj.transform.position, Quaternion.identity) as GameObject;
		if (!m_bWave)
			go.transform.SetParent (hitObj.transform);
		Destroy (go, 1);
	}

	public void FadeIn() {
		m_OVRScreenFade.fadeTime = 10;
		StartCoroutine (m_OVRScreenFade.FadeIn ());
	}

	void ShootEffect(){
		GameObject tmp = BattleManager.Instance.LoadResource ("Prefabs/PistolBulletImpact");
		GameObject go = Instantiate (tmp, shootPosObj.transform.position, Quaternion.identity) as GameObject;
		Destroy (go, 1);
	}
}
