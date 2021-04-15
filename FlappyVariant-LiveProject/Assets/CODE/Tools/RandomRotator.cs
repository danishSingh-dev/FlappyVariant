using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Tools
{


    #region Component Overview
    /// <summary>
    /// [Adds random rotation to the object this component is placed on]
    /// </summary>
    #endregion
    [RequireComponent(typeof(Rigidbody))]
    public class RandomRotator : MonoBehaviour
    {
        #region Public Variables
        [Range(1f, 40f)]
        [SerializeField] private float rotationModifierValue = 1f;


        [Range(1, 3)]
        [SerializeField] private int numberOfAxis = 1;

        [SerializeField] private bool randomRotation = false;
        #endregion



        #region Private Variables
        private Vector3 axisOfRotation = Vector3.zero;
        private Rigidbody rb = null;
        #endregion



        #region MonoBehaviour Methods
        private void Start()
        {
            TryGetComponent(out rb);

            SetRigidbodyParameters();

            if (!randomRotation)
            {
                axisOfRotation = DetermineAxisOfRotation();
            }
            else
            {
                // TODO add randomizer
            }
        }


        private void Update()
        {
            AddRandomRotation();
        }

        #endregion



        #region Custom Methods
        void SetRigidbodyParameters()
        {
            rb.angularDrag = 0f;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }


        void AddRandomRotation()
        {
            if(rotationModifierValue < 1f) { return; }

            Vector3 rotationVelocity = Vector3.zero;
            rotationVelocity = axisOfRotation * rotationModifierValue;

            rb.angularVelocity = rotationVelocity;
        }

        Vector3 DetermineAxisOfRotation()
        {
            Vector3 axis = Vector3.zero;

            switch (numberOfAxis)
            {
                case 1:
                    axis = Vector3.right;
                    break;
                case 2:
                    axis = Vector3.up;
                    break;
                case 3:
                    axis = Vector3.forward;
                    break;
                default:
                    Debug.LogError("Some error occured in the rotator");
                    break;
            }

            return axis;
        }
        #endregion
    }
}