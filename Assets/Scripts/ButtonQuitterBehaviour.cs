using UnityEngine;

public class ButtonQuitterBehaviour:MonoBehaviour
{
    public void OnclickQuitter()
    {
        Debug.Log("quitter");
        Application.Quit();
    }
}
