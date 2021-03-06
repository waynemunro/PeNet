using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeNet.Structures;
// <copyright file="ImportedFunctionsParserTest.cs">Copyright ©  2016</copyright>

namespace PeNet.Parser.Tests
{
    [TestClass]
    [PexClass(typeof(ImportedFunctionsParser))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ImportedFunctionsParserTest
    {
        [PexMethod]
        internal ImportedFunctionsParser Constructor(
            byte[] buff,
            IMAGE_IMPORT_DESCRIPTOR[] importDescriptors,
            IMAGE_SECTION_HEADER[] sectionHeaders,
            bool is64Bit
            )
        {
            var target
                = new ImportedFunctionsParser(buff, importDescriptors, sectionHeaders, is64Bit);
            return target;
            // TODO: add assertions to method ImportedFunctionsParserTest.Constructor(Byte[], IMAGE_IMPORT_DESCRIPTOR[], IMAGE_SECTION_HEADER[], Boolean)
        }
    }
}