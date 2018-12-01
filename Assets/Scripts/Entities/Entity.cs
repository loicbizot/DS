using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour{

    new public string name = "Insert Entity Name";

    public double health = 1;
    public double energy = 1;

    public double stamina = 5;
    public double will = 5;
    public double strength = 5;
    public double malice = 5;

    public double speed = 10;

    public abstract string GetInformation();

}
