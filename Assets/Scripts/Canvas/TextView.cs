using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextView : MonoBehaviour
{
    public GameObject canvas;
    public string text;
    public Text textCanvas;
    
    private void Start()
    {
        textCanvas.text = text;
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
    }
}
