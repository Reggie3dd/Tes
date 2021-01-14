using UnityEngine;
using UnityEngine.AI;


namespace HorrorGame
{
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
    public class PlayerControler : MonoBehaviour
    {


        public LayerMask whatCanBeClick;
        public NavMeshAgent myAgent;

        private Animator m_Animator;

        public bool Fire = false;
        public Data playerData;

        Vector2 smoothDeltaPosition = Vector2.zero;
        Vector2 velocity = Vector2.zero;

        void Start()
        {
            myAgent = GetComponent<NavMeshAgent>();
            m_Animator = GetComponent<Animator>();
            myAgent.updatePosition = false;
            GetComponent<NavMeshAgent>().speed = playerData.Struct.playerSpeed;
        }
        public void Move()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;

                if (Physics.Raycast(myRay, out raycastHit, 100, whatCanBeClick))
                {
                    myAgent.SetDestination(raycastHit.point);
                }
            }

            Vector3 worldDeltaPosition = myAgent.nextPosition - transform.position;


            float dx = Vector3.Dot(transform.right, worldDeltaPosition);
            float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
            Vector2 deltaPosition = new Vector2(dx, dy);


            float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
            smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);


            if (Time.deltaTime > 1e-5f)
                velocity = smoothDeltaPosition / Time.deltaTime;

            bool shouldMove = velocity.magnitude > 0.5f && myAgent.remainingDistance > myAgent.radius;

            m_Animator.SetBool("Run", shouldMove);


        }

        private void Rotating()
        {
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0;

            if (playerPlane.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                gameObject.transform.LookAt(targetPoint);
            }
            m_Animator.SetBool("Run", false);
        }

        private void FixedUpdate()
        {
            if (Fire)
            {
                Rotating();
                GetComponent<AtackShoot>().FireShoot();
            }
        }

        void Update()
        {

            if (!Fire)
            {
                Move();
            }

        }

        void OnAnimatorMove()
        {

            transform.position = myAgent.nextPosition;
        }
    }
}
