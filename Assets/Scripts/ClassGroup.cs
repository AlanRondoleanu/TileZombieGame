using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassGroup : MonoBehaviour
{
    private List<GameObject> students = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform student in transform)
        {
            students.Add(student.gameObject);
        }
    }

    public List<GameObject> getStudents()
    {
        return students;
    }

    public void removeStudent(GameObject t_student)
    {
        students.Remove(t_student);
    }
}
