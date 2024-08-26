using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private int _currentStateIndex;
    private List<string> _animations;
    
    #region Unity Events Functions
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        PopulateAnimationList();
    }
    #endregion
    private void PopulateAnimationList()
    {
        _animations = new List<string>
        {
            "Idle Animation",
            "Walk Animation",
            "Run Animation"
        };
    }
    #region Animation Controller
    public void ChangeAnimationState(int animationIndex)
    {
        // Stop same animation interrupting itself
        if (_currentStateIndex == animationIndex) return;
        _animator.Play(_animations[animationIndex]);
        _currentStateIndex = animationIndex;
    }
    public float GetCurrentAnimationLength()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).length;
    }
    #endregion
}
