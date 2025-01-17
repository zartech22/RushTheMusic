using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aloha
{
    /// <summary>
    /// Class for the fireball spell
    /// </summary>
    public class Fireball : MonoBehaviour
    {
        private bool isAutonomous = false;
        public Wizard Wizard;
        public int Power;

        /// <summary>
        /// Send the fireball forward
        /// <example> Example(s):
        /// <code>
        ///     Fireball fireball = Instantiate(fireballPrefab);
        ///     fireball.Launch();
        /// </code>
        /// </example>
        /// </summary>
        public void Launch()
        {
            isAutonomous = true;
            GetComponent<Rigidbody>().AddForce(transform.forward * 5, ForceMode.Impulse);
            transform.parent = null;
            ContainerManager.Instance.AddToContainer(ContainerTypes.Projectile, this.gameObject);
            Destroy(gameObject, 2f);
        }

        /// <summary>
        /// If the fireball touches an Object
        /// </summary>
        public void OnTriggerEnter(Collider collider)
        {
            if (isAutonomous && collider.tag == "Enemy")
            {
                collider.gameObject.GetComponent<Entity>().TakeDamage(this.Power);
                Wizard.BumpEntity(collider.GetComponent<Entity>());
                Destroy(gameObject);
            }
        }
    }
}
