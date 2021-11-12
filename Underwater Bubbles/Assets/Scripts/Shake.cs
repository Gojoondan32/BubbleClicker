using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator animator;
    
    public void ShakeCamera()
    {
        animator.SetTrigger("shake");
    }
}
