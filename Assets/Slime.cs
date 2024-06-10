using UnityEngine;

public class Slime : MonoBehaviour
{
    // This method is called when the slime enters a trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is an environment obstacle
        if (other.CompareTag("Environment"))
        {
            // Handle the collision with the environment obstacle
            Debug.Log("Slime collided with environment obstacle!");

            // Here, you can implement logic to react to the collision,
            // such as changing movement behavior or taking damage.
        }
    }
}
