using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class ProjectileLine : MonoBehaviour {
	static public ProjectileLine S; 

	
	public float minDist = 0.1f;
	public bool ____________;

	public LineRenderer line;
	private GameObject _poi;
	public List<Vector3> points;
	
	void Awake() {
		S = this; 
		line = GetComponent<LineRenderer>();
		
		line.enabled = false;
		
		points = new List<Vector3>();
	}

    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    public void AddPoint()
    {

        Vector3 lookForTransform = _poi.transform.position;
        if (points.Count > 0 && (lookForTransform - lastPoint).magnitude < minDist)
        {

            return;
        }
        if (points.Count == 0)
        {

            Vector3 startPosition = Slingshot.S.launchPoint.transform.position;
            Vector3 deltaStartPosition = lookForTransform - startPosition;

            points.Add(lookForTransform + deltaStartPosition);
            points.Add(lookForTransform);
            line.SetVertexCount(2);

            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);

            line.enabled = true;
        }
        else
        {
            points.Add(lookForTransform);
            line.SetVertexCount(points.Count);
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }


    public Vector3 lastPoint
    {
        get
        {
            if (points == null)
            {

                return (Vector3.zero);
            }
            return (points[points.Count - 1]);
        }
    }


    void FixedUpdate () {
		if (poi == null) {
			
			if (FollowCam.S.poi != null) {
				if (FollowCam.S.poi.tag == "Projectile") {
					poi = FollowCam.S.poi;
				} else {
					return;
				}
			} else {
				return;
			}	
		}
		
		AddPoint();
		if (poi.GetComponent<Rigidbody>().IsSleeping()) {
			
			poi = null;
		}
	}
	
	
	public GameObject poi {
		get {
			return ( _poi );
		}
		set {
			_poi = value;
			if (_poi != null) {
				
				line.enabled = false;
				points = new List<Vector3>();
				AddPoint();
			}
		}
	}
	
	
	
}
