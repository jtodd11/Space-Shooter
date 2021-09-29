//C# code

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	/*what this does is make things avaibale for the editors to change things but not in-game objects or things*/

	[SerializeField]

	/*public means everybody can see it private only that cahracter cansee it. 
	 * Ex mario kart when using pwerup, it adjust your speed because speed in that case is public
	NOTE: using private put an underscore so you know exactly if its public or provate.*/ 
	private float _speed = 5.0f;
	private float _multiplier = 3;

	[SerializeField]
	private GameObject _laserprefab;

	[SerializeField]
	private GameObject _QuadShotprefab;

	[SerializeField]
	private float _firingRate = 0.5f;
	private float _NextShot = 0.0f;

	[SerializeField]
	private int _lives = 3;


	private SpawnManager _spawnManager;
	private bool _QuadShot;
	private bool _MaxSpeed;

	[SerializeField]
	private bool _Shield = false;
	[SerializeField]
	private GameObject _ShieldVisual;

	[SerializeField]
	private GameObject _rightEngine;
	[SerializeField]
	private GameObject _leftEngine;

	[SerializeField]
	private int _playerscore;

	private UIManager _UIManager;

	[SerializeField]
	private AudioClip _laserClip;
	private AudioSource _laserAudioSource;




	//Note you have to fix the issues first before building 


	// Use this for initialization
	void Start () 
	{

		//take the current postion = it new position
		transform.position = new Vector3(0, 0, 0);
		_spawnManager = GameObject.FindGameObjectWithTag("Spawn_Manager").GetComponent<SpawnManager> ();
		_UIManager = GameObject.Find ("Canvas").GetComponent<UIManager> ();
		_laserAudioSource = GetComponent<AudioSource>();

		if (_laserAudioSource == null) 
		{
		
			Debug.LogError ("Audio is null");
		}
		else
		{
			_laserAudioSource.clip = _laserClip;

		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		CalculateMovement();

		if (Input.GetKeyDown(KeyCode.Space) && Time.time > _NextShot) 
		{
			firingLaser ();
		}
	}
		
	void CalculateMovement()
	{
		/* what this is give the user the ability to move their character left or right*/
		float horizontalInput = Input.GetAxis ("Horizontal");
		float verticalInput = Input.GetAxis ("Vertical");


			transform.Translate (Vector3.right * horizontalInput * _speed * Time.deltaTime); //time.deltatime to get one meter per second or real time
			transform.Translate (Vector3.up * verticalInput * _speed * Time.deltaTime);
	
		/*BETTTER CODE 	(vector3 direction = new vector3 ( horizontalInput, verticalInput, 0));
		transform.Translate(direction * _speed * Time.deltaTime)*/

		/*boundaries on the player*/

		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0),0);
		//^^clamping is for saving lines and not having to use so many if statements.
		if (transform.position.x > 11.5f)
		{
			transform.position = new Vector3 (-11.5f, transform.position.y, 0);
		}
		else if (transform.position.x < -11.5f) 
		{
			transform.position = new Vector3 (11.5f, transform.position.y, 0);
		}
	}
	void firingLaser()
	{
		_NextShot = Time.time + _firingRate;


		if(_QuadShot == true) 
		{
			Instantiate (_QuadShotprefab, transform.position + new Vector3(-0.67f, 0.50f, 0), Quaternion.identity);
		} else
		{
			Instantiate(_laserprefab, transform.position + new Vector3(0.26f, 0.71f, 0), Quaternion.identity);
			Instantiate(_laserprefab, transform.position + new Vector3(-0.238f, 0.71f, 0), Quaternion.identity);
		}
		_laserAudioSource.Play ();
	}
	public void Damage()
	{
		if (_Shield == true) 
		{
			_Shield = false;
			_ShieldVisual.SetActive (false);
			return;
		}
		_lives -= 1;
		if (_lives == 2) 
		{
			_rightEngine.SetActive (true);
			_speed = 3.5f;

		} else if (_lives == 1) 
		{
			_leftEngine.SetActive (true);
			_speed = 2.0f;
		}
		_UIManager.UpdateLives (_lives);
		if (_lives < 1) 
		{
			_spawnManager.OnPlayerDeath ();
			_UIManager.CheckForBestScore();
			Destroy (this.gameObject);

		}
	}
	public void Points (int points)
	{

		_playerscore += points; //use an if for if laser and enemy collide add 1 to score
		_UIManager.UpdateScore();
	}
	public void QuadShotActive()
	{
		_QuadShot = true;
		StartCoroutine(QuadShotPowerDown());
	}
	IEnumerator QuadShotPowerDown()
	{
		yield return new WaitForSeconds(5.0f);
		_QuadShot = false;
	}
	public void MaxSpeedActive()
	{
		_MaxSpeed = true;
		_speed *= _multiplier;
		StartCoroutine (MaxSpeedPowerDown ());

	}
	IEnumerator MaxSpeedPowerDown()
	{
		yield return new WaitForSeconds (6.0f);
		_MaxSpeed = false;
		_speed /= _multiplier;
	}
	public void ShieldActive()
	{
		_Shield = true;
		StartCoroutine (ShieldPowerDown ());
		_ShieldVisual.SetActive (true);
	}
	IEnumerator ShieldPowerDown()
	{
		yield return new WaitForSeconds(5.0f);
		_Shield = false;
		_ShieldVisual.SetActive (false);
	
	}
}
