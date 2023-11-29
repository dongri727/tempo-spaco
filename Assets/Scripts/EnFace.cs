using UnityEngine;
using System.Collections;

public class EnFace : MonoBehaviour
{
	void Start()
	{
		transform.localScale = new Vector3(-1, 1, 1);
	}

	void Update()
	{
		transform.LookAt(Camera.main.transform);
	}
}
