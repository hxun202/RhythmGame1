using UnityEngine;

public class MainBackground : MonoBehaviour
{
    public float xMove;
    public float yMove;
    public float speed;

    RectTransform rect;

    Vector2 vector2;

    void Start()
    {
        rect = GetComponent<RectTransform>();

        vector2 = rect.anchoredPosition;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * xMove;
        float y = Mathf.Cos(Time.time * speed * 0.7f) * yMove;

        rect.anchoredPosition = vector2 + new Vector2(x, y);
    }
}
