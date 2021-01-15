using System;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Data;
using DefaultNamespace.Systems;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameProperties properties;
    private List<IGameSystem> systems = new List<IGameSystem>();

    public static event Action UpdateEvent;
    public static GameProperties Properties { get; private set; }
    public static PlayerController PlayerController { get; private set; }
    public static GameCharacterSystem GameCharacter { get; private set; }

    public static ObjectSpawner ObjectSpawner { get; private set; }
    public static ProjectileSystem Projectiles { get; private set; }

    public static CollidersSystem Colliders { get; private set; }
    public static NpcSystem NpcSystem { get; private set; }

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


        systems.ForEach(system => system.Init());
        systems.ForEach(system => system.Start());
    }


    private void OnDestroy()
    {
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