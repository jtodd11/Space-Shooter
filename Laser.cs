using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	//speed variable of 8 meters
	// Use this for initialization
	private float _speed = 8.0f;


	// Update is called once per frame
	void Update () {
		//moving without control of movement but by the speed amount 
		transform.Translate(Vector3.up * _speed * Time.deltaTime);

		if(transform.position.y > 6f)
		{
			if(transform.parent != null) 
			{
				Destroy (transform.parent.gameObject);
			}
			Destroy (this.gameObject);
		}
	}
}
