﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {

	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	public List<Transform> visibleTargets = new List<Transform>();
    public List<Transform> visibleDeathNpcs = new List<Transform>();

    bool enableFOV;

	void Start() {
		StartCoroutine ("FindTargetsWithDelay", .2f);
	}

    void Update()
    {
        if (enableFOV)
        {
            FindVisibleTargets();
        }
    }


    IEnumerator FindTargetsWithDelay(float delay) {
		yield return new WaitForSeconds (delay);
        enableFOV = true;
	}

	void FindVisibleTargets() {
		visibleTargets.Clear ();
        visibleDeathNpcs.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.position);

				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
                    if (!visibleTargets.Exists(o => o == target))
                    {
                        if (target.gameObject.layer == 8)
                        {
                            visibleTargets.Add(target);
                        }
                        else
                        {
                            visibleDeathNpcs.Add(target);
                        }
                    }
				}
			}
		}
	}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
