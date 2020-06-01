using Game;
using Player;
using StateMachine;
using UnityEngine;

namespace NPC
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class NpcController : StateMachine<NpcState>
    {
        [SerializeField] internal float speed = 2.5f;
        [SerializeField] private AudioClip[] coughs;
        [SerializeField] private AudioClip[] onCollisionSounds;
        
        internal GameManager Game;
        internal Rigidbody2D Rigidbody;
        internal Animator Animator;

        private AudioSource _audioSource;
        private float _nextCough;
        
        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();

            _audioSource = GetComponent<AudioSource>();
            _nextCough = Mathf.NegativeInfinity;
            // ReSharper disable once Unity.IncorrectMonoBehaviourInstantiation
            SetState(new NpcDestinationReachedState(this, null));
        }

        private new void Update()
        {
            base.Update();

            if (!(Time.time > _nextCough)) return;
            
            _audioSource.clip = coughs[Random.Range(0, coughs.Length - 1)];
            _audioSource.Play();
            _nextCough = Time.time + Random.Range(3, 5);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.tag.Equals("Player")) return;
            if (!(Game.player.State is PlayerMoveState)) return;

            _audioSource.clip = onCollisionSounds[Random.Range(0, onCollisionSounds.Length - 1)];
            _audioSource.Play();
            
            Game.player.SetState(new PlayerDamagedState(Game.player));
            Game.LooseHealth();
        }
    }
}