using UnityEngine;

namespace HorrorGame
{
    public class ShootPlite : MonoBehaviour
    {
        private bool fire = true;

        private void OnTriggerEnter(Collider collision)
        {


            if (collision.gameObject.CompareTag("Player"))
            {
                var player = collision.gameObject.GetComponent<PlayerControler>();
                player.Fire = fire;
            }
        }
    }
}
