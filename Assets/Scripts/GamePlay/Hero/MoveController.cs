using UnityEngine;

namespace GamePlay.Hero
{
    public sealed class MoveController : MonoBehaviour
    {
        [SerializeField]
        private HeroModel hero;

        private void Update()
        {
            /*var onMove = this.hero.Core.CharacterMoveComp.OnMove;
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                onMove.Invoke(Vector3.left);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                onMove.Invoke(Vector3.right);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                onMove.Invoke(Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                onMove.Invoke(Vector3.back);
            }*/
        }
    }
}