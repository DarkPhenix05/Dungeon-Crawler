using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public const string ANIM_MOVE_SPEED = "MoveSpeed";
    public const string ANIM_ATTACK = "Attack";
    public const string ANIM_HURT = "Hurt";

    public void SetAnimMovement(float speedMagnitude)
    {
        animator.SetFloat(ANIM_MOVE_SPEED, speedMagnitude);
    }

    public void SetAnimAttack()
    {
        animator.SetTrigger(ANIM_ATTACK);
    }

    public void SetHurtAttack()
    {
        animator.SetTrigger(ANIM_HURT);
    }

}