using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLogic : MonoBehaviour
{
    // Start est appel�e avant le premier frame update
    void Start()
    {

    }

    // Update est appel�e � chaque frame
    void Update()
    {

    }

    // Appel�e lorsque la grenade entre en collision avec un autre objet
    public void OnTriggerEnter(Collider other)
    {
        GameObject parent = GameObject.Find("M26 1(Clone)");
        if (VerifyIfEnemyInRadius(other))
        {
            GrenadeDps(parent, other);
        }
        Destroy(parent);
    }

    // V�rifie si le collider appartient � un ennemi ou un joueur
    public bool VerifyIfEnemyInRadius(Collider other)
    {
        return other.name.Equals("enemy") || other.name.Equals("player");
    }

    // Applique des d�g�ts en fonction de la distance entre la grenade et le collider
    public void GrenadeDps(GameObject grenade, Collider collider)
    {

        float distance = Vector3.Distance(grenade.transform.position, collider.transform.position);

        //float damage = Mathf.RoundToInt(-100f * distance + 600f);
        float damage = 100f;
        GameManager.Instance.GrenadeDps(collider, damage);

        Destroy(grenade);
    }
}
