using UnityEngine;
using UnityEngine.EventSystems;

public class RotateController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject furniture;
    private bool isRotating = false;
    private float rotateSpeed = 50;
    private PlacementObject[] placementObjects;

    void Start()
    {
        placementObjects = FindObjectsOfType<PlacementObject>();
        foreach (PlacementObject placementObject in placementObjects)
        {
            if (placementObject.Selected)  // Get reference to the selected object among all other objects
            {
                furniture = placementObject.gameObject.transform.GetChild(0).gameObject;
                return;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)  // While pressing down the rotate arrow button
    {
        isRotating = true;
    }

    public void OnPointerUp(PointerEventData eventData)  // On leaving the rotate arrow button
    {
        isRotating = false;
    }

    void Update()
    {
        if (isRotating)
        {
            if(gameObject == GameObject.FindGameObjectWithTag("Clockwise"))  // Control the direction of rotation
            {
                furniture.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
            }
            else
            {
                furniture.transform.Rotate(Vector3.down, rotateSpeed * Time.deltaTime);
            } 
        }
    }

}
