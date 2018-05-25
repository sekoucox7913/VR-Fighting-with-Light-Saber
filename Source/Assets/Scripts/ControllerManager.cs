using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerManager : MonoBehaviour {

	public static ControllerManager Instance = null;
	public GameObject mainMenuPannel;
	public GameObject laserColorPannel;
	public GameObject leaderBoardPannel;
	public GameObject laserObj;
	public GameObject swordObj;
	public GameObject pointerObj;
	public bool m_bLightTurnOn = true;

	GameObject currentHitObj, previousHitObj;
	// Use this for initialization
	void Start () {
		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			swordObj.SetActive (false);
			laserObj.SetActive (true);
			pointerObj.SetActive (true);
		} else if (SceneManager.GetActiveScene ().buildIndex == 2) {
			Instance = this;
			swordObj.SetActive (true);
			laserObj.SetActive (false);
			pointerObj.SetActive (false);
			m_bLightTurnOn = true;
			SetSwordColor ();
		}
	}

	void SetSwordColor () {
		swordObj.GetComponent<Dynamic_Laser> ().colorMesh.material = GameManager.Instance.m_leaserMat;
	}
	
	// Update is called once per frame
	void Update () {
		if (!pointerObj.activeSelf)
			return;
		RaycastHit hit = new RaycastHit();
		Vector3 forward = pointerObj.transform.forward;
		Ray castedRay = new Ray (pointerObj.transform.position, forward);

		if (Physics.Raycast (castedRay, out hit)) {
			if (hit.collider.gameObject != null) {
				currentHitObj = hit.collider.gameObject;
				if (previousHitObj != currentHitObj) {
					if (previousHitObj != null && previousHitObj.GetComponent<Button> ()) {
						previousHitObj.GetComponent<Animator> ().SetTrigger ("Normal");
					}

					if (currentHitObj.GetComponent<Button> ()) {
						Debug.Log (hit.collider.gameObject.name);
						SoundManager.Instance.PlaySound (SoundManager.Instance.button);
						currentHitObj.GetComponent<Animator> ().SetTrigger ("Highlighted");
					}
					previousHitObj = currentHitObj;
				}

				if (OVRInput.GetUp (OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyUp (KeyCode.A)) {					
					OnButtonClicked (currentHitObj);
				}
			}
		}

		if (Input.GetKeyUp (KeyCode.B)) {
			SceneManager.LoadScene (2);
		}
	}

	public void TurnLightSaber()
	{
		if (SceneManager.GetActiveScene ().buildIndex != 2)
			return;
		m_bLightTurnOn = !m_bLightTurnOn;
		if (m_bLightTurnOn)
			swordObj.GetComponent<Dynamic_Laser> ()._Enable ();
		else
			swordObj.GetComponent<Dynamic_Laser> ()._Disable ();
	}

	void OnButtonClicked (GameObject hitObj)
	{
		if (hitObj.name.Equals ("NewGame")) {
			SoundManager.Instance.PlaySound (SoundManager.Instance.btnPress);
			StartCoroutine (StartGame ());
		}else if (hitObj.name.Equals ("GotoMain")) {
			SoundManager.Instance.PlaySound (SoundManager.Instance.btnPress);
			StartCoroutine (RestartGame ());
		}else if (hitObj.name.Equals ("Restart")) {
			SoundManager.Instance.PlaySound (SoundManager.Instance.btnPress);
			SceneManager.LoadScene (2);
		} else if (hitObj.name.Equals ("laserswordcolor")) {
			SoundManager.Instance.PlaySound (SoundManager.Instance.btnPress);
			StartCoroutine (ShowColorPannel ());
		} else if (hitObj.name.Equals ("laserColor_Blue")) {
			SoundManager.Instance.PlaySound (SoundManager.Instance.btnPress);
			GameManager.Instance.m_leaserMat = GameManager.Instance.m_blueMat;
			StartCoroutine (ShowMainPannel ());
		} else if (hitObj.name.Equals ("laserColor_Green")) {
			SoundManager.Instance.PlaySound (SoundManager.Instance.btnPress);
			GameManager.Instance.m_leaserMat = GameManager.Instance.m_greenMat;
			StartCoroutine (ShowMainPannel ());
		} else if (hitObj.name.Equals ("laserColor_Red")) {
			SoundManager.Instance.PlaySound (SoundManager.Instance.btnPress);
			GameManager.Instance.m_leaserMat = GameManager.Instance.m_redMat;
			StartCoroutine (ShowMainPannel ());
		} else if (hitObj.name.Equals ("laserColor_Purple")) {
			SoundManager.Instance.PlaySound (SoundManager.Instance.btnPress);
			GameManager.Instance.m_leaserMat = GameManager.Instance.m_magnetaMat;
			StartCoroutine (ShowMainPannel ());
		}
	}

	IEnumerator StartGame()
	{
		yield return new WaitForSeconds (2);
		if (GameManager.Instance.m_bIntroLoaded)
			SceneManager.LoadScene (2);
		else
			SceneManager.LoadScene (1);		
	}

	IEnumerator RestartGame(){
		yield return new WaitForSeconds (1.5f);
		SceneManager.LoadScene (0);
	}

	IEnumerator ShowColorPannel()
	{
		mainMenuPannel.GetComponent<Animator> ().SetInteger ("action", 2);
		yield return new WaitForSeconds (0.7f);
		laserColorPannel.GetComponent<Animator> ().SetInteger ("action", 1);
	}

	IEnumerator ShowMainPannel()
	{
		laserColorPannel.GetComponent<Animator> ().SetInteger ("action", 2);
		yield return new WaitForSeconds (1.2f);
		mainMenuPannel.GetComponent<Animator> ().SetInteger ("action", 1);
	}
}
