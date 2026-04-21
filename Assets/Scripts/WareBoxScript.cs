using UnityEngine;
using UnityEngine.Events;

public class WareBoxScript : MonoBehaviour
{



    [SerializeField]
    private GameObject warePrefab, sign;

    [SerializeField]
    private eShelfType myType;

    [SerializeField]
    MeshRenderer mr;

    private void Start()
    {
        GameManager.Instance.GetColor(myType, mr.material);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBackpack pB = other.gameObject.GetComponent<PlayerBackpack>();
        if (pB != null)
        {
            pB.OnInteract.AddListener(SpawnWare);
            sign.SetActive(true);
        }
    }

    private void SpawnWare()
    {
        wareOfShelf tmp = Instantiate(warePrefab, GameManager.Instance.playerBackpack.transform.position, Quaternion.identity).GetComponent<wareOfShelf>();

        tmp.ResetWare(myType);
        GameManager.Instance.playerBackpack.AddWare(tmp);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerBackpack pB = other.gameObject.GetComponent<PlayerBackpack>();
        if (pB != null)
        {
            pB.OnInteract.RemoveListener(SpawnWare);
            sign.SetActive(false);
        }
    }
}
