using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

	// Use this for initialization
	[SerializeField]
	private bool _isGameOver;

	[SerializeField]
	private GameObject _pauseMenu;
	public Animator _pauseAnimator;


/*	void Start()
    {
	//nimator = GameObject.Find("PauseMenu").GetCompnent<Animator>;
		//auseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
	}*/
	public void GameOver()
	{
		_isGameOver = true;
	}
	public void ResumeGame()
	{
		Debug.Log("stops here");
		//_pauseMenu.SetActive(false);
		//Time.timeScale = 1f;
	}
	public void Update () 
	{
		
		if (Input.GetKeyDown (KeyCode.R) && _isGameOver == true)
		{
			SceneManager.LoadScene(0);//current game scene 
		}
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{ 
			SceneManager.LoadScene(0); // quits game using escape key
		}
		if (Input.GetKeyDown(KeyCode.P)) 
		{
			_pauseMenu.SetActive(true);
			_pauseAnimator.SetBool("isPaused", true);
			Time.timeScale = 0f;

		}

	}

}


	// Update is called once per frame
