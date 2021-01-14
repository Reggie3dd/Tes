using UnityEngine;


public class CollisionBullet : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Enemy")) 
        {
            GameObject.Find("Enemy").GetComponent<Animator>().enabled = false;
        }
    }
    
}
