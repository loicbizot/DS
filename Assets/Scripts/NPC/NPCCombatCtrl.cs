using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCombatCtrl : EntityCtrl {

    private bool isTurn;
    private BattleHandler battleHandler;

    protected override void Start()
    {
        base.Start();
        isTurn = false;
        battleHandler = null;
    }

    public override void ActiveTurn(BattleHandler handler)
    {
        //throw new System.NotImplementedException();
        isTurn = true;
        battleHandler = handler;
    }

    public override void EndTurn()
    {
        //throw new System.NotImplementedException();
        isTurn = false;
        battleHandler = null;
    }

    public void Do()
    {
        if(!isTurn)
            return;
        else {
            isTurn = false;
            StartCoroutine(Act());
        }
    }

    public IEnumerator Act()
    {
        yield return new WaitForSeconds(0.1f);

        // choix de la cible
        // Action action = IA.ChooseAction();
        // action.perform()

    }

}
