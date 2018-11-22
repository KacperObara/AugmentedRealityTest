using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;

public class Sound : MonoBehaviour, ITrackableEventHandler
{
    //public VideoPlayer vidPlayer;
    private TrackableBehaviour m;
    bool showing = false;

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            showing = true;
        }
        else
        {
            showing = false;
        }

    }

    // Use this for initialization
    void Start () {
        m = GetComponent<TrackableBehaviour>();
        m.RegisterTrackableEventHandler(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (showing == true)
        {
            transform.GetChild(0).transform.Rotate(Vector3.down * Time.deltaTime * 100);
        }
    }
}
