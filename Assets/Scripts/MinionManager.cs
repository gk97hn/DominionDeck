using UnityEngine;
using System.Collections.Generic;
using static GameEvents;

public class MinionManager : MonoBehaviour
{
    public static MinionManager Instance { get; private set; }

    private List<Minion> _activeMinions = new List<Minion>();
    private List<EnemyMinion> _activeEnemyMinions = new List<EnemyMinion>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterMinion(Minion minion)
    {
        _activeMinions.Add(minion);
        //EventManager.Instance.Raise(new MinionSpawnedEvent(minion));
    }

    public void UnregisterMinion(Minion minion)
    {
        _activeMinions.Remove(minion);
    }
    public void RegisterEnemyMinion(EnemyMinion minion)
    {
        _activeEnemyMinions.Add(minion);
        //EventManager.Instance.Raise(new MinionSpawnedEvent(minion));
    }

    public void UnregisterEnemyMinion(EnemyMinion minion)
    {
        _activeEnemyMinions.Remove(minion);
    }

    public bool DamageFirstMinion(int damage)
    {
        if (_activeEnemyMinions.Count == 0) return false;

        return _activeEnemyMinions[0].TakeDamage(damage);
       
    }
    public bool DamageFirstPlayerMinion(int damage)
    {
        if (_activeMinions.Count == 0) { return false; }

        return _activeMinions[0].TakeDamage(damage);
       
    }
    public List<Minion> GetActiveMinions() => _activeMinions;
}