
// Code reference: https://github.com/dilmerv/UnityARFoundationEssentials/blob/master/Assets/Scripts/TrackedImageInfoMultipleManager.cs
// The code was taken from the repo (link) above.
// The code from the referenced repository had some issues.
// As a result, certain parts have been rewritten.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoMultipleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject uiMenuObj;

    [SerializeField]
    private Button uiEnterButton;
    
    [SerializeField]
    private GameObject[] arObjectsToPlace;
    
    private GameObject activeArPrefab;

    [SerializeField]
    private Vector3 scale = new Vector3(0.1f,0.1f,0.1f);
    
    private ARTrackedImageManager _imageManager;
    
    private Vector2 previousDistance = Vector2.zero;
    
    private float previousRotation = 0f;

    private Dictionary<string, GameObject> arObjectList = new Dictionary<string, GameObject>();

    private void Awake()
    {
        uiEnterButton.onClick.AddListener(Dismiss);
        _imageManager = GetComponent<ARTrackedImageManager>();
        
        // setup all game objects in dictionary
        foreach(var arObject in arObjectsToPlace)
        {
            var newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newARObject.name = arObject.name;
            newARObject.SetActive(false);  // Ensure they start as inactive
            arObjectList.Add(arObject.name, newARObject);
        }
    }
    
    private void Update()
    {
        if (activeArPrefab && Input.touchCount == 2) 
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Calculate the current distance and rotation
            Vector2 currentDistance = touch1.position - touch0.position;
            float currentRotation = Vector2.SignedAngle(touch1.position - touch0.position, Vector2.up);

            if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved) 
            {
                // Pinch Gesture
                float prevMagnitude = previousDistance.magnitude;
                float currentMagnitude = currentDistance.magnitude;

                float difference = currentMagnitude - prevMagnitude;
                float scaleFactor = 1 + difference * 0.0025f; // F IS SENSITIVITY VALUE

                activeArPrefab.transform.localScale *= scaleFactor;

                // Rotation Gesture
                float rotationDifference = currentRotation - previousRotation;
                activeArPrefab.transform.Rotate(Vector3.up, -rotationDifference);
            }
            previousDistance = currentDistance;
            previousRotation = currentRotation;
        }
    }


    private void OnEnable()
    {
        _imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        _imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void Dismiss()
    {
        uiMenuObj.SetActive(false);
        uiEnterButton.gameObject.SetActive(false);
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
         
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateARImage(trackedImage);
        }
 
        foreach (var trackedImage in eventArgs.updated)
        {
            // If the updated image is currently being tracked, assign its prefab
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                UpdateARImage(trackedImage);
            }
        }
 
 
    }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);
    }

    private void AssignGameObject(string name, Vector3 newPosition)
    {
        //remove already active prefab before setting up a new one - required behavior
        if (activeArPrefab != null)
        {
            activeArPrefab.SetActive(false);
        }

        if (arObjectsToPlace == null) return;
        activeArPrefab = arObjectList[name];
        activeArPrefab.SetActive(true);
        activeArPrefab.transform.position = newPosition;
        activeArPrefab.transform.localScale = scale;
    }
}