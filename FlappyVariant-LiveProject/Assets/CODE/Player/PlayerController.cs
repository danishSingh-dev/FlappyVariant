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
            gameManager = GameManager.GM_Instance;
            
            // TODO find a workaround for this
            Physics.gravity = Vector3.down * gravityForce;

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
        #endregion



        #region Custom Methods
        void GetInput()
        {
            if(Input.GetKeyDown(KeyCode.Space) && !b_jump)
            {
                b_jump = true;
            }
        }


        void Jump()
        {
            if (!b_jump)
            {
                return;
            }

            rb.velocity = Vector3.up * upVelocity;
            b_jump = false;
        }
        #endregion
    }
}