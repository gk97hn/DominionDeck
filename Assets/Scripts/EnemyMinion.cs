using UnityEngine;
using static GameEvents;

public class EnemyMinion : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private int _attackPower = 2;
    private int _currentHealth;

    public int AttackPower => _attackPower;
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public bool IsAlive => _currentHealth > 0;

    public bool IsOnFight  = false;

    private void Start()
    {
        _currentHealth = _maxHealth;
        MinionManager.Instance.RegisterEnemyMinion(this);
    }

    public bool TakeDamage(int amount)
    {
        if (!IsAlive || IsOnFight) return false;

        _currentHealth = Mathf.Max(0, _currentHealth - amount);
        EventManager.Instance.Raise(new EnemyMinionDamagedEvent(this, amount));

        if (!IsAlive)
        {
            EventManager.Instance.Raise(new EnemyMinionDiedEvent(this));
            MinionManager.Instance.UnregisterEnemyMinion(this);
            Destroy(gameObject, 0.5f); 
        }
        return true;
    }

    private void OnDestroy()
    {
        if (MinionManager.Instance != null)
            MinionManager.Instance.UnregisterEnemyMinion(this);
    }
}