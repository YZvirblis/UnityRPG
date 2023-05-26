using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] float radius = 3f;
    private bool playerInRange;
    [SerializeField] SphereCollider spehereTrigger;

    private void Start()
    {
        spehereTrigger.radius = radius;
    }

    public virtual void Interact()
    {
        // This method is meant to be overwriten.
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                Interact();
            }
        }
    }
}
