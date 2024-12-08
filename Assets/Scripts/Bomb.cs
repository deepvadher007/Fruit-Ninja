using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().Explode();
            
        }
    }
}
  