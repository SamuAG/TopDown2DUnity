using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/GameManagerSO")]
public class GameManagerSO : ScriptableObject
{
    [NonSerialized]
    private Dictionary<int, bool> items = new Dictionary<int, bool>();
    [NonSerialized]
    private Vector3 initPlayerPosition;
    [NonSerialized]
    private Vector2 initPlayerRotation;
    [NonSerialized]
    private Vector3 initPlayerScale = Vector3.one;
    [NonSerialized]
    private float speed = 5.0f;
    [NonSerialized]
    private Color initPlayerColor = Color.white;

    [NonSerialized]
    private int coins;
    [NonSerialized]
    private List<ItemSO> inventoryItems = new List<ItemSO>();

    private InventorySystem inventory;
    private Player player;

    public Vector3 InitPlayerPosition { get => initPlayerPosition; set => initPlayerPosition = value; }
    public Vector2 InitPlayerRotation { get => initPlayerRotation; set => initPlayerRotation = value; }
    public int Coins { get => coins; set => coins = value; }
    public Dictionary<int, bool> Items { get => items; set => items = value; }
    public InventorySystem Inventory { get => inventory;}
    public Player Player { get => player;}
    public List<ItemSO> InventoryItems { get => inventoryItems; set => inventoryItems = value; }
    public Vector3 InitPlayerScale { get => initPlayerScale; set => initPlayerScale = value; }
    public global::System.Single Speed { get => speed; set => speed = value; }
    public Color InitPlayerColor { get => initPlayerColor; set => initPlayerColor = value; }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        player = FindObjectOfType<Player>();
        inventory = FindObjectOfType<InventorySystem>();
        if (inventory)
        {
            inventory.ResetInventory(inventoryItems);
        }
    }

    public void LoadNewScene(Vector3 newPosition, Vector2 newRotation, int newSceneIndex)
    {
        initPlayerPosition = newPosition;
        initPlayerRotation = newRotation;
        SceneManager.LoadScene(newSceneIndex);
    }
}
