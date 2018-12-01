using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat1 : Entity {

    public override string GetInformation()
    {

        int threshold = (int)(health * 4);

        switch(threshold)
        {

            case 0:
                return "Meow0";
            case 1:
                return "Meow1";
            case 2:
                return "Meow2";
            case 3:
                return "Meow3";
            case 4:
                return "This cat looks aggressive !";
            default:
                return "This cat is in a surnatural state.";

        }

    }

}
