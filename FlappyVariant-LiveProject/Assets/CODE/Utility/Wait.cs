using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flappy.Utility
{


    #region Component Overview
    /// <summary>
    /// [Enter a short description of this component here]
    /// </summary>
    #endregion

    public class Wait : MonoBehaviour
    {
        #region Public Variables
        [SerializeField] private float waitTime = 10f;
        #endregion



        #region Private Variables
        //private 
        #endregion



        #region MonoBehaviour Methods
        private void Start()
        {
            StartCoroutine(WaitForIntro());
        }


        private void Update()
        {

        }

        private void FixedUpdate()
        {

        }
        #endregion



        #region Custom Methods
        IEnumerator WaitForIntro()
        {
            yield return new WaitForSeconds(waitTime);

            SceneManager.LoadScene(1);
        }
        #endregion
    }
}