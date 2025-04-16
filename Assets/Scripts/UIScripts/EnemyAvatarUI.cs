using UnityEngine;
using UnityEngine.UI;
using static GameEvents;

public class EnemyAvatarUI : MonoBehaviour
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

        EventManager.Instance.AddListener<EnemyHealthChangedEvent>(OnHealthChanged);
        EventManager.Instance.AddListener<EnemyManaChangedEvent>(OnManaChanged);
    }

    private void OnDestroy()
    {

        EventManager.Instance.RemoveListener<EnemyHealthChangedEvent>(OnHealthChanged);
        EventManager.Instance.RemoveListener<EnemyManaChangedEvent>(OnManaChanged);
    }

    public void Initialize(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
  

        UpdateHealthUI();

    }

    private void OnHealthChanged(EnemyHealthChangedEvent e)
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

    private void OnManaChanged(EnemyManaChangedEvent e)
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