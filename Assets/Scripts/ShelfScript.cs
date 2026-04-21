using System.Collections.Generic;
using UnityEngine;

public class ShelfScript : MonoBehaviour
{
    public eShelfType myType;

    public MeshRenderer mr;
    public int MaxCapacity;
    public int currentCapacity;
    public Transform CustomerTarget;
    List<CustomerScript> customersNearMe = new List<CustomerScript>();

    [SerializeField]
    GameObject warePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCapacity = 0;
        GameManager.Instance.GetColor(myType, mr.material);
        GameManager.Instance.AddShelfLocation(CustomerTarget, myType);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DropWare()
    {
        PlayerBackpack pB = GameManager.Instance.playerBackpack;
        if (pB != null)
        {
            if (pB.DropFirst(myType) == true)
            {
                currentCapacity++;
                CheckNearCustomers();
            }
        }
    }

    private void CheckNearCustomers()
    {
        if (customersNearMe.Count > 0)
        {
            GivePackageToCustomer(customersNearMe[0]);
        }
    }

    private void GivePackageToCustomer(CustomerScript cs)
    {
        wareOfShelf tmp = Instantiate(warePrefab, cs.transform.position, Quaternion.identity).GetComponent<wareOfShelf>();
        tmp.ResetWare(myType);
        cs.GiveWare(tmp);
        currentCapacity--;
        customersNearMe.Remove(cs);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBackpack pB = other.gameObject.GetComponent<PlayerBackpack>();
        if (pB != null)
        {
            pB.OnInteract.AddListener(DropWare);
        }

        CustomerScript cs = other.gameObject.GetComponent<CustomerScript>();
        if (cs != null)
        {
            customersNearMe.Add(cs);
            eShelfType dT = cs.destinationType;
            if (myType == dT)
            {
                if (currentCapacity > 0)
                {
                    GivePackageToCustomer(cs);
                }
            }

        }
    }


    private void OnTriggerExit(Collider other)
    {
        PlayerBackpack pB = other.gameObject.GetComponent<PlayerBackpack>();
        if (pB != null)
        {
            pB.OnInteract.RemoveListener(DropWare);
            return;
        }

        CustomerScript cs = other.gameObject.GetComponent<CustomerScript>();
        if (cs != null)
        {
            if (customersNearMe.Contains(cs))
            {
                customersNearMe.Remove(cs);
            }
        }

    }
}

public enum eShelfType
{
    NONE = 0,
    GARDENING = 1,
    BATHROOM = 2,
    WOOD = 3,
    PLANTS = 4,
    STORAGE = 5,
    LIGHTS = 6,
    BUS = 7,
    CASHTHING = 8,
    INFO = 9
}
