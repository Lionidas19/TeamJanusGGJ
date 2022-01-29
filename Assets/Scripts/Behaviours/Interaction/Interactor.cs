using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public ControlScheme controls;
    [SerializeField] private float interactableRadius;

    private Interactable _nearestInteractable;

    private void Awake()
    {
        controls = new ControlScheme();
        controls.Player.Interact.performed += ctx => Test();
    }

    private void Start()
    {
        Debug.Log("Blargh");
        StartCoroutine(UpdateNearestInteractable());
    }

    public void Test()
    {
        Debug.Log("Test!");
    }

    // THIS IN TUT WAS PRIVATE, maybe we should have the player controller launch an event for interact, and this signs on instead of calling directly?
    public void OnFire()
    {
        Debug.Log("Firing interaction");
        if(_nearestInteractable == null) return;
        Debug.Log("Something interactable was near");
        _nearestInteractable.Interact();
        _nearestInteractable = null;
    }

    private float DistanceFrom(Component interactable)
    {
        return Vector3.Distance(gameObject.transform.position, interactable.gameObject.transform.position);
    }

    private IEnumerator UpdateNearestInteractable()
    {
        // Making this coroutine run every 0.3f seconds, maybe there is a better / easier way to handle this, this was just in one tutorial, ill figure it out, 
        // maybe just an interact collider range and we sort objects into an array of interactables when they enter that like in IOW, TODO(Aria): Fix this (if needed);
        // Honestly this probably isnt that horrible performance wise, not that the game needs huge performance really...
        // This is honestly kind of a smart way of doing repeating events like in Unreal that I wanted but we can just add a break or something for it. 
        // Actually might make a util timer class for that..., or just a set of functions, it would have to be a monobehaviour...
        for (;;)
        {
            _nearestInteractable = null;
            var colliders = new Collider[20];
            Physics.OverlapSphereNonAlloc(transform.position, interactableRadius, colliders, LayerMask.GetMask("Interactable"));

            foreach(var c in colliders)
            {
                if (c == null) continue;
                if (!c.TryGetComponent<Interactable>(out var interactable)) continue;
                if(_nearestInteractable == null || DistanceFrom(interactable) < DistanceFrom(_nearestInteractable))
                {
                    _nearestInteractable = interactable;
                }
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
}
