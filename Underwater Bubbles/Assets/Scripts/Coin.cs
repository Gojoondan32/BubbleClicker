using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject coinParticles;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime);
        
    }

    private void OnMouseDown()
    {
        GameManager.highScore += 50;

        //Spawn the particles
        Destroy(Instantiate(coinParticles, transform.position, Quaternion.identity) as GameObject, 1f);
        Destroy(gameObject);
    }
    
}
