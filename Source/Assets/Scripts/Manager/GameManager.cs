using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;
	public bool m_bGameLoaded = false;
	public bool m_bEditorTest = false;
	public int m_PlayerScore;
	public bool m_bIntroLoaded = false;

	public Material m_leaserMat;
	public Material m_redMat;
	public Material m_blueMat;
	public Material m_greenMat;
	public Material m_magnetaMat;

	// Use this for initialization
	void Start () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
		Initialize ();
	}

	public void Initialize()
	{
		SoundManager.Instance.PlayMusic(SoundManager.Instance.homeBackground);
	}

	void Update () {
		
	}
}
