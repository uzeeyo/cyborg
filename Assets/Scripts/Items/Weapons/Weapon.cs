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
            PlayOneShotSound();

            BeginCoolDown();
        }

        protected void Fire(Vector2 direction)
        {
            var projectile = Instantiate(_data.ProjectilePrefab, transform.position, Quaternion.LookRotation(Vector3.forward, direction));
            projectile.SetDirection(direction);
        }
        protected T Fire<T>(T t, Vector2 direction) where T : Projectile
        {
            T projectile = Instantiate(t, transform.position, Quaternion.LookRotation(Vector3.forward, direction));
            projectile.SetDirection(direction);
            return t;
        }
        protected T Fire<T>(T t) where T : Projectile
        {
            T projectile = Instantiate(t, transform.position, Quaternion.identity);
            return projectile;
        }

        protected bool IsAbleToFire()
        {
            if (_onCooldown || !EnergyManager.Instance.TryToRemoveEnergy(_data.EnergyCost))
                return false;
            return true;
        }
        protected Vector2 GetForwardDirection()
        {
            Vector3 mousePos = GetMousePosition();
            mousePos -= transform.position;
            Vector2 forward = mousePos;
            return forward.normalized;
        }
        protected Vector2 GetMousePosition()
        {
            return GlobalObjects.MainCamera.ScreenToWorldPoint(Input.mousePosition);
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
        protected virtual void PlayOneShotSound()
        {
            EventHub.PlayOneShotSound(_data._FmodEvent);
        }
        protected virtual void WeaponReady()
        {

        }
    }
}