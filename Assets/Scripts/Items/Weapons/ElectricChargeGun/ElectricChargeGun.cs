using Cyborg.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cyborg.Items
{
    public class ElectricChargeGun : Weapon
    {
        [SerializeField] private float ChargeSpeed;
        [SerializeField] private float MaxCharge;

        private float BulletSpeed = 10;
        
        float Charge;
        public override void Shoot()
        {
            if (!IsAbleToFire())
                return;
            
            StartCoroutine(ChargeWeapon());
        }

        
        public override void ShootCancel()
        {        
            BeginCoolDown();
            StopAllCoroutines();
            StartCoroutine(FireOperations());
        }

        private IEnumerator FireOperations()
        {
            int bulletCount = (int)(Charge * 5) + 1;

            Vector2 direction;
            for(int i = 0; i < bulletCount; i++)
            {
                direction = GetForwardDirection();
                Fire(direction);

                yield return new WaitForSeconds(0.05f);
            }
        }

        IEnumerator ChargeWeapon()
        {
            Charge = 0;
            while (true)
            {
                Charge += ChargeSpeed * Time.deltaTime;
                Charge = Mathf.Min(Charge, MaxCharge);
  
                yield return null;
            }
        }

    }
}
