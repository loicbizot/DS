using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOrientation : MonoBehaviour {
    
    private Animator animator;
    public Vector2 orientation = new Vector2(0,-1);

    // Use this for initialization
    void Start () {
        
        animator = GetComponent<Animator>();

        // init orientation
        ChangeOrientation(orientation);
        ChangeOrientation(Vector2.zero);
    }
	
	// Update is called once per frame
	public void ChangeOrientation (Vector2 inputs) {

        if(inputs != Vector2.zero)
            orientation = inputs.normalized;

        if (inputs.magnitude > 0.1)
        {
            animator.SetBool("walking", true);
            animator.SetFloat("x", inputs.x);
            animator.SetFloat("y", inputs.y);

        }
        else
        {
            animator.SetBool("walking", false);
        }

    }
}
