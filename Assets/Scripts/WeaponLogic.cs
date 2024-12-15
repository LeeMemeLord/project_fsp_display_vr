using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    // Définir le nombre de dégâts par seconde (DPS)
    [SerializeField]
    float dps;

    // Définir la capacité du chargeur
    [SerializeField]
    int mag;

    [SerializeField]
    GameObject spark;

    [SerializeField]
    GameObject muzzle;

    // Variables pour le nombre de munitions actuelles et l'état de rechargement
    private int currentAmmo;
    private bool isReloading = false;

    // Start est appelée avant le premier frame update
    void Start()
    {
        currentAmmo = mag;
    }

    // Update est appelée à chaque frame
    void Update()
    {
        // Ajoutez le code nécessaire ici, si besoin
    }

    // Retourne le DPS de l'arme
    public float Hit()
    {
        GameObject sparks = Instantiate(spark, muzzle.transform.position, muzzle.transform.rotation);
        StartCoroutine(DestroyAfterDelay(sparks, 0.035f));
        return dps;
    }

    // Décrémente les munitions actuelles lors d'un tir
    public void Shoot()
    {
        if (currentAmmo == 0)
        {
            return;
        }
        currentAmmo -= 1;

        GameObject sparks = Instantiate(spark, muzzle.transform.position, muzzle.transform.rotation);
        StartCoroutine(DestroyAfterDelay(sparks, 0.035f));
    }

    // Coroutine pour détruire un objet après un délai
    private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }

    // Retourne le nombre actuel de munitions
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    // Retourne la capacité du chargeur
    public int GetMag()
    {
        return mag;
    }

    // Définit le nombre actuel de munitions
    public void SetCurrentAmmo(int newValue)
    {
        currentAmmo = newValue;
    }

    // Retourne si l'arme est en cours de rechargement
    public bool IsReloading()
    {
        return isReloading;
    }

    // Définit l'état de rechargement de l'arme
    public void SetIsReloading(bool value)
    {
        isReloading = value;
    }
}
