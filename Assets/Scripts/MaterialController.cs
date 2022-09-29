using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialController : MonoBehaviour
{
    [SerializeField]
    private List<MaterialCategory> materialCategories;  // Available material categories for the selected furniture

    [SerializeField]
    Text chooseLabel;  // Label for the materialCategories dropdown

    [SerializeField]
    Text label;  // Label for the material dropdown

    [SerializeField]
    Text noCategoriesLabel;  // Label for the case in which there are categories available

    [SerializeField]
    Dropdown materialDropDown;

    private void Start()
    {
        var dropDown = transform.GetComponent<Dropdown>();  // Get reference to materialCategories dropdown
        if (materialCategories.Count == 0)  // If there are no available categories
        {
            noCategoriesLabel.gameObject.SetActive(true);
            dropDown.gameObject.SetActive(false);
            chooseLabel.gameObject.SetActive(false);
            label.gameObject.SetActive(false);
            materialDropDown.gameObject.SetActive(false);

        }
        else
        {
            noCategoriesLabel.gameObject.SetActive(false);      
            dropDown.gameObject.SetActive(true);
            chooseLabel.gameObject.SetActive(true);

            dropDown.options.Clear();
            for (int i = 0; i < materialCategories.Count; i++)  // Set materialCategories dropdown options
            {
                dropDown.options.Add(new Dropdown.OptionData() { text = materialCategories[i].categoryName });
            }

            dropDown.onValueChanged.AddListener(delegate { DropDownItemSelected(dropDown); });
        }
    }

    void DropDownItemSelected(Dropdown dropDown)  // On selecting a category, update the material dropdown
    {
        label.gameObject.SetActive(true);  
        label.text = materialCategories[dropDown.value].categoryName;
        materialDropDown.gameObject.SetActive(true);
        materialDropDown.GetComponent<MaterialSwapper>().SetDropDown(materialDropDown, materialCategories[dropDown.value].materials, dropDown.value);
    }

}
