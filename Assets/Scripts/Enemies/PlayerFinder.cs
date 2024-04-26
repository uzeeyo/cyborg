using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Cyborg.Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class PlayerFinder : MonoBehaviour
    {
        private Transform _self;
        private ContactFilter2D _filter;
        private float _detectionRange;
        private float _fovAngle;
        private Enemy _enemy;
        private Transform _player;


        [SerializeField] LayerMask _playerLayer;
        [SerializeField] LayerMask _obstacleLayer;

        private void Start()
        {
            _self = transform;
            _filter = new ContactFilter2D()
            {
                useLayerMask = true,
                layerMask = _playerLayer
            };

            _enemy = GetComponent<Enemy>();
            _detectionRange = _enemy.DetectionRange;
            _fovAngle = _enemy.Fov;
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            _enemy ??= GetComponent<Enemy>();

            Handles.color = Color.red;
            Handles.DrawWireArc(transform.position, Vector3.forward, Quaternion.Euler(0, 0, -_enemy.Fov / 2) * transform.up, _enemy.Fov, _enemy.DetectionRange);

            var fovAngleA = Quaternion.Euler(0, 0, _enemy.Fov / 2) * transform.up;
            var fovAngleB = Quaternion.Euler(0, 0, -_enemy.Fov / 2) * transform.up;

            Handles.DrawLine(transform.position, transform.position + fovAngleA * _enemy.DetectionRange);
            Handles.DrawLine(transform.position, transform.position + fovAngleB * _enemy.DetectionRange);

            if (_player != null)
            {
                Handles.color = Color.green;
                Handles.DrawLine(_self.position, _player.position);
            }
        }
#endif

        public bool PlayerIsVisible()
        {
            List<Collider2D> results = new();

            Physics2D.OverlapCircle(_self.position, _detectionRange, _filter, results);

            if (results.Count != 0)
            {
                var player = results[0].transform;
                var directionToPlayer = (player.position - _self.position).normalized;
                if (Vector3.Angle(_self.up, directionToPlayer) < _fovAngle / 2)
                {
                    var distanceToPlayer = Vector3.Distance(_self.position, player.position);

                    if (!Physics2D.Raycast(_self.position, directionToPlayer, distanceToPlayer, _obstacleLayer))
                    {
                        _player = player;
                        return true;
                    }
                }
            }
            _player = null;
            return false;
        }
    }
}