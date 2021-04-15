using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flappy.Management;
using Flappy.Behaviours;


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
        [SerializeField] private float gravityForce = 1.2f;

        [Header("Jump Parameters")]
        [SerializeField] private float upVelocity = 1f;
        [SerializeField] private bool b_jump = false;

        [Header("Shoot Parameters")]
        [SerializeField] private bool b_shoot = false;

        #endregion



        #region Private Variables
        private GameManager gameManager = null;
        private Rigidbody rb = null;
        private AnimationControl animationControl = null;
        private ShootingBehaviour shooter = null;

        private SFXManager sfxManager = null;

        private Vector3 startPosition = Vector3.zero;
        #endregion



        #region MonoBehaviour Methods
        private void Start()
        {
            // cache the gameManager reference to the gameManager singleton
            gameManager = GameManager.GM_Instance;

            // cache the sfxManager reference to the SFXManager singleton
            sfxManager = SFXManager.SFX_Instance;
            
            // set gravity to predefined value in the down direction
            // TODO find a workaround for this
            Physics.gravity = Vector3.down * gravityForce;


            // cache the rigidbody reference
            TryGetComponent(out rb);

            // cache the shootingMechanic reference
            TryGetComponent(out shooter);

            // cache the animationControl reference
            animationControl = GetComponentInChildren<AnimationControl>();



            startPosition = transform.localPosition;
        }


        private void Update()
        {
            GetInput();

            UpdateZRotatation();
        }

        private void FixedUpdate()
        {
            Jump();
            Shoot();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == 9 || other.gameObject.layer == 8)
            {
                gameManager.SetGameOver();
                Debug.Log("Game Over");
            }
        }

        #endregion



        #region Custom Methods
        void GetInput()
        {
            // * KEYBOARD CONTROLS*
            // Checks if space is pressed on the keyboard and if jump boolean is false
            //if(Input.GetKeyDown(KeyCode.Space) && !b_jump)
            //{
            //    // set jump boolean to true
            //    b_jump = true;
            //}

            //if(Input.GetMouseButtonDown(0) && !b_shoot)
            //{
            //    b_shoot = true;
            //}


            // * TOUCH CONTROLS*
            // Checks if there is at least one touch input and if jump boolean is false
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if(Input.GetTouch(i).position.x < Screen.width / 2)
                    {
                        b_jump = true;
                    }
                    else if(Input.GetTouch(i).position.x > Screen.width / 2)
                    {
                        b_shoot = true;
                    }
                }

                //// If the first touch input has just started
                //if (Input.GetTouch(0).phase == TouchPhase.Began)
                //{
                //    // set jump boolean to true
                //    b_jump = true;
                //}
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

            // play jump sound effect
            sfxManager.PlayBounceSound();

            // set the rigidbody velocity in the up direction multiplied by the set upVelocity parameter
            rb.velocity = Vector3.up * upVelocity;
            // set jump boolean back to false
            b_jump = false;
        }

        void Shoot()
        {
            if (!b_shoot)
            {
                return;
            }
            b_shoot = false;

            animationControl.ActivateFireTrigger();
            shooter.TriggerShot();
            //animationControl.ResetFireTrigger();

            sfxManager.PlayFireSound();

        }

        void UpdateZRotatation()
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(Vector3.zero);
            
            if(rb.velocity.y > 0f)
            {
                targetRotation = Quaternion.Euler(0f, 0f, 20f);
            }
            else if(rb.velocity.y < 0f)
            {
                targetRotation = Quaternion.Euler(0f, 0f, -20f);
            }
            
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime);
        }


        public void ResetPlayer()
        {
            transform.localPosition = startPosition;
            transform.rotation = Quaternion.Euler(Vector3.zero);

            rb.velocity = Vector3.zero;


        }
        #endregion
    }
}