using System;
using System.Collections.Generic;
using System.Linq;

namespace WaveletTreeNS
{
    /// <summary>
    /// ウェーブレット木を表します
    /// </summary>
    /// <typeparam name="T">列挙される値の型</typeparam>
    public class WaveletTree<T>
    {
        #region Private variables

        /// <summary> 出現する値と符号 </summary>
        private Dictionary<T, int> _dist { get; set; }
        /// <summary> 葉までの深さ </summary>
        private int _depth { get; set; }

        #endregion

        #region Public properties

        /// <summary> 左の子節 </summary>
        public WaveletTree<T> Left { get; private set; }
        /// <summary> 右の子節 </summary>
        public WaveletTree<T> Right { get; private set; }
        /// <summary> 割り当てられている列挙可能な値 </summary>
        public IEnumerable<T> Value { get; private set; }
        /// <summary> 割り当てられている列挙可能な値と符号 </summary>
        public IEnumerable<ValueTuple<T, int>> Pairs { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// ウェーブレット木を作成します
        /// </summary>
        /// <param name="value">列挙可能な値</param>
        public WaveletTree(IEnumerable<T> value)
        {
            // 値の種類ごとに符号をつける
            var dist = value
                .Distinct()
                .Select((item, index) => (index, item))
                .ToDictionary((item) => item.item, (item) => item.index);

            // 符号と実際の値をペアにする
            var pairs = value
                .Select((item) => (item, dist[item]));

            // 葉までの深さを計算する
            var depth = (int)Math.Ceiling(Math.Log(dist.Count(), 2));

            // フィールドに格納
            this._dist = dist;
            this._depth = depth;
            this.Value = value;
            this.Pairs = pairs;

            // 子節の計算
            if (dist.Count() != 1)
            {
                // 符号の最上位ビットを見て左右に分割
                this.Left = new WaveletTree<T>(
                    pairs
                        .Where((item) => (item.Item2 & (1 << (depth - 1))) == 0)
                        .Select((item) => item.item)
                        .ToArray()
                    );
                this.Right = new WaveletTree<T>(
                    pairs
                        .Where((item) => (item.Item2 & (1 << (depth - 1))) != 0)
                        .Select((item) => item.item)
                        .ToArray()
                    );
            }
            else
            {
                // 葉になるため子を持たない
                this.Left = this.Right = null;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 指定された値の出現回数を取得します
        /// </summary>
        /// <param name="value">数える値</param>
        /// <returns>出現回数</returns>
        /// <exception cref="ArgumentNullException"><c>value</c> is <c>null</c>.</exception>
        public int Rank(T value) => _dist.ContainsKey(value) ? this._RankInner(value) : 0;

        #endregion

        #region Private methods

        /// <summary>
        /// 指定された値の出現回数を取得します
        /// </summary>
        /// <param name="value">数える値</param>
        /// <returns>出現回数</returns>
        /// <exception cref="ArgumentNullException"><c>value</c> is <c>null</c>.</exception>
        /// <exception cref="KeyNotFoundException">
        /// The property is retrieved and<c>value</c> does not exist in the collection.
        /// </exception>
        private int _RankInner(T value) =>
            this._depth == 0 ?
            this.Pairs.Count() :
            (((_dist[value] & (1 << (_depth - 1))) == 0) ? this.Left : this.Right).Rank(value);

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
