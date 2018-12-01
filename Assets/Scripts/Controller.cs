using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour {

    public enum ControllerState
    {
        DOWN,
        BATTLE,
        WANDERING
    }

    public ControllerState controllerState;

    public void RecenterAttention(Vector2 middle_ennemies)
    {
        ObjectOrientation orientation = GetComponent<ObjectOrientation>();
        Vector2 position2D = new Vector2(transform.position.x, transform.position.z);
        Vector2 newOrientation = middle_ennemies - position2D;
        orientation.ChangeOrientation(newOrientation);
        orientation.ChangeOrientation(Vector2.zero);
    }

}
