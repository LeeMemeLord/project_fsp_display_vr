using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    // D�finir le nombre de d�g�ts par seconde (DPS)
    [SerializeField]
    float dps;

    // D�finir la capacit� du chargeur
    [SerializeField]
    int mag;

    [SerializeField]
    GameObject spark;

    [SerializeField]
    GameObject muzzle;

    // Variables pour le nombre de munitions actuelles et l'�tat de rechargement
    private int currentAmmo;
    private bool isReloading = false;

    // Start est appel�e avant le premier frame update
    void Start()
    {
        currentAmmo = mag;
    }

    // Update est appel�e � chaque frame
    void Update()
    {
        // Ajoutez le code n�cessaire ici, si besoin
    }

    // Retourne le DPS de l'arme
    public float Hit()
    {
        GameObject sparks = Instantiate(spark, muzzle.transform.position, muzzle.transform.rotation);
        StartCoroutine(DestroyAfterDelay(sparks, 0.035f));
        return dps;
    }

    // D�cr�mente les munitions actuelles lors d'un tir
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

    // Coroutine pour d�truire un objet apr�s un d�lai
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

    // Retourne la capacit� du chargeur
    public int GetMag()
    {
        return mag;
    }

    // D�finit le nombre actuel de munitions
    public void SetCurrentAmmo(int newValue)
    {
        currentAmmo = newValue;
    }

    // Retourne si l'arme est en cours de rechargement
    public bool IsReloading()
    {
        return isReloading;
    }

    // D�finit l'�tat de rechargement de l'arme
    public void SetIsReloading(bool value)
    {
        isReloading = value;
    }
}
