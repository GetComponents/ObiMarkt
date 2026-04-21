using UnityEngine;

public class dooropen : MonoBehaviour
{
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.playerBackpack.OnInteract.AddListener(OpenDoors);
        anim = GetComponent<Animator>();
    }

    public void OpenDoors()
    {
        anim.SetInteger("glassDoorOpen", 1);

    }

    public void CloseDoors()
    {
        anim.SetInteger("glassDoorOpen", 0);

    }
}
