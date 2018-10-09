using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Този клас управлява таймера на играта и го визуализира
public class TimerManager : MonoBehaviour { 

    public static float Timer;

    Text text;                      // Reference to the Text component

    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
        Timer = 250;
    }


    void Update()
    {
        text.text = "" + Timer;
    }
}