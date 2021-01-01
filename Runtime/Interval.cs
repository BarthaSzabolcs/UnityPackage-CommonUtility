using System;

using UnityEngine;

namespace BarthaSzabolcs.CommonUtility
{
    [Serializable]
    public struct Interval<T> where T : IComparable
    {
        #region Datamembers

        #region Editor Settings

        [SerializeField] private T _min;
        [SerializeField] private T _max;
        [SerializeField] private bool _inclusive;

        #endregion
        #region Public Properties

        public T Min
        {
            get
            {
                return _min;
            }
        }

        public T Max
        {
            get
            {
                return _max;
            }
        }

        public bool Inclusive
        {
            get
            {
                return _inclusive;
            }
        }

        #endregion

        #endregion


        #region Methods

        #region Public

        /// <summary>
        /// Check if the given value is inside the interval or not.
        /// <remarks>
        /// <para>
        /// Does consider <see cref="Inclusive"/>.
        /// </para>
        /// </remarks>
        /// </summary>
        public bool Contains(T value)
        {
            if (_inclusive)
            {
                return _min.CompareTo(value) <= 0 && _max.CompareTo(value) >= 0;
            }
            else
            {
                return _min.CompareTo(value) < 0 && _max.CompareTo(value) > 0;
            }
        }

        /// <summary>
        /// Clamps the value.
        /// <remarks>
        /// <para>
        /// Does not consider <see cref="Inclusive"/>.
        /// </para>
        /// </remarks>
        /// </summary>
        public T Clamp(T value)
        {
            if (_min.CompareTo(value) >= 0)
            {
                return _min;
            }
            else if(_max.CompareTo(value) <= 0)
            {
                return _max;
            }
            else
            {
                return value;
            }
        }

        #endregion

        #endregion
    }
}
