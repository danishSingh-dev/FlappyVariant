using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Player
{


    #region Component Overview
    /// <summary>
    /// [Sets which animation is playing depending on playerController]
    /// </summary>
    #endregion

    public class AnimationControl : MonoBehaviour
    {
        #region Public Variables
        //[SerializeField] private
        #endregion



        #region Private Variables
        private Animator animator = null;
        #endregion



        #region MonoBehaviour Methods
        private void Start()
        {
            TryGetComponent(out animator);
        }


        #endregion



        #region Custom Methods

        public void ActivateFireTrigger()
        {
            animator.SetTrigger("FireProjectile");
        }

        #endregion
    }
}