using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
	static public Slingshot S;
	
	public GameObject prefabProjectile;
	public float velocityMult = 4f;
	public bool _________;
	public GameObject launchPoint;
	public Vector3 launchPos;
	public GameObject projectile;
	public bool aimingMode;


    void OnMouseEnter()
    {

        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {

        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {

        aimingMode = true;

        projectile = Instantiate(prefabProjectile) as GameObject;

        projectile.transform.position = launchPos;

    }


    void Update()
    {

        if (!aimingMode)
        {
            return;
        }

        Vector3 onScreenPositionMouse = Input.mousePosition;

        onScreenPositionMouse.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(onScreenPositionMouse);

        Vector3 changeInPositionMouse = mousePos3D - launchPos;

        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (changeInPositionMouse.magnitude > maxMagnitude)
        {
            changeInPositionMouse.Normalize();
            changeInPositionMouse *= maxMagnitude;
        }

        Vector3 positionBall = launchPos + changeInPositionMouse;
        projectile.transform.position = positionBall;

        if (Input.GetMouseButtonUp(0))
        {

            aimingMode = false;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.GetComponent<Rigidbody>().velocity = -changeInPositionMouse * velocityMult;
            FollowCam.S.poi = projectile;
            projectile = null;
            MissionDemolition.ShotFired();
        }
    }

    void Awake() {
		
		S = this;
		Transform changeInLaunch = transform.Find ("LaunchPoint");
		launchPoint = changeInLaunch.gameObject;
		launchPoint.SetActive (false);
		launchPos = changeInLaunch.position;
	}
	
	
	
	

}
