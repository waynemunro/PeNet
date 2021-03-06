// <copyright file="ImageDosHeaderParserTest.cs">Copyright ©  2016</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PeNet.Parser.Tests
{
    [TestClass]
    [PexClass(typeof(ImageDosHeaderParser))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ImageDosHeaderParserTest
    {
        [PexMethod]
        internal ImageDosHeaderParser Constructor(byte[] buff, uint offset)
        {
            var target = new ImageDosHeaderParser(buff, offset);
            return target;
            // TODO: add assertions to method ImageDosHeaderParserTest.Constructor(Byte[], UInt32)
        }
    }
}