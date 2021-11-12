using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    
    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Text valueText;

    [SerializeField]
    private Color fullColour;
    [SerializeField]
    private Color lowColour;

    [SerializeField]
    private bool lerpColours;
    
    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            //Adding all strings before ":" to an array so the health string is not overidden
            string[] temp = valueText.text.Split(':');
            valueText.text = temp[0] + ": " + value;
            //Set the fill amount the output of the Map function
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (lerpColours)
        {
            //Set contents colour to full at the start of the game
            content.color = fullColour;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }
    private void HandleBar()
    {
        //Update the fill amount if the bar is not equal to the value 
        if (fillAmount != content.fillAmount)
        {
            //Move the fill bar over time rather than instant
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }
        if (lerpColours)
        {
            //Lerp the colour values between low colour and full colour based on the fill amount
            content.color = Color.Lerp(lowColour, fullColour, fillAmount);
        }
        
        
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        //Value - current health, inMin - minimum health (normally 0), outMax - the max value of the fill bar (normally 1)
        //outMin - the min value of the fill bar (normally 0), inMax - max health the player can have (can be anything like 100 or 230)
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
