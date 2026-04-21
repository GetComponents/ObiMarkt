using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject CustomerPrefab;

    public Transform BusSpawn;

    public PlayerBackpack playerBackpack;

    public Color gardeningCol, bathroomCol, lightsCol, woodCol, PlantsCol, StorageCol;

    public int cash = 0;

    [SerializeField]
    TextMeshProUGUI cashText;

    Dictionary<eShelfType, Transform> ShelfLocations = new Dictionary<eShelfType, Transform>();


    private void Awake()
    {
    
        if (Instance == null)
        {
            Instance = this;
        }
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            Instantiate(CustomerPrefab, BusSpawn.position, Quaternion.identity);
        }
    }

    public void GetColor(eShelfType shelfType, Material mr)
    {
        switch (shelfType)
        {
            case eShelfType.NONE:
                break;
            case eShelfType.GARDENING:
                 mr.color = gardeningCol;
                break;
            case eShelfType.BATHROOM:
                mr.color = bathroomCol;
                break;
            case eShelfType.LIGHTS:
                mr.color = lightsCol;
                break;
            case eShelfType.WOOD:
                mr.color = woodCol;
                break;
            case eShelfType.PLANTS:
                mr.color = PlantsCol;
                break;
            case eShelfType.STORAGE:
                mr.color = StorageCol;
                break;
            default:
                break;
        }
    }


    public void AddShelfLocation(Transform location, eShelfType myType)
    {
        ShelfLocations.Add(myType, location);
    }

    public Transform GetShelfLocation(eShelfType myType)
    {
        if (!ShelfLocations.ContainsKey(myType))
            return null;
        return ShelfLocations[myType];
    }
    public void AddCash(int mon)
    {
        cash += mon;
        cashText.text = "Money:"+cash;
    }

}

