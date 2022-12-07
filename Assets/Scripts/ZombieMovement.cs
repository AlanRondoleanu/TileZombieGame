using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    public GameObject leader;
    public bool partrolling = true;

    private NavMeshAgent agent;
    private ZombieScript zScript;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        zScript = GetComponent<ZombieScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leader != null && partrolling)
        {
            agent.SetDestination(leader.transform.position);
        }
    }

    public void setTarget(Vector3 t_target)
    {
        agent.SetDestination(t_target);
        partrolling = false;
        zScript.setChasing(false);
    }


}
