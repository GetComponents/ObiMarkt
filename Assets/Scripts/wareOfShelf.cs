using UnityEngine;

public class wareOfShelf : MonoBehaviour
{
    public eShelfType myType;

    [SerializeField]
    private MeshRenderer mr;

    public void ResetWare(eShelfType sType)
    {
        myType = sType;
        GameManager.Instance.GetColor(myType, mr.material);
        transform.localScale *= Random.Range(0.01f, 2f);
    }
}
