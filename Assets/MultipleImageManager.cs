
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