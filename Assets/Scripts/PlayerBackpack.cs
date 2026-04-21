using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBackpack : MonoBehaviour
{
    List<wareOfShelf> Inventory = new List<wareOfShelf>();
    public Transform WareTransform;
    public UnityEvent OnInteract = new UnityEvent();
    public Transform FollowPos;
    public Transform camPos;

    private void Awake()
    {
        camPos = Camera.main.transform;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        wareOfShelf wf = collision.gameObject.GetComponent<wareOfShelf>();
        if (wf != null)
        {
            AddWare(wf);
        }
    }





    public void AddWare(wareOfShelf ware)
    {
        ware.transform.SetParent(transform);
        Inventory.Add(ware);
        ware.transform.position = WareTransform.position;
        ware.transform.position += new Vector3(0, ware.transform.lossyScale.y * 0.5f, 0);
        WareTransform.position += new Vector3(0, ware.transform.lossyScale.y, 0);
        ware.GetComponent<BoxCollider>().enabled = false;
        ware.GetComponent<Rigidbody>().isKinematic = true;
    }

    public bool DropFirst(eShelfType shelfType)
    {
        if (Inventory.Count == 0)
            return false;

        wareOfShelf tmp = Inventory[Inventory.Count - 1];
        if (tmp.myType != shelfType)
            return false;

        tmp.transform.position = WareTransform.position;
        WareTransform.position -= new Vector3(0, tmp.transform.lossyScale.y, 0);
        Inventory.Remove(tmp);
        Destroy(tmp.gameObject);
        return true;
    }
}
