namespace SleepyYuzutan.IWaveletTree
{
    public interface IWaveletTree<T>
    {
        int Rank(T value);
        int Rank(T value, int pos);
    }
}
