using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

using static GameEvents;
public class MinionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Minion _minion;

    private void OnEnable()
    {
        EventManager.Instance.AddListener<MinionDamagedEvent>(OnMinionDamaged);
        EventManager.Instance.AddListener<MinionHealedEvent>(OnMinionHealed);
        UpdateUI();
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<MinionDamagedEvent>(OnMinionDamaged);
        EventManager.Instance.RemoveListener<MinionHealedEvent>(OnMinionHealed);
    }

    private void OnMinionDamaged(MinionDamagedEvent e)
    {
        if (e.Minion == _minion)
        {
            UpdateUI();
            StartCoroutine(DamageAnimation());
        }
    }

    private void OnMinionHealed(MinionHealedEvent e)
    {
        if (e.Minion == _minion)
        {
            UpdateUI();
            StartCoroutine(HealAnimation());
        }
    }

    private void UpdateUI()
    {
       
        _healthText.text =_minion.CurrentHealth.ToString();
    }

    private IEnumerator DamageAnimation()
    {
        GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Image>().color = Color.white;
    }
    private IEnumerator HealAnimation()
    {
        GetComponent<Image>().color = Color.green;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Image>().color = Color.white;
    }
}