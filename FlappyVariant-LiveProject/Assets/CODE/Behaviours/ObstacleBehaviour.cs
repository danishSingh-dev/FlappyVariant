using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Behaviours
{



    #region Component Overview
    /// <summary>
    /// [Handles all obstacle related mechanics]
    /// </summary>
    #endregion

    [RequireComponent(typeof(Rigidbody))]
    public class ObstacleBehaviour : MonoBehaviour
    {
        #region Public Variables
        [SerializeField] private float movementSpeed = 5f;
        #endregion



        #region Private Variables
        private Rigidbody _rb = null;
        #endregion



        #region MonoBehaviour Methods

        private void Start()
        {
            TryGetComponent(out _rb);

            _rb.velocity = Vector3.left * movementSpeed;
        }



        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == 12)
            {
                gameObject.SetActive(false);
            }
        }
        #endregion



        #region Custom Methods


        #endregion
    }
}