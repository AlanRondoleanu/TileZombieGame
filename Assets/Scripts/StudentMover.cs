using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StudentMover : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool panic = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = Random.Range(1.5f, 2.5f);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 moveDirection = agent.velocity;

        // Flip Image
        if (moveDirection.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -2f;

            transform.localScale = scale;
        }
        else if (moveDirection.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = +2f;

            transform.localScale = scale;
        }
    }

    public void setTarget(Vector3 t_target)
    {
        agent.SetDestination(t_target);
        animator.SetInteger("AnimState", 1);
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
