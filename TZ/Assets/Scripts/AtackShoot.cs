using System.Collections;
using UnityEngine;


namespace HorrorGame
{
    public class AtackShoot : MonoBehaviour
    {
        private GameObject bullet;
        private float BulletForce;
        private bool CanFire = true;
        private Animator m_Animator;
        private Vector3 point;
        
        public Data bulletData;

        IEnumerator Timer()
        {
            yield return new WaitForSeconds(0.5f);
            CanFire = true;

        }
        void Start()
        {
            m_Animator = GetComponent<Animator>();
            bullet = bulletData.Struct.bullet; 
            BulletForce = bulletData.Struct.bulletSpeed;
        }

        public void FireShoot()
        {


            if (Input.GetMouseButton(0))
            {


                if (CanFire)
                {
                    Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    
                    
                    RaycastHit raycastHit;
                    if (Physics.Raycast(myRay, out raycastHit, 200)) 
                    {
                        point = raycastHit.point;
                        Debug.DrawLine(transform.position + new Vector3(0, 1, 0), point, Color.green, 20.0f);
                    }

                    m_Animator.SetBool("Fire", true);
                    GameObject BulletInstance = Instantiate(bullet, GameObject.Find("BulletSpawnPoint").transform.position, Quaternion.identity);
                    BulletInstance.GetComponent<Rigidbody>().AddForce((point - (transform.position + new Vector3(0, 1, 0))).normalized * BulletForce);
                    Destroy(BulletInstance.gameObject, 5);
                    StartCoroutine("Timer");
                    CanFire = false;

                }
                else
                {
                    m_Animator.SetBool("Fire", false);
                }
            }

        }

    }
}
        
    

