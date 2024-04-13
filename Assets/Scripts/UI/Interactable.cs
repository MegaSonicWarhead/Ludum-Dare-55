using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactMessage = "Press E to interact"; // Message displayed when player can interact with the object

    public virtual void Interact()
    {
        // This method is meant to be overridden by child classes
        Debug.Log("Interacting with " + gameObject.name);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere around the interactable object for visualization in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
