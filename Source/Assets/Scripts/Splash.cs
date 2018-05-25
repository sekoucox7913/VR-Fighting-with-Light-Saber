using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

	public GameObject m_Logo, m_GameStart, m_Message, m_GameMenu;
	public Text noticeText;
	public Image logoImage;
	// Use this for initialization
	void Start () {
		if (Application.internetReachability == NetworkReachability.NotReachable) {
			noticeText.enabled = true;
			logoImage.enabled = false;
			return;
		}
		if (GameManager.Instance == null || !GameManager.Instance.m_bGameLoaded) {
			m_Logo.SetActive (true);
			m_GameStart.SetActive (false);
			Invoke ("GotoStartScene", 3.5f);
		} else if (GameManager.Instance != null && GameManager.Instance.m_bGameLoaded) {
			m_Logo.SetActive (false);
			m_GameStart.SetActive (true);
			StartGame ();
		}
	}

	void GotoStartScene() {
		m_Logo.SetActive (false);
		m_GameStart.SetActive (true);
		Invoke ("StartGame", 4.5f);
	}

	void StartGame () {
		m_Message.SetActive (false);
		m_GameMenu.SetActive (true);
		GameManager.Instance.m_bGameLoaded = true;
	}
}
