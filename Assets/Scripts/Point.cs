using UnityEngine;
using TMPro;

    public class Point : MonoBehaviour
    {
        public TextMeshProUGUI txtBanane;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void setBanane(float banane)
        {
            txtBanane.text =  banane.ToString(); 
        }
    }