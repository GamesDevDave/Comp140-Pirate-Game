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

    RaycastHit hit;

    [Header("Conditioning")]
    [SerializeField] State _currentState;
    [SerializeField] float distanceFromPlayer;
    [SerializeField] float rotation;
    [SerializeField] Vector3 rotationAmount = new Vector3(0f, 360f, 0f);
    [SerializeField] bool canShoot = true;
    [SerializeField] float firingSpeed = 1000f;
    [SerializeField] float xSpread;
    [SerializeField] float zSpread;
    [SerializeField] float yOffset;
    [SerializeField] float rotSpeed;
    [SerializeField] float timer = 0f;


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
                    AI.SetDestination(player.transform.position);

                    if (distanceFromPlayer < 20)
                    {
                        _currentState = State.Circling;
                    }

                    break;
                }

            case State.Circling:
                {

                    timer += Time.deltaTime * rotSpeed;

                    transform.LookAt(player.transform.position, Vector3.left);
                    Rotate();

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

    void Rotate()
    {
        float x = -Mathf.Cos(timer) * xSpread;
        float z = Mathf.Sin(timer) * zSpread;
        Vector3 pos = new Vector3(x, yOffset, z);
        transform.position = pos + player.transform.position;
        transform.localRotation = Quaternion.Euler(Mathf.Clamp(0, 0, 0), transform.rotation.y, Mathf.Clamp(0, 0, 0));

        if (Physics.Raycast(midPoint.position, midPoint.TransformDirection(Vector3.right), out hit) && hit.collider.gameObject.CompareTag("Player"))
        {
            Debug.DrawRay(midPoint.position, midPoint.TransformDirection(Vector3.right) * 20, Color.red);
            Debug.Log("Did Hit");
            if (canShoot)
            {
                GameObject cannonballClone;
                StartCoroutine(shootingCooldown());
                cannonballClone = Instantiate(cannonball, midPoint.transform.position, transform.rotation);
                cannonballClone.GetComponent<Rigidbody>().AddForce(midPoint.transform.right * firingSpeed);
                Destroy(cannonballClone, 5f);
                canShoot = false;
            }
        }

        else
        {
            rotation = 200f * Time.deltaTime;
            transform.Rotate(0f, -rotation, 0f);
        }
    }

}
