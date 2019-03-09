namespace SleepyYuzutan.WaveletTree
{

    /// <summary>
    /// ウェーブレット木の状態を表します
    /// </summary>
    public enum WaveletTreeStatus
    {
        /// <summary> 初期化されていません </summary>
        NotInitialized,
        /// <summary> 実行可能 </summary>
        Completion,
        /// <summary> エラー </summary>
        Error,
    }
}
