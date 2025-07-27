using UnityEngine;

public class AttackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _attackPrefab;

    private GameObject _attackProjectile;
    public Attack attack { get; private set; }

    private Vector3 _attackTargetPositionOffset = new(0f, 27, 0);
    public void ActivateAttack(Vector3 rocketPosition)
    {
        _attackProjectile.transform.position = rocketPosition;
        attack.SetTargetPosition(rocketPosition, _attackTargetPositionOffset);
        _attackProjectile.SetActive(true);
    }

    public void DeactivateAttack() => _attackProjectile.SetActive(false);

    public void SetupAttack()
    {
        _attackProjectile = Instantiate(_attackPrefab);
        attack = _attackProjectile.GetComponent<Attack>();
        _attackProjectile.SetActive(false);
    }
}
