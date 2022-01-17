using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // Register events

        AnimatorEventManager.Instance.OnPlayerDeath += Kill;
        AnimatorEventManager.Instance.OnPlayerRun += IsRunning;
        AnimatorEventManager.Instance.OnPlayerWalk += IsWalking;
        AnimatorEventManager.Instance.OnPlayerShoot += IsShooting;
        AnimatorEventManager.Instance.OnPlayerJump += IsJumping;
        AnimatorEventManager.Instance.OnPlayerAim += IsAiming;
        AnimatorEventManager.Instance.OnPlayerSpeedChange += SetMoveSpeed;
        
    }

    private void OnDisable()
    {
        AnimatorEventManager.Instance.OnPlayerDeath -= Kill;
        AnimatorEventManager.Instance.OnPlayerRun -= IsRunning;
        AnimatorEventManager.Instance.OnPlayerWalk -= IsWalking;
        AnimatorEventManager.Instance.OnPlayerShoot -= IsShooting;
        AnimatorEventManager.Instance.OnPlayerJump -= IsJumping;
        AnimatorEventManager.Instance.OnPlayerAim -= IsAiming;
        AnimatorEventManager.Instance.OnPlayerSpeedChange -= SetMoveSpeed;
    }


    public void Kill()
    {
        animator.SetTrigger("IsDead");
    }

    public void IsRunning(bool state)
    {
        animator.SetBool("IsRunning", state);
    }

    public void IsWalking(bool state)
    {
        animator.SetBool("IsWalking", state);
    }

    public void IsJumping(bool state)
    {
        animator.SetBool("isJumping", state);
    }

    public void IsShooting(bool state)
    {

    }

    public void IsAiming(bool state)
    {
        animator.SetBool("IsAiming", state);
    }

    public void SetMoveSpeed(Vector2 speed)
    {
        animator.SetFloat("moveX", speed.x, 0.1f, Time.deltaTime);
        animator.SetFloat("moveY", speed.y, 0.1f, Time.deltaTime);
    }
}
