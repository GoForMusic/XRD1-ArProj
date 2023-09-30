using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [SerializeField] private GameObject[] placePrefabs;

    private Dictionary<string, GameObject> spawnPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager _trackedImageManager;

    private void Awake()
    {
        _trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach (var prefab in placePrefabs)
        {
            GameObject newPref = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPref.name = prefab.name;
            spawnPrefabs.Add(prefab.name,newPref);
            
        }
    }

    private void OnEnable()
    {
        _trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        _trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (var trackedImage in args.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (var trackedImage in args.removed)
        {
            spawnPrefabs[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage image)
    {
        string name = image.referenceImage.name;
        Vector3 position = image.transform.position;

        GameObject prefab = spawnPrefabs[name];
        prefab.transform.position = position;
        prefab.SetActive(true);

        foreach (var go in spawnPrefabs.Values)
        {
            if (go.name != name)
            {
                go.SetActive(false);
            }
        }
    }
    
}

