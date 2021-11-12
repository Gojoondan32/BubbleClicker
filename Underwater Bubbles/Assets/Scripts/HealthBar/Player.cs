using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Stats health;

    [SerializeField]
    private Stats stamina;

    private void Awake()
    {
        //Call initialize function
        health.Initialize();
        stamina.Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health.CurrentVal -= 10;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            health.CurrentVal += 10;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            stamina.CurrentVal -= 10;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            stamina.CurrentVal += 10;
        }
    }
}
