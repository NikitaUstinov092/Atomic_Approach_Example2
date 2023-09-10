using UnityEngine;

namespace GamePlay.Custom.Engines
{
    public class RotationEngine
    {
        private Transform _transform;
        private Camera _playerCamera;
        
        private float _rotationSpeed;
        private const float _minCursorDistance = 5f; // Минимальное расстояние от курсора до игрока, при котором не будет поворота
        
        public void Construct(Transform transform, Camera playerCam, float speed)
        {
            _transform = transform;
            _playerCamera = playerCam;
            _rotationSpeed = speed;
        }

        public void UpdateRotation(Vector3 cursorScreenPos)
        {
            var cursorWorldPos = _playerCamera.ScreenToWorldPoint(new Vector3(cursorScreenPos.x, cursorScreenPos.y, _playerCamera.transform.position.y));

            // Игрок не поворачивается, если курсор слишком близко
            var cursorDistance = Vector3.Distance(cursorWorldPos, _transform.position);
            
            if (cursorDistance < _minCursorDistance)
                return;
            
            var direction = cursorWorldPos - _transform.position;
            direction.y = 0f;
           
            var targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
