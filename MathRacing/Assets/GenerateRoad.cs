using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoad : MonoBehaviour
{
    public Transform follow;
    public GameObject road;
    public GameObject roadFinishLine;
    public Vector3 roadPos;
    public int raceLength = 4;

    private Queue<GameObject> roads;
    private int roadCount = 1;
    private RaceController rController;


    void Start()
    {
        roads = new Queue<GameObject>();
        AddRoad();
        AddRoad();

        rController = this.gameObject.GetComponent<RaceController>();
    }

    void AddRoad()
    {
        if (roadCount < raceLength)
        {
            roads.Enqueue(Instantiate(road, new Vector3(roadPos.x, roadPos.y, roadPos.z*roadCount), Quaternion.identity));
        }
        else if(roadCount == raceLength)
        {
            roads.Enqueue(Instantiate(roadFinishLine, new Vector3(roadPos.x, roadPos.y, roadPos.z*roadCount), Quaternion.identity));
        }
        else
        {
            rController.FinishRace();
            roads.Enqueue(Instantiate(road, new Vector3(roadPos.x, roadPos.y, roadPos.z*roadCount), Quaternion.identity));
        }
        roadCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (follow.transform.position.z > roadPos.z * (roadCount - 2))
        {
            AddRoad();
            Destroy(roads.Dequeue());
        }
    }
}
