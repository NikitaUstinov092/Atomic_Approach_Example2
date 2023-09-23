using GamePlay.Components.Interfaces;
using UnityEngine;

namespace GamePlay.Custom
{
   public class CollideHitEntity : MonoBehaviour
   {
      [SerializeField] 
      private int _damage;
      private void OnCollisionEnter(Collision other)
      {
         if (!other.gameObject.TryGetComponent(out Entity.Entity entity)) 
            return;
         if(entity.TryGet(out ITakeDamageable damage))
            damage.TakeDamage(_damage);
         Destroy(gameObject);
      }
   }
}
