using System;
using System.Collections;
using Pancake.Apex;
using Pancake.Scriptable;
using Pancake.Threading.Tasks;
using Spine.Unity;
using UnityEngine;

namespace Pancake.Spine
{
    public class TrackChainSpineGraphic : GameComponent
    {
        [SerializeField] private SkeletonGraphic skeleton;
        [SerializeField] private bool playOnAwake;
        [SerializeField] private bool loopLastestTrack;

        [SerializeField, Array] private TrackData[] datas;

        [Header("EVENT")] [SerializeField] private ScriptableEventNoParam playAnimationEvent;

        private IEnumerator _coroutine;

        private async void Awake()
        {
            await UniTask.WaitUntil(() => skeleton != null && skeleton.skeletonDataAsset != null);

            if (playOnAwake) Play();
        }

        protected override void OnEnabled()
        {
            if (playAnimationEvent != null) playAnimationEvent.OnRaised += Play;
        }

        protected override void OnDisabled()
        {
            if (_coroutine != null)
            {
#if UNITY_EDITOR
                // avoid case app be destroy soon than other component
                try
                {
#endif
                    App.StopCoroutine(_coroutine);
#if UNITY_EDITOR
                }
                catch (Exception)
                {
                    // ignored
                }
#endif
            }

            if (playAnimationEvent != null) playAnimationEvent.OnRaised -= Play;
        }

        private void Play()
        {
            _coroutine = IePlay();
            App.StartCoroutine(_coroutine);
        }


        private IEnumerator IePlay()
        {
            for (var i = 0; i < datas.Length; i++)
            {
                if (i == datas.Length - 1)
                {
                    skeleton.PlayOnly(datas[i].trackName, loopLastestTrack);
                }
                else
                {
                    uint count = datas[i].loopCount;
                    while (count > 0)
                    {
                        skeleton.PlayOnly(datas[i].trackName, count != 1);
                        if (datas[i].overrideDuration)
                        {
                            yield return new WaitForSeconds(datas[i].duration);
                        }
                        else
                        {
                            string trackName = datas[i].trackName;
                            yield return new WaitForSeconds(skeleton.Duration(trackName));
                        }

                        count--;
                    }
                }
            }
        }
    }
}