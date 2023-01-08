using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SchoolManager : MonoBehaviour
{
    public static SchoolManager instance;

    // Text
    public TMP_Text gameOver;
    public TMP_Text win;
    public TMP_Text studentsCount;
    public TMP_Text zombiesCount;
    public GameObject button;

    public GameObject[] classes;
    public GameObject[] schoolAreas;
    public ClassGroup[] classGroups;
    public GameObject player;

    private List<GameObject> students = new List<GameObject>();
    private List<GameObject> zombies = new List<GameObject>();
    private int period = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        InvokeRepeating("updateClasses", 1f, 30f);
        InvokeRepeating("updateZombies", 1f, 3f);

        foreach (ClassGroup classGroup in classGroups)
        {
            students.AddRange(classGroup.getStudents());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;
        }
        checkForZombies();

        if (player == null)
        {
            gameOver.gameObject.SetActive(true);
        }

        studentsCount.text = "Students: " + students.Count;
        zombiesCount.text = "Zombies: " + zombies.Count;
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
            
            int currentClass = i;

            for (int y = 0; y < period; y++)
            {
                currentClass++;

                if (currentClass > 9)
                {
                    currentClass = 0;
                }
            }

            foreach (GameObject student in students)
            {
                int bathRoomChance = Random.Range(0, 100);
                int hungerChance = Random.Range(0, 100);

                if (bathRoomChance < 5)
                {
                    student.GetComponent<StudentMover>().setTarget(goToRoom(schoolAreas[0]));
                }
                else if (hungerChance < 10)
                {
                    student.GetComponent<StudentMover>().setTarget(goToRoom(schoolAreas[1]));
                }
                else
                {
                    student.GetComponent<StudentMover>().setTarget(goToRoom(classes[currentClass]));
                }
            }
        }

        period++;
        if (period > 9)
        {
            period = 0;
        }
    }

    void updateZombies()
    {
        zombies.Clear();
        students.Clear();
        zombies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        students.AddRange(GameObject.FindGameObjectsWithTag("Student"));

        if (zombies.Count <= 0)
        {
            win.gameObject.SetActive(true);
            button.gameObject.SetActive(true);
        }

        foreach (GameObject zombie in zombies)
        {
            if (zombie != null)
            {
                ZombieScript zScript = zombie.GetComponent<ZombieScript>();
                ZombieMovement zMover = zombie.GetComponent<ZombieMovement>();

                if (zScript.leader == true && zScript.target == null)
                {
                    zMover.setTarget(goToRoom(classes[Random.Range(0, classes.Length)]));
                }
                else if (zScript.leader == false)
                {
                    zMover.partrolling = true;
                }
            }
        }
    }

    public List<GameObject> allStudents()
    {
        return students;
    }

    public void removeStudent(GameObject t_student)
    {
        students.Remove(t_student);

        foreach (ClassGroup group in classGroups)
        {
            if (group.getStudents().Contains(t_student))
            {
                group.getStudents().Remove(t_student);
            }
        }

        Destroy(t_student);
    }

    void checkForZombies()
    {
        foreach (GameObject student in students)
        {
            foreach (GameObject zombie in zombies)
            {
                if (zombie != null)
                {
                    float distance = Vector2.Distance(student.transform.position, zombie.transform.position);

                    if (distance < 3)
                    {
                        student.GetComponent<StudentMover>().runAway();
                    }
                }
            }
        }
    }

    public void PassLeaderRole(GameObject t_zombieToRemove)
    {
        zombies.Remove(t_zombieToRemove);

        foreach (GameObject zombie in zombies)
        {
            if (zombie != null)
            {
                ZombieScript zs = zombie.GetComponent<ZombieScript>();

                if (zs.leader == false)
                {
                    zs.leader = true;
                    break;
                }
            }
        }
    }

    public void replay()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
