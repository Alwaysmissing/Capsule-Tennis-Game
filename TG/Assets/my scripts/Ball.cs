using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TG
{
    public class Ball : MonoBehaviour
    {

        Vector3 initialPos;
        // Use this for initialization
        void Start()
        {
            initialPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Wall"))
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;//当球撞到墙，速度变成0
                transform.position = initialPos;//重置位置
            }
        }
    }
}
