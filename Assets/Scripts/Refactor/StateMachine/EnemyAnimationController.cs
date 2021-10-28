using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator _animator;
    
    private readonly int _walking = Animator.StringToHash("Walking");
    private readonly int _watch = Animator.StringToHash("Watch");
    private readonly int _running = Animator.StringToHash("Running");
    private readonly int _death = Animator.StringToHash("Death");
    private readonly int _attack = Animator.StringToHash("Attack");
    private readonly int _detect = Animator.StringToHash("Detect");
    private readonly int _waitWhat = Animator.StringToHash("WaitWhat");
    private readonly int _bang = Animator.StringToHash("Bang");
    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }
    
    public void PlayAnimationWalking()
    {
        _animator.SetBool(_walking, true);
    }

    public void StopAnimationWalking()
    {
        _animator.SetBool(_walking, false);
    }
    
    public void PlayAnimationAttack()
    {
        _animator.SetTrigger(_attack);    
    }
    
    public void PlayAnimationBang()
    {
        _animator.SetTrigger(_bang);    
    }
    
    public void PlayAnimationRunning()
    {
        _animator.SetBool(_running, true);
    }
    
    public void StopAnimationRunning()
    {
        _animator.SetBool(_running, false);
    }
    
    public void PlayAnimationDetect()
    {
        _animator.SetBool(_detect, true);
    }
    
    public void StopAnimationDetect()
    {
        _animator.SetBool(_detect, false);
    }
    
    public void PlayAnimationDeath()
    {
        _animator.SetTrigger(_death);
    }

    public void PlayAnimationWatch()
    {
        _animator.SetTrigger(_watch);
    }
    
    public void PlayAnimationWaitWatch()
    {
        _animator.SetTrigger(_waitWhat);
    }
}
