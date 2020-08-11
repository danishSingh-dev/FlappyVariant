using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Management
{


    #region Component Overview
    /// <summary>
    /// [Keeps track of player score]
    /// </summary>
    #endregion

    public class ScoreSystem : MonoBehaviour
    {
        #region Public Variables
        [SerializeField] private int scoreValue = 0;

        public static ScoreSystem SS_Instance = null;
        #endregion



        #region Private Variables
        private UIManager uiManager = null;
        #endregion



        #region MonoBehaviour Methods
        private void Awake()
        {
            // Check if singleton has a reference
            if (SS_Instance != null)
            {
                // check if the reference is assigned to another gameObject
                if (SS_Instance != this)
                {
                    // if true, destroy this gameObject
                    Destroy(this);
                }
                // if reference is assigned to this object
                else if (SS_Instance == this)
                {
                    // return out of function
                    return;
                }
            }
            // if the reference isn't assigned
            else if (SS_Instance == null)
            {
                // set the reference to this gameObject
                SS_Instance = this;
            }
        }

        private void Start()
        {
            uiManager = UIManager.UI_Instance;
        }


        #endregion



        #region Custom Methods

        public void AddToScore(int value)
        {
            scoreValue += value;

            uiManager.UpdateScore(scoreValue);
        }

        public void ResetScore()
        {
            scoreValue = 0;
            uiManager.UpdateScore(scoreValue);
        }

        #endregion
    }
}