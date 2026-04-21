using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class CustomerScript : MonoBehaviour
{
    public bool GoingHome;
    public Transform target;
    public eShelfType destinationType;
    public Transform WareTransform;
    [SerializeField]
    NavMeshAgent myAgent;

    [SerializeField]
    NewTextScript myImage;

    [SerializeField]
    Animator animControl;

    private void Start()
    {

        MaybeNeedsHelp();
        target = GameManager.Instance.GetShelfLocation(destinationType);
        animControl.SetFloat("animSpeed", UnityEngine.Random.Range(0.5f, 1.5f));
    }

    void Update()
    {
        if (target != null)
            myAgent.destination = target.position;
    }


    public void GiveWare(wareOfShelf ware)
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        ware.transform.SetParent(transform);
        ware.transform.position = WareTransform.position;
        ware.GetComponent<BoxCollider>().enabled = false;
        ware.GetComponent<Rigidbody>().isKinematic = true;
        destinationType = eShelfType.CASHTHING;
        target = GameManager.Instance.GetShelfLocation(destinationType);
        GoingHome = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        CashScript cS = other.gameObject.GetComponent<CashScript>();
        if (cS != null)
        {
            if (GoingHome)
            {
                GameManager.Instance.AddCash(UnityEngine.Random.Range(1, 10));
                destinationType = eShelfType.BUS;
                target = GameManager.Instance.GetShelfLocation(destinationType);
            }
        }
        BusScript bs = other.gameObject.GetComponent<BusScript>();
        if (bs != null)
        {
            if (GoingHome)
            {
                Destroy(gameObject);
            }
        }
        CustomerInfoScript ci = other.gameObject.GetComponent<CustomerInfoScript>();
        if (ci != null)
        {
            destinationType = GiveRandomShelftype();
            myImage.ChangeDisplayedDestination(destinationType);
        }
    }
    public eShelfType GiveRandomShelftype()
    {
        eShelfType tmp;
        do
        {
            tmp = (eShelfType)UnityEngine.Random.Range(1, 7);
        } while (tmp == eShelfType.STORAGE || tmp == eShelfType.NONE);

        return tmp;
    }
    public void MaybeNeedsHelp()
    {
        float mnh = UnityEngine.Random.Range(0f, 1f);

        if (mnh < 0.3f)
        {
            destinationType = eShelfType.INFO;
        }
        else
        {
            destinationType = GiveRandomShelftype();
        }
    }

}
