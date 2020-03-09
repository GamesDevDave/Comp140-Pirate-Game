using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent AI;
    public GameObject player;
    public Transform midPoint;
    public GameObject cannonball;
    public GameObject waypoint;

    RaycastHit hit;

    [Header("Conditioning")]
    [SerializeField] State _currentState;
    [SerializeField] float distanceFromPlayer;
    [SerializeField] bool canShoot = true;
    [SerializeField] float firingSpeed = 1000f;
    [SerializeField] float speed = 5f;



    void Start()
    {
        AI = GetComponent<NavMeshAgent>();
        midPoint = GetComponentInChildren<Transform>();

        _currentState = State.Idle;
    }

    void Update()
    {

        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (_currentState)
        {
            case State.Idle:
                {
                    AI.isStopped = true;

                    if (distanceFromPlayer < 50)
                    {
                        _currentState = State.Following;
                    }

                    break;
                }

            case State.Patrolling:
                {
                    
                    
                        
                    break;
                }

            case State.Following:
                {
                    AI.isStopped = false;

                    AI.SetDestination(player.transform.position);

                    if (distanceFromPlayer < 20)
                    {
                        _currentState = State.Circling;
                    }

                    break;
                }

            case State.Circling:
                {

                    Vector3 pos1 = new Vector3(20 , 0, 15f);
                    Vector3 pos2 = new Vector3(20 , 0, -15f);

                    waypoint.transform.localPosition = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));

                    AI.SetDestination(waypoint.transform.position);

                    transform.localRotation = player.transform.localRotation;

                    Shoot();

                    break;
                }

            case State.Dead:
                {
                    break;
                }
        }
    }

    public enum State
    {
        Patrolling,
        Following,
        Circling,
        Idle,
        Dead
    }

    IEnumerator shootingCooldown()
    {
        yield return new WaitForSeconds(4);
        canShoot = true;
    }

    private void Shoot()
    {
        if (Physics.Raycast(midPoint.position, midPoint.TransformDirection(Vector3.left), out hit) && hit.collider.gameObject.CompareTag("Player"))
        {
            Debug.DrawRay(midPoint.position, midPoint.TransformDirection(Vector3.left) * 20, Color.red);
            Debug.Log("Did Hit");
            if (canShoot)
            {
                GameObject cannonballClone;
                StartCoroutine(shootingCooldown());
                cannonballClone = Instantiate(cannonball, midPoint.transform.position, transform.rotation);
                cannonballClone.GetComponent<Rigidbody>().AddForce(-midPoint.transform.right * firingSpeed);
                Destroy(cannonballClone, 5f);
                canShoot = false;
            }
        }
    }
}
