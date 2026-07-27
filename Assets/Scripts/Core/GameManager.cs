using System;
using Character;
using Core.GameMode;
using Enums;
using Services;
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
        
        public RoomManager Room => room;
        public CharacterBrain Character => character;
        public LootManager Loot => loot;
        public DropManager Drop => drop;
        public RewardManager Reward => reward;
        public UpgradeManager Upgrade => upgrade;
        
        public Action<bool> EndGame { get; set; }

        private void Awake()
        {
            ServiceLocator.Register(this);
            room.Setup();
            gameMode.Setup();
            difficulty.Setup();
            spawn.Setup();
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister(this);
        }

        private void OnEnable()
        {
            gameMode.OnStartMode += StartMode;
            gameMode.OnEndMode += EndMode;
            gameMode.OnCompleteMode += CompleteMode;
            character.OnDeath += CharacterDie;
        }

        private void OnDisable()
        {
            gameMode.OnStartMode -= StartMode;
            gameMode.OnEndMode -= EndMode;
            gameMode.OnCompleteMode -= CompleteMode;
            character.OnDeath -= CharacterDie;
        }

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            character.ChangeState(CharacterStateType.Idle);
            difficulty.RefreshDifficultyData(Progress);
            gameMode.NextMode();
        }

        private void StartMode()
        {
            spawn.StartSpawn(Difficulty.SpawnInterval, Difficulty.SpawnCount);
        }

        private void EndMode()
        {
            character.ChangeState(CharacterStateType.Stop);
            spawn.EndSpawn();
            drop.DespawnAll();
            Upgrade.Roll(character.Level.LevelUpgrade());
        }

        private void CompleteMode()
        {
            EndGame?.Invoke(true);
            character.ChangeState(CharacterStateType.Stop);
            spawn.EndSpawn();
            drop.DespawnAll();
            upgrade.OpenUIViewOnly();
        }

        private void CharacterDie()
        {
            gameMode.StopMode();
            EndGame?.Invoke(false);
            character.ChangeState(CharacterStateType.Death);
            spawn.EndSpawn();
            drop.DespawnAll();
            upgrade.OpenUIViewOnly();
        }

        public float Progress => gameMode.Progress;
        public DifficultyData Difficulty => difficulty.Data;
        public float RandomMultiplier => difficulty.RandomMultiplier();
        public EnemyType EnemyType => gameMode.GetEnemyType();
        public Vector2 SpawnPosition => room.GetSpawnPointPosition();
    }
}