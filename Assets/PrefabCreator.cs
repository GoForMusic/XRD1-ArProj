using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject objPrefab;
    [SerializeField] private Vector3 prefabOffset;

    private GameObject gameObject;
    private ARTrackedImageManager arTrackedImageManager;

    private void OnEnable()
    {
        arTrackedImageManager = gameObject.GetComponent<ARTrackedImageManager>();

        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (ARTrackedImage image in obj.added)
        {
            gameObject = Instantiate(objPrefab, image.transform);
            gameObject.transform.position += prefabOffset;
        }
    }
}
