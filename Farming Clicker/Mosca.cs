using UnityEngine;

public class Mosca : MonoBehaviour
{
    MinigameController controller;

    
    Vector3 startPosition;
    Vector3 targetPosition;
    float sinTime;
    float movSpeed;

    private void Start()
    {
        controller = GameObject.Find("MiniGameController").GetComponent<MinigameController>();
        CambioPosicion();
    }

    private void Update()
    {
        if (transform.position != targetPosition)
        {
            sinTime += Time.deltaTime * movSpeed;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
            float t = Evaluate(sinTime);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
        }

        CambioPosicion();
    }

    private void OnMouseDown()
    {
        GameManager.Instance.GetComponent<GameStats>().AddMoscas(1);
        controller.moscas++;
        controller.UpdateTextos();
        Destroy(gameObject);
    }

    void CambioPosicion()
    {
        movSpeed = Random.Range(1, 5);
        if (transform.position != targetPosition)
        {
            return;
        }
        startPosition = transform.position;
        targetPosition = controller.RandomPointInBox();
        sinTime = 0;
    }
    public float Evaluate(float x)
    {
        return 0.5f * Mathf.Sin(x - Mathf.PI / 2) + 0.5f;
    }
}
