using System;
using UnityEngine;

namespace Pancake.MobileInput
{
    [EditorIcon("scriptable_input")]
    [CreateAssetMenu(fileName = "scriptable_input_on_finger_down.asset", menuName = "Pancake/Input/Events/on finger down")]
    public class ScriptableInputFingerDown : ScriptableInput
    {
        private Action<Vector3> _onRaised;

        /// <summary>
        /// Action raised when this event is raised.
        /// </summary>
        public event Action<Vector3> OnRaised { add => _onRaised += value; remove => _onRaised -= value; }

        /// <summary>
        /// Raise the event
        /// </summary>
        internal void Raise(Vector3 position)
        {
            if (!Application.isPlaying) return;
            _onRaised?.Invoke(position);
        }
    }
}