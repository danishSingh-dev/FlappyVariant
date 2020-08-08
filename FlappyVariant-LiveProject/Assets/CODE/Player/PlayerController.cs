using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flappy.Management;


namespace Flappy.Player
{



    #region Component Overview
    /// <summary>
    /// [Component to control Player actions]
    /// </summary>
    #endregion

    public class PlayerController : MonoBehaviour
    {
        #region Public Variables
        [SerializeField] private float upVelocity = 1f;
        [SerializeField] private float gravityForce = 1.2f;
        [SerializeField] private bool b_jump = false;
        #endregion



        #region Private Variables
        private GameManager gameManager = null;
        private Rigidbody rb = null;
        #endregion



        #region MonoBehaviour Methods
        private void Start()
        {
            // cache the gameManager reference to the gameManager singleton
            gameManager = GameManager.GM_Instance;
            
            // set gravity to predefined value in the down direction
            // TODO find a workaround for this
            Physics.gravity = Vector3.down * gravityForce;


            // cache the rigidbody reference
            TryGetComponent(out rb);
        }


        private void Update()
        {
            GetInput();

        }

        private void FixedUpdate()
        {
            Jump();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == 9 || other.gameObject.layer == 8)
            {
                Debug.Log("Game Over");
            }
        }

        #endregion



        #region Custom Methods
        void GetInput()
        {
            // Checks if space is pressed on the keyboard and if jump boolean is false
            if(Input.GetKeyDown(KeyCode.Space) && !b_jump)
            {
                // set jump boolean to true
                b_jump = true;
            }

            // Checks if there is at least one touch input and if jump boolean is false
            if (Input.touchCount > 0 && !b_jump)
            {
                // If the first touch input has just started
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    // set jump boolean to true
                    b_jump = true;
                }
            }
        }


        void Jump()
        {
            // check if jump boolean is false
            if (!b_jump)
            {
                // return out of function
                return;
            }

            // else if jump boolean is true

            // set the rigidbody velocity in the up direction multiplied by the set upVelocity parameter
            rb.velocity = Vector3.up * upVelocity;
            // set jump boolean back to false
            b_jump = false;
        }
        #endregion
    }
}