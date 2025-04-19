using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class CamController : MonoBehaviour
{
	public static CamController Instance { get; private set; }

	[SerializeField] private GameObject lastBox;
	private float camOffset;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		camOffset = transform.position.y;
	}
	private void Update()
	{
		/*if (lastBox != null)
		{
			if (lastBox.transform.position.y > transform.position.y)
			{
				transform.position = new Vector3(transform.position.x, 
									Mathf.Lerp(transform.position.y, lastBox.transform.position.y, lastBox.transform.localScale.y /2),
									transform.position.z);
			}
		}*/
	}

	public void MoveCamera(float new_y)
	{
		StartCoroutine(MovingCamera(new_y + camOffset));
	}

	IEnumerator MovingCamera (float new_y)
	{
		float t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime / .5f;
			transform.position = new Vector3(transform.position.x,
								Mathf.Lerp(transform.position.y, new_y, t),
								transform.position.z);

			yield return null;
		}
	}

	public void SetLastBox(GameObject box)
	{
		lastBox = box;
	}

	public GameObject GetLastBox()
	{
		return lastBox;
	}
}
