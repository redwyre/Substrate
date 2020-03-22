using System;
using System.Collections.Generic;
using System.Text;
using Substrate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Substrate.Core;
using Substrate.Nbt;
using System.IO;
using System.Diagnostics;
using Substrate.Source.Nbt;

namespace Substrate.Tests
{
    [TestClass]
    public class LevelTests
    {
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            NbtVerifier.UnexpectedTag += new VerifierEventHandler((TagEventArgs e) =>
            {
                var fullName = e.Schema.Name + "." + e.TagName;

                Trace.WriteLine($"UnexpectedTag {fullName}");

                return TagEventCode.NEXT;
            });
        }

        NbtTree LoadLevelTree(string path)
        {
            NBTFile nf = new NBTFile(path);
            NbtTree tree = null;

            using (Stream nbtstr = nf.GetDataInputStream())
            {
                if (nbtstr == null)
                {
                    return null;
                }

                tree = new NbtTree(nbtstr);
            }

            return tree;
        }

        [TestMethod]
        public void LoadTreeTest_1_6_4_survival()
        {
            NbtTree levelTree = LoadLevelTree(TestHelper.DataPath(@"1_6_4-survival\level.dat"));

            Level level = new Level(null);
            level = level.LoadTreeSafe(levelTree.Root);
            Assert.IsNotNull(level);
        }

        [TestMethod]
        public void LoadTreeTest_1_7_2_survival()
        {
            NbtTree levelTree = LoadLevelTree(TestHelper.DataPath(@"1_7_2-survival\level.dat"));

            Level level = new Level(null);
            level = level.LoadTreeSafe(levelTree.Root);
            Assert.IsNotNull(level);
        }

        [TestMethod]
        public void LoadTreeTest_1_7_10_survival()
        {
            NbtTree levelTree = LoadLevelTree(TestHelper.DataPath(@"1_7_10-survival\level.dat"));

            Level level = new Level(null);
            level = level.LoadTreeSafe(levelTree.Root);
            Assert.IsNotNull(level);
        }

        [TestMethod]
        public void LoadTreeTest_1_8_3_survival()
        {
            NbtTree levelTree = LoadLevelTree(TestHelper.DataPath(@"1_8_3-survival\level.dat"));

            Level level = new Level(null);
            level = level.LoadTreeSafe(levelTree.Root);
            Assert.IsNotNull(level);
        }

        [TestMethod]
        public void LoadTreeTest_1_9_2_survival()
        {
            NbtTree levelTree = LoadLevelTree(TestHelper.DataPath(@"1_9_2-survival\level.dat"));

            Level level = new Level(null);
            level = level.LoadTreeSafe(levelTree.Root);
            Assert.IsNotNull(level);
        }

        [TestMethod]
        public void LoadTreeTest_1_12_2_survival()
        {
            NbtTree levelTree = LoadLevelTree(TestHelper.DataPath(@"1_12_2-survival\level.dat"));

            Level level = new Level(null);
            level = level.LoadTreeSafe(levelTree.Root);
            Assert.IsNotNull(level);


            NbtTree mineshaftTree = LoadLevelTree(TestHelper.DataPath(@"1_12_2-survival\data\Mineshaft.dat"));
            //Assert.IsTrue(new NbtVerifier(mineshaftTree.Root, _schema).Verify());

            NbtTree templeTree = LoadLevelTree(TestHelper.DataPath(@"1_12_2-survival\data\Temple.dat"));

            NbtTree villageTree = LoadLevelTree(TestHelper.DataPath(@"1_12_2-survival\data\Village.dat"));

            NbtTree villagesTree = LoadLevelTree(TestHelper.DataPath(@"1_12_2-survival\data\villages.dat"));
            Assert.IsTrue(new NbtVerifier(villagesTree.Root, Villages.Schema).Verify());

            NbtTree villagesEndTree = LoadLevelTree(TestHelper.DataPath(@"1_12_2-survival\data\villages_end.dat"));
            Assert.IsTrue(new NbtVerifier(villagesEndTree.Root, Villages.Schema).Verify());

            NbtTree villagesNetherTree = LoadLevelTree(TestHelper.DataPath(@"1_12_2-survival\data\villages_nether.dat"));
            Assert.IsTrue(new NbtVerifier(villagesNetherTree.Root, Villages.Schema).Verify());
        }

        [TestMethod]
        public void LoadTreeTest_1_12_2_survival_SchemaBuilderLoader()
        {
            NbtTree levelTree = LoadLevelTree(TestHelper.DataPath(@"1_12_2-survival\level.dat"));
            Assert.IsTrue(new NbtVerifier(levelTree.Root, Level.Schema).Verify());

            var level = new Level(null);
            var level2 = new Level(null);

            SchemaBuilder.LoadCompound(level, levelTree.Root, Level.Schema);

            level2.LoadTree(levelTree.Root);

            Assert.IsNotNull(level);
            Assert.IsNotNull(level2);
        }

        [TestMethod]
        public void LoadTreeTest_Climatic_Islands_survival()
        {
            if (!Directory.Exists(TestHelper.DataPath(@"Climatic Islands [ENG]\")))
            {
                Assert.Inconclusive("Level not found, skipping test");
            }

            NbtWorld world = NbtWorld.Open(TestHelper.DataPath(@"Climatic Islands [ENG]\"));
            Assert.IsNotNull(world);

            NbtTree villagesNetherTree = LoadLevelTree(TestHelper.DataPath(@"Climatic Islands [ENG]\level.dat"));
            Assert.IsTrue(new NbtVerifier(villagesNetherTree.Root, Level.Schema).Verify());
        }
    }
}
