using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TG
{
    public class Exit : MonoBehaviour
    {

        public Button ext;
        // Use this for initialization
        void Start()
        {
            ext = this.GetComponent<Button>();
            ext.onClick.AddListener(OnClick);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnClick()
        {
            Application.Quit();
        }
    }
}
