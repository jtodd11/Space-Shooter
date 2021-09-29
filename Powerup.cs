using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
	[SerializeField]
	private float _fallspeed = 3.0f;
	//[SerializeField]
	//private float _PowerUpPrefab;
	[SerializeField]//0 = speed, 1 = quad shot, 2 = shield
	private int _PowerUpID;
	[SerializeField]
	private AudioClip _audioClip;
	// Use this for initialization

	// Update is called once per frame
	void Update () 
	{
		
		transform.Translate(Vector3.down * _fallspeed * Time.deltaTime);
		falling ();

	}
	void falling()
	{
		//float randomX = Random.Range (-7.0f, 7.0f);
		if (transform.position.y < -8.0f) 
		{
			
			Destroy (this.gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			
			AudioSource.PlayClipAtPoint (_audioClip, transform.position);
			switch(_PowerUpID)
			{
			case 0: 
				other.transform.GetComponent<Player>().MaxSpeedActive ();
				Destroy (this.gameObject);
				break;
			case 1:
				other.transform.GetComponent<Player>().ShieldActive();
				Destroy (this.gameObject);

				break;
			case 2: 
				other.transform.GetComponent<Player>().QuadShotActive();
				Destroy (this.gameObject);
				break;
			default:
				Debug.Log ("Nothing");
				break;

			}
		}

	}
	/*IEnumerator SpawnRoutine()
	{
		Vector3 postospawn = new Vector3(Random.Range(-7f, 7f), 7, 0);
		//GameObject newPower = Instantiate(_PowerUpPrefab, postospawn, Quaternion.identity);
		yield return new WaitForSeconds(2.0f);
	}*/
}
