using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
    [SerializeField]
    private Camera arCamera;

    private GameObject placedPrefab;
    private Vector2 touchPosition = default;
    private ARRaycastManager arRaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private PlacementObject lastSelectedObject;
    private bool addItem = false;
    private bool hitCanvas = false;

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    public void setPlacedPrefab(GameObject placedFurniture)
    {
        placedPrefab = placedFurniture;  // Reference to the furniture that will be instantiated
    }

    void Update()
    {
        Touch touch;

        if (Input.touchCount > 0)  // On touching the screen
        {
            touch = Input.GetTouch(0);
            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);  // Generates ray to the touch position
                RaycastHit hitObject;

                if (Physics.Raycast(ray, out hitObject))  // If the ray intersects with a collider
                {
                    // If the ray hits a canvas, set "hitCanvas" to true so that the selected object doesn't move
                    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                    eventDataCurrentPosition.position = new Vector2(touchPosition.x, touchPosition.y);
                    List<RaycastResult> results = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
                    hitCanvas = results.Count > 0;

                    if(!hitCanvas)  // If it doesn't hit a canvas, then check whether it hits a placed furniture (lastSelectedObject refers to PlacementObject of the placed furniture), otherswise lastSelectedObject = null
                    {
                        lastSelectedObject = hitObject.transform.GetComponent<PlacementObject>();
                    }
                    
                    ControlCanvas();
                }
            }
        }


        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;  // Track hit position (where the furniture will be instantiated)

            if (lastSelectedObject == null)
            {
                if(addItem)  // If the user selected an item to place
                {
                    lastSelectedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation).GetComponent<PlacementObject>();
                    addItem = false;
                }
            }
            else
            {
                if (lastSelectedObject.Selected)  // Limits dragging to only when the furniture is selected
                {
                    touch = Input.GetTouch(0);
                    if(!hitCanvas)  // In order not to move the furniture while tapping on the canvas
                    {
                        if(!(touch.phase == TouchPhase.Ended))  // While the user is dragging, update the postion of the furniture
                        {
                            lastSelectedObject.transform.position = hitPose.position;
                            lastSelectedObject.transform.rotation = hitPose.rotation;
                        }
                    }
                }
            }
        }
    }

    public void AddItem()  // Called when the user selects an item to place
    {
        addItem = true;
    }

    void ControlCanvas()  // Enable or disable canvas based on the furniture being selected or not
    {
        PlacementObject[] allOtherObjects = FindObjectsOfType<PlacementObject>();
        foreach (PlacementObject placementObject in allOtherObjects)
        {
            placementObject.Selected = placementObject == lastSelectedObject;
            placementObject.ToggleCanvas();
            placementObject.ToggleRotationCanvas();
            placementObject.ToggleMaterialCanvas();
            placementObject.TogglePriceCanvas();
        }
    }

}