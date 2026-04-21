using UnityEngine;


public class BusScript : MonoBehaviour
{
    public Transform CustomerTarget;
    public eShelfType myType;
    public MeshRenderer mr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.GetColor(myType, mr.material);
        GameManager.Instance.AddShelfLocation(CustomerTarget, myType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
