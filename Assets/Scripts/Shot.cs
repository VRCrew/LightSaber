using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour 
{
	public float speed = 1;
	public GameObject floor;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider other) 
	{
		GetComponent<Collider>().enabled = false;
		
		if (other.transform.parent != null && other.transform.parent.CompareTag("saber"))
		{
			Debug.Log("saber");
			Destroy (gameObject);
			
			int i = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
			
			Debug.Log("device id:" + i);
			
			SteamVR_Controller.Input(i).TriggerHapticPulse(1000, Valve.VR.EVRButtonId.k_EButton_Axis0);
		}
		else
		{
			StartCoroutine(HitEvent());
			Debug.Log (other);
		}
	}
	
	private IEnumerator HitEvent()
	{
		//Color origColor = floor.GetComponent<MeshRenderer> ().material.color;
		floor.GetComponent<MeshRenderer> ().material.color = new Color (1f, 0f, 0f);
		//SteamVR_Fade.Start(new Color(0.5f, 0f, 0f), 0);
		//SteamVR_Fade.Start(Color.clear, 1);
		
		//Camera.main.backgroundColor = new Color(0.2f, 0f, 0f);
		
		yield return new WaitForSeconds(0.25f);
		
		//Camera.main.backgroundColor = Color.black;
		
		floor.GetComponent<MeshRenderer> ().material.color = new Color (0.0f, 0.5f, 1f);
		
		Destroy (gameObject);
	}
	
}