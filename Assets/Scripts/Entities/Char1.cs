using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char1 : Entity {

    public override string GetInformation()
    {

        int threshold = (int) (health * 4);

        switch(threshold){

            case 0:
                return "Your vision start to blur, as the blood is flooding out of your body.";
            case 1:
                return "You're suffering from a critical injury.";
            case 2:
                return "You start feeling pain from your wounds.";
            case 3:
                return "You've taken minor damages.";
            case 4:
                return "Everything is all right.";
            default:
                return "You cannot understand the state in which you are.";

        }

    }

}
