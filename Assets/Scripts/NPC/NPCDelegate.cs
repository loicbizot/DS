using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDelegate : Controller {

    private NPCRoutine nPCRoutine;
    private NPCCombatCtrl nPCCombatCtrl;

    // Use this for initialization
    void Start () {
        nPCRoutine = GetComponent<NPCRoutine>();
        nPCCombatCtrl = GetComponent<NPCCombatCtrl>();
	}
	
	// Update is called once per frame
	void Update () {

        switch(controllerState)
        {
            case ControllerState.DOWN:
                break;
            case ControllerState.WANDERING:
                nPCRoutine.Do();
                break;
            case ControllerState.BATTLE:
                nPCCombatCtrl.Do();
                break;
        }

	}
}
