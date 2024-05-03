using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float timeCounter;
    [SerializeField] private float countdownTimer = 120f;

    [SerializeField] private bool isCountdown;
    [SerializeField] private TextMeshProUGUI timerText;

    // Singleton pattern to ensure only one instance of TimerController exists
    private static TimerController instance;
    public static TimerController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
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
