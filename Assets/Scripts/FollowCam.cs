using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	static public FollowCam S; 

	
	public float easing = 0.05f;
	public Vector2 minXY;
	public bool _______;
	
	public GameObject poi;
	public float camZ;


    void FixedUpdate()
    {
        Vector3 endGoal;
        if (poi == null)
        {
            endGoal = Vector3.zero;
        }
        else
        {

            endGoal = poi.transform.position;

            if (poi.tag == "Projectile")
            {

                if (poi.GetComponent<Rigidbody>().IsSleeping())
                {

                    poi = null;
                    Destroy(poi);
                    MissionDemolition.SwitchView("Both");

                    return;
                }
            }
        }

        endGoal.x = Mathf.Max(minXY.x, endGoal.x);
        endGoal.y = Mathf.Max(minXY.y, endGoal.y);
        endGoal = Vector3.Lerp(transform.position, endGoal, easing);

        endGoal.z = camZ;

        this.transform.position = endGoal;
        this.GetComponent<Camera>().orthographicSize = endGoal.y + 10;
    }


    void Awake() {
		S = this;
		camZ = this.transform.position.z;
	}
	
	
}
