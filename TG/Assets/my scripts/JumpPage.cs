using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TG
{
    public class JumpPage : MonoBehaviour
    {

        public Button login;

        // Use this for initialization
        void Start()
        {
            login = this.GetComponent<Button>();
            login.onClick.AddListener(OnLoginClick);

        }


        private void OnLoginClick()
        {
            SceneManager.LoadScene(1);
        }

    }
}
