using Unity.VisualScripting;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
	public static BlockSpawner Instance {  get; private set; }

	[SerializeField] private GameObject p_box;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}else
		{
			Destroy(gameObject);
		}
	}
	void Start()
    {
		NewBlock();
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void NewBlock()
	{
		if (ScoreManager.Instance.GetIsGameOver() == true)
			return ;
		//La y siempre estará en la parte superior de la pantalla
		float y = (Camera.main.transform.position.y + Camera.main.orthographicSize) - 2f;

		//Posible x random:
		Vector2 spawnPoint = new Vector2 (0f, y);
		GameObject box = Instantiate(p_box, spawnPoint, Quaternion.identity);
		SetBlockSize (box); //Le da tamaño al cubo

	}

	void SetBlockSize(GameObject block)
	{
		float height = Random.Range(0.25f, 1.2f);

		float width = Random.Range(1f, 3f);
		block.transform.localScale = new Vector2(width, height);
	}
}
