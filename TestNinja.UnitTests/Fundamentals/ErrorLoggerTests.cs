﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    public class ErrorLoggerTests
    {
        private ErrorLogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new ErrorLogger();
        }

        [Test]
        public void Log_InputIsNotNull_SetTheLastErrorProperty()
        {
            _logger.Log("a");

            Assert.That(_logger.LastError,Is.EqualTo("a"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullExcepiton(string error)
        {
            Assert.That(() => _logger.Log(error),Throws.ArgumentNullException);
            //Assert.That(() => _logger.Log(error),Throws.Exception.TypeOf<ArgumentNullException>);
        }

        [Test]
        public void Log_ValidError_RaseErrorLoggedEvent()
        {
            var id = Guid.Empty;
            _logger.ErrorLogged += (sender, args) => { id = args; };
            _logger.Log("a");

            Assert.That(id, Is.Not.EqualTo(Guid.Empty));            
        }
    }
}
