using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using Oculus.Platform;

public class BattleManager : MonoBehaviour {

	public static BattleManager Instance = null;
	public GameObject ninjaObj, blasterSoliderObj, soliderObj;
	public ControllerManager m_Controller;
	public GameObject m_Canvas;
	public GameObject gameOverPannel;
	public Text m_gameOverTxt, m_playerScore;
	public Text m_countDownText;
	public Text m_LevelText;
	public bool m_bStartGame = false;
	public bool m_bEndGame 	 = false;
	public int m_level;
	public int m_GameCountDown;
	public int m_goalDeadCount;
	public int m_saberMonsterCnt;

	public Vector3[] createPosition = new Vector3[7];
	string m_PlayerDieStr = "You were murdered.";
	// Use this for initialization
	void Start () {
		if (Instance == null)
			Instance = this;
		StartGame ();
	}

	public void StartGame(){
		GameManager.Instance.m_PlayerScore = 0;
		if (!GameManager.Instance.m_bEditorTest)
			m_level = 1;
		else
			m_level = 5;
		m_LevelText.text = "Level : " + m_level.ToString ();
		m_bEndGame = false;
		ActorPlayer.Instance.currentHP = 100;
		m_GameCountDown = 5;
		SoundManager.Instance.PlayMusic (SoundManager.Instance.battleBackground);
		SoundManager.Instance.PlaySound (SoundManager.Instance.sword);

		m_goalDeadCount = LoadData.Instance.m_LevelData [m_level-1].m_monsterCount;
		m_saberMonsterCnt = LoadData.Instance.m_LevelData [m_level - 1].m_saberMonCnt;

		InvokeRepeating ("SetCountDown", 1, 1);
	}

	void SetCountDown() {
		m_GameCountDown--;
		m_countDownText.text = m_GameCountDown.ToString ();
		if (m_GameCountDown == 0) {
			m_countDownText.text = "Start";
		} else if (m_GameCountDown < 0) {
			CancelInvoke ("SetCountDown");
			m_GameCountDown = 0;
			m_countDownText.enabled = false;
			StartCoroutine (PlayLevel ());
		}
	}

	IEnumerator PlayLevel(){
		m_LevelText.GetComponent<Animator>().SetInteger("disappear",0);
		yield return new WaitForSeconds (2.5f);
		m_LevelText.GetComponent<Animator>().SetInteger("disappear",1);
		yield return new WaitForSeconds (1);
		m_bStartGame = true;
		if (m_level == 5)
			Invoke ("GenerateSolider", 2);
		else if (m_level < 5)
			GenerateMonster ();
		else
			GotoHangarLevel ();
	}

	void GenerateMonster()
	{
		DeleteAllMonsters ();
		foreach (MonsterData data in LoadData.Instance.m_MonsterData) {
			if (data.m_levelID == m_level) {
				if (data.m_bSolider == 0) {
//					GameObject mon = LoadResource (data.m_sModelName);
					GameObject go = Instantiate (ninjaObj, data.m_vCreatePos, ninjaObj.transform.rotation) as GameObject;
					go.transform.SetParent (GameObject.Find ("Monsters").transform);
//					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);	 // CUC
					go.GetComponent<ActorMonster> ()._monsterData = data;
					Vector3 lookPos = new Vector3 (ActorPlayer.Instance.transform.position.x, go.transform.position.y, ActorPlayer.Instance.transform.position.z);
					go.transform.LookAt (lookPos);
				}
			}
		}
	}

	void GenerateSolider(){
		foreach (MonsterData data in LoadData.Instance.m_MonsterData) {
			if (data.m_levelID == m_level) {
				if (data.m_bSolider == 1 && data.m_ID == ActorPlayer.Instance.m_DeadMonsterCount + 1) {
//					GameObject mon = LoadResource (data.m_sModelName);
					GameObject tmp = null;
					if (data.m_sModelName == "Prefabs/BlasterSolider")
						tmp = blasterSoliderObj;
					else if (data.m_sModelName == "Prefabs/Solider")
						tmp = soliderObj;
					GameObject go = Instantiate (tmp, data.m_vCreatePos, soliderObj.transform.rotation) as GameObject;
//					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);	 // CUC
					go.transform.SetParent (GameObject.Find ("Monsters").transform);
					go.GetComponent<ActorMonster> ()._monsterData = data;
					Vector3 lookPos = new Vector3 (ActorPlayer.Instance.transform.position.x, go.transform.position.y, ActorPlayer.Instance.transform.position.z);
					go.transform.LookAt (lookPos);
				}
			}
		}
	}

	void DeleteAllMonsters(){
		ActorMonster[] mons = FindObjectsOfType<ActorMonster> ();
		foreach (ActorMonster mon in mons) {
			mon.currentHP = 0;
		}
	}

	public GameObject LoadResource (string prefName){
		GameObject go = Resources.Load (prefName) as GameObject;
		return go;
	}

	// Update is called once per frame
	void Update () {

	}

	public void EndMonster(ActorMonster entity){
		if (m_level == 6) {
			GenerateHangarSolider ();
			return;
		}
		if (m_goalDeadCount == ActorPlayer.Instance.m_DeadMonsterCount) {
			NextLevel ();
		} else {
			if (ActorPlayer.Instance.m_DeadMonsterCount >= m_saberMonsterCnt) {
				Invoke ("GenerateSolider", 2);
			}
		}
	}

	public void EndGame(){
		if (ActorPlayer.Instance.currentHP <= 0) {
			m_bEndGame = true;
			m_gameOverTxt.text = m_PlayerDieStr;
			m_playerScore.text = "Your score : " + GameManager.Instance.m_PlayerScore.ToString ();
			gameOverPannel.GetComponent<Animator> ().SetBool ("gameover",true);
			m_Controller.swordObj.SetActive (false);
			m_Controller.laserObj.SetActive (true);
			m_Controller.pointerObj.SetActive (true);
			Leaderboards.GetEntries ("PlayerScore", 0, LeaderboardFilterType.None, LeaderboardStartAt.CenteredOnViewer);
		}
	}

	void NextLevel(){
		DeleteAllMonsters ();
		m_level++;
		if (m_level == 6)
			m_LevelText.text = "Hangar Level";
		else
			m_LevelText.text = "Level : " + m_level.ToString();
		ActorPlayer.Instance.Init();
		StartCoroutine (PlayLevel ());
		if (m_level <= 5) {
			m_goalDeadCount = LoadData.Instance.m_LevelData [m_level - 1].m_monsterCount;
			m_saberMonsterCnt = LoadData.Instance.m_LevelData [m_level - 1].m_saberMonCnt;
		}
	}

	public void Success (){
		m_bEndGame = true;
		m_gameOverTxt.text = "Congratulation \n You Win";
		m_playerScore.text = "Your score : " + GameManager.Instance.m_PlayerScore.ToString ();
		gameOverPannel.GetComponent<Animator> ().SetBool ("gameover",true);
		m_Controller.swordObj.SetActive (false);
		m_Controller.laserObj.SetActive (true);
		m_Controller.pointerObj.SetActive (true);
		Leaderboards.GetEntries ("PlayerScore", 0, LeaderboardFilterType.None, LeaderboardStartAt.CenteredOnViewer);
	}

	public void GotoHangarLevel () {
		ActorPlayer.Instance.FadeIn ();
		ActorPlayer.Instance.transform.position = new Vector3 (-26f, 1.7f, -40f);
//		ActorPlayer.Instance.transform.position = new Vector3 (-5.5f, 0.63f, -7.9f);
		ActorPlayer.Instance.m_MainCamera.farClipPlane = 100;
		m_Canvas.GetComponent<RectTransform> ().position = new Vector3 (-26, 2.2f, -42f);
//		m_Canvas.GetComponent<RectTransform> ().position = new Vector3 (-5.5f, 0.91f, -10.1f);
		Invoke ("GenerateHangarSolider", 7);
	}

	public void GenerateHangarSolider() {
		int rand = Random.Range (1, 3);
		foreach (MonsterData data in LoadData.Instance.m_MonsterData) {
			if (data.m_levelID == m_level) {
				for (int i = 0; i < rand; i++) {
//					GameObject mon = LoadResource (data.m_sModelName);
					int randIdx = Random.Range (0, 7);
					GameObject go = Instantiate (blasterSoliderObj, createPosition[randIdx], blasterSoliderObj.transform.rotation) as GameObject;
					go.transform.SetParent (GameObject.Find ("Monsters").transform);
//					go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);	 // CUC
					go.GetComponent<ActorMonster> ()._monsterData = data;
					Vector3 lookPos = new Vector3 (ActorPlayer.Instance.transform.position.x, go.transform.position.y, ActorPlayer.Instance.transform.position.z);
					go.transform.LookAt (lookPos);
				}
			}
		}		
	}
}
