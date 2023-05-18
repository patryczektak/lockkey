using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance { get; private set; }

    private void Awake()
    {
        // Upewnij siê, ¿e istnieje tylko jedna instancja VibrationManagera w scenie
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Vibrate(float duration, float intensity)
    {
        // SprawdŸ, czy urz¹dzenie obs³uguje wibracje
        if (SystemInfo.supportsVibration)
        {
            // Konwertuj moc wibracji na zakres od 0.0f do 1.0f
            intensity = Mathf.Clamp01(intensity);

            // Oblicz czas trwania wibracji w milisekundach
            long milliseconds = (long)(duration * 1000f);

            // Wibracja na urz¹dzeniu mobilnym z okreœlonym czasem trwania i moc¹
            Handheld.Vibrate();
        }
        else
        {
            Debug.LogWarning("Urz¹dzenie nie obs³uguje wibracji.");
        }
    }
}
