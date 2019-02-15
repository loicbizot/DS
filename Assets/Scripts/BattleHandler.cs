using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour {

    public bool battleActive;

    List<EntityCtrl> controllersA;
    List<EntityCtrl> controllersB;

    EntityCtrl focused;

    new public FollowingCamera camera;
    public ScrollingText scrollingText;
    public GameObject battlePanel;

    private void Start()
    {
        controllersA = new List<EntityCtrl>();
        controllersB = new List<EntityCtrl>();
    }

    public void StartBattle(EntityCtrl player, EntityCtrl foe)
    {

        if(battleActive)
        {
            AddToBattle(foe);
            return;
        }

        battleActive = true;
        //battlePanel.SetActive(true);

        controllersA.Add(player);
        controllersB.Add(foe);

        player.InitATM();
        foe.InitATM();

        Controller playerController = player.gameObject.GetComponent<Controller>();
        playerController.controllerState = Controller.ControllerState.BATTLE;
        playerController.RecenterAttention(MiddleFoes());
        Controller foeController = foe.gameObject.GetComponent<Controller>();
        foeController.controllerState = Controller.ControllerState.BATTLE;
        foeController.RecenterAttention(MiddleAllies());

        //focused = GameObject.FindGameObjectWithTag("main_char").GetComponent<EntityCtrl>();
        //focused.ActiveTurn(this);

        //ShowInformation();

        StartCoroutine(DefferedBattleBegin());

    }

    private IEnumerator DefferedBattleBegin()
    {

        yield return new WaitForSeconds(1);
        NextTurn();

    }

    public void AddToBattle(EntityCtrl foe)
    {

        controllersB.Add(foe);
        foe.InitATM();

        Controller foeController = foe.gameObject.GetComponent<Controller>();
        foeController.controllerState = Controller.ControllerState.BATTLE;
        foeController.RecenterAttention(MiddleAllies());

        foreach(EntityCtrl ctrl in controllersA)
        {
            Controller controller = ctrl.gameObject.GetComponent<Controller>();
            controller.RecenterAttention(MiddleFoes());
        }

    }

    public Vector2 MiddleAllies()
    {
        Vector2 middle = Vector2.zero;
        foreach(EntityCtrl ctrl in controllersA)
        {
            Vector2 position2D = new Vector2(ctrl.transform.position.x, ctrl.transform.position.z);
            middle += position2D;
        }

        return middle / controllersA.Count;

    }

    public Vector2 MiddleFoes()
    {
        Vector2 middle = Vector2.zero;
        foreach(EntityCtrl ctrl in controllersB)
        {
            Vector2 position2D = new Vector2(ctrl.transform.position.x, ctrl.transform.position.z);
            middle += position2D;
        }

        return middle / controllersB.Count;

    }

    public void NextTurn()
    {
        
        double minATMTime = Mathf.Infinity;
        EntityCtrl nextCtrl = null;
        int ally = 0;

        foreach(EntityCtrl ctrlA in controllersA)
            if(ctrlA.ATMBeforeTurn() < minATMTime)
            {
                nextCtrl = ctrlA;
                minATMTime = ctrlA.ATMBeforeTurn();
                ally = 1;
            }

        foreach(EntityCtrl ctrlB in controllersB)
            if(ctrlB.ATMBeforeTurn() < minATMTime)
            {
                nextCtrl = ctrlB;
                minATMTime = ctrlB.ATMBeforeTurn();
                ally = -1;
            }

        if(nextCtrl == null)
        {
            Debug.Log("Nani the fucc");
            return; 
        }

        Debug.Log(nextCtrl.gameObject.name);
        
        foreach(EntityCtrl ctrlA in controllersA)
        {
            ctrlA.IncreaseATM(minATMTime);
            Debug.Log(ctrlA.name + " " + ctrlA.GetATM());
        }

        foreach(EntityCtrl ctrlB in controllersB)
        {
            ctrlB.IncreaseATM(minATMTime);
            Debug.Log(ctrlB.name + " " + ctrlB.GetATM());
        }

        nextCtrl.ActiveTurn(this);

    }

    public void EndTurn()
    {
        // vérifications fin combat
    }

    public void ChangeFocus(Vector2 direction)
    {

        List<EntityCtrl> entities = new List<EntityCtrl>();
        foreach(EntityCtrl e in controllersA)
            entities.Add(e);
        foreach(EntityCtrl e in controllersB)
            entities.Add(e);

        entities.Remove(focused);

        entities.Sort(new EntityPositionComparer(camera.followed, direction));

        focused = entities[entities.Count - 1];
        camera.followed = focused.transform;

        ShowInformation();

    }

    private void ShowInformation()
    {

        Debug.Log(focused);

        scrollingText.message = focused.GetEntity().GetInformation();
        scrollingText.OnTextChange();

    }

    public void EndBattle()
    {
        foreach(EntityCtrl entity in controllersA)
        {
            entity.gameObject.GetComponent<Controller>().controllerState = Controller.ControllerState.WANDERING;
        }

        foreach(EntityCtrl entity in controllersB)
        {
            entity.gameObject.GetComponent<Controller>().controllerState = Controller.ControllerState.WANDERING;
        }

        controllersA.Clear();
        controllersB.Clear();

        battlePanel.SetActive(false);
        camera.followed = GameObject.FindGameObjectWithTag("main_char").transform;
    }

}
