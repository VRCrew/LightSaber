using UnityEngine;
using System.Collections;

public class Probe : MonoBehaviour 
{
	public Transform rotationGO;
	public Transform distanceGO;
	public Transform probeGO;

	private Vector3 movePosition;
	private Vector3 origRotation;
	private Vector3 targetRotation;

	public float moveSpeed = 1f;
	public float rotateSpeed = 1f;
	public float distance = 2f;
	public float timeToMoveRnd = 2f;

	public TimeTrigger timeTrigger;


	void Start () 
	{
		timeTrigger = gameObject.AddComponent<TimeTrigger>();

		timeTrigger.Register(Random.Range(0f, 3f) + Time.time, Rotate);
		timeTrigger.Register(Random.Range(0f, timeToMoveRnd) + Time.time, Move);
	}


	private void Rotate()
	{
		timeTrigger.Register(Random.Range(0f, 3f) + Time.time, Rotate);

		origRotation = probeGO.transform.eulerAngles;
		targetRotation = new Vector3 (0f, Random.Range (0f, 360f), 0f);
	}


	void Update () 
	{
		rotationGO.eulerAngles = Vector3.Lerp (rotationGO.eulerAngles, new Vector3 (0, movePosition.x, 0), rotateSpeed * Time.deltaTime);
		distanceGO.localPosition = Vector3.Lerp (distanceGO.localPosition, new Vector3 (distance, movePosition.y, 0), moveSpeed * Time.deltaTime);
		probeGO.eulerAngles = Vector3.Lerp (probeGO.eulerAngles, targetRotation, rotateSpeed * Time.deltaTime);
	}

	private void Move()
	{
		movePosition = CalculateMovePosition();

		timeTrigger.Register(Random.Range(0f, timeToMoveRnd) + Time.time, Move);
	}

	private Vector3 CalculateMovePosition()
	{
		float angle = Random.Range(0f, 360f);
		float y = Random.Range (1.0f, 2.5f);
		float y2 = Random.Range (1f, 2f);

		return new Vector3(angle, y, y2);
	}

	
}
