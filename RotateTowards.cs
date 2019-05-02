using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour {

    Transform focusMarker;

	// Use this for initialization
	void Awake () {

        focusMarker = GetComponentInParent<EnemyBehav>().focusMarker;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 targetDir = focusMarker.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = 3.0f * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);

        transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);

    }
}
