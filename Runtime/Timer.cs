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

        public enum OverflowHandlingType { None, AutoReset, Clamp };

        #endregion
        #region Editor Settings

        [SerializeField] private float _interval;
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

        public Timer() { }

        public Timer(float interval, bool startElapsed, OverflowHandlingType overflowHandlingType)
        {
            _interval = interval;
            if (startElapsed)
            {
                _elapsedTime = _interval;
            }

            SetOverflowHanding(overflowHandlingType);
        }

        public void Tick(float deltaTime)
        {
            ElapsedTime += deltaTime;
        }

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