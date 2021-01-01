using System;

using UnityEngine;

namespace BarthaSzabolcs.CommonUtility
{
    [Serializable]
    public class TempValue<T>
    {
        #region Datamembers

        #region Editor Settings

        [SerializeField] private Timer timer;

        #endregion
        #region Public Properties

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

        #endregion
        #region Backing Fields

        private T _value;

        #endregion

        #endregion


        #region Methods

        #region Public

        public void Init()
        {
            timer.OnTimeElapsed += () => _value = default;
        }

        public void Tick(float deltaTime)
        {
            timer.Tick(deltaTime);
        }

        public void Default()
        {
            _value = default;
        }
        
        #endregion
        
        #endregion
    }
}
