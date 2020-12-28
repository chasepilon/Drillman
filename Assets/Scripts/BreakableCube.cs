using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableCube : MonoBehaviour
{
    bool isAlive;
    public GameObject breakParticles;
    
    void Start()
    {
        isAlive = true;
    }

    private void Update()
    {
        if (!isAlive)
            gameObject.SetActive(false);
    }

    public void DrillThroughCube()
    {
        Instantiate(breakParticles, transform.position, breakParticles.transform.rotation);
        isAlive = false;
    }
}
