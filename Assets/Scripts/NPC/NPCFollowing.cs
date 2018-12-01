using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCFollowing : NPCRoutine {

    private ObjectOrientation orientation;
    public Transform toFollow;
    public NavMeshAgent agent;
    
	void Start () {
        orientation = GetComponent<ObjectOrientation>();
        agent.updatePosition = false;
        agent.updateRotation = false;
	}
	
	public override void Do () {

		if(toFollow != null)
        {
            agent.destination = toFollow.position;
            agent.stoppingDistance = 2;
        }
        else
        {
            agent.stoppingDistance = 0;
        }

        // suivre l'entité juste qu'au dernier endroit vu

        Vector3 velocity = (agent.nextPosition - transform.position).normalized;
        orientation.ChangeOrientation(new Vector2(velocity.x, velocity.z));

        transform.position = agent.nextPosition;

        Debug.DrawLine(transform.position, transform.position + new Vector3(orientation.orientation.x, 0, orientation.orientation.y));

        if(toFollow != null && Vector3.Distance(transform.position, toFollow.transform.position) < 3)
            EngageFight();

    }

    private void EngageFight()
    {

        GameObject battleHandler = GameObject.Find("CombatHandler");
        BattleHandler battleHandlerScript = battleHandler.GetComponent<BattleHandler>();

        EntityCtrl foe = toFollow.gameObject.GetComponent<EntityCtrl>();
        EntityCtrl ally = GetComponent<EntityCtrl>();

        battleHandlerScript.StartBattle(foe, ally);

    }
    
}
