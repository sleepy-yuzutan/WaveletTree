using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SleepyYuzutan.WaveletTree;

namespace WaveletTreeTest
{
    [TestClass]
    public class SelectTest
    {
        private string _testString1 = "abcdeabcdeaaaaabbbbcccdde";
        private string[] _testStringArray1 = "that that is is that that is not is not is that it it is".Split(' ');

        /// <summary>
        /// <c>testString1</c>から<c>'a'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_1()
        {
            var obj = WaveletTree.Create(_testString1);
            var pos = obj.Select('a');
            Assert.IsTrue(pos == 0);
        }

        /// <summary>
        /// <c>testString1</c>から<c>'b'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_2()
        {
            var obj = WaveletTree.Create(_testString1);
            var pos = obj.Select('b');
            Assert.IsTrue(pos == 1);
        }

        /// <summary>
        /// <c>testString1</c>から<c>'c'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_3()
        {
            var obj = WaveletTree.Create(_testString1);
            var pos = obj.Select('c');
            Assert.IsTrue(pos == 2);
        }

        /// <summary>
        /// <c>testString1</c>から<c>'d'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_4()
        {
            var obj = WaveletTree.Create(_testString1);
            var pos = obj.Select('d');
            Assert.IsTrue(pos == 3);
        }

        /// <summary>
        /// <c>testString1</c>から<c>'e'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_5()
        {
            var obj = WaveletTree.Create(_testString1);
            var pos = obj.Select('e');
            Assert.IsTrue(pos == 4);
        }

        /// <summary>
        /// <c>testString1</c>から<c>'g'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_6()
        {
            var obj = WaveletTree.Create(_testString1);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => obj.Select('g'));
        }

        /// <summary>
        /// <c>testString1</c>から<c>'a'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_7()
        {
            var obj = WaveletTree.Create(_testString1);
            var pos = obj.Select('a', 1);
            Assert.IsTrue(pos == 5);
        }

        /// <summary>
        /// <c>testString1</c>から<c>'b'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_8()
        {
            var obj = WaveletTree.Create(_testString1);
            var pos = obj.Select('b', 3);
            Assert.IsTrue(pos == 16);
        }

        /// <summary>
        /// <c>testString1</c>から<c>'c'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_9()
        {
            var obj = WaveletTree.Create(_testString1);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => obj.Select('c', 50));
        }

        /// <summary>
        /// <c>testStringArray1</c>から<c>"that"</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod2_1()
        {
            var obj = WaveletTree.Create(_testStringArray1);
            var pos = obj.Select("that");
            Assert.IsTrue(pos == 0);
        }

        /// <summary>
        /// <c>testStringArray1</c>から<c>"eat"</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod2_2()
        {
            var obj = WaveletTree.Create(_testStringArray1);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => obj.Select("eat"));
        }

        /// <summary>
        /// <c>testStringArray1</c>から<c>null</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod2_3()
        {
            var obj = WaveletTree.Create(_testStringArray1);
            Assert.ThrowsException<ArgumentNullException>(() => obj.Select(null));
        }
    }
}
