using System;

using UnityEngine;

namespace BarthaSzabolcs.CommonUtility
{
    [Serializable]
    public class Timer
    {
        #region Datamembers

        #region Events

        public event Action OnTimeElapsed;

        #endregion
        #region Enums

        /// <summary>
        /// Overflow is the state of the timer, when <see cref="ElapsedTime"/> >= <see cref="Interval"/>.
        /// </summary>
        public enum OverflowHandlingType 
        { 
            /// <summary>
            /// Does nothing on overflow.
            /// </summary>
            None,

            /// <summary>
            /// Will reset time to 0 on overflow.
            /// </summary>
            AutoReset,

            /// <summary>
            /// Clamps the time to <see cref="Interval"/> on overflow.
            /// </summary>
            Clamp
        };

        #endregion
        #region Editor Settings

        [SerializeField] private float _interval;

        [Tooltip("Overflow is the state of the timer, when ElapsedTime >= Interval." 
            + "\n"
            + "\nNone = Does nothing on overflow." 
            + "\nAutoReset = Will reset time to 0 on overflow."
            + "\nClamp = Clamps the time to Interval on overflow.")]
        [SerializeField] private OverflowHandlingType _overflowHandling;

        #endregion
        #region Public Properties

        public float Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                _interval = value;
            }
        }

        public OverflowHandlingType OverflowHandling
        {
            get
            {
                return _overflowHandling;
            }
            set
            {
                if (_overflowHandling != value)
                {
                    SetOverflowHanding(value);
                }
            }
        }

        public float ElapsedTime
        {
            get
            {
                return _elapsedTime;
            }
            private set
            {
                if (OnTimeElapsed != null && _elapsedTime < _interval && value > _interval)
                {
                    OnTimeElapsed.Invoke();
                }

                _elapsedTime = value;

                if (overflowHandlingAction != null)
                {
                    overflowHandlingAction.Invoke();
                }
            }
        }

        public bool Elapsed
        {
            get
            {
                return _elapsedTime >= _interval;
            }
        }

        public float ElapsedPercentage
        {
            get
            {
                return _elapsedTime / _interval;
            }
        }

        #endregion
        #region Backing Fields

        private float _elapsedTime;

        #endregion
        #region Private Fields

        private Action overflowHandlingAction;

        #endregion

        #endregion


        #region Methods

        #region Public

        /// <summary>
        /// Call this to properly initialize.
        /// </summary>
        /// <param name="startElapsed"></param>
        public void Init(bool startElapsed = false)
        {
            _elapsedTime = startElapsed ? _interval : 0;

            SetOverflowHanding(_overflowHandling);
        }

        /// <summary>
        /// Advances the timer by <paramref name="deltaTime"/>.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Tick(float deltaTime)
        {
            ElapsedTime += deltaTime;
        }

        /// <summary>
        /// Resets the timer to <paramref name="resetTime"/>.
        /// </summary>
        /// <param name="resetTime"></param>
        public void Reset(float resetTime = 0)
        {
            ElapsedTime = resetTime;
        }

        #endregion
        #region Private

        private void SetOverflowHanding(OverflowHandlingType value)
        {
            _overflowHandling = value;

            switch (value)
            {
                case OverflowHandlingType.AutoReset:
                    overflowHandlingAction = ResetOverflow;
                    break;

                case OverflowHandlingType.Clamp:
                    overflowHandlingAction = ClampOverflow;
                    break;

                default:
                    overflowHandlingAction = null;
                    break;
            }
        }

        private void ResetOverflow()
        {
            if (_elapsedTime >= _interval)
            {
                _elapsedTime %= _interval;
            }
        }

        private void ClampOverflow()
        {
            _elapsedTime = Mathf.Clamp(_elapsedTime, 0, _interval);
        }

        #endregion

        #endregion
    }
}