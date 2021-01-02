using System;

using UnityEngine;

namespace BarthaSzabolcs.CommonUtility
{
    /// <summary>
    /// Stores a temporary value for <see cref="LifeTime"/> seconds, 
    /// resets to the default value after.
    /// </summary>
    [Serializable]
    public class TempValue<T>
    {
        #region Datamembers

        #region Editor Settings


        [SerializeField] private float _lifeTime;

        #endregion
        #region Public Properties

        /// <summary>
        /// The value itself.
        /// <remarks>
        /// <para>
        /// Setting the value will reset it's life time.
        /// </para>
        /// </remarks>
        /// </summary>
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                timer.Reset();
            }
        }

        /// <summary>
        /// How long will the value be stored in seconds.
        /// </summary>
        public float LifeTime
        {
            get
            {
                return _lifeTime;
            }
            set
            {
                if (_lifeTime != value)
                {
                    _lifeTime = value;
                    timer.Interval = value;
                }
            }
        }

        #endregion
        #region Backing Fields

        private T _value;

        #endregion
        #region Private Fields

        private Timer timer;

        #endregion

        #endregion


        #region Methods

        #region Public

        /// <summary>
        /// Call this to properly initialize.
        /// </summary>
        public void Init()
        {
            timer = new Timer()
            {
                Interval = _lifeTime,
                OverflowHandling = Timer.OverflowHandlingType.None,
                
            };

            timer.OnTimeElapsed += () =>
            {
                _value = default;
            };
        }

        /// <summary>
        /// Advances the timer by <paramref name="deltaTime"/>.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Tick(float deltaTime)
        {
            timer.Tick(deltaTime);
        }

        /// <summary>
        /// Reset the value without reseting it's life time.
        /// </summary>
        public void Default()
        {
            _value = default;
        }
        
        #endregion
        
        #endregion
    }
}
