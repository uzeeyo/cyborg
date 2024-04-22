using Cyborg.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cyborg.Items
{
    public class OtoRifle : Weapon
    {
        private Coroutine FireCoroutine;
        public override void Shoot()
        {
            FireCoroutine = StartCoroutine(FireEnumerator());
        }

        public override void ShootCancel()
        {
            StopCoroutine(FireCoroutine);
        }

        private IEnumerator FireEnumerator()
        {
            while (true)
            {
                if(IsAbleToFire())
                {
                    BeginCoolDown();
                    Fire(GetForwardDirection());
                    PlayOneShotSound();
                }
                yield return null;
            }
        }


    }
}
