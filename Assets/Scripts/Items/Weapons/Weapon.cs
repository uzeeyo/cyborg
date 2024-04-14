using System.Collections;
using UnityEngine;

namespace Cyborg.Items
{
    public class Weapon : MonoBehaviour
    {
        private bool _onCooldown;

        [SerializeField] private WeaponData _data;

        public WeaponData Data
        {
            get => _data;
            set => _data = value;
        }

        public void Shoot()
        {
            if (_onCooldown) return;

            var v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            v.z = 0;

            Vector2 direction = (v - transform.position).normalized;
            var obj = Instantiate(_data.ProjectilePrefab, transform.position, Quaternion.LookRotation(Vector3.forward, direction));
            obj.GetComponent<Rigidbody2D>().velocity = direction * _data.ProjectileSpeed;
            StartCoroutine(StartCooldown());
        }

        private IEnumerator StartCooldown()
        {
            _onCooldown = true;
            yield return new WaitForSeconds(_data.CoolDown);
            _onCooldown = false;
        }
    }
}