using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityCtrl : MonoBehaviour {

    private static readonly double ATM_MAX = 100;
    
    private Entity entity;

    double current_health;
    double current_energy;

    double current_stamina;
    double current_will;
    double current_strength;
    double current_malice;

    double current_speed;

    double current_ATM;

    protected virtual void Start()
    {
        entity = GetComponent<Entity>();
        Debug.Log(entity.name);

        current_health = entity.health;
        current_energy = entity.energy;

        current_stamina = entity.stamina;
        current_will = entity.will;
        current_strength = entity.strength;
        current_malice = entity.malice;

        current_speed = entity.speed;
        current_ATM = 0;

    }

    public Entity GetEntity()
    {
        return entity;
    }

    public void InitATM()
    {

        current_ATM = 50 + (Random.value - 0.5) * 2 * 20;

    }

    public double ATMBeforeTurn()
    {
        if(current_speed == 0)
            return Mathf.Infinity;
        return (ATM_MAX - current_ATM) / current_speed;
    }
        
    public void IncreaseATM(double amount)
    {
        current_ATM += current_speed * amount;
    }

    public double GetATM()
    {
        return current_ATM;
    }

    public abstract void ActiveTurn(BattleHandler handler);
    public abstract void EndTurn();

}

public class EntityPositionComparer : IComparer<EntityCtrl>
{

    private static float epsilon = Mathf.PI/8;

    public EntityPositionComparer(Transform r, Vector2 dir)
    {
        reference = r;
        direction = new Vector3(dir.x, 0, dir.y);
    }

    private Transform reference;
    private Vector3 direction;

    public int Compare(EntityCtrl a, EntityCtrl b)
    {

        Vector3 ra = a.transform.position - reference.position;
        Vector3 rb = b.transform.position - reference.position;

        float angleA = Vector3.Angle(ra, direction);
        float angleB = Vector3.Angle(rb, direction);

        // egalité
        if(Mathf.Abs(angleA - angleB) < epsilon)
        {
            if(ra.magnitude > rb.magnitude)
                return -1;
            else
                return 1;
        }
        else if(angleA > angleB)
            return -1;
        else
            return 1;

    }

}

public class EntityATMComparer : IComparer<EntityCtrl>
{

    public int Compare(EntityCtrl a, EntityCtrl b)
    {
        if(a.ATMBeforeTurn() < b.ATMBeforeTurn())
            return -1;
        if(a.ATMBeforeTurn() > b.ATMBeforeTurn())
            return 1;
        return 0;
    }

}