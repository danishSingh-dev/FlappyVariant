using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Management
{
    #region Component Overview
    /// <summary>
    /// [Keeps track of the game state, ie. isGameOver?]
    /// </summary>
    #endregion

    public class GameManager : MonoBehaviour
    {
        #region Public Variables
        // Boolean representing gameOver state
        [SerializeField] private bool b_isGameOver = false;

        // Singleton reference for GameManager
        public static GameManager GM_Instance = null;
        #endregion



        #region Private Variables
        private BackgroundSoundManager bgSound = null;
        private ScoreSystem scoreSystem = null;
        #endregion



        #region MonoBehaviour Methods


        private void OnEnable()
        {

            // Check if singleton has a reference
            if(GM_Instance != null)
            {
                // check if the reference is assigned to another gameObject
                if (GM_Instance != this)
                {
                    // if true, destroy this gameObject
                    Destroy(this);
                }
                // if reference is assigned to this object
                else if (GM_Instance == this)
                {
                    // return out of function
                    return;
                }
            }
            // if the reference isn't assigned
            else if(GM_Instance == null)
            {
                // set the reference to this gameObject
                GM_Instance = this;
            }
        }

        private void Start()
        {
            bgSound = BackgroundSoundManager.SM_Instance;
            scoreSystem = ScoreSystem.SS_Instance;
        }

        private void Update()
        {

            // if gameOver boolean is true
            if (b_isGameOver)
            {
                // check if TimeScale is not 0
                if(Time.timeScale != 0)
                {
                    // set TimeScale to 0
                    Time.timeScale = 0;

                    bgSound.SetVolume(true);

                    // TODO perform some gameOver action
                    Debug.Log("Game is Over");

                }

                // check for player input and if TimeScale is set to 0
                if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0)
                {
                    bgSound.SetVolume(false);
                    scoreSystem.ResetScore();

                    // reset gameOver boolean to false
                    b_isGameOver = false;

                    // reset TimeScale to 1
                    Time.timeScale = 1;
                }
            }
        }

        #endregion



        #region Custom Methods

        public void SetGameOver()
        {
            b_isGameOver = true;
        }

        #endregion
    }
}