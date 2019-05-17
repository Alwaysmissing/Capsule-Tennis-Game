using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TG
{
    public class Player : MonoBehaviourPun
    {

        public Transform aimTarget; // the target where we aim to land the ball
        float speed = 10f; // move speed
        float force = 20; // 击打球的力量

        bool hitting; // boolean to know if we are hitting the ball or not 

        public Transform ball; // the ball 
        Animator animator;
        AudioSource audioPlay;//music
        Vector3 aimTargetInitialPosition; // initial position of the aiming gameObject which is the center of the opposite court

        //ShotManager shotManager; // reference to the shotmanager component
        //Shot currentShot; // the current shot we are playing to acces it's attributes

        private void Awake()
        {
            animator = GetComponent<Animator>(); // referennce out animator
            aimTargetInitialPosition = aimTarget.position; // initialise the aim position to the center
            audioPlay = GetComponent<AudioSource>();
            //destroy the player.cs if it is not controlled by me
            //if (!photonView.IsMine && transform.GetComponent<Player>() != null)
            //    Destroy(GetComponent<Player>());
        }
        private void Start()
        {
            //shotManager = GetComponent<ShotManager>(); // accesing our shot manager component 
            //currentShot = shotManager.topSpin; // defaulting our current shot as topspin
        }

        void Update()
        {
            float h = Input.GetAxisRaw("Horizontal"); // get the horizontal axis of the keyboard
            float v = Input.GetAxisRaw("Vertical"); // get the vertical axis of the keyboard
                                                    //Debug.Log(h);
                                                    //Debug.Log(v);
            if (Input.GetKeyDown(KeyCode.F))
            {
                hitting = true; // we are trying to hit the ball and aim where to make it land
                                //currentShot = shotManager.topSpin; // set our current shot to top spin
            }
            else if (Input.GetKeyUp(KeyCode.F))
            {
                hitting = false; // we let go of the key so we are not hitting anymore and this 
            }                    // is used to alternate between moving the aim target and ourself

            if (Input.GetKeyDown(KeyCode.E))
            {
                hitting = true; // we are trying to hit the ball and aim where to make it land
                                //currentShot = shotManager.flat; // set our current shot to top spin
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                hitting = false;
            }
            if (hitting)  // if we are trying to hit the ball
            {
                aimTarget.Translate(new Vector3(h, 0, 0) * speed * 2 * Time.deltaTime); //translate the aiming gameObject on the court horizontallly
            }

            //player move
            if ((h != 0 || v != 0) && !hitting)
            {

                transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); // move on the court
            }



        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball")) // if we collide with the ball 
            {
                Vector3 dir = aimTarget.position - transform.position; // get the direction to where we want to send the ball
                other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 12, 0);
                //add force to the ball plus some upward force according to the shot being played

                Vector3 ballDir = ball.position - transform.position; // get the direction of the ball compared to us to know if it is
                if (ballDir.x >= 0)                                   // on out right or left side 
                {
                    animator.Play("forehand");                        // play a forhand animation if the ball is on our right
                    audioPlay.Play();
                }
                else                                                  // otherwise play a backhand animation 
                {
                    animator.Play("backhand");
                    audioPlay.Play();
                }

                aimTarget.position = aimTargetInitialPosition; // reset the position of the aiming gameObject to it's original position

            }
        }
        //public static void RefreshInstance(ref Player player,Player Prefab)
        //{
        //    var position = Vector3.zero;
        //    var rotation = Quaternion.identity;
        //    if (player != null)
        //    {
        //        position = player.transform.position;
        //        rotation = player.transform.rotation;
        //        PhotonNetwork.Destroy(player.gameObject);
        //    }
        //    player = PhotonNetwork.Instantiate(Prefab.gameObject.name, position, rotation).GetComponent<Player>();
        //}
    }
}
