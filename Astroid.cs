using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour {

	[SerializeField]
	private float _rotateSpeed = 19.6f;
	[SerializeField]
	private GameObject _explosionprefab;

	private SpawnManager _spawnManager;
	// Update is called once per frame
	private void Start()
	{
		_spawnManager = GameObject.FindWithTag ("Spawn_Manager").GetComponent<SpawnManager> ();
	}
	void Update () 
	{
		transform.Rotate (Vector3.forward * _rotateSpeed * Time.deltaTime);
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Laser") 
		{
			Instantiate (_explosionprefab , transform.position, Quaternion.identity);
			Destroy (other.gameObject);
			_spawnManager.StartSpawning ();
			Destroy (this.gameObject, 0.23f);

		}
	}
}
