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
    
        AnimatorEventManager.Instance.OnPlayerRun += IsRunning;
        AnimatorEventManager.Instance.OnPlayerWalk += IsWalking;     
        AnimatorEventManager.Instance.OnPlayerJump += IsJumping;
        AnimatorEventManager.Instance.OnPlayerTouchGround += IsOnGround;
        
        AnimatorEventManager.Instance.OnPlayerSpeedChange += SetMoveSpeed;

        AnimatorEventManager.Instance.OnPlayerDeath += Kill;
        AnimatorEventManager.Instance.OnPlayerHurt += IsHurt;

        AnimatorEventManager.Instance.OnPlayerShoot += IsShooting;
        AnimatorEventManager.Instance.OnPlayerAim += IsAiming;
        AnimatorEventManager.Instance.OnPlayerReload += IsPlayerReload;
        AnimatorEventManager.Instance.OnPlayerHasReloaded += HasReloaded;
    }

    private void OnDisable()
    {    
        AnimatorEventManager.Instance.OnPlayerRun -= IsRunning;
        AnimatorEventManager.Instance.OnPlayerWalk -= IsWalking; 
        AnimatorEventManager.Instance.OnPlayerJump -= IsJumping;
        AnimatorEventManager.Instance.OnPlayerTouchGround -= IsOnGround;
        AnimatorEventManager.Instance.OnPlayerSpeedChange -= SetMoveSpeed;

        AnimatorEventManager.Instance.OnPlayerDeath -= Kill;
        AnimatorEventManager.Instance.OnPlayerHurt -= IsHurt;

        AnimatorEventManager.Instance.OnPlayerAim -= IsAiming;
        AnimatorEventManager.Instance.OnPlayerShoot -= IsShooting;
        AnimatorEventManager.Instance.OnPlayerReload -= IsPlayerReload;
        AnimatorEventManager.Instance.OnPlayerHasReloaded -= HasReloaded;
    }


    // Movement Animations
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
        animator.SetBool("IsJumping", state);
    }

    public void IsOnGround(bool state)
    {
        animator.SetBool("IsOnGround", state);
    }


    public void SetMoveSpeed(Vector3 speed)
    {
        animator.SetFloat("moveX", speed.x, 0.1f, Time.deltaTime);
        animator.SetFloat("moveZ", speed.z, 0.1f, Time.deltaTime);
    }


    // Health Animations
    public void IsHurt(bool state)
    {
        animator.SetBool("IsHurt", state);
    }
    public void Kill()
    {
        animator.SetTrigger("IsDead");
    }


    // Weapon Animations
    public void IsAiming(bool state)
    {
        animator.SetBool("IsAiming", state);
    }

    public void IsShooting(bool state)
    {
        animator.SetBool("IsShooting", state);
    }

    public void IsPlayerReload(bool state)
    {
        animator.SetBool("IsReloading", state);
    }

    public void HasReloaded(bool state)
    {
        animator.SetBool("HasReloaded", state);
    }

}
