using GamePlay.Custom.GameMachine;
using UnityEngine;

namespace GamePlay.Custom.Input
{
   public class ShootInput : MonoBehaviour, IUpdateListener
   {
      [SerializeField]
      private Entity.Entity _entity;

      void IUpdateListener.Update()
      {
         if (UnityEngine.Input.GetMouseButtonDown(0))
            OnMousePressed();
      }
      private void OnMousePressed()
      {
         if (!_entity.TryGet(out IShootable shootable))
            return;
         
         shootable.PressedFireButton();
      }
   }
}

