using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GemCollectable gem = other.GetComponent<GemCollectable>(); 

        if (gem != null)
        {
            GameManager.Instance.CollectGem(gem.CollectGem());
        }
    }
}
