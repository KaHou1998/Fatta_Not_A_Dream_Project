using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public CharacterBase characterBase;
    private Animator animator;

    public void Awake()
    {
        animator = characterBase.animator;
    }

    public void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        animator.SetTrigger("IsAttack");
    }
   
}
