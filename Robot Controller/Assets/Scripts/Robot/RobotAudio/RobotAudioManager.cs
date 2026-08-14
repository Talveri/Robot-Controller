
using System.Collections;
using UnityEngine;

public class RobotAudioManager : MonoBehaviour
{
    [Header("Robot Sounds")]
    [SerializeField] AudioClip moveStepFloor;
    [SerializeField] AudioClip moveStepGoo;
    [SerializeField] AudioClip turn;
    [SerializeField] AudioClip goal;

    private AudioSource source;

    [SerializeField] private float stepIntervall = 0.4f;




    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        Robot.OnMove += PlayMoveSound;
        Robot.OnTurn += PlayTurnSound;
        ButtonEvents.OnGoalReached += PlayGoalSound;
        ButtonEvents.OnRestart += HandleReset;
    }

    void OnDisable()
    {
        Robot.OnMove -= PlayMoveSound;
        Robot.OnTurn -= PlayTurnSound;
        ButtonEvents.OnGoalReached -= PlayGoalSound;
        ButtonEvents.OnRestart -= HandleReset;
    }

    bool goalReached = false;
    Coroutine stepRoutine;
    void PlayMoveSound(bool moving)
    {
        if (goalReached) return;

        if (moving)
        {
            if (stepRoutine == null)
                stepRoutine = StartCoroutine(StepLoop());
        }
        else
            if (stepRoutine != null)
            {
                StopCoroutine(stepRoutine);
                stepRoutine = null;
            }
    }

    private IEnumerator StepLoop()
    {
        while (true)
        {
            source.PlayOneShot(moveStepFloor);
            yield return new WaitForSeconds(stepIntervall);
        }
    }


    void PlayTurnSound(FacingDirection dir) => source.PlayOneShot(turn);

    void PlayGoalSound()
    {
        goalReached = true;

        if (stepRoutine != null)
        {
            StopCoroutine(stepRoutine);
            stepRoutine = null;
        }

        source.Stop();
        source.PlayOneShot(goal);
    }

    void HandleReset()
    {
        source.Stop();
        if (stepRoutine != null)
        {
            StopCoroutine(stepRoutine);
            stepRoutine = null;
        }
    }
}