using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float timeCounter;
    [SerializeField] private float countdownTimer = 120f;
    [SerializeField] private bool isCountdown;
    [SerializeField] private TextMeshProUGUI timerText;

    // Singleton instance
    private static TimerController instance;

    // Accessor for the instance
    public static TimerController Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // Ensure there's only one instance of the TimerController
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scene loads
        }
    }

    private void Update()
    {
        if (isCountdown && countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
        }
        else if (!isCountdown)
        {
            timeCounter += Time.deltaTime;
        }

        int minutes = Mathf.FloorToInt(isCountdown ? countdownTimer / 60f : timeCounter / 60f);
        int seconds = Mathf.FloorToInt(isCountdown ? countdownTimer - minutes * 60 : timeCounter - minutes * 60f);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
