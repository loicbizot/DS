using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleController : EntityCtrl {

    public enum BattleControllerState
    {
        ACTION_MENU,
        SPELL_MENU,
        ITEM_MENU,
        CIBLE_MENU,
        NONE
    }

    private BattleControllerState state;

    private bool combatActive;
    private BattleHandler battleHandler;

    // joystick state machine
    bool activeState;
    bool triggered;

    public override void ActiveTurn(BattleHandler handler) {
        state = BattleControllerState.ACTION_MENU;
        combatActive = true;
        battleHandler = handler;
        activeState = true;
    }

    public override void EndTurn() {
        state = BattleControllerState.NONE;
        combatActive = false;
        battleHandler = null;
    }
	
	// Update is called once per frame
	public void Do () {

        if(!combatActive)
            return;

        Vector2 inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        triggered = !activeState && inputs.magnitude > 0.8;
        activeState = inputs.magnitude > 0.8;

        if(triggered)
            battleHandler.ChangeFocus(inputs);

	}
    
}
