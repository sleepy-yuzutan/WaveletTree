namespace SleepyYuzutan.WaveletTree
{
    /// <summary>
    /// 列挙された値
    /// </summary>
    /// <seealso cref="WaveletTree{T}"/>
    /// <typeparam name="T">列挙された値の型</typeparam>
    internal class InnerWaveletTreeItem<T>
    {
        /// <summary> 位置 </summary>
        public int Index { get; set; }

        /// <summary> 値 </summary>
        public T Item { get; set; }

        /// <summary> 値の種類ごとの違い </summary>
        public int Id { get; set; }
    }
}
