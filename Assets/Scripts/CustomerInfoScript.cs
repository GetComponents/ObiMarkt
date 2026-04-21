using System.Collections.Generic;
using UnityEngine;

public class CustomerInfoScript : MonoBehaviour
{

    List<CustomerScript> customersNearMe = new List<CustomerScript>();
    [SerializeField]
    private GameObject sign;
    public Transform CustomerTarget;
    public eShelfType myType;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.AddShelfLocation(CustomerTarget, myType);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FollowPlayer()
    {
        foreach (CustomerScript customer in customersNearMe)
        {
            customer.target = GameManager.Instance.playerBackpack.FollowPos;
        }
        customersNearMe.Clear();
    }


    private void OnTriggerEnter(Collider other)
    {
        CustomerScript cs = other.gameObject.GetComponent<CustomerScript>();
        if (cs != null)
        {
            sign.SetActive(true);
            customersNearMe.Add(cs);
        }
        PlayerBackpack pb = other.gameObject.GetComponent<PlayerBackpack>();
        if (pb != null)
        {
            //Debug.Log("Added EVent");
            pb.OnInteract.AddListener(FollowPlayer);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        CustomerScript cs = other.gameObject.GetComponent<CustomerScript>();
        if (cs != null)
        {
            if (customersNearMe.Contains(cs))
                customersNearMe.Remove(cs);
            sign.SetActive(true);
        }
        PlayerBackpack pb = other.gameObject.GetComponent<PlayerBackpack>();
        if (pb != null)
        {
            pb.OnInteract.RemoveListener(FollowPlayer);
        }
    }
}
