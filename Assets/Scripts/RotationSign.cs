using UnityEngine;

public class RotationSign : MonoBehaviour
{
   
    void Update()
    {
        transform.forward = (GameManager.Instance.playerBackpack.camPos.position - transform.position) * -1;
    }
}
