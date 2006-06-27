using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Puzzle.NCore.Runtime.Serialization;

namespace NCoreMain.Serialization
{
    [TestClass]
    public class XmlSerializerTest
    {
        public XmlSerializerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        [TestMethod]
        public void DeserializeValueObject()
        {
            ValueObject obj = GetIntegerValueObject(123456);

            int res = (int)obj.GetValue();

            Assert.AreEqual(123456,res);
        }

        private static ValueObject GetIntegerValueObject(int value)
        {
            ValueObject obj = new ValueObject();
            obj.ID = 0;
            obj.Type = typeof(int);
            obj.Value = value.ToString();
            return obj;
        }

        [TestMethod]
        public void Deserialize_1D_ArrayObject()
        {
            ArrayObject arr = GetIntegerArrayObject();

            int[] res = (int[])arr.GetValue();

            for (int i=0;i<res.Length;i++)
            {
                Assert.AreEqual(i,res[i]);
            }
        }

        [TestMethod]
        public void Deserialize_Jagged_ArrayObject()
        {
            ArrayObject arr = new ArrayObject();
            arr.ID = 0;
            arr.Type = typeof(int[][]);
            arr.Items.Add (GetIntegerArrayObject());
            arr.Items.Add (GetIntegerArrayObject());
            arr.Items.Add (GetIntegerArrayObject());
            arr.Items.Add (GetIntegerArrayObject());

            int[][] res = (int[][])arr.GetValue ();

            for (int i=0;i<4;i++)
            {
                for (int j=0;j<4;j++)
                {
                    Assert.AreEqual(j,res[i][j]);
                }
            }
        }

        private static ArrayObject GetIntegerArrayObject()
        {
            ArrayObject arr = new ArrayObject();
            arr.ID = 0;
            arr.Type = typeof(int[]);
            arr.Items.Add(GetIntegerValueObject(0));
            arr.Items.Add(GetIntegerValueObject(1));
            arr.Items.Add(GetIntegerValueObject(2));
            arr.Items.Add(GetIntegerValueObject(3));
            return arr;
        }
    }
}
