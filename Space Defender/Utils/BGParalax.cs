using UnityEngine;

public class BGParalax : MonoBehaviour
{
	[SerializeField] private float speedParallax = 0.2f;
	Material mat;
	Vector2 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
		offset.y += Time.deltaTime * speedParallax;
		mat.mainTextureOffset = offset;
    }
}
