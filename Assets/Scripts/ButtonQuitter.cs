using UnityEngine;

public class ButtonQuitter:MonoBehaviour
{
    public void OnclickQuitter()
    {
        Debug.Log("quitter");
        Application.Quit();
    }
}
