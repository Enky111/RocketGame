using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    private SpriteRenderer _backgroundSprite;
    void Start()
    {
        _backgroundSprite = GetComponent<SpriteRenderer>();

        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        Vector2 BackgroundSize = _backgroundSprite.sprite.bounds.size;
        transform.localScale = new Vector3(screenWidth / BackgroundSize.x, screenHeight / BackgroundSize.y, 1);
    }
}
