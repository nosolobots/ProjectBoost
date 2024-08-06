using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                Debug.Log("You've finished the level!");
                break;
            case "Fuel":
                Debug.Log("You've refueled!");
                break;                
            default:
                Debug.Log("This thing is not friendly");
                break;
        }
    }
}
