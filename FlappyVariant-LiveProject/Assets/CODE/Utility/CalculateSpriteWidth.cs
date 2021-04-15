using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Component Overview
/// <summary>
/// [Enter a short description of this component here]
/// </summary>
#endregion

public class CalculateSpriteWidth : MonoBehaviour
{
    #region Public Variables
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    #endregion



    #region Private Variables
    //private 
    #endregion



    #region MonoBehaviour Methods
    private void Start()
    {
        TryGetComponent(out spriteRenderer);

        Debug.Log(spriteRenderer.size.x);
    }

        
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
    #endregion



    #region Custom Methods

    #endregion
}
