using UnityEngine;
using UnityEngine.UI;
using static GameEvents;


public class PlayerAvatarUI : MonoBehaviour
{
    
   
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _manaText;
    [SerializeField] private Image _avatarImage;

 
    [SerializeField] private Color _damageColor = Color.red;
    [SerializeField] private Color _healColor = Color.green;
    [SerializeField] private Color _manaUsedColor = Color.blue;
    [SerializeField] private float _feedbackDuration = 0.3f;

    private Color _originalColor;
    private int _currentHealth;
    private int _maxHealth;
    private int _currentMana;
    private int _maxMana;

    private void Awake()
    {
        _originalColor = _avatarImage.color;

        EventManager.Instance.AddListener<PlayerHealthChangedEvent>(OnHealthChanged);
        EventManager.Instance.AddListener<ManaChangedEvent>(OnManaChanged);
    }

    private void OnDestroy()
    {

        EventManager.Instance.RemoveListener<PlayerHealthChangedEvent>(OnHealthChanged);
        EventManager.Instance.RemoveListener<ManaChangedEvent>(OnManaChanged);
    }

    public void Initialize(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
  

        UpdateHealthUI();

    }

    private void OnHealthChanged(PlayerHealthChangedEvent e)
    {
        int previousHealth = _currentHealth;
        _currentHealth = e.CurrentHealth;

       
        if (_currentHealth < previousHealth)
        {
      
            StartCoroutine(FlashImage(_damageColor));
        }
        else if (_currentHealth > previousHealth)
        {
        
            StartCoroutine(FlashImage(_healColor));
        }

        UpdateHealthUI();
    }

    private void OnManaChanged(ManaChangedEvent e)
    {
        int previousMana = _currentMana;
        _currentMana = e.CurrentMana;
        _maxMana = e.MaxMana;

    
        if (_currentMana < previousMana)
        {
          
            StartCoroutine(FlashImage(_manaUsedColor));
        }

        UpdateManaUI();
    }

    private void UpdateHealthUI()
    {
       
        _healthText.text = _currentHealth.ToString();
    }

    private void UpdateManaUI()
    {

        _manaText.text = _currentMana.ToString();
    }

    private System.Collections.IEnumerator FlashImage(Color flashColor)
    {
        _avatarImage.color = flashColor;
        yield return new WaitForSeconds(_feedbackDuration);
        _avatarImage.color = _originalColor;
    }
}