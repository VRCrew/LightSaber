using UnityEngine;
using System.Collections;

public class Lightsaber : MonoBehaviour 
{
	public GameObject saber;
	public Color color;

	private bool isAnimating;
	private bool isOpening = true;

	private Vector3 targetScale;

	public float speed = 10f;

	public AudioSource audioOpen;
	public AudioSource audioHum;
	public AudioSource audioSwing;
	public AudioSource audioBlock;

	public Transform posTracker1;
	public Transform posTracker2;

	private Vector3 lastPos1;
	private Vector3 lastPos2;

	public SteamVR_TrackedObject controller;
	

	void Start () 
	{
		LisghtsaberGlow[] glows = GetComponentsInChildren<LisghtsaberGlow> (true);

		foreach (var glow in glows)
		{
			glow.GetComponent<MeshRenderer>().material.SetColor("_TintColor", color);
		}

	}

	public void ChangeState()
	{
		if (isAnimating)
		{
			return;
		}

		isOpening = !isOpening;

		if (isOpening)
		{
			saber.SetActive(true);
			targetScale = new Vector3(1f, 1f, 1f);
			audioOpen.Play();
			audioHum.PlayDelayed(0.2f);
		}
		else
		{
			targetScale = new Vector3(1f, 1f, 0.01f);
			audioHum.Stop();
			audioOpen.Play();
		}

		isAnimating = true;
	}

	void FixedUpdate()
	{
		var device = SteamVR_Controller.Input((int)controller.index);
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			ChangeState ();
		}
	}

	public void Update()
	{
		if (isAnimating)
		{
			saber.transform.localScale = Vector3.Lerp(saber.transform.localScale, targetScale, speed * Time.deltaTime);

			if (Vector3.Distance(saber.transform.localScale, targetScale) < 0.1f)
			{
				saber.transform.localScale = targetScale;
				isAnimating = false;

				if (!isOpening)
				{
					saber.SetActive(false);
				}
			}
		}


		if (Input.GetMouseButton (0))
		{
			ChangeState();
		}

		if (isOpening && !isAnimating) 
		{
			float distance1 = Vector3.Distance(posTracker1.position, lastPos1);
			float distance2 = Vector3.Distance(posTracker2.position, lastPos2);

			float d = Mathf.Max(distance1, distance2);

			Debug.Log(d);

			audioHum.volume = 0.3f + Mathf.Min(d * 7f, 0.7f);

			Debug.Log(d + " -- " + audioHum.volume );

			if (d > 0.04f)
			{
				if (!audioSwing.isPlaying)
				{
					audioSwing.Play();
				}

				audioSwing.volume = 0.1f + Mathf.Min(d * 1f, 0.1f);
			}

			lastPos1 = posTracker1.position;
			lastPos2 = posTracker2.position;

		}

//		Debug.Log(Input.gyro.gravity + " : " + Input.acceleration + " : ");
		
		//transform.eulerAngles = Input.gyro.gravity * 90 + Vector3.up * 180 + Vector3.right * 90;

	}

	public void Block()
	{
		audioBlock.Play();
	}

	void OnTriggerEnter(Collider other) 
	{
		audioBlock.Play();
	}

}
