using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbreakableCube : MonoBehaviour
{
    AudioSource unbreakableSFX;
    public GameObject unbreakableParticles;

    private void Start()
    {
        unbreakableSFX = GetComponent<AudioSource>();
    }

    public void FailDrillingThroughCube()
    {
        if(!unbreakableSFX.isPlaying)
        {
            Instantiate(unbreakableParticles, transform.position, unbreakableParticles.transform.rotation);
            unbreakableSFX.Play();
        }
    }
}
