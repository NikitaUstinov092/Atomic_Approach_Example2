using System.Declarative.Scripts;
using System.Declarative.Scripts.Attributes;
using UnityEngine;

namespace GamePlay.Hero
{
    public sealed class HeroModel : DeclarativeModel
    {
        [Section]
        [SerializeField]
        public HeroModel_Core Core = new();
        
        [Section]
        [SerializeField]
        public HeroModel_View View = new();
    }
}
