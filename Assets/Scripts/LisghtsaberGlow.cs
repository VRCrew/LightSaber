using UnityEngine;

public class LisghtsaberGlow : MonoBehaviour
{
	Material material;

	void Awake()
	{
		material = GetComponent<MeshRenderer>().material;
	}

	void Update()
	{
		material.color = new Color (material.color.r, material.color.g, material.color.b, 0.8f + Random.Range(0f, 0.2f));//(byte)Random.Range(0, 255));
		//renderer.material.color.a = Random.value;
	}
}
