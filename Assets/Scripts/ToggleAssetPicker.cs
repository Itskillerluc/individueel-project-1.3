using System;
using TMPro;
using UnityEngine;

public class ToggleAssetPicker : MonoBehaviour
{
    private PropCategory _selectedPropCategory;

    public GameObject room;
    public GameObject wc;
    public GameObject bathroom;
    public GameObject kitchen;
    public GameObject garage;
    public GameObject floors;
    public GameObject walls;
    public TextMeshProUGUI switchText;

    public void TogglePicker()
    {
        room.SetActive(false);
        wc.SetActive(false);
        bathroom.SetActive(false);
        kitchen.SetActive(false);
        garage.SetActive(false);
        floors.SetActive(false);
        walls.SetActive(false);

        _selectedPropCategory = (PropCategory) ((int) ++_selectedPropCategory % 7);
        
        switch (_selectedPropCategory)
        {
            case PropCategory.Room:
                room.SetActive(true);
                break;
            case PropCategory.Wc:
                wc.SetActive(true);
                break;
            case PropCategory.Bathroom:
                bathroom.SetActive(true);
                break;
            case PropCategory.Kitchen:
                kitchen.SetActive(true);
                break;
            case PropCategory.Garage:
                garage.SetActive(true);
                break;
            case PropCategory.Floors:
                floors.SetActive(true);
                break;
            case PropCategory.Walls:
                walls.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        switchText.text = ((PropCategory)(((int)_selectedPropCategory + 1) % 7)).ToString();
    }

    private enum PropCategory
    {
        Room,
        Wc,
        Bathroom,
        Kitchen,
        Garage,
        Floors,
        Walls
    }
}