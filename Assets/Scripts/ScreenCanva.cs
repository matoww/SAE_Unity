using UnityEngine;
using UnityEngine.UI;

public class ScreenCanva : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<CanvasScaler>().referenceResolution=new Vector2 (Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
