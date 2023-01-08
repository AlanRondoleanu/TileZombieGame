using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    public GameObject leader;
    public bool partrolling = true;
    public GameObject body;

    private NavMeshAgent agent;
    private ZombieScript zScript;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        zScript = GetComponent<ZombieScript>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leader != null && partrolling && zScript.hit == false)
        {
            agent.SetDestination(leader.transform.position);
        }

        Vector3 moveDirection = agent.velocity;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            animator.SetInteger("AnimState", 1);
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }

        // Keep Child spites rotated correctly
        body.transform.rotation = Quaternion.identity;

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
        partrolling = false;
    }


}
