using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeNet.Structures;
// <copyright file="PKCS7ParserTest.cs">Copyright ©  2016</copyright>

namespace PeNet.Parser.Tests
{
    [TestClass]
    [PexClass(typeof(PKCS7Parser))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PKCS7ParserTest
    {
        [PexMethod]
        internal PKCS7Parser Constructor(WIN_CERTIFICATE winCertificate)
        {
            var target = new PKCS7Parser(winCertificate);
            return target;
            // TODO: add assertions to method PKCS7ParserTest.Constructor(WIN_CERTIFICATE)
        }
    }
}