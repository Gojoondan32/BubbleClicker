using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AnimalStats 
{
    public string name;
    public int speed;

    public AnimalStats (string newName, int newSpeed)
    {
        this.name = newName;
        this.speed = newSpeed;
    }
}
