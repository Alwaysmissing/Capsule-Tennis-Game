using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TG
{
    public class JumpPage2 : MonoBehaviour
    {

        public Button regist;
        // Use this for initialization
        void Start()
        {
            regist = this.GetComponent<Button>();
            regist.onClick.AddListener(OnRegistClick);
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnRegistClick()
        {
            SceneManager.LoadScene(2);
        }
    }
}
