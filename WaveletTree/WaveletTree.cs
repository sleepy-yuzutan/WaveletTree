#nullable enable

using System;
using System.Collections.Generic;
using SleepyYuzutan.IWaveletTree;

namespace SleepyYuzutan.WaveletTree
{
    /// <summary>
    /// ウェーブレット木を表します
    /// </summary>
    /// <typeparam name="T">列挙される値の型</typeparam>
    public class WaveletTree<T> : IWaveletTree<T>
    {
        #region Private Property

        /// <summary> ウェーブレット木本体 </summary>
        private InnerWaveletTree<T>? _body { get; set; } = null;

        /// <summary> ウェーブレット木操作用 </summary>
        private InnerWaveletTree<T> _bodyWrapper
        {
            get
            {
                switch (this.Status)
                {
                    case WaveletTreeStatus.NotInitialized:
                        throw new Exception("WaveletTree is not initialized.");
                    case WaveletTreeStatus.Completion:
                        return this._body ?? throw new Exception("WaveletTree is not initialized.");
                    default:
                        throw new Exception("Occurred exception in WaveletTree.");
                }
            }
            set => this._body = value;
        }

        #endregion

        #region Public Property

        public WaveletTreeStatus Status { get; private set; } = WaveletTreeStatus.NotInitialized;

        #endregion

        #region ctor

        /// <summary>
        /// ウェーブレット木を作成します
        /// </summary>
        /// <param name="value">列挙可能な値</param>
        public WaveletTree(IEnumerable<T> value)
        {
            try
            {
                this._body = new InnerWaveletTree<T>(value);
            }
            finally
            {
                this.Status = this._body != null ? WaveletTreeStatus.Completion : WaveletTreeStatus.Error;
            }
        }

        #endregion

        #region Public Method

        public int Rank(T value) => _bodyWrapper.Rank(value);
 
        public int Rank(T value, int pos) => _bodyWrapper.Rank(value, pos);

        public int Select(T value) => _bodyWrapper.Select(value);

        public int Select(T value, int index) => _bodyWrapper.Select(value, index);

        #endregion
    }

    /// <summary>
    /// ウェーブレット木に対する汎用的な操作を提供します
    /// </summary>
    public static class WaveletTree
    {
        /// <summary>
        /// ウェーブレット木を作成します
        /// </summary>
        /// <typeparam name="T">列挙される値の型</typeparam>
        /// <param name="value">列挙可能な値</param>
        /// <returns>ウェーブレット木</returns>
        public static WaveletTree<T> Create<T>(IEnumerable<T> value) => new WaveletTree<T>(value);
    }
}


