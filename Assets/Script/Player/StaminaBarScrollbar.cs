using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScrollbar : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    private float currentStamina;

    [Header("UI References")]
    public Scrollbar staminaBar;   
    public Image fillImage;        

    [Header("Color Settings")]
    public Color fullStaminaColor = Color.blue;
    public Color lowStaminaColor = new Color(0f, 0f, 0.3f); 

    void Start()
    {
        currentStamina = maxStamina;
        UpdateStaminaUI();
    }

    public void UseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        UpdateStaminaUI();
    }

    public void RecoverStamina(float amount)
    {
        currentStamina += amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        UpdateStaminaUI();
    }

    private void UpdateStaminaUI()
    {
        if (staminaBar != null)
        {
            // Scrollbar size goes from 0 (empty) to 1 (full)
            staminaBar.size = currentStamina / maxStamina;

            if (fillImage != null)
            {
                fillImage.color = Color.Lerp(lowStaminaColor, fullStaminaColor, currentStamina / maxStamina);
            }
        }
    }
}
