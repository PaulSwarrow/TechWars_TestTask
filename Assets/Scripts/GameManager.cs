using System;
using System.Collections.Generic;
using Systems;
using Data;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameProperties properties;
    private List<IGameSystem> systems = new List<IGameSystem>();

    public static event Action StartEvent;
    public static event Action UpdateEvent;
    public static event Action EndEvent;
    public static bool GameStarted { get; private set; }
    public static GameProperties Properties { get; private set; }
    public static PlayerController PlayerController { get; private set; }
    public static GameCharacterSystem GameCharacter { get; private set; }

    public static ObjectSpawner ObjectSpawner { get; private set; }
    public static ProjectileSystem Projectiles { get; private set; }

    public static CollidersSystem Colliders { get; private set; }
    public static NpcSystem NpcSystem { get; private set; }
    public static RespawnSystem RespawnSystem { get; private set; }
    public static VfxSystem VFX { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Properties = properties;
        PlayerController = InitSystem<PlayerController>();
        GameCharacter = InitSystem<GameCharacterSystem>();
        ObjectSpawner = InitSystem<ObjectSpawner>();
        Projectiles = InitSystem<ProjectileSystem>();
        Colliders = InitSystem<CollidersSystem>();
        NpcSystem = InitSystem<NpcSystem>();
        RespawnSystem = InitSystem<RespawnSystem>();
        VFX = InitSystem<VfxSystem>();


        systems.ForEach(system => system.Init());
        systems.ForEach(system => system.Start());
        GameStarted = true;
        StartEvent?.Invoke();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    private void OnDestroy()
    {
        EndEvent?.Invoke();
        systems.ForEach(system => system.Destroy());
    }

    private T InitSystem<T>() where T : IGameSystem, new()
    {
        var item = new T();
        systems.Add(item);
        return item;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEvent?.Invoke();
    }
}