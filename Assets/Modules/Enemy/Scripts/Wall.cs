using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Aloha.EntityStats;

namespace Aloha
{
    public class Wall : Enemy<WallStats>
    {
        private Hero hero;

        private void Start()
        {
            hero = GameManager.Instance.GetHero();
        }

        protected override IEnumerator AI()
        {
            Debug.Log("AI Wall");
            TilesManager.Instance.ChangeTileSpeed(0);
            yield return null;
        }

        public override void Die()
        {
            //TilesManager.Instance.ChangeTileSpeed(10);
            base.Die();
            Destroy(this.gameObject);
        }

    }
}
