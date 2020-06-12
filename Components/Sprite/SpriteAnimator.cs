using System;
using System.Collections.Generic;

namespace Zen.Components
{
    public class SpriteAnimator : SpriteRenderer, IUpdatable
    {
        public enum LoopMode
        {
            Loop,
            FreezeAtLastFrame,
        }

        public event Action OnAnimationFinish;
        SpriteAtlas _atlas;
        Dictionary<string, SpriteAnimation> _animations = new Dictionary<string, SpriteAnimation>();
        public SpriteAnimation CurrentAnimation;
        LoopMode _loopMode;
        float _elapsedTime;
        string _currentAnimationName;
        bool _animationActive;

        public SpriteAnimator(SpriteAtlas atlas)
        {
            _atlas = atlas;

            for (var i = 0; i < atlas.AnimationNames.Length; i++)
                _animations.Add(atlas.AnimationNames[i], atlas.SpriteAnimations[i]);

            CurrentAnimation = _animations[atlas.AnimationNames[0]];
            SetSprite(CurrentAnimation.Sprites[0]);
        }

        public void Play(string name, LoopMode? loopMode = null)
        {
            _animationActive = true;
            CurrentAnimation = _animations[name];
            SetSprite(CurrentAnimation.Sprites[0]);
            _elapsedTime = 0;
            _loopMode = loopMode ?? LoopMode.Loop;
            _currentAnimationName = name;
        }

        public void Update()
        {
            if (!_animationActive)
                return;

            float secondsPerFrame = 1 / (CurrentAnimation.FrameRate);
            float iterationDuration = secondsPerFrame * CurrentAnimation.Sprites.Length;
            _elapsedTime += Time.DeltaTime;

            if (_loopMode == LoopMode.Loop && _elapsedTime > iterationDuration)
            {
                OnAnimationFinish?.Invoke();
                _animationActive = false;
                Play(_currentAnimationName, _loopMode);
                return;
            }
            else if (_loopMode == LoopMode.FreezeAtLastFrame && _elapsedTime > iterationDuration)
            {
                OnAnimationFinish?.Invoke();
                _animationActive = false;
                return;
            }

            CurrentAnimation.CurrentFrame = (int)(_elapsedTime / secondsPerFrame);
            SetSprite(CurrentAnimation.Sprites[CurrentAnimation.CurrentFrame]);
        }
    }
}