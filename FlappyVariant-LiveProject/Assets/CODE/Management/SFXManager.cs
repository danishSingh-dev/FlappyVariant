using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

#region Component Overview
/// <summary>
/// [Enter a short description of this component here]
/// </summary>
#endregion

public class SFXManager : MonoBehaviour
{
    #region Public Variables
    [SerializeField] private AudioSource bounceSFX = null;
    [SerializeField] private AudioSource impactSFX = null;
    [SerializeField] private AudioSource fireSFX = null;

    public static SFXManager SFX_Instance = null;
    #endregion



    #region Private Variables
    //private 
    #endregion



    #region MonoBehaviour Methods

    private void Awake()
    {
        // Check if singleton has a reference
        if (SFX_Instance != null)
        {
            // check if the reference is assigned to another gameObject
            if (SFX_Instance != this)
            {
                // if true, destroy this gameObject
                Destroy(this);
            }
            // if reference is assigned to this object
            else if (SFX_Instance == this)
            {
                // return out of function
                return;
            }
        }
        // if the reference isn't assigned
        else if (SFX_Instance == null)
        {
            // set the reference to this gameObject
            SFX_Instance = this;
        }
    }

    #endregion



    #region Custom Methods

    public void PlayBounceSound()
    {
        bounceSFX.Play();
    }

    public void PlayImpactSound()
    {
        impactSFX.Play();
    }

    public void PlayFireSound()
    {
        fireSFX.Play();
    }
    #endregion
}
