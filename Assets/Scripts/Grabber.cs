using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
	public GameObject room;
	public float power = 1f;
	
	private SteamVR_TrackedObject to;
	private SteamVR_TrackedController tc;

	private bool activated = false;

	public float transitionTime = 0.5f;
	private float transitionStartTime;
	private Vector3 transitionOrigin;
	private Vector3 transitionTarget;
	
	// Use this for initialization
	void Start ()
	{
		to = GetComponent<SteamVR_TrackedObject>();
		tc = GetComponent<SteamVR_TrackedController>();
		
		if(room == null)
			throw new Exception("Need a model to move around");
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit rh;
		if (tc.triggerPressed && !activated && Physics.Raycast(transform.position, transform.forward, out rh))
		{
			/*room.transform.position = rh.collider.gameObject.transform.position;/*/
			transitionOrigin = room.transform.position;
			transitionTarget = rh.collider.gameObject.transform.position;
			transitionStartTime = Time.time;//*/
			activated = true;
		}
		else if (!tc.triggerPressed && transitionStartTime + transitionTime < Time.time)
			activated = false;

		if (transitionStartTime + transitionTime >= Time.time)
			room.transform.position =
				Vector3.Lerp(transitionOrigin, transitionTarget, Mathf.Pow((Time.time - transitionStartTime) / transitionTime, power));
	}
}
