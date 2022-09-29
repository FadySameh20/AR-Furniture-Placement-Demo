using UnityEngine;
using UnityEngine.UI;

public class PlacementObject : MonoBehaviour
{
    [SerializeField]
    private bool IsSelected;

    public bool Selected  // Tracks if the furniture is selected by the user or not
    {
        get
        {
            return this.IsSelected;
        }
        set
        {
            IsSelected = value;
        }
    }

    [SerializeField]
    private Canvas canvasComponent;

    [SerializeField]
    private Canvas rotationCanvas;

    [SerializeField]
    private Canvas materialCanvas;

    [SerializeField]
    private Canvas priceCanvas;

    [SerializeField]
    private Furniture furniture;

    [SerializeField]
    private Text priceText;

    private bool isRotating = false;
    private bool isChangingMaterial = false;


    void Awake()
    {
        priceText.text = "EGP " + furniture.price.ToString("0");
        FindObjectOfType<TotalPriceCalculation>().AddToPriceList(furniture.price);  // On instantiating the furniture, add its price to the total price
    }

    public void ToggleCanvas()
    {
        canvasComponent?.gameObject.SetActive(IsSelected);
    }


    public void ToggleRotationCanvas()
    {
        if (IsSelected)
        {
            rotationCanvas?.gameObject.SetActive(isRotating);  // Enable or disable rotationCanvas according to the "Rotation" button
        }
        else  // Disable canvas
        {
            isRotating = false;
            rotationCanvas?.gameObject.SetActive(false);
        }
    }

    public void ChangeRotationStatus()  // Called on pressing "Rotation" button to toggle the rotationCanvas
    {
        isRotating = !isRotating;
        ToggleRotationCanvas();
    }

    public void ToggleMaterialCanvas()
    {
        if (IsSelected)  // Enable or disable rotationCanvas according to the "Rotation" button
        {
            materialCanvas?.gameObject.SetActive(isChangingMaterial);
        }
        else  // Disable canvas
        {
            isChangingMaterial = false;
            materialCanvas?.gameObject.SetActive(false);
        }
    }

    public void ChangeMaterial()  // Called on pressing "Material" button to toggle the rotationCanvas
    {
        isChangingMaterial = !isChangingMaterial;
        ToggleMaterialCanvas();
    }

    public void TogglePriceCanvas()  // Enable or disable priceCanvas according to the furniture being selected or not
    {
        priceCanvas?.gameObject.SetActive(IsSelected);
    }

    public void DeleteItem()  // Called on pressing "Delete" button to delete furniture
    {
        Destroy(gameObject);
        FindObjectOfType<TotalPriceCalculation>().RemoveFromPriceList(furniture.price);
    }

}