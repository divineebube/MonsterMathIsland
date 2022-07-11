
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

[SerializeField] private int _hp;
[SerializeField] private Transform _healthBarUI;

public int maxHp = 150;
    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthBarUI();
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            _hp = 0;
        }
        UpdateHealthBarUI();
    }

    public void SetHealthBar(Transform healthBarUi)
    {
        _healthBarUI = healthBarUi;
        UpdateHealthBarUI();
    }

    private void UpdateHealthBarUI()
    {
        if (!_healthBarUI) return;
        _healthBarUI.GetChild(1).GetComponent<Image>().fillAmount = (float)_hp / maxHp;
        _healthBarUI.GetChild(2).GetComponent<TMP_Text>().text = $"{_hp} / {maxHp}";
    }
}
