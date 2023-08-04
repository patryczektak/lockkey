using UnityEngine;
using TMPro;

public class ChangeValueAnimator : MonoBehaviour
{
    public TextMeshProUGUI valueText; // Referencja do obiektu Text, który bêdzie wyœwietla³ wartoœæ
    public float animationDuration = 1.0f; // Czas trwania animacji
    public AnimationCurve animationCurve = AnimationCurve.Linear(0, 0, 1, 1); // Krzywa animacji (mo¿esz dostosowaæ)

    private int currentValue;
    private int targetValue;
    private float animationStartTime;

    private void Start()
    {
        // Przyk³adowe ustawienie wartoœci pocz¹tkowej
        currentValue = PlayerPrefs.GetInt("star");
        targetValue = currentValue;
        UpdateValueText();

        // Przyk³adowe zmiany wartoœci po 3 sekundach
        Invoke("IncreaseValue", 3.0f);
    }

    private void Update()
    {
        if (currentValue != targetValue)
        {
            // Obliczanie postêpu animacji
            float timeSinceStart = Time.time - animationStartTime;
            float progress = Mathf.Clamp01(timeSinceStart / animationDuration);

            // Aktualizacja wartoœci na podstawie animacji
            int animatedValue = Mathf.RoundToInt(Mathf.Lerp(currentValue, targetValue, animationCurve.Evaluate(progress)));
            currentValue = animatedValue;

            // Aktualizacja tekstu z wartoœci¹
            UpdateValueText();

            // Zakoñczenie animacji
            if (progress >= 1.0f)
            {
                currentValue = targetValue;
                UpdateValueText();
            }
        }
    }

    private void UpdateValueText()
    {
        valueText.text = currentValue.ToString();
    }

    public void IncreaseValue()
    {
        int newValue = currentValue + 1;
        ChangeValue(newValue);
    }

    public void DecreaseValue()
    {
        int newValue = currentValue - 1;
        ChangeValue(newValue);
    }

    private void ChangeValue(int newValue)
    {
        if (newValue != currentValue)
        {
            targetValue = newValue;
            animationStartTime = Time.time;
        }
    }

    public void ChangeValueUp(int prizeUp)
    {        
        PlayerPrefs.SetInt("star", PlayerPrefs.GetInt("star") + prizeUp);
        targetValue = PlayerPrefs.GetInt("star");
        animationStartTime = Time.time;
    }

    public void ChangeValueDown(int prizeDown)
    {
        PlayerPrefs.SetInt("star", PlayerPrefs.GetInt("star") - prizeDown);
        targetValue = PlayerPrefs.GetInt("star");
        animationStartTime = Time.time;
    }
}
