using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.AI;
using UnityEngine.AI;

public class ActorMonster : MonoBehaviour {

	public FSMMotion fsmMotion;
	public MonsterAI monsterAI;
	public MonsterData _monsterData;

	public Transform target;
	public NavMeshAgent agent;
	public Animator m_Animator;
	public string currentState = MotionState.IDLE;
	public string previousState = MotionState.IDLE;
	public bool m_bBlaster = false;
	public bool m_bInAttackAnimation = false;
	public bool m_bInHitAnimation = false;
	public bool m_bSolider = false;
	public Transform m_bulletTrans;
	public int m_monID;
	public int currentHP;

	// Use this for initialization
	void Start () {
		Initialize ();
	}

	void Initialize()
	{
		target = ActorPlayer.Instance.transform;
		fsmMotion = new FSMMotion();
		monsterAI = new MonsterAI ();
		m_Animator = GetComponent<Animator> ();
		m_monID = _monsterData.m_ID;
		currentHP = _monsterData.m_iBaseHP;
		agent.speed = _monsterData.m_iSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (BattleManager.Instance.m_bStartGame)
			monsterAI.CheckState (this);
	}

	public void GotoState(string newState)
	{
		fsmMotion.ChangeStatus (this, newState);
	}

	public void SetDamage (int damage) {
		SoundManager.Instance.PlaySound (SoundManager.Instance.enemyHitSounds [Random.Range (0, 2)]);
//		SoundManager.Instance.PlaySound (SoundManager.Instance.enemyHitSounds[Random.Range(0, 2)]);
		if (currentHP <= 0)
			return;
		currentHP -= damage;
		if (currentHP <= 0) {
			currentHP = 0;
		} else {
			m_bInHitAnimation = true;
			GotoState (MotionState.HIT);
		}
	}

	public void Fire(){
		SoundManager.Instance.PlaySound (SoundManager.Instance.soliderShootSound);
		GenerateEffect ();
		if (m_bBlaster) {
			FlyBlaster ();
		} else {
//			Ray ray = new Ray (m_bulletTrans.position, -m_bulletTrans.right);
//			RaycastHit hit = new RaycastHit ();
//			if (Physics.Raycast (ray, out hit)) {
//				ActorPlayer player = hit.collider.gameObject.GetComponent<ActorPlayer> ();
//				if (player) {
//					player.SetDamage (_monsterData.m_iAttackPower);
//				}
//			}
		}
	}

	void GenerateEffect(){
		GameObject tmp = BattleManager.Instance.LoadResource ("Prefabs/PistolBulletImpact");
		GameObject go = Instantiate (tmp, m_bulletTrans.position, Quaternion.identity) as GameObject;
		Destroy (go, 1);
	}

	void FlyBlaster ()
	{
		GameObject tmp = BattleManager.Instance.LoadResource ("Prefabs/laserBlaster");
		GameObject go = Instantiate (tmp, m_bulletTrans.position, Quaternion.identity) as GameObject;

		float yAngle = transform.localRotation.eulerAngles.y - 90;
		go.transform.localRotation = Quaternion.Euler (new Vector3(0, yAngle, 90));
		go.GetComponent<MonsterWeapon> ().m_actorMonster = this;
		go.GetComponent<MonsterWeapon> ().m_blaster = true;
	}
}
