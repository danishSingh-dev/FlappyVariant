using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flappy.Management
{


    #region Component Overview
    /// <summary>
    /// [Updates any and all UI elements as needed]
    /// </summary>
    #endregion

    public class UIManager : MonoBehaviour
    {
        #region Public Variables
        [SerializeField] private Text scoreText = null;

        public static UIManager UI_Instance = null;
        #endregion



        #region Private Variables
        private int score = 0;
        private string scoreString = "";
        #endregion



        #region MonoBehaviour Methods
        private void Awake()
        {
            // Check if singleton has a reference
            if (UI_Instance != null)
            {
                // check if the reference is assigned to another gameObject
                if (UI_Instance != this)
                {
                    // if true, destroy this gameObject
                    Destroy(this);
                }
                // if reference is assigned to this object
                else if (UI_Instance == this)
                {
                    // return out of function
                    return;
                }
            }
            // if the reference isn't assigned
            else if (UI_Instance == null)
            {
                // set the reference to this gameObject
                UI_Instance = this;
            }
        }

        #endregion



        #region Custom Methods


        public void UpdateScore(int newScore)
        {
            score = newScore;
            scoreString = string.Format("{0:D4}", score);
            scoreText.text = "SCORE: " + scoreString;
        }
        #endregion
    }
}