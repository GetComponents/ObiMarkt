using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewTextScript : MonoBehaviour
{
    //public TextMeshProUGUI CustomerDisplayedDestination;
    [SerializeField]
    Sprite gardening, bathroom, wood, plants, lights;
    public Image myImage;
    bool spriteBool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spriteBool = !spriteBool;
            //ChangeDisplayedDestination("hello");
        }
    }
    public void ChangeDisplayedDestination(eShelfType shelfType)
    {
        //CustomerDisplayedDestination.text = nd;
        myImage.enabled = true;
        
            switch (shelfType)
            {
                case eShelfType.GARDENING:
                    myImage.sprite = gardening;
                    break;
                case eShelfType.BATHROOM:
                    myImage.sprite = bathroom;
                    break;
                case eShelfType.WOOD:
                    myImage.sprite = wood;
                    break;
                case eShelfType.PLANTS:
                    myImage.sprite = plants;
                    break;
                case eShelfType.LIGHTS:
                    myImage.sprite = lights;
                    break;
                default:
                    break;
            }
    }
}
