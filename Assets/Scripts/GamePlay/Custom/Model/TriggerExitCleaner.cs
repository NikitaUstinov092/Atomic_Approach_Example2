using UnityEngine;

namespace GamePlay.Custom.Model
{
   public class TriggerExitCleaner : MonoBehaviour
   {
      [SerializeField] 
      private string _tag;
      private void OnTriggerExit(Collider other)
      {
         if(other.CompareTag(_tag))
            Destroy(other.gameObject);
      }
   }
}
