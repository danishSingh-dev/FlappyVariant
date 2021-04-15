using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy.Tools
{


    #region Component Overview
    /// <summary>
    /// [Moves and resets the backgrounds as they pass a preset limit]
    /// </summary>
    #endregion

    public class MovingBackgrounds : MonoBehaviour
    {
        #region Public Variables
        [SerializeField] private List<Transform> backgroundCopies = new List<Transform>();
        [SerializeField] private List<Vector3> startingPositions = new List<Vector3>();

        [SerializeField] private float leftXLimit = 0f;
        [SerializeField] private float moveSpeed = 2f;

        #endregion



        #region Private Variables
        private Queue<GameObject> bgQueue = new Queue<GameObject>();
        #endregion



        #region MonoBehaviour Methods
        private void Start()
        {
            InitializeBackgrounds();

           
        }


        private void Update()
        {
            ApplyMovement();

            
        }

        private void FixedUpdate()
        {

        }
        #endregion



        #region Custom Methods
        void InitializeBackgrounds()
        {
            for (int i = 0; i < backgroundCopies.Count; i++)
            {
                Vector3 temp = backgroundCopies[i].localPosition;

                startingPositions.Add(temp);

                bgQueue.Enqueue(backgroundCopies[i].gameObject);
            }
        }


        void ApplyMovement()
        {
            Vector3 currentPosition = Vector3.zero;
            Vector3 offsetPosition = Vector3.zero;
            
            Vector3 translation = Vector3.left * moveSpeed * Time.deltaTime;

            foreach(var background in backgroundCopies)
            {
                currentPosition = background.localPosition;
                offsetPosition = currentPosition + translation;

                background.localPosition = offsetPosition;

                if(background.localPosition.x < leftXLimit)
                {
                    ResetPosition(bgQueue.Dequeue());
                }
                else
                {
                    currentPosition = Vector3.zero;
                    offsetPosition = Vector3.zero;
                    continue;
                }

            }
        }

        void ResetPosition(GameObject backgroundToReset)
        {
            Vector3 currentPosition = backgroundToReset.transform.localPosition;
            Vector3 newPosition = startingPositions[2];
            
            newPosition += (Vector3.left * moveSpeed * Time.deltaTime);

            backgroundToReset.transform.localPosition = newPosition;

            bgQueue.Enqueue(backgroundToReset);
        }

        #endregion
    }
}