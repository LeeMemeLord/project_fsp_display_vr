using UnityEngine;

public class PickUpsLogic : MonoBehaviour
{
    bool isPickUpable = true;

    [SerializeField]
    string typeOfPickUp;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        CheckIfIsPickedUp();
    }

    public bool GetisPickUpable()
    { 
        return isPickUpable;
    }

    public string GetPickUpType()
    {
        return typeOfPickUp;
    }

    public void SetisPickUpable(bool isPickUpState)
    {
        this.isPickUpable = isPickUpState;
    }

    public void CheckIfIsPickedUp()
    {
        if (!GetisPickUpable())
        {
            Destroy(gameObject);
        }
    }
}
