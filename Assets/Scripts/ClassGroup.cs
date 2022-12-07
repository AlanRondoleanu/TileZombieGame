using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassGroup : MonoBehaviour
{
    private List<GameObject> students = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform student in transform)
        {
            students.Add(student.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<GameObject> getStudents()
    {
        return students;
    }
}