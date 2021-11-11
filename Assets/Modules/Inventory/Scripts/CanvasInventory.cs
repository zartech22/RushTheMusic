using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Aloha
{
    public class CanvasInventory : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            int nbItems = 3;
            //int nbItems = Inventory.Instance.getMaxItems();

            GameObject horizontalLayout = this.gameObject.transform.GetChild(1).gameObject;

            RectTransform horizontalLayoutTransform = horizontalLayout.GetComponent<RectTransform>();




            if (nbItems == 1)
            {
                Debug.Log("nbItems == 1");
                Destroy(horizontalLayout);
            }
            else if( nbItems >= 3){
                for(int i=2; i < nbItems; i++)
                {
                    horizontalLayoutTransform.sizeDelta = new Vector2(horizontalLayoutTransform.sizeDelta.x + 75, horizontalLayoutTransform.sizeDelta.y);

                    GameObject image = new GameObject();
                    image.AddComponent<Image>();
                    image.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(50, 50);
                    image.transform.SetParent(horizontalLayoutTransform);
                }
            }

            for (int i=0; i < nbItems; i++)
            {
                //Instantiate(cube, )
                Debug.Log("bite");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

