using UnityEngine;

public class UFOAttack : Attack
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<RocketView>(out RocketView rocketview) )
        {
            gameObject.SetActive(false);
        }
    }
}
