using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour, ITrackableEventHandler
{

    public List<Transform> waypoints;
    private TrackableBehaviour m;
    bool showing = false;
    public Transform activeWaypoint;
    public Transform waypointsFolder;
    public NavMeshAgent agent;

    bool start = false;
    // Use this for initialization
    void Start()
    {
        start = true;

        agent = GetComponent<NavMeshAgent>();

        waypoints = new List<Transform>();

        for (int i = 0; i < waypointsFolder.childCount; ++i)
        {
            waypoints.Add(waypointsFolder.GetChild(i));
        }


        activeWaypoint = waypoints[0];
        agent.SetDestination(activeWaypoint.position);

        m = transform.parent.GetComponent<TrackableBehaviour>();
        m.RegisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (start == false)
            Start();
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            showing = true;
            if (agent.isStopped == true)
                agent.isStopped = false;
        }
        else
        {
            showing = false;
            agent.isStopped = true;
        }

    }

    void Update()
    {
        if (start == false)
            Start();
        if (showing == false)
        {
            return;
        }

        agent.SetDestination(activeWaypoint.position);
        if (Vector3.Distance(transform.position, activeWaypoint.position) <= 0.2)
        {
            activeWaypoint = waypoints[Random.Range(0, waypoints.Count - 1)];
        }
    }
}
