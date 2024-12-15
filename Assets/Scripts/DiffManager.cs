using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiffManager : MonoBehaviour
{
    private bool isDifficult;

    public static DiffManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getIsDiff()
    {
        return isDifficult;
    }

    public void setIsDiff(bool other)
    {
        isDifficult = other;
    }
}
