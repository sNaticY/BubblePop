
    using System;
    using System.Collections.Generic;
    using Entitas;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class PlayAudioSystem: ReactiveSystem<GameEntity>, IInitializeSystem, ICleanupSystem
    {
        private readonly GameContext _gameContext;
        private readonly InputContext _inputContext;
        
        public PlayAudioSystem(Contexts contexts) : base(contexts.game)
        {
            _gameContext = contexts.game;
            _inputContext = contexts.input;
        }
        
        public void Initialize()
        {
            _gameContext.ReplaceAudioSource(GameObject.Find("Main Camera").GetComponent<AudioSource>());
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.PlayAudio);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.playAudio.Value != AudioType.None;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            if (entities.Count == 0 || !_inputContext.isSoundOn) return;
            foreach (var entity in entities)
            {
                var audioSource = _gameContext.audioSource.Value;
                if(audioSource.clip != _gameContext.settings.Value.Explode2K && audioSource.clip != _gameContext.settings.Value.Perfect)
                    audioSource.Stop();
                switch (entity.playAudio.Value)
                {
                    case AudioType.Transition:
                        var transitionClips = _gameContext.settings.Value.TransitionAudioClips;
                        audioSource.clip = transitionClips[Random.Range(0, transitionClips.Count)];
                        audioSource.Play();
                        break;
                    case AudioType.Bubble:
                        if(audioSource.isPlaying) break;
                        var pops = _gameContext.settings.Value.BubbleAudioClips;
                        audioSource.clip = pops[Random.Range(0, pops.Count)];
                        audioSource.Play();
                        break;
                    case AudioType.Explode2K:
                        audioSource.clip = _gameContext.settings.Value.Explode2K;
                        audioSource.Play();
                        break;
                    case AudioType.Perfect:
                        audioSource.clip = _gameContext.settings.Value.Perfect;
                        audioSource.Play();
                        break;
                }
            }
        }


        public void Cleanup()
        {
            var entities = _gameContext.GetEntities(GameMatcher.PlayAudio);
            for (var i = entities.Length - 1; i >= 0; i--)
            {
                var entity = entities[i];
                entity.Destroy();
            }
        }
    }
