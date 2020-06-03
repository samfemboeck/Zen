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

        SpriteAtlas _atlas;
        Dictionary<string, SpriteAnimation> _animations = new Dictionary<string, SpriteAnimation>();
        SpriteAnimation _currentAnimation;
        LoopMode _loopMode;
        float _elapsedTime;
        string _currentAnimationName;


        public SpriteAnimator(SpriteAtlas atlas)
        {
            _atlas = atlas;

            for (var i = 0; i < atlas.AnimationNames.Length; i++)
                _animations.Add(atlas.AnimationNames[i], atlas.SpriteAnimations[i]);

            Play(atlas.AnimationNames[0]);
        }

        public void Play(string name, LoopMode? loopMode = null)
        {
            _currentAnimation = _animations[name];
            Sprite = _currentAnimation.Sprites[0];
            _elapsedTime = 0;
            _loopMode = loopMode ?? LoopMode.Loop;
            _currentAnimationName = name;
        }

        public void Update()
        {
            float secondsPerFrame = 1 / (_currentAnimation.FrameRate);
            float iterationDuration = secondsPerFrame * _currentAnimation.Sprites.Length;
            _elapsedTime += Time.DeltaTime;

            if (_loopMode == LoopMode.Loop && _elapsedTime > iterationDuration)
            {
                Play(_currentAnimationName, _loopMode);
            }
            else if (_loopMode == LoopMode.FreezeAtLastFrame && _elapsedTime > iterationDuration)
            {
                _elapsedTime -= Time.DeltaTime;
            }

            int currentFrame = (int)(_elapsedTime / secondsPerFrame);
            Sprite = _currentAnimation.Sprites[currentFrame];
        }
    }
}