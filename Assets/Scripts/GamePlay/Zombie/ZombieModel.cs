using System.Declarative.Scripts;
using System.Declarative.Scripts.Attributes;
using UnityEngine;


namespace GamePlay.Zombie
{
    public class ZombieModel : DeclarativeModel
    {
        [Section]
        [SerializeField]
        public ZombieModel_Core Core = new();
        
        [Section]
        [SerializeField]
        public ZombieModel_View View = new();
    }
}



