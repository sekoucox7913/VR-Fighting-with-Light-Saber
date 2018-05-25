using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RenderHeads.Media.AVProVideo;

public class Intro : MonoBehaviour {

	public MediaPlayer m_MediaPlayer;
	public float delay = 0;
	bool m_bClickable = false;
	// Use this for initialization
	void Start () {
		SoundManager.Instance.StopMusic ();
		m_bClickable = false;
		delay = 5;
		Invoke ("SetClick", delay);

		OVRManager.HMDMounted += HandleHMDMounted;
		OVRManager.HMDUnmounted += HandleHMDUnmounted;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_bClickable) {
			if (OVRInput.GetUp (OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyUp (KeyCode.A)) {
				if (m_MediaPlayer.Control.IsPlaying ())
					m_MediaPlayer.Control.Stop ();
				SceneManager.LoadScene (2);
			}

			if (m_MediaPlayer.Control.IsFinished ()) {
				SceneManager.LoadScene (2);
			}
		}
	}

	void SetClick(){
		m_bClickable = true;
		GameManager.Instance.m_bIntroLoaded = true;
	}

	void HandleHMDMounted () {
		OVRManager.HMDMounted -= HandleHMDMounted;
		if (m_MediaPlayer.Control.IsPaused ()) {
			m_MediaPlayer.Control.Play ();
		}
	}

	void HandleHMDUnmounted () {
		OVRManager.HMDUnmounted -= HandleHMDUnmounted;
		m_MediaPlayer.Control.Pause ();
	}
}
