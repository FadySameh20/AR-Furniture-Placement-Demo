using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectingFurniture : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> furnitureList;

    [SerializeField]
    private List<Button> itemButtons;

    private GameObject scrollArea;

    // Start is called before the first frame update
    void Start()
    {
        scrollArea = gameObject;
        for (int i = 0; i < itemButtons.Count; i++)  // Setting listeners for buttons to be called when the button is pressed
        {
            int buttonIndex = i;
            itemButtons[buttonIndex].onClick.AddListener(delegate { OnChoosingItem(buttonIndex); });
        }

    }

    public void OnChoosingItem(int buttonIndex)  // On selecting an item
    {
        scrollArea.SetActive(false);
        FindObjectOfType<ARTapToPlaceObject>().setPlacedPrefab(furnitureList[buttonIndex]);
        FindObjectOfType<ARTapToPlaceObject>().AddItem();
    }
}
