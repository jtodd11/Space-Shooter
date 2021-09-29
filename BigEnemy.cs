using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : MonoBehaviour 
{
	[SerializeField]
	private float _fallSpeed = 2.0f;

	private Player player;
	private Animator _anim;
	private AudioSource _audioSource;

	void Start () 
	{
		transform.position = new Vector3(0, 8.0f , 0);
		_anim = GetComponent<Animator> ();
		_audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(Vector3.down * _fallSpeed * Time.deltaTime);	
		Falling ();
	}
	void Falling()
	{
	if (transform.position.y < -8.5f) 
		{
			float randomX = Random.Range (-7.0f, 7.0f);
			transform.position = new Vector3 (randomX, 8.5f, 0);
		}

	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Shields") 
		{
			_anim.SetTrigger("EnemyDeath");

			_fallSpeed = 0;
			_audioSource.Play ();
			Destroy(GetComponent<Collider2D>());
			Destroy (this.gameObject, 2.6f);
		}
		if (other.tag == "Player")
		{
			other.transform.GetComponent<Player>().Damage();
			//^^this damages player
			/*if (player != null) 
			{
				player.Damage();
			}*/
			//this ^^ is safety check if anything goes wrong like  player is deleted or anything the systmen will still run as nnormal
			_anim.SetTrigger("BigEnemyDeath");

			_fallSpeed = 0;
			_audioSource.Play ();
			Destroy(GetComponent<Collider2D>());
			Destroy(this.gameObject, 2.6f);
		}

		if (other.tag == "Laser")
		{


			Destroy(other.gameObject);

			if (player != null) 
			{
				player.Points (5);

			}
			_anim.SetTrigger("BigEnemyDeath");

			_fallSpeed = 0;
			_audioSource.Play ();
			Destroy (GetComponent<Collider2D> ());
			Destroy(this.gameObject, 2.6f);
		}
	}
}
