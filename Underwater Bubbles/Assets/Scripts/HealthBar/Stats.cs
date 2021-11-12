using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stats 
{
    [SerializeField]
    private Bar bar;

    [SerializeField]
    private float maxVal;

    [SerializeField]
    private float currentVal;


    //Initiate a property of CurrentVal
    public float CurrentVal
    {
        get
        {
            return currentVal;
        }
        set
        {
            //Clamp the values so it cannot excceed the maximum and minimum
            this.currentVal = Mathf.Clamp(value, 0, MaxVal);
            //Update the bar's value to the current value
            bar.Value = currentVal;
        }
    }
    public float MaxVal
    {
        get
        {
            return maxVal;
        }
        set
        {
            //Update the max value of the bar
            this.maxVal = value;
            bar.MaxValue = maxVal; 
        }
    }
    public void Initialize()
    {
        //Set players max health and current health to the values 
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}
