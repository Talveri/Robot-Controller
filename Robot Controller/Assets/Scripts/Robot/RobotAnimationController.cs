
using UnityEngine;

public class RobotAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private static readonly int Dir = Animator.StringToHash("Direction");
    private static readonly int Moving = Animator.StringToHash("IsMoving");

    public void SetDirection(FacingDirection dir)
    {
        animator.SetFloat(Dir, (float)dir);
    }

    public void SetMoving(bool moving)
    {
        animator.SetBool(Moving, moving);
    }

    public void ResetMoving() => SetMoving(false);
    public void ResetDirection() => SetDirection(FacingDirection.Down);

    void OnEnable()
    {
        Robot.OnMove += SetMoving;
        Robot.OnTurn += SetDirection;

        ButtonEvents.OnRestart += ResetMoving;
        ButtonEvents.OnRestart += ResetDirection;

    }

    void OnDisable()
    {
        Robot.OnMove -= SetMoving;
        Robot.OnTurn -= SetDirection;

        ButtonEvents.OnRestart -= ResetMoving;
        ButtonEvents.OnRestart -= ResetDirection;
    }
}