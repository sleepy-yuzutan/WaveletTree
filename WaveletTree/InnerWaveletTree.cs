#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepyYuzutan.WaveletTree
{
    /// <summary>
    /// <see cref="WaveletTree"/> の内部クラス
    /// </summary>
    /// <typeparam name="T">列挙する値の型</typeparam>
    internal class InnerWaveletTree<T>
    {

        #region Private variables

        /// <summary> 出現する値と符号 </summary>
        private Dictionary<T, int> _ids { get; set; }

        /// <summary> 葉までの深さ </summary>
        private int _depth { get; set; }

        /// <summary> 左の子節に持っている情報 </summary>
        private List<InnerWaveletTreeItem<T>>? _leftPairs { get; set; }

        /// <summary> 右の子節に持っている情報 </summary>
        private List<InnerWaveletTreeItem<T>>? _rightPairs { get; set; }

        #endregion

        #region Public properties

        /// <summary> 左の子節 </summary>
        public InnerWaveletTree<T>? Left { get; private set; }

        /// <summary> 右の子節 </summary>
        public InnerWaveletTree<T>? Right { get; private set; }

        /// <summary> 割り当てられている列挙可能な値 </summary>
        public IEnumerable<T> Value { get; private set; }

        /// <summary> 割り当てられている列挙可能な値と符号 </summary>
        public IEnumerable<InnerWaveletTreeItem<T>> Pairs { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// ウェーブレット木を作成します
        /// </summary>
        /// <param name="value">列挙可能な値</param>
        public InnerWaveletTree(IEnumerable<T> value)
        {
            // 値の種類ごとに符号をつける
            var ids = value
                .Distinct()
                .Select((item, index) => (index, item))
                .ToDictionary((item) => item.item, (item) => item.index);

            // インデックスと符号と実際の値をペアにする
            var pairs = value
                .Select((item, index) => new InnerWaveletTreeItem<T> { Index = index, Item = item, Id = ids[item], });

            // 葉までの深さを計算する
            var depth = (int)Math.Ceiling(Math.Log(ids.Count(), 2));

            // フィールドに格納
            this._ids = ids;
            this._depth = depth;
            this.Value = value;
            this.Pairs = pairs;

            // 子節の計算
            if (ids.Count() != 1)
            {
                // 符号の最上位ビットを見て左右に分割
                bool isLeft(int value) => (value & (1 << (depth - 1))) == 0;

                // 左右の子節を算出
                this._leftPairs = pairs
                    .Where((item) => isLeft(item.Id))
                    .ToList();
                this._rightPairs = pairs
                    .Where((item) => !isLeft(item.Id))
                    .ToList();

                // 子節を実際に作成
                this.Left = new InnerWaveletTree<T>(this._leftPairs.Select((item) => item.Item));
                this.Right = new InnerWaveletTree<T>(this._rightPairs.Select((item) => item.Item));
            }
            else
            {
                // 葉になるため子を持たない
                this._leftPairs = this._rightPairs = null;
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
        public int Rank(T value) => this.Rank(value, this.Pairs.Count());

        /// <summary>
        /// 指定された値の出現回数を取得します
        /// </summary>
        /// <param name="value">数える値</param>
        /// <param name="pos">数え終える位置</param>
        /// <returns>出現回数</returns>
        /// <exception cref="ArgumentNullException"><c>value</c> is <c>null</c>.</exception>
        public int Rank(T value, int pos) => _ids.ContainsKey(value) ? this._RankInner(value, pos) : 0;

        /// <summary>
        /// 指定された値が最初に出現する位置を取得します
        /// </summary>
        /// <param name="value">位置を取得する値</param>
        /// <returns>位置</returns>
        public int Select(T value) => this.Select(value, 0);

        /// <summary>
        /// 指定された値がn回目に出現する位置を取得します
        /// </summary>
        /// <param name="value">位置を取得する値</param>
        /// <param name="index">n</param>
        /// <returns>位置</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of tree values.</exception>
        public int Select(T value, int index) => _ids.ContainsKey(value) ? this._SelectInner(value, index) : throw new ArgumentOutOfRangeException();

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
        private int _RankInner(T value, int pos) =>
            this._depth == 0 ?
                this.Pairs.Where((item) => item.Index < pos).Count() :
            this._CheckTop(value) ?
                this.Right?.Rank(value, this._rightPairs?.Where((item) => item.Index < pos).Count() ?? 0) ?? 0 :
                this.Left?.Rank(value, this._leftPairs?.Where((item) => item.Index < pos).Count() ?? 0) ?? 0;

        /// <summary>
        /// 指定された値がn番目に出現する位置を取得します
        /// </summary>
        /// <param name="value">位置を取得する値</param>
        /// <param name="index">n</param>
        /// <returns>位置</returns>
        private int _SelectInner(T value, int index) =>
            this._depth == 0 ?
                (0 <= index && index <= this.Pairs.Count()) ? index : throw new ArgumentOutOfRangeException() :
            this._CheckTop(value) ?
                this._rightPairs?[this.Right?.Select(value, index) ?? 0].Index ?? 0 :
                this._leftPairs?[this.Left?.Select(value, index) ?? 0].Index ?? 0;

        /// <summary>
        /// 最上位ビットを見て0か1を判定します
        /// </summary>
        /// <param name="value">検査する値</param>
        /// <returns><c>true</c>: 1, <c>false</c>: 0</returns>
        private bool _CheckTop(T value) => (this._ids[value] & (1 << (this._depth - 1))) != 0;

        #endregion
    }
}
