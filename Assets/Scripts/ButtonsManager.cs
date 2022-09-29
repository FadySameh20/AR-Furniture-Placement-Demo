using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject categoriesScrollArea;

    [SerializeField]
    private List<GameObject> furnitureScrollAreas;

    [SerializeField]
    private List<Button> categoriesButtons;


    private void Start()
    {
        for(int i = 0; i < categoriesButtons.Count; i++)  // Setting listeners for buttons to be called when the button is pressed
        {
            int buttonIndex = i;
            categoriesButtons[buttonIndex].onClick.AddListener(delegate { OnChoosingCategory(buttonIndex); });
        }   
    }

    public void showCategoriesScrollBar()  // Called on pressing "Add" button
    {
        categoriesScrollArea.SetActive(true);
    }

    public void OnChoosingCategory(int buttonIndex)  // Called on choosing a category
    {
        categoriesScrollArea.SetActive(false);
        furnitureScrollAreas[buttonIndex].SetActive(true);  // Enable the ScrollArea of the chosen furniture category
    }

}
