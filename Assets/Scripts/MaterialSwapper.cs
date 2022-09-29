using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSwapper : MonoBehaviour
{
    private Dropdown dropDown;  // Chosen material dropdown
    private Renderer rend;
    private Material[] tempMaterials;
    private List<Material> availableMaterials = new List<Material>();  // Available materials that the use can choose for that furniture
    private int index;
    private bool isListenerSet = false;


    private void Awake()
    {
        rend = transform.root.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();  // Get reference to the Renderer of the furniture to check its materials
    }

    public void SetDropDown(Dropdown materialDropDown, List<Material> materials, int categoryIndex)
    {
        if(!isListenerSet)  // In order to add listener to dropdown options once (at the beginning) and set reference to the dropdown
        {
            isListenerSet = true;
            dropDown = materialDropDown;
            dropDown.onValueChanged.AddListener(delegate { DropDownItemSelected(); });
        }

        index = categoryIndex;  // Index of chosen category from the category dropdown
        availableMaterials.Clear();
        dropDown.options.Clear();

        int tempIndex = 0;
        for (int i = 0; i < materials.Count; i++)
        {
            string materialName = materials[i].name + " (Instance)";
            availableMaterials.Add(materials[i]);
            dropDown.options.Add(new Dropdown.OptionData() { text = materials[i].name });  // Add material options
            
            if (materialName == rend.materials[categoryIndex].name)
            {
                tempIndex = i;  // Store the index of current chosen material to be shown in the dropdown as the currently selected material for the furniture
            }
        }
        dropDown.value = tempIndex;
        dropDown.RefreshShownValue();
    }


    void DropDownItemSelected()  // Updating materials on selecting a dropdown option
    {
        tempMaterials = rend.materials;  // Should make a copy of renderer materials first, change it and then assign it back to the renderer materials
        tempMaterials[index] = availableMaterials[dropDown.value];
        rend.materials = tempMaterials;
    }

}
