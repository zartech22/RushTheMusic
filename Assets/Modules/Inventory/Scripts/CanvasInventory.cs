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
            
            int nbItems = Inventory.Instance.getMaxItems();

            Queue<Item> items = Inventory.Instance.GetItems();

            GameObject horizontalLayout = this.gameObject.transform.GetChild(1).gameObject;

            RectTransform horizontalLayoutTransform = horizontalLayout.GetComponent<RectTransform>();




            // Creation of the dynamic interface
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
                Item item = items.Dequeue();
                if(item != null)
                {
                    if (i == 0)
                    {
                        this.gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                    }
                    else
                    {
                        this.gameObject.transform.GetChild(1).transform.GetChild(i - 1).GetComponent<Image>().color = Color.blue;
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

