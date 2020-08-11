using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Flappy.Management
{


    #region Component Overview
    /// <summary>
    /// [Manages sound effects in the game]
    /// </summary>
    #endregion

    public class BackgroundSoundManager : MonoBehaviour
    {
        #region Public Variables
        [SerializeField] private AudioSource bgAudio = null;

        [SerializeField] private float minVolume = 0.3f;
        [SerializeField] private float maxVolume = 1f;



        public static BackgroundSoundManager SM_Instance = null;
        #endregion



        #region Private Variables
        //private 
        #endregion



        #region MonoBehaviour Methods

        private void Awake()
        {
            // Check if singleton has a reference
            if (SM_Instance != null)
            {
                // check if the reference is assigned to another gameObject
                if (SM_Instance != this)
                {
                    // if true, destroy this gameObject
                    Destroy(this);
                }
                // if reference is assigned to this object
                else if (SM_Instance == this)
                {
                    // return out of function
                    return;
                }
            }
            // if the reference isn't assigned
            else if (SM_Instance == null)
            {
                // set the reference to this gameObject
                SM_Instance = this;
            }
            
            TryGetComponent(out bgAudio);
        }
        private void Start()
        {
        }


        private void Update()
        {

        }

        private void FixedUpdate()
        {

        }
        #endregion



        #region Custom Methods

        public void SetVolume(bool min)
        {
            if (min)
            {
                bgAudio.volume = minVolume;
            }
            else
            {
                bgAudio.volume = maxVolume;
            }
        }

        #endregion
    }
}