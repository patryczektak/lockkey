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
            // Zmniejsz intensywnoœæ wibracji do s³abego impulsu
            intensity = Mathf.Clamp01(intensity * 0.1f);

            // Skróæ czas trwania wibracji do krótkiego impulsu
            duration = Mathf.Clamp(duration, 0.05f, 0.5f);

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
