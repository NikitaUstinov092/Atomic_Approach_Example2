using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDisable : MonoBehaviour
{
   [SerializeField] private Animator _anim;


   public void DisableAnimator()
   {
      _anim.enabled = false;
   }
}
