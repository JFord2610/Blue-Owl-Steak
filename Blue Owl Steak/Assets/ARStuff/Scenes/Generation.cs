using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Generation : MonoBehaviour
{
    public Transform Player;
    public Transform StartPos;
    public NavMeshSurface[] surfaces;
    [Space(10)]
    public List<Transform> points = new List<Transform>();
    public List<Transform> startingpoints = new List<Transform>();
    public Transform startingnode;
    public List<Transform> Puzzlenodes = new List<Transform>();
    public List<Transform> Fillernodes = new List<Transform>();

    int num;
    Transform currentlyMoving;

    private void Awake()
    {
        GenerateWorld();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartPos = GameObject.Find("StartPos").transform;
        Player.position = StartPos.position;
        
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateWorld()
    {
        num = Random.Range(0, startingpoints.Count);
        startingnode.position = startingpoints[num].position;
        points.Remove(startingpoints[num]);

        for (int x = 0; x < Puzzlenodes.Count; x++)
        {
            num = Random.Range(0, points.Count);
            int rotmulti = Random.Range(0, 4);
            currentlyMoving = Puzzlenodes[x];
            Puzzlenodes[x].position = points[num].position;
            //Fillernodes[x].localRotation = Quaternion.Euler(0, 90 * rotmulti, 0);

            for (int y = 0; y < Puzzlenodes.Count; y++)
            {
                DistanceChek(Puzzlenodes[x].position, Puzzlenodes[y].position);
            }
            points.RemoveAt(num);
        }
        for (int i = 0; i < Fillernodes.Count; i++)
        {
            num = Random.Range(0, points.Count);
            int rotmulti = Random.Range(0, 4);
            Fillernodes[i].position = points[num].position;
            //Fillernodes[i].localRotation = Quaternion.Euler(0, 90 * rotmulti, 0);
            points.RemoveAt(num);
        }

    }

    void DistanceChek(Vector3 a, Vector3 b)
    {
        if (Vector3.Distance(a, b) <= 150 && a != b)
        {
            num = Random.Range(0, points.Count);
            print("no");
            currentlyMoving.position = points[num].position;
            DistanceChek(currentlyMoving.position, b);
        }
    }
}
