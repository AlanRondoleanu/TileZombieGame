using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StudentMover : MonoBehaviour
{
    public SchoolManager school;

    private NavMeshAgent agent;
    private bool panic = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = Random.Range(1.5f, 2.5f);
    }

    public void setTarget(Vector3 t_target)
    {
        agent.SetDestination(t_target);
    }

    public void runAway()
    {
        if (panic == false)
        {
            agent.speed = Random.Range(3.0f, 4.0f);
            panic = true;
        }
    }

}
