using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrowLogic : MonoBehaviour
{
    // Start est appelée avant le premier frame update
    void Start()
    {

    }

    // Update est appelée à chaque frame
    void Update()
    {

    }

    // Appelée lorsque l'objet entre en collision avec un autre objet
    public void OnTriggerEnter(Collider other)
    {
        SFXmanager.Instance.PlaySound(this.transform.position, "grenadeExplosion");
        if (!other.name.Equals("Capsule") && !other.name.Equals("zoneDetection") && !other.name.Equals("player"))
        {
            GameObject explosion = GameObject.Find("zoneExplosion");

            explosion.AddComponent<GrenadeLogic>();
            BoxCollider boxCollider = explosion.GetComponent<BoxCollider>();
            boxCollider.enabled = true;
        }
    }
}
