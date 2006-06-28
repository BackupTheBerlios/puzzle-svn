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
        public void Deserialize_2D_ArrayObject()
        {
            int z = 0;

            ArrayObject arr = new ArrayObject();
            arr.ID = 0;
            arr.Type = typeof(int[,]);

            ObjectBase[,] items = new ObjectBase[4,4];

           for(int x=0;x<4;x++)
                for(int y=0;y<4;y++)
                    items[x,y] = GetIntegerValueObject(z++);

            arr.Items = items;

            int[,] res = (int[,])arr.GetValue ();

            z = 0;
            for(int x=0;x<4;x++)
                for(int y=0;y<4;y++)
                {
                    Assert.AreEqual(z,res[x,y]);
                    z++;
                }
        }

        [TestMethod]
        public void Deserialize_Jagged_ArrayObject()
        {
            ArrayObject arr = new ArrayObject();
            arr.ID = 0;
            arr.Type = typeof(int[][]);

            ArrayObject[] arrObjects = new ArrayObject[4];
            arrObjects[0] = GetIntegerArrayObject();
            arrObjects[1] = GetIntegerArrayObject();
            arrObjects[2] = GetIntegerArrayObject();
            arrObjects[3] = GetIntegerArrayObject();

            arr.Items = new ObjectBase[4] {arrObjects[0],arrObjects[1],arrObjects[2],arrObjects[3]};

            

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

            int[] ints = new int[4];
            for (int i=0;i<4;i++)
                ints[i] = i;

            arr.Items = new ObjectBase[4] { GetIntegerValueObject(0), GetIntegerValueObject(1), GetIntegerValueObject(2), GetIntegerValueObject(3) };
           
            return arr;
        }
    }
}
