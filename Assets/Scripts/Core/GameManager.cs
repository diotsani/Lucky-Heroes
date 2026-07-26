using System;
using Character;
using Core.GameMode;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Character")]
        [SerializeField] private CharacterBrain character;
        
        [Header("Manager")]
        [SerializeField] private RoomManager room;
        [SerializeField] private GameModeManager gameMode;
        [SerializeField] private DifficultyManager difficulty;
        [SerializeField] private SpawnManager spawn;
        [SerializeField] private LootManager loot;
        [SerializeField] private DropManager drop;
        [SerializeField] private RewardManager reward;
        [SerializeField] private UpgradeManager upgrade;
        
        public CharacterBrain Character => character;
        public LootManager Loot => loot;
        public DropManager Drop => drop;
        public RewardManager Reward => reward;
        public UpgradeManager Upgrade => upgrade;

        private void Awake()
        {
            Services.Services.Register(this);
            room.Setup();
            gameMode.Setup();
            difficulty.Setup();
            spawn.Setup();
        }

        private void OnEnable()
        {
            gameMode.OnStartMode += StartMode;
            gameMode.OnEndMode += EndMode;
            gameMode.OnCompleteMode += CompleteMode;
        }

        private void OnDisable()
        {
            gameMode.OnStartMode -= StartMode;
            gameMode.OnEndMode -= EndMode;
            gameMode.OnCompleteMode -= CompleteMode;
        }

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            difficulty.RefreshDifficultyData(Progress);
            gameMode.NextMode();
        }

        private void StartMode()
        {
            spawn.StartSpawn(Difficulty.SpawnInterval, Difficulty.SpawnCount);
        }

        private void EndMode()
        {
            spawn.EndSpawn();
            drop.DespawnAll();
            Upgrade.Roll(character.Level.LevelUpgrade());
        }

        private void CompleteMode()
        {
            spawn.CompleteSpawn();
        }

        public float Progress => gameMode.Progress;
        public DifficultyData Difficulty => difficulty.Data;
        public float RandomMultiplier => difficulty.RandomMultiplier();
        public EnemyType EnemyType => gameMode.GetEnemyType();
        public Vector2 SpawnPosition => room.GetSpawnPointPosition();
    }
}