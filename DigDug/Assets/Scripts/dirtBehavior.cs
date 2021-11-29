using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dirtBehavior : MonoBehaviour
{
    int currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 1;
    }

    public void Digging(int dig)
    {
        currentHealth -= dig;

        if (currentHealth <= 0)
        {
            SpriteRenderer dirtColor = GetComponent<SpriteRenderer>();
            Destroy(dirtColor);
        }
    }
   
}
