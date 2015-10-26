using UnityEngine;
using System.Collections;

public class ProbeShooter : MonoBehaviour 
{
	public GameObject shotPrefab;
	public GameObject shotHolder;
	public GameObject floor;
	public Transform player;

	private float timeToShoot;
	private float lastTimeToMove;

	private Vector2 movePosition;

	public float moveSpeed = 1f;
	public float rotateSpeed = 1f;
	public float distance = 2f;
	public float timeToMoveRnd = 2f;

	public AudioSource audioShot;


	void Update () 
	{
		if (IsTimeToShoot())
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		timeToShoot = Time.time + Random.Range (0f, timeToMoveRnd);

		GameObject shot = Instantiate(shotPrefab, transform.position, transform.rotation) as GameObject;

		shot.GetComponent<Shot> ().floor = floor;

		shot.transform.parent = shotHolder.transform;
		shot.transform.LookAt(new Vector3(player.position.x, Random.Range(1f, 1.8f), player.position.z));

		audioShot.pitch = Random.Range (0.7f, 1.3f);
		audioShot.volume = Random.Range (0.7f, 1f);
		audioShot.Play();
	}

	private bool IsTimeToShoot()
	{
		return Time.time >= timeToShoot;
	}

	private Vector2 CalculateMovePosition()
	{
		float angle = Random.Range(0f, 360f);
		float y = Random.Range (1.5f, 2.5f);

		return new Vector2(angle, y);
	}

	
}
