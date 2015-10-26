using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public Transform cameraTransform;

	void Update()
	{
		if (cameraTransform != null)
		{
			transform.position = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);
		}
	}
}