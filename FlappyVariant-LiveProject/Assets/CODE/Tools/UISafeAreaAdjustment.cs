using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flappy.Tools
{
    #region Component Overview
    /// <summary>
    /// [Resizes the SafeArea panel so all UI elements fit on any screen]
    /// </summary>
    #endregion

    public class UISafeAreaAdjustment : MonoBehaviour
    {
        #region Public Variables
        [SerializeField] private Canvas canvas = null;
        #endregion



        #region Private Variables
        private RectTransform panelSafeArea = null;

        private Rect currentSafeArea = new Rect();
        private ScreenOrientation currentOrientation = ScreenOrientation.AutoRotation;
        #endregion



        #region MonoBehaviour Methods
        private void Start()
        {
            TryGetComponent<RectTransform>(out panelSafeArea);

            //Screen.orientation = ScreenOrientation.Landscape;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;

            currentSafeArea = Screen.safeArea;
            currentOrientation = Screen.orientation;

            ApplySafeArea();
        }


        private void Update()
        {
            if(currentOrientation != Screen.orientation || currentSafeArea != Screen.safeArea)
            {
                ApplySafeArea();
            }
        }

        private void FixedUpdate()
        {

        }
        #endregion



        #region Custom Methods

        void ApplySafeArea()
        {
            if(panelSafeArea == null) { return; }

            Rect safeArea = Screen.safeArea;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= canvas.pixelRect.width;
            anchorMin.y /= canvas.pixelRect.height;

            anchorMax.x /= canvas.pixelRect.width;
            anchorMax.y /= canvas.pixelRect.height;

            panelSafeArea.anchorMin = anchorMin;
            panelSafeArea.anchorMax = anchorMax;

            currentOrientation = Screen.orientation;
            currentSafeArea = Screen.safeArea;
        }

        #endregion
    }
}