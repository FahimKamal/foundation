using Pancake.Scriptable;
using UnityEngine;

namespace Pancake.Sound
{
    [EditorIcon("script_mono")]
    public sealed class AudioManager : GameComponent
    {
        [Header("Sound Emitter Pool")] [SerializeField] private SoundEmitterPool pool;
        [SerializeField] private int initialSize = 10;

        [Header("Listening Channel")] [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to play SFXs")] [SerializeField]
        private AudioPlayEvent sfxPlayChannel;

        [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to stop SFXs")] [SerializeField]
        private AudioHandleEvent sfxStopChannel;

        [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to finish SFXs")] [SerializeField]
        private AudioHandleEvent sfxFinishChannel;

        [Space] [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to play Music")] [SerializeField]
        private AudioPlayEvent musicPlayChannel;

        [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to stop Music")] [SerializeField]
        private AudioHandleEvent musicStopChannel;

        [Header("Audio Control")] [SerializeField] private FloatVariable musicVolume;
        [SerializeField] private FloatVariable sfxVolume;

        private SoundEmitterVault _soundEmitterVault;
        private SoundEmitter _musicSoundEmitter;

        private void Awake()
        {
            //TODO: Get the initial volume levels from the settings
            _soundEmitterVault = new SoundEmitterVault();

            pool.Prewarm(initialSize);
            pool.SetParent(transform);
        }

        protected override void OnEnabled()
        {
            base.OnEnabled();
            sfxPlayChannel.OnRaised += PlayAudio;
            sfxStopChannel.OnRaised += StopAudio;
            sfxFinishChannel.OnRaised += FinishAudio;

            musicPlayChannel.OnRaised += PlayMusic;
            musicStopChannel.OnRaised += StopMusic;
        }

        protected override void OnDisabled()
        {
            base.OnDisabled();
            sfxPlayChannel.OnRaised -= PlayAudio;
            sfxStopChannel.OnRaised -= StopAudio;
            sfxFinishChannel.OnRaised -= FinishAudio;

            musicPlayChannel.OnRaised -= PlayMusic;
            musicStopChannel.OnRaised -= StopMusic;
        }

        /// <summary>
        /// Plays an AudioCue by requesting the appropriate number of SoundEmitters from the pool.
        /// </summary>
        public AudioHandle PlayAudio(Audio audio, AudioConfig settings, Vector3 position = default)
        {
            var clipsToPlay = audio.GetClips();
            var soundEmitterArray = new SoundEmitter[clipsToPlay.Length];

            int nOfClips = clipsToPlay.Length;
            for (int i = 0; i < nOfClips; i++)
            {
                soundEmitterArray[i] = pool.Request();
                if (soundEmitterArray[i] != null)
                {
                    soundEmitterArray[i].PlayAudioClip(clipsToPlay[i], settings, audio.loop, position);
                    if (!audio.loop) soundEmitterArray[i].OnCompleted += OnSoundEmitterFinishedPlaying;
                }
            }

            return _soundEmitterVault.Add(audio, soundEmitterArray);
        }

        public bool StopAudio(AudioHandle handle)
        {
            bool isFound = _soundEmitterVault.Get(handle, out SoundEmitter[] soundEmitters);

            if (isFound)
            {
                for (int i = 0; i < soundEmitters.Length; i++)
                {
                    StopAndCleanEmitter(soundEmitters[i]);
                }

                _soundEmitterVault.Remove(handle);
            }

            return isFound;
        }

        public bool FinishAudio(AudioHandle handle)
        {
            bool isFound = _soundEmitterVault.Get(handle, out SoundEmitter[] soundEmitters);

            if (isFound)
            {
                for (int i = 0; i < soundEmitters.Length; i++)
                {
                    soundEmitters[i].Finish();
                    soundEmitters[i].OnCompleted += OnSoundEmitterFinishedPlaying;
                }
            }
            else
            {
                Debug.LogWarning("Finishing an Audio was requested, but the Audio was not found.");
            }

            return isFound;
        }

        /// <summary>
        /// Only used by the timeline to stop the gameplay music during cutscenes.
        /// Called by the SignalReceiver present on this same GameObject.
        /// </summary>
        public void TimelineInterruptsMusic() { StopMusic(AudioHandle.invalid); }

        private AudioHandle PlayMusic(Audio audio, AudioConfig setting, Vector3 position)
        {
            const float fadeDuration = 2f;
            var startTime = 0f;

            if (_musicSoundEmitter != null && _musicSoundEmitter.IsPlaying())
            {
                AudioClip songToPlay = audio.GetClips()[0];
                if (_musicSoundEmitter.GetClip() == songToPlay) return AudioHandle.invalid;

                //Music is already playing, need to fade it out
                startTime = _musicSoundEmitter.FadeMusicOut(fadeDuration);
            }

            _musicSoundEmitter = pool.Request();
            _musicSoundEmitter.FadeMusicIn(audio.GetClips()[0], setting, 1f, startTime);
            _musicSoundEmitter.OnCompleted += StopMusicEmitter;

            return AudioHandle.invalid;
            //No need to return a valid key for music
        }

        private bool StopMusic(AudioHandle handle)
        {
            if (_musicSoundEmitter != null && _musicSoundEmitter.IsPlaying())
            {
                _musicSoundEmitter.Stop();
                return true;
            }

            return false;
        }

        private void OnSoundEmitterFinishedPlaying(SoundEmitter soundEmitter) { StopAndCleanEmitter(soundEmitter); }

        private void StopAndCleanEmitter(SoundEmitter soundEmitter)
        {
            if (!soundEmitter.IsLooping()) soundEmitter.OnCompleted -= OnSoundEmitterFinishedPlaying;

            soundEmitter.Stop();
            pool.Return(soundEmitter);

            //TODO: is the above enough?
            //_soundEmitterVault.Remove(audioCueKey); is never called if StopAndClean is called after a Finish event
            //How is the key removed from the vault?
        }

        private void StopMusicEmitter(SoundEmitter soundEmitter)
        {
            soundEmitter.OnCompleted -= StopMusicEmitter;
            pool.Return(soundEmitter);
        }
    }
}