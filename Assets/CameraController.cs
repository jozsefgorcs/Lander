using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public GameObject player;
	private Rigidbody2D _rigidbody2D;
	private Vector3 offset;

	public Camera camera;
	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
		_rigidbody2D = player.GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		var speed = _rigidbody2D.velocity.magnitude;
	
		//camera.orthographicSize = Math.Abs(speed+6f);
		
	}
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
