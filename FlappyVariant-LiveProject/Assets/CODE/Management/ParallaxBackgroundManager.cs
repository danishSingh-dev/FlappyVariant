using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Management
{


    #region Component Overview
    /// <summary>
    /// [Implements a parallax background effect]
    /// </summary>
    #endregion

    public class ParallaxBackgroundManager : MonoBehaviour
    {
        #region Public Variables
        [SerializeField] private List<Transform> parallaxBackgrounds = new List<Transform>();
        [SerializeField] private float[] parallaxScales;
        [SerializeField] private float smoothing = 1f;
        #endregion



        #region Private Variables
        #endregion



        #region MonoBehaviour Methods
        private void Start()
        {
            parallaxScales = new float[parallaxBackgrounds.Count];

            for (int i = 0; i < parallaxBackgrounds.Count; i++)
            {
                parallaxScales[i] = parallaxBackgrounds[i].position.z * -1;
            }
        }


        private void Update()
        {
            ParallaxMovement();
        }

        private void FixedUpdate()
        {

        }
        #endregion



        #region Custom Methods

        void ParallaxMovement()
        {
            for (int i = 0; i < parallaxBackgrounds.Count; i++)
            {
                float parallax = Time.deltaTime * parallaxScales[i];

                float targetXPosition = parallaxBackgrounds[i].localPosition.x + parallax;

                Vector3 backgroundTargetPosition = new Vector3(targetXPosition, parallaxBackgrounds[i].localPosition.y, parallaxBackgrounds[i].localPosition.z);

                parallaxBackgrounds[i].transform.localPosition = Vector3.Lerp(parallaxBackgrounds[i].localPosition, backgroundTargetPosition, smoothing * Time.deltaTime);
            }
        }

        #endregion
    }
}