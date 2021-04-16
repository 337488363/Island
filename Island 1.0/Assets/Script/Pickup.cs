using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject Game1;
    public GameObject Game2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "GunPickup"&&Game1.activeInHierarchy==true)
        {
            // Game2.SetActive(true);
            Game1.SetActive(false);
        }
    }
}
