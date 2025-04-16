using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static GameEvents;
using System.Collections;

public class CardUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _manaCostText;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Text _healthText;
    [SerializeField] private Image _artworkImage;
    private Minion _minion;

    public Card Card { get; private set; }

    public void Initialize(Card card)
    {
        Card = card;
        _nameText.text = card.Data.cardName;
        _manaCostText.text = card.Data.manaCost.ToString();
        _healthText.text = card.Data.health.ToString();
        _descriptionText.text = card.Data.description;
        _artworkImage.sprite = card.Data.artwork;
        _minion = GetComponent<Minion>();
    }
    private void OnEnable()
    {
        EventManager.Instance.AddListener<MinionDamagedEvent>(OnMinionDamaged);
        EventManager.Instance.AddListener<MinionHealedEvent>(OnMinionHealed);
        //UpdateUI();
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<MinionDamagedEvent>(OnMinionDamaged);
        EventManager.Instance.RemoveListener<MinionHealedEvent>(OnMinionHealed);
    }
    public void OnPointerClick(PointerEventData eventData)
    {   
        if (!Card.CanBePlayed) return;

        if (GameManager.Instance.StateMachine.GetCurrentState() is PlayerTurnState)
        {
            GameManager.Instance.Player.PlayCard(Card);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
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

        _healthText.text = _minion.CurrentHealth.ToString();
    }

    private IEnumerator DamageAnimation()
    {
        _artworkImage.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _artworkImage.color = Color.white;
    }
    private IEnumerator HealAnimation()
    {
        _artworkImage.color = Color.green;
        yield return new WaitForSeconds(0.1f);
        _artworkImage.color = Color.white;
    }

}