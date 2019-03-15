using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SleepyYuzutan.WaveletTree;

namespace WaveletTreeTest
{
    [TestClass]
    public class RankTest
    {
        private string _testString1 = "abcdeabcdeaaaaabbbbcccdde";
        private string[] _testStringArray1 = "that that is is that that is not is not is that it it is".Split(' ');
        private string[] _testStringArray2 = { "", "", "", "", "", null, "", };

        /// <summary>
        /// <c>testString1</c>����<c>'a'</c>��T��
        /// </summary>
        [TestMethod]
        public void TestMethod1_1()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('a');
            Assert.IsTrue(count == 7);
        }

        /// <summary>
        /// <c>testString1</c>����<c>'b'</c>��T��
        /// </summary>
        [TestMethod]
        public void TestMethod1_2()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('b');
            Assert.IsTrue(count == 6);
        }

        /// <summary>
        /// <c>testString1</c>����<c>'c'</c>��T��
        /// </summary>
        [TestMethod]
        public void TestMethod1_3()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('c');
            Assert.IsTrue(count == 5);
        }

        /// <summary>
        /// <c>testString1</c>����<c>'d'</c>��T��
        /// </summary>
        [TestMethod]
        public void TestMethod1_4()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('d');
            Assert.IsTrue(count == 4);
        }

        /// <summary>
        /// <c>testString1</c>����<c>'e'</c>��T��
        /// </summary>
        [TestMethod]
        public void TestMethod1_5()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('e');
            Assert.IsTrue(count == 3);
        }

        /// <summary>
        /// <c>testString1</c>����<c>'g'</c>��T��
        /// </summary>
        [TestMethod]
        public void TestMethod1_6()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('g');
            Assert.IsTrue(count == 0);
        }

        /// <summary>
        /// <c>testString1</c>����<c>'a'</c>��T�� (10�����ڂ܂�)
        /// </summary>
        [TestMethod]
        public void TestMethod1_7()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('a', 10);
            Assert.IsTrue(count == 2);
        }

        /// <summary>
        /// <c>testString1</c>����<c>'b'</c>��T�� (20�����ڂ܂�)
        /// </summary>
        [TestMethod]
        public void TestMethod1_8()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('b', 20);
            Assert.IsTrue(count == 6);
        }

        /// <summary>
        /// <c>testString1</c>����<c>'c'</c>��T�� (50�����ڂ܂�)
        /// </summary>
        [TestMethod]
        public void TestMethod1_9()
        {
            var obj = WaveletTree.Create(_testString1);
            var count = obj.Rank('c', 50);
            Assert.IsTrue(count == 5);
        }

        /// <summary>
        /// <c>testStringArray1</c>����<c>"that"</c>��T��
        /// </summary>
        [TestMethod]
        public void TestMethod2_1()
        {
            var obj = WaveletTree.Create(_testStringArray1);
            var count = obj.Rank("that");
            Assert.IsTrue(count == 5);
        }

        /// <summary>
        /// <c>testStringArray1</c>����<c>"eat"</c>��T��
        /// </summary>
        [TestMethod]
        public void TestMethod2_2()
        {
            var obj = WaveletTree.Create(_testStringArray1);
            var count = obj.Rank("eat");
            Assert.IsTrue(count == 0);
        }

        /// <summary>
        /// <c>testStringArray1</c>����<c>null</c>��T��
        /// </summary>
        [TestMethod]
        public void TestMethod2_3()
        {
            var obj = WaveletTree.Create(_testStringArray1);
            Assert.ThrowsException<ArgumentNullException>(() => obj.Rank(null));
        }

        /// <summary>
        /// <c>testStringArray2</c>��E�F�[�u���b�g�؂ɕϊ�
        /// </summary>
        [TestMethod]
        public void TestMethod3_1()
        {
            Assert.ThrowsException<ArgumentNullException>(() => WaveletTree.Create(_testStringArray2));
        }
    }
}
