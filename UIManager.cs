using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour {


	[SerializeField]
	private Text _playerscoreText, bestText;
	[SerializeField]
	private Image _liveimage;
	[SerializeField]
	private Sprite[] _livesSprite;
	[SerializeField]
	private Text _GameOverText;
	[SerializeField]
	private Text _RestartText;

	public GameManager _GameManager;
	public int playerScore, bestScore; 
	// Use this for initialization
	void Start () {
		
		_playerscoreText.text = "Score: " + 0;
		bestText.text = "Best Score: " + 0;
		//_GameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();


	}
	
	// Update is called once per frame
	public void UpdateScore () 
	{
		_playerscoreText.text = "Score: " + playerScore;
	}
	public void CheckForBestScore ()
    {
		if (playerScore > bestScore)
        {
			bestScore = playerScore;
			bestText.text = "Best Score: " + bestScore;
        }
    }
	public void UpdateLives(int currentlives)
	{
		_liveimage.sprite = _livesSprite [currentlives];
		if (currentlives == 0)
		{
			gameOverSequence ();
		}
	}
	void gameOverSequence()
	{
		_GameManager.GameOver ();
		_GameOverText.gameObject.SetActive (true);
		_RestartText.gameObject.SetActive (true);
		StartCoroutine (GameOverFlickerRoutine());

	}
	IEnumerator GameOverFlickerRoutine ()
	{
		while (true) 
		{
			_GameOverText.text = "GAME OVER";
			yield return new WaitForSeconds (0.5f);
			_GameOverText.text = "";
			yield return new WaitForSeconds (0.5f);

		}	
	}
	public void Resume()
    {
		Debug.Log("works");
		_GameManager.ResumeGame ();
	
    }
	public void BackToMain()
	{
		SceneManager.LoadScene(0);
	}
}
