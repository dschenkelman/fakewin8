namespace FakeWin8.Generator.Core.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AliasMappingsTests
    {
        [TestMethod]
        public void ShouldReturnCorrectAliasForAliasedType()
        {
            Assert.AreEqual("bool", AliasMappings.GetAliasForType(typeof(Boolean)));
            Assert.AreEqual("byte", AliasMappings.GetAliasForType(typeof(Byte)));
            Assert.AreEqual("sbyte", AliasMappings.GetAliasForType(typeof(SByte)));
            Assert.AreEqual("char", AliasMappings.GetAliasForType(typeof(Char)));
            Assert.AreEqual("decimal", AliasMappings.GetAliasForType(typeof(Decimal)));
            Assert.AreEqual("double", AliasMappings.GetAliasForType(typeof(Double)));
            Assert.AreEqual("float", AliasMappings.GetAliasForType(typeof(Single)));
            Assert.AreEqual("int", AliasMappings.GetAliasForType(typeof(Int32)));
            Assert.AreEqual("uint", AliasMappings.GetAliasForType(typeof(UInt32)));
            Assert.AreEqual("long", AliasMappings.GetAliasForType(typeof(Int64)));
            Assert.AreEqual("ulong", AliasMappings.GetAliasForType(typeof(UInt64)));
            Assert.AreEqual("object", AliasMappings.GetAliasForType(typeof(Object)));
            Assert.AreEqual("short", AliasMappings.GetAliasForType(typeof(Int16)));
            Assert.AreEqual("ushort", AliasMappings.GetAliasForType(typeof(UInt16)));
            Assert.AreEqual("string", AliasMappings.GetAliasForType(typeof(String)));
            Assert.AreEqual("void", AliasMappings.GetAliasForType(typeof(void)));
        }

        [TestMethod]
        public void ShouldReturnTypeNameForNonAliasedType()
        {
            Assert.AreEqual("DateTime", AliasMappings.GetAliasForType(typeof(DateTime)));
        }
    }
}
