using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Item;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Counts")]
    public int NoWindCount;
    public int PlasticCount;
    public int StrongCount;
    public int SpikeCount;
    [Header("Ball Reference")]
    public GameObject NoWindBall;
    public GameObject StrongBall;
    public GameObject SpikeBall;
    public GameObject PlasticBall;

    public Transform Spawnpoint;

    private Vector3 PlasticSpawn = new Vector3(10, 10, 10);
    private Vector3 NoWindSpawn = new Vector3(20, 20, 20);
    private Vector3 StrongSpawn = new Vector3(30, 30, 30);
    private Vector3 SpikeSpawn = new Vector3(40, 40, 40);

    public enum itemTypes
    {
        NoWind = 0,
        Plastic = 1,
        Strong = 2,
        Spike =3
    }
    //Changing array to linklist
    public List<Stack<GameObject>> DataInventory = new List<Stack<GameObject>>();
    public static Dictionary<int, itemTypes> Convention = new Dictionary<int, itemTypes>();
    public static Stack<GameObject> CurrentStack;
    public GameObject[] Inventory;
    private static int currentIndex;

    [HideInInspector]
    public static GameObject current;
    public float envWind;
    public static float windLevel;

    private int curIndex;
    public static float levelNumber;
    public static bool hasCurChanged = false;
    public static bool isBallLeft = true;

    public CinemachineVirtualCamera defaultCam;
    public CinemachineVirtualCamera ballCam;

    public static int MapEnumToInt(itemTypes itemType)
    {
        switch (itemType)
        {
            case itemTypes.Plastic: return 0;
            case itemTypes.NoWind: return 1;
            case itemTypes.Strong: return 2;
            case itemTypes.Spike: return 3;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    private void Awake()
    {
        DataInventory.Add(new Stack<GameObject>());
        DataInventory.Add(new Stack<GameObject>());
        DataInventory.Add(new Stack<GameObject>());
        DataInventory.Add(new Stack<GameObject>());

        SpawnSpecific(PlasticCount, PlasticBall, MapEnumToInt(itemTypes.Plastic), PlasticSpawn);
        SpawnSpecific(NoWindCount, NoWindBall, MapEnumToInt(itemTypes.NoWind), NoWindSpawn);
        SpawnSpecific(StrongCount, StrongBall, MapEnumToInt(itemTypes.Strong), StrongSpawn);
        SpawnSpecific(SpikeCount, SpikeBall, MapEnumToInt(itemTypes.Spike), SpikeSpawn);
        Debug.Log("AWAKE");
        SelectCurrent(0);
    }
    private void SpawnSpecific(int Count, GameObject reference, int type, Vector3 location)
    {
        for (int i = 0; i < Count; i++)
        {
            //location + new Vector3(3, 0, 0), Quaternion.identity
            GameObject go = Instantiate(reference, Spawnpoint.position, Quaternion.identity);
            go.SetActive(false);
            DataInventory[type].Push(go);
        }
    }
    private void SelectCurrent(itemTypes item)
    {
        CurrentStack = DataInventory[MapEnumToInt(item)];
        hasCurChanged = true;
        currentIndex = MapEnumToInt(item);
    }
    private void SelectCurrent(int index)
    {
        Debug.Log(index);
        CurrentStack = DataInventory[RectifyIndex(index)];
        hasCurChanged = true;
        currentIndex = RectifyIndex(index);
        CurrentStack.Peek().SetActive(true);
        Debug.Log(CurrentStack.Peek().name);
        CurrentStack.Peek().GetComponent<BallScript>().deActivate();
        resetBall(currentIndex);
        Debug.Log("tried to activate");
        if (DataInventory.Count <= 1) return;
        DataInventory[RectifyIndex(index - 1)].Peek().SetActive(false);

    }
    private int RectifyIndex(int index)
    {
        //Game Won
        index = index < 0 ? (index = DataInventory.Count - 1) : index;
        index = index > DataInventory.Count - 1 ? 0 : index;
        return index;
    }

    public void DeleteFromCurrent()
    {
        
        CurrentStack.Pop();
        hasCurChanged= true;
        if(CurrentStack.Count == 0)
        {
            Debug.Log("Delete");
            DataInventory.RemoveAt(currentIndex);
            SelectCurrent(currentIndex + 1);
        }
        else
        { 
            CurrentStack.Peek().SetActive(true);
            CurrentStack.Peek().GetComponent<BallScript>().deActivate();
            resetBall(currentIndex);
        }
        
    }

    public void stopBall(int index)
    {
        DataInventory[index].Peek().GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    public void resetBall(int index)
    {
        stopBall(index);
        DataInventory[index].Peek().GetComponent<Transform>().position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    }
    public void resetBall()
    {
        stopBall(currentIndex);
        CurrentStack.Peek().GetComponent<Transform>().position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    }

    /*private void Highlight(int index)
    {
        for(int i = 0; i <= DataInventory.Count - 1; i++)
        {
            if(i != index)
            {
                for(int j = 0;)
            }
        }
    }*/

    private void Start()
    {
        windLevel = envWind;
        //setAsCurrent(1);
    }

    private void Update()
    {
        /*isBallLeft = Inventory.Length > 0;
        Debug.Log(Inventory.Length);

        if (isBallLeft && Input.GetKeyUp(KeyCode.RightArrow) && !BallScript.isActivated)
        {
            setAsCurrent((curIndex + 1));
        }
        if(isBallLeft && Input.GetKeyUp(KeyCode.LeftArrow) && !BallScript.isActivated)
        {
            setAsCurrent((curIndex - 1));
        }
        if (BallScript.isActivated)
        {
            focusOnball();
        }
        if (!BallScript.isActivated) { 
            focusOnSlingShot();
        }*/
        isBallLeft = DataInventory.Count > 0;
        if (isBallLeft && Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetMouseButton(1) && !BallScript.isActivated)
        {
            SelectCurrent(currentIndex + 1);
        }   
        if (BallScript.isActivated)
        {
            focusOnball();
        }
        if (!BallScript.isActivated)
        {
            focusOnSlingShot();
        }

    }
    public void focusOnball()
    {
        defaultCam.Priority = ballCam.Priority - 1;
        ballCam.LookAt = CurrentStack.Peek().transform;
        ballCam.Follow = CurrentStack.Peek().transform;
    }

    public void focusOnSlingShot()
    {
        defaultCam.Priority = ballCam.Priority + 1;
    }
    public static void curChanged()
    {
        hasCurChanged = !hasCurChanged;
    }

    /*void setAsCurrent(int index)
    {
        if (Inventory.Length <= 0) return;
        index = index - 1;
        index = index == -1? Inventory.Length-1 : (index == Inventory.Length  ? 0 : index);
        current = Inventory[index];
        for(int i = 0; i < Inventory.Length; i++)
        {
            if(i != index)
            {
                Inventory[i].SetActive(false);
            }
        }
        Inventory[index].SetActive(true);
        Inventory[index].GetComponent<BallScript>().deActivate();
        curIndex = index + 1;
        resetBall(curIndex);
        BallScript.isActivated = false;
        hasCurChanged = true;
        focusOnSlingShot();
    }
    void setAsCurrent(int index, int subindex)
    {
        if (Inventory.Length <= 0) return;
        index = rectifyIndex(index);
        current = Inventory[index];
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (i != index)
            {
                Inventory[i].SetActive(false);
            }
        }
        Inventory[index].SetActive(true);
        Inventory[index].GetComponent<BallScript>().deActivate();
        curIndex = index + 1;
        resetBall(curIndex);
        hasCurChanged = true;
        focusOnSlingShot();
    }

    private int rectifyIndex(int index)
    {
        index = index - 1;
        index = index == -1 ? Inventory.Length - 1 : (index == Inventory.Length ? 0 : index);
        return index;
    }



 
    public void deleteCurrent()
    {
        Inventory[curIndex -1].SetActive(false);
        setAsCurrent(curIndex+1);
    }

    public void setNextAsCurrentByDestruction()
    {

        if(Inventory.Length == 0)
        {
            isBallLeft = false;
            return;
        }
        Inventory = RemoveElementAtIndex(Inventory, curIndex);
        setAsCurrent(curIndex+1, curIndex);
    }

    private GameObject[] RemoveElementAtIndex(GameObject[] original, int indexToRemove)
    {
        indexToRemove = rectifyIndex(indexToRemove);

        // Create a new array with a size smaller by 1
        GameObject[] newArray = new GameObject[original.Length - 1];

        // Copy elements, skipping the one at indexToRemove
        int newIndex = 0;
        for (int i = 0; i < original.Length; i++)
        {
            if (i == indexToRemove) continue;

            newArray[newIndex] = original[i];
            newIndex++;
        }

        return newArray;
    }
    */
}
