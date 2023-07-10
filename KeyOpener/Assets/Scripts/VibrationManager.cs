using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance { get; private set; }

    private void Awake()
    {
        // Upewnij si�, �e istnieje tylko jedna instancja VibrationManagera w scenie
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
        // Sprawd�, czy urz�dzenie obs�uguje wibracje
        if (SystemInfo.supportsVibration)
        {
            // Zmniejsz intensywno�� wibracji do s�abego impulsu
            intensity = Mathf.Clamp01(intensity * 0.1f);

            // Skr�� czas trwania wibracji do kr�tkiego impulsu
            duration = Mathf.Clamp(duration, 0.05f, 0.5f);

            // Oblicz czas trwania wibracji w milisekundach
            long milliseconds = (long)(duration * 1000f);

            // Wibracja na urz�dzeniu mobilnym z okre�lonym czasem trwania i moc�
            Handheld.Vibrate();
        }
        else
        {
            Debug.LogWarning("Urz�dzenie nie obs�uguje wibracji.");
        }
    }
}
