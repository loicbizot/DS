using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    ObjectOrientation orientation;
    new Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        orientation = GetComponent<ObjectOrientation>();
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	public void Do() {
        Vector2 inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        orientation.ChangeOrientation(inputs);

        if (inputs.magnitude > 0.1)
            rigidbody.velocity = new Vector3(inputs.x*3, rigidbody.velocity.y, inputs.y*3);
        else
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
    }
}
