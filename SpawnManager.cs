using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
	[SerializeField]
	private GameObject _EnemyPrefabBig;
	[SerializeField]
	private GameObject _EnemyPrefabSm;
	[SerializeField]
	private GameObject[] _PowerUpPrefabs;
	[SerializeField]
	private GameObject _EnemyContainer;
	private bool _StopSpawn = false;



	// Use this for initialization
	void Start () {

	}
	public void StartSpawning()
	{

		StartCoroutine (SpawnBigEnemyRoutine ());
		StartCoroutine (SpawnQuadShotPowerUp ());
		StartCoroutine (SpawnSmallEnemyRoutine ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator SpawnBigEnemyRoutine()
	{
		while (_StopSpawn == false) {
			Vector3 postospawn = new Vector3 (Random.Range (-7f, 7f), 7, 0);
		
			GameObject newEnemy = Instantiate (_EnemyPrefabBig, postospawn, Quaternion.identity);
			newEnemy.transform.parent = _EnemyContainer.transform;
			yield return new WaitForSeconds (10.0f);
		}
	}
	IEnumerator SpawnSmallEnemyRoutine() 
	{
		while (_StopSpawn == false) 
		{
			Vector3 postospawn = new Vector3 (Random.Range (-7f, 7f), 7, 0);
			GameObject newEnemy = Instantiate (_EnemyPrefabSm, postospawn, Quaternion.identity);
			newEnemy.transform.parent = _EnemyContainer.transform;
			yield return new WaitForSeconds (3.0f);
		}
	}
	/* what this while loop does is 1. keep respawning within certain parameters. 2. puts all the 
		 * newly spawned items(prefabs) in a container. 3. keep goig until player dies or points are achieved.*/

	IEnumerator SpawnQuadShotPowerUp()
		{
		while (_StopSpawn == false)
			{
			Vector3 postospawn = new Vector3 (Random.Range (-7f, 7f), 7, 0);
			int RandomPowerUp = Random.Range (0, 3);
			Instantiate (_PowerUpPrefabs[RandomPowerUp], postospawn, Quaternion.identity);
			yield return new WaitForSeconds (Random.Range(10.0f, 15.0f));
			}
		}


	public void OnPlayerDeath()
	{
		_StopSpawn = true; 
	}


}
