using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTagBehaviour : MonoBehaviour
{
   [SerializeField] private bool _isTagged = false;

    [SerializeField] private ParticleSystem _taggedParticles;

    private bool _canBeTagged = true;

    public bool IsTagged { get { return _isTagged; } }

    public bool Tag()
    {
        //If cannot be tagged, return false.
        if(!_canBeTagged)
            return false;

        //Set that we're tagged.
        _isTagged = true;
        _canBeTagged = false;

        if (_taggedParticles != null)
            _taggedParticles.Play();

        //Turn our Trail renderer on.
        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (trail != null)
            trail.enabled = true;

        return true;
    }
    private void SetCanBeTagged()
    {
        _canBeTagged = true;
    }
    private void Start()
    {
        //Get my trail renderer
        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (trail == null)
            return;

        //If I am tagged, then turn trail on, otherwise turn off
        if (IsTagged)
            trail.enabled = true;
        else
            trail.enabled = false;

    }
    private void OnCollisionEnter(Collision collision)
    {
        //If we are not it, do nothing.
        if (!IsTagged)
            return;

        //Attempt to get the PlayerTagBehaviour from what we hit.
        PlayerTagBehaviour tagBehaviour = collision.gameObject.GetComponent<PlayerTagBehaviour>();

        //If it didn't have 1, return
        if (tagBehaviour == null)
            return;

        //Tag the other player.
        if (!tagBehaviour.Tag())
            return;

        //Set ourselves as not it.
        _isTagged = false;
        _canBeTagged = false;

        TrailRenderer trailRenderer = GetComponent<TrailRenderer>();
        if(trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        Invoke("SetCanBeTagged", 1f);
    }
}
