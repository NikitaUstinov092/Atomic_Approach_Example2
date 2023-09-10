using UnityEngine;

namespace GamePlay.Custom.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "ScriptableObjects/BulletConfig")]
    public class BulletConfig: ScriptableObject
    {
        public Rigidbody Bullet;
        public float SpeedShoot;
    }
}
