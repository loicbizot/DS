using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {

    public Transform followed;
    public float speed = 0.125f;
    public Vector3 delta;

	// Use this for initialization
	void Start () {

        transform.position = followed.transform.position + delta;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.position = Vector3.Lerp(transform.position, followed.transform.position + delta, speed);

	}
}
