using TMPro;
using UnityEngine;

public class TextView : MonoBehaviour
{
    public GameObject canvas;
    public string text;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == this.transform)
                {
                    ShowCanvas();
                }
            }
        }
    }

    private void ShowCanvas()
    {
        canvas.SetActive(true);
        canvas.GetComponentInChildren<TextMeshPro>().text = text;
    }
}
