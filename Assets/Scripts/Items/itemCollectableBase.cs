using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCollectableBase : MonoBehaviour
{
    public String tagToCompare = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagToCompare))
        {
            collect();
        }
    }

    protected virtual void collect(){
        gameObject.SetActive(false);
        onCollect();
    }

    protected virtual void onCollect(){}
}
