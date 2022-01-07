using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace Aloha
{
    public class Content : MonoBehaviour
    {
        [SerializeField]
        private GameObject panel;

        [SerializeField]
        private Text panelNum;



        // Duration of the music (in seconds)
        [SerializeField]
        private float duration;

        // number of tiles during one second
        [SerializeField]
        private float tileSpeed = 7;

        // size of the tiles
        [SerializeField]
        private float titleSize = 5;

        // Start is called before the first frame update
        void Start()
        {
            float numTiles = ((this.duration * this.tileSpeed) / this.titleSize);
            for (int i = 0; i < numTiles; i++)
            {
                Instantiate(panel).transform.SetParent(this.transform);
                Text text = Instantiate(panelNum);
                text.text = i.ToString();
                text.transform.SetParent(this.transform);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}