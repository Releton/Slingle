using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private GameManager GM;

    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public Ball[] ballref;

    private void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.transform.Find("ItemSlotTemplate");

    }

    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        float itemSlotCellSizeX = 50f;
        float itemSlotCellSizeY = 10f;
        foreach (Stack<GameObject> stck in GM.DataInventory) {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSizeX, y * itemSlotCellSizeY);
            Debug.Log(stck.Peek().name);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = stck.Peek().GetComponent<BallScript>().type.icon;
            TMP_Text count = itemSlotRectTransform.Find("Count").GetComponent<TMP_Text>();
            count.text = stck.Count.ToString();
            if(stck == GameManager.CurrentStack)
            {
                itemSlotRectTransform.Find("Background").GetComponent<Image>().color = new Color(174f, 157f, 34f, 30f);
            }
            x++;
            if(x > 3)
            {
                x = 0;
                y++;
            }
        }
    }

    public void SetInventory()
    {
        foreach (Transform child in itemSlotContainer.transform)
        {
            if(child.gameObject.name != "ItemSlotTemplate")
            {
                Destroy(child.gameObject);
            }
        }
        RefreshInventoryItems();
    }
}
