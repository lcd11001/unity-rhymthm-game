using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public AudioSource theMuics;
    public bool startPlaying;
    public BeatScroller theBS;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier = 1;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;

    public float totalNotes;
    public float normalHit;
    public float goodHit;
    public float perfectHit;
    public float missHit;

    public GameObject resultScreen;
    public Text percentHitText, normalText, goodText, perfectText, missText, rankText, finalText;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

        currentScore = 0;
        scoreText.text = "Score: " + currentScore;

        currentMultiplier = 1;
        multiText.text = "Multiplier: x" + currentMultiplier;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMuics.Play();
            }
        }
        else
        {
            float totalHit = normalHit + goodHit + perfectHit;
            if (totalNotes == totalHit + missHit)
            {
                theMuics.Stop();
            }


            if (!theMuics.isPlaying && !resultScreen.activeInHierarchy)
            {
                resultScreen.SetActive(true);

                normalText.text = "" + normalHit;
                goodText.text = "" + goodHit;
                perfectText.text = "" + perfectHit;

                missText.text = "" + missHit;


                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = string.Format("{0:f1}%", percentHit);

                string rankVal = "S";
                if (percentHit < 95)
                {
                    rankVal = "A";
                }
                else if (percentHit < 85)
                {
                    rankVal = "B";
                }
                else if (percentHit < 70)
                {
                    rankVal = "C";
                }
                else if (percentHit < 55)
                {
                    rankVal = "D";
                }
                else if (percentHit < 40)
                {
                    rankVal = "F";
                }

                rankText.text = rankVal;
                finalText.text = currentScore.ToString();
            }
        }
    }

    public void NoteHit()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;

                multiText.text = "Multiplier: x" + currentMultiplier;
            }
        }

        // currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        normalHit++;
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        goodHit++;
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        perfectHit++;
        NoteHit();
    }

    public void NoteMissed()
    {
        missHit++;

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;
    }
}
