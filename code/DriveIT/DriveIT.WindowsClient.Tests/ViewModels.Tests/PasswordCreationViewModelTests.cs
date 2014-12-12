using System;
using System.Linq;
using DriveIT.Models;
using DriveIT.WindowsClient.ViewModels;
using NUnit.Framework;

namespace DriveIT.WindowsClient.Tests.ViewModels.Tests
{
    [TestFixture]
    public class PasswordCreationViewModelTests
    {
        [Test]
        public void ConstructorWithCustomer()
        {
            var passwordCreationViewModel = new PasswordCreationViewModel(new CustomerDto());
            Assert.AreEqual(true, passwordCreationViewModel.Roletypes.Contains(Role.Customer));
            Assert.AreEqual(false, passwordCreationViewModel.Roletypes.Contains(Role.Administrator));
            Assert.AreEqual(false, passwordCreationViewModel.Roletypes.Contains(Role.Employee));
        }

        [Test]
        public void ConstructorWithEmployee()
        {
            var passwordCreationViewModel = new PasswordCreationViewModel(new EmployeeDto());
            Assert.AreEqual(false, passwordCreationViewModel.Roletypes.Contains(Role.Customer));
            Assert.AreEqual(true, passwordCreationViewModel.Roletypes.Contains(Role.Administrator));
            Assert.AreEqual(true, passwordCreationViewModel.Roletypes.Contains(Role.Employee));
        }

        [Test]
        public void TooShortPassword()
        {
            var passwordCreationViewModel = new PasswordCreationViewModel(new EmployeeDto());
            var CheckStatus = "Password must be between 6 and 100 characters.";
            passwordCreationViewModel.Password = "Aa1";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
            passwordCreationViewModel.Password = "a";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
            passwordCreationViewModel.Password = "12222";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
        }

        [Test]
        public void TooLongPassword()
        {
            var passwordCreationViewModel = new PasswordCreationViewModel(new EmployeeDto());
            var CheckStatus = "Password must be between 6 and 100 characters.";
            passwordCreationViewModel.Password = "d7XQ897P7xGgVytfxH9exIyPJe7GixzxzwnDMRkYl" + "d7XQ897P7xGgVytfxH9exIyPJe7GixzxzwnDMRkYl" + "d7XQ897P7xGgVytfxH9exIyPJe7GixzxzwnDMRkYl" + "d7XQ897P7xGgVytfxH9exIyPJe7GixzxzwnDMRkYl" + "d7XQ897P7xGgVytfxH9exIyPJe7GixzxzwnDMRkYl";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
            passwordCreationViewModel.Password = "C6SPHJLHJ2NCRR20ANNGJK7YQSHBG0IQDSEHD4WX" + "C6SPHJLHJ2NCRR20ANNGJK7YQSHBG0IQDSEHD4WX" + "C6SPHJLHJ2NCRR20ANNGJK7YQSHBG0IQDSEHD4WX" + "C6SPHJLHJ2NCRR20ANNGJK7YQSHBG0IQDSEHD4WX" + "C6SPHJLHJ2NCRR20ANNGJK7YQSHBG0IQDSEHD4WX";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
            passwordCreationViewModel.Password = "ERwztXKdeMTGncUXpCQVfpKMzUVbDtmciQTUvYto" + "ERwztXKdeMTGncUXpCQVfpKMzUVbDtmciQTUvYto" + "ERwztXKdeMTGncUXpCQVfpKMzUVbDtmciQTUvYto" + "ERwztXKdeMTGncUXpCQVfpKMzUVbDtmciQTUvYto" + "ERwztXKdeMTGncUXpCQVfpKMzUVbDtmciQTUvYto";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
        }


        [Test]
        public void PasswordWithoutDigit()
        {
            var passwordCreationViewModel = new PasswordCreationViewModel(new EmployeeDto());
            var CheckStatus = "Password must contain a digit";
            passwordCreationViewModel.Password = "AAAAAA";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
            passwordCreationViewModel.Password = "aaaAAA";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
        }

        [Test]
        public void PasswordWithoutUpper()
        {
            var passwordCreationViewModel = new PasswordCreationViewModel(new EmployeeDto());
            var CheckStatus = "Password must contain an upper case and lowercase letter";
            passwordCreationViewModel.Password = "aaaa1234";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
            passwordCreationViewModel.Password = "aaaaaaa2";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
        }

        [Test]
        public void PasswordWithoutLower()
        {
            var passwordCreationViewModel = new PasswordCreationViewModel(new EmployeeDto());
            var CheckStatus = "Password must contain an upper case and lowercase letter";
            passwordCreationViewModel.Password = "AAAA1234";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
            passwordCreationViewModel.Password = "AAAAAAA2";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
        }

        [Test]
        public void MismatchPasswordConfirmationPassword()
        {
            var passwordCreationViewModel = new PasswordCreationViewModel(new EmployeeDto());
            var CheckStatus = "Password and confirmation password must be equal";
            passwordCreationViewModel.Password = "AAaa1234";
            passwordCreationViewModel.ConfirmationPassword = "Bla";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
            passwordCreationViewModel.Password = "AAaa1234";
            passwordCreationViewModel.ConfirmationPassword = "AAaa1234Bla";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(false, passwordCreationViewModel.CanCreateProfile);
        }

        [Test]
        public void PasswordOK()
        {
            var passwordCreationViewModel = new PasswordCreationViewModel(new EmployeeDto());
            var CheckStatus = "Password OK";
            passwordCreationViewModel.Password = "AAaa1234";
            passwordCreationViewModel.ConfirmationPassword = "AAaa1234";
            Assert.AreEqual(CheckStatus, passwordCreationViewModel.Status);
            Assert.AreEqual(true, passwordCreationViewModel.CanCreateProfile);
        }
    }
}
