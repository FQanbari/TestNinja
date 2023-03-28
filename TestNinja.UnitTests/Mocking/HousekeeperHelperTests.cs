using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
using static TestNinja.Mocking.HousekeeperHelper;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HousekeeperHelperTests
    {
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _xtraMessageBox;
        private Mock<IUnitOfwork> _unitOfWork;
        private HousekeeperHelper _housekeeperHelper;
        DateTime _statementDate = new DateTime(2023, 3, 28);
        private string _statementFileName;
        private Housekeeper _housekeeper;

        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper
            {
                Email = "a",
                StatementEmailBody = "b",
                FullName = "c",
                Oid = 1
            };

            _unitOfWork = new Mock<IUnitOfwork>();            
            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _statementFileName = "fileName";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator.Setup(s => s.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);

            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();
           
            _housekeeperHelper = new HousekeeperHelper(_unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _xtraMessageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _housekeeperHelper.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(s => s.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }
        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_HouseKeeperEmailIsNullOrWhiteSpaceOrEmptyString_ShouldNullGenerateStatements(string email)
        {
            _housekeeper.Email = email;

            _housekeeperHelper.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(s => s.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate),
                Times.Never);
        }
        [Test]
        public void SendStatementEmails_WhenCalled_ShouldEmailedStatements()
        {
            _housekeeperHelper.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }        

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_FileNameIsNullOrWiteSpaceOrEmptyString_ShouldNotEmailedStatements(string statementFileName)
        {
            _statementFileName = statementFileName;

            _housekeeperHelper.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
                )).Throws<Exception>();

            _housekeeperHelper.SendStatementEmails(_statementDate);

            _xtraMessageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<string>()
                            ), Times.Never);
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                            _housekeeper.Email,
                            _housekeeper.StatementEmailBody,
                            _statementFileName,
                            It.IsAny<string>()
                            ));
        }
    }
}
