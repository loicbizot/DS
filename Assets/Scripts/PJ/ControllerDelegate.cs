using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDelegate : Controller {
    
    private PlayerMovement playerMovement;
    private PlayerBattleController playerController;

	// Use this for initialization
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
        playerController = GetComponent<PlayerBattleController>();
	}
	
	// Update is called once per frame
	void Update () {

        switch(controllerState)
        {
            case ControllerState.DOWN:
                break;
            case ControllerState.WANDERING:
                playerMovement.Do();
                break;
            case ControllerState.BATTLE:
                playerController.Do();
                break;
            default:
                break;
        }

	}

}
