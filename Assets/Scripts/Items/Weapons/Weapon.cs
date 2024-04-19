using System.Collections;
using UnityEngine;

namespace Cyborg.Items
{
    public class Weapon : MonoBehaviour
    {
        protected bool _onCooldown;

        [SerializeField] protected WeaponData _data;

        public WeaponData Data
        {
            get => _data;
            set => _data = value;
        }

        public virtual void Shoot()
        {
            if (!IsAbleToFire()) 
                return;

            Vector2 direction = GetForwardDirection();
            Fire(direction);
            
            BeginCoolDown();
        }

        protected void Fire(Vector2 direction)
        {
            var projectile = Instantiate(_data.ProjectilePrefab, transform.position, Quaternion.LookRotation(Vector3.forward, direction));
            projectile.Init(direction * _data.ProjectileSpeed, _data);
        }
        protected bool IsAbleToFire()
        {
            if(_onCooldown || !EnergyManager.Instance.TryToRemoveEnergy(_data.EnergyCost))
                return false;
            return true;
        }
        protected Vector2 GetForwardDirection()
        {
            Vector3 mousePos = GlobalObjects.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos -= transform.position;
            Vector2 forward = mousePos;
            return forward.normalized;
        }

        public virtual void ShootCancel()
        {
        }

        protected void BeginCoolDown()
        {
            StartCoroutine(StartCooldown());

        }
        private IEnumerator StartCooldown()
        {
            _onCooldown = true;
            yield return new WaitForSeconds(_data.CoolDown);
            _onCooldown = false;
            WeaponReady();
        }

        protected virtual void WeaponReady()
        {

        }
    }
}