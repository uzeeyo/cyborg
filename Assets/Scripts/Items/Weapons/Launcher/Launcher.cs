using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cyborg.Items
{
    public class Launcher : Weapon
    {
        public override void Shoot()
        {
            if (!IsAbleToFire())
                return;

            LaunchProjectile projectile = Fire((LaunchProjectile)_data.ProjectilePrefab);
            projectile.SetTargerPosition(GetMousePosition());

            PlayOneShotSound();

            BeginCoolDown();
        }

    }
}
