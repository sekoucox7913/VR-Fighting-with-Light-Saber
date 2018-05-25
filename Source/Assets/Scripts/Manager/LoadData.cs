using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class MonsterData
{
	public int m_levelID;
	public int m_ID;
	public int m_bSolider;
	public string m_sModelName;
	public float m_iAttackRange;
	public int m_iAttackAnimCount;
	public float m_iSpeed;
	public int m_iDeadTime;
	public int m_iAttackPower;
	public int m_iBaseHP;
	public Vector3 m_vCreatePos;

	public MonsterData()
	{
	}
}

public class LevelData
{
	public int m_level;
	public int m_monsterCount;
	public int m_saberMonCnt;
	public LevelData()
	{
	}
}

public class LoadData : MonoBehaviour {

	public static LoadData Instance = null;
	public string m_MonsterDataFile;
	public string m_LevelInfoFile;
	public string m_WaveFile;

	public List<MonsterData> m_MonsterData;
	public List<LevelData>   m_LevelData;
	// Use this for initialization
	void Start () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}

		m_LevelData = new List<LevelData> ();
		m_LevelInfoFile = "Json/level";
		LoadLevelData ();

		m_MonsterData = new List<MonsterData> ();
		m_MonsterDataFile = "Json/monster";
		LoadMonsterData ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadMonsterData()
	{
		TextAsset monsterAsset = Resources.Load (m_MonsterDataFile) as TextAsset;
		if (monsterAsset == null)
		{
			Debug.Log ("Load monsterAsset Data failed");
			return;
		}
		JsonData data = JsonMapper.ToObject (monsterAsset.text);
		for (int i = 0; i < data.Count; i++)
		{
			JsonData item = data [i];
			MonsterData tmpData = new MonsterData ();
			tmpData.m_levelID 			= int.Parse (item ["Level"].ToString ());
			tmpData.m_ID 				= int.Parse (item ["ID"].ToString ());
			tmpData.m_bSolider			= int.Parse (item ["bSolider"].ToString ());
			tmpData.m_sModelName 		= item ["modelName"].ToString ();
			tmpData.m_iAttackRange 		= float.Parse (item ["attackRange"].ToString ());
			tmpData.m_iAttackAnimCount 	= int.Parse (item ["attackAnimCount"].ToString ());
			tmpData.m_iSpeed 			= float.Parse (item ["speed"].ToString ());
			tmpData.m_iDeadTime 		= int.Parse (item ["deadTime"].ToString ());
			tmpData.m_iAttackPower 		= int.Parse (item ["attackPower"].ToString ());
			tmpData.m_iBaseHP 			= int.Parse (item ["baseHP"].ToString ());
			tmpData.m_vCreatePos 		= new Vector3 (float.Parse(item["xPos"].ToString()), float.Parse(item["yPos"].ToString()), float.Parse(item["zPos"].ToString()));
			m_MonsterData.Add (tmpData);
		}

		Debug.Log ("monsterCount ============   " + m_MonsterData.Count.ToString());
	}

	void LoadLevelData()
	{
		TextAsset levelAsset = Resources.Load (m_LevelInfoFile) as TextAsset;
		if (levelAsset == null)
		{
			Debug.Log ("Load levelAsset Data failed");
			return;
		}
		JsonData data = JsonMapper.ToObject (levelAsset.text);
		for (int i = 0; i < data.Count; i++) {
			JsonData item = data [i];
			LevelData tmpData = new LevelData ();
			tmpData.m_level 		= int.Parse (item ["level"].ToString ());
			tmpData.m_monsterCount  = int.Parse (item ["monsterCount"].ToString ());
			tmpData.m_saberMonCnt 	= int.Parse (item ["saberMonCnt"].ToString ());
			m_LevelData.Add (tmpData);
		}
		Debug.Log ("LevelDataInfo =================     " + m_LevelData.Count.ToString ());
	}

}
