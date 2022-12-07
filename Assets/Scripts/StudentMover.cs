using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StudentMover : MonoBehaviour
{
    public SchoolManager school;

    private NavMeshAgent agent;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTarget(Vector3 t_target)
    {
        target = t_target;
        agent.SetDestination(t_target);
    }

}
