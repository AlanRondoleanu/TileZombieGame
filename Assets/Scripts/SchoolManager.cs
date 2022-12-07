using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolManager : MonoBehaviour
{
    public GameObject[] classes;
    public ClassGroup[] classGroups;

    private List<GameObject> students = new List<GameObject>();
    private int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("clock", 1f, 60f);

        //foreach (Transform student in schoolBoard.transform)
        //{
        //    students.Add(student.gameObject);
        //}
        //Debug.Log(students.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;

            //students[0].GetComponent<StudentMover>().setTarget(goToRoom(classes[0]));
        }
    }

    void clock()
    {
        timer++;
        if (timer > 10)
        {
            timer = 1;
        }

        updateClasses();
    }

    public Vector3 goToRoom(GameObject t_room)
    {
        Bounds bounds = t_room.GetComponent<Collider2D>().bounds;

        return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z));
    }

    void updateClasses()
    {
        for (int i = 0; i < classGroups.Length; i++)
        {
            List<GameObject> students = classGroups[i].getStudents();
            int period = 0;

            period = timer + i;
            if (period > 9)
            {
                period = 0;
            }

            Debug.Log("Class " + (i + 1) + " to Period " + period);

            foreach (GameObject student in students)
            {
                student.GetComponent<StudentMover>().setTarget(goToRoom(classes[period].gameObject));
            }
        }
    }
}
