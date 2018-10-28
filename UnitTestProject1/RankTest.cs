using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletTreeNS;

namespace UnitTestProject1
{
    [TestClass]
    public class RankTest
    {
        private string _testString1 = "abcdeabcdeaaaaabbbbcccdde";
        private string[] _testStringArray1 = "that that is is that that is not is not is that it it is".Split(' ');
        private string[] _testStringArray2 = { "", "", "", "", "", null, "", };

        /// <summary>
        /// <c>testString1</c>から<c>'a'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_1()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('a');
            Assert.IsTrue(count == 7);
        }

        /// <summary>
        /// <c>testString1</c>から<c>'b'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_2()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('b');
            Assert.IsTrue(count == 6);
        }

        /// <summary>
        /// <c>testString1</c>から<c>'c'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_3()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('c');
            Assert.IsTrue(count == 5);
        }

        /// <summary>
        /// <c>testString1</c>から<c>'d'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_4()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('d');
            Assert.IsTrue(count == 4);
        }

        /// <summary>
        /// <c>testString1</c>から<c>'e'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_5()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('e');
            Assert.IsTrue(count == 3);
        }

        /// <summary>
        /// <c>testString1</c>から<c>'g'</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod1_6()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('g');
            Assert.IsTrue(count == 0);
        }

        /// <summary>
        /// <c>testStringArray1</c>から<c>"that"</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod2_1()
        {
            var obj = WaveletTree.Create(_testStringArray1);
            var count = obj.Rank("that");
            Assert.IsTrue(count == 5);
        }

        /// <summary>
        /// <c>testStringArray1</c>から<c>"eat"</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod2_2()
        {
            var obj = WaveletTree.Create(_testStringArray1);
            var count = obj.Rank("eat");
            Assert.IsTrue(count == 0);
        }

        /// <summary>
        /// <c>testStringArray1</c>から<c>null</c>を探す
        /// </summary>
        [TestMethod]
        public void TestMethod2_3()
        {
            var obj = WaveletTree.Create(_testStringArray1);
            Assert.ThrowsException<ArgumentNullException>(() => obj.Rank(null));
        }

        /// <summary>
        /// <c>testStringArray2</c>をウェーブレット木に変換
        /// </summary>
        [TestMethod]
        public void TestMethod3_1()
        {
            Assert.ThrowsException<ArgumentNullException>(() => WaveletTree.Create(_testStringArray2));
        }
    }
}
