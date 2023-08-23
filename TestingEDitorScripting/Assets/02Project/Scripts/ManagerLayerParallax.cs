using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLayerParallax : MonoBehaviour {

    #region Inspector
    [Header("Dependencies")]
    public GameObject targetCamera;
    [Header("Parameters")]
	public Vector2 moveSpeed;

	#endregion

	private Vector2 lastPosition;

	void Start () {
		lastPosition = targetCamera.transform.position;
	}

	void FixedUpdate () {

		Vector2 newPosition = targetCamera.transform.position;

		gameObject.transform.position = new Vector3(
			gameObject.transform.position.x - ((newPosition.x - lastPosition.x)*(moveSpeed.x * Time.deltaTime)),
			gameObject.transform.position.y - ((newPosition.y - lastPosition.y)*(moveSpeed.y  * Time.deltaTime)),
			gameObject.transform.position.z
		);

		lastPosition = newPosition;
	}
}
