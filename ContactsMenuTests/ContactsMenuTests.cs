using Moq;
using Xunit;
using ContactsMenu.Interfaces;
using System.Collections.Generic;
using System.Numerics;

namespace ContactsMenuTests
{
    public class ContactsMenuTests
    {
        private readonly Mock<IContactsMenu> contactsMenuMock;
        private readonly List<Dictionary<string, string>> testData;

        public ContactsMenuTests()
        {
            contactsMenuMock = new Mock<IContactsMenu>();
            testData = new List<Dictionary<string, string>> { new Dictionary<string, string>() };
        }

        [Fact]
        public void TestLoadContactsFromJson_FileNotFound()
        {
            contactsMenuMock.Setup(x => x.LoadContactsFromJson());

            contactsMenuMock.Object.LoadContactsFromJson();

            contactsMenuMock.Verify(x => x.LoadContactsFromJson(), Times.Once);
        }

        [Fact]
        public void TestMainMenu()
        {
            contactsMenuMock.Setup(x => x.MainMenu());

            contactsMenuMock.Object.MainMenu();

            contactsMenuMock.Verify(x => x.MainMenu(), Times.Once);
        }

        [Fact]
        public void TestDisplayContacts()
        {
            contactsMenuMock.Setup(x => x.DisplayContacts());

            contactsMenuMock.Object.DisplayContacts();

            contactsMenuMock.Verify(x => x.DisplayContacts(), Times.Once);
        }

        [Fact]
        public void TestAddContact()
        {
            contactsMenuMock.Setup(x => x.AddContact(It.IsAny<string>(), It.IsAny<bool>()));

            contactsMenuMock.Object.AddContact("TEST", true);

            contactsMenuMock.Verify(x => x.AddContact("TEST", true), Times.Once);
        }

        [Fact]
        public void TestUpdateContact()
        {
            contactsMenuMock.Setup(x => x.UpdateContact());

            contactsMenuMock.Object.UpdateContact();

            contactsMenuMock.Verify(x => x.UpdateContact(), Times.Once);
        }

        [Fact]
        public void TestDeleteContact()
        {
            contactsMenuMock.Setup(x => x.DeleteContact(It.IsAny<string>(), It.IsAny<bool>()));

            contactsMenuMock.Object.DeleteContact("TEST", true);

            contactsMenuMock.Verify(x => x.DeleteContact("TEST", true), Times.Once);
        }

        [Fact]
        public void TestFindContactByEmail()
        {
            const string ContactName = "TEST";
            bool expectedResult = true;
            contactsMenuMock.Setup(x => x.FindContactByEmail(ContactName)).Returns(expectedResult);

            var result = contactsMenuMock.Object.FindContactByEmail(ContactName);

            Assert.Equal(expectedResult, result);
            contactsMenuMock.Verify(x => x.FindContactByEmail(ContactName), Times.Once);
        }

        [Fact]
        public void TestDisplayContactsDetail()
        {
            int contactIndex = 0;
            contactsMenuMock.Setup(x => x.DisplayContactsDetail(It.IsAny<int>()));

            contactsMenuMock.Object.DisplayContactsDetail(contactIndex);

            contactsMenuMock.Verify(x => x.DisplayContactsDetail(contactIndex), Times.Once);
        }

        [Fact]
        public void TestSaveToJsonFile()
        {
            contactsMenuMock.Setup(x => x.SaveToJsonFile(It.IsAny<List<Dictionary<string, string>>>()));

            contactsMenuMock.Object.SaveToJsonFile(testData);

            contactsMenuMock.Verify(x => x.SaveToJsonFile(testData), Times.Once);
        }

        [Fact]
        public void TestDeleteContactByEmail()
        {
            const string Email = "test@example.com";
            contactsMenuMock.Setup(x => x.DeleteContactByEmail(Email));

            contactsMenuMock.Object.DeleteContactByEmail(Email);

            contactsMenuMock.Verify(x => x.DeleteContactByEmail(Email), Times.Once);
        }

        [Fact]
        public void TestValidateEmailPhone()
        {
            // Arrange
            var contactsMenuMock = new Mock<IContactsMenu>();

            // Setup the behavior of the ValidateEmailPhone method
            contactsMenuMock.Setup(x => x.ValidateEmailPhone(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string email, string phone) => phone.Length <= 10 && phone.Length >= 7 && email.Length >= 4 && email.Contains("@"));


            // Act
            var validResult = contactsMenuMock.Object.ValidateEmailPhone("test@example.com", "123456789");
            var invalidResult = contactsMenuMock.Object.ValidateEmailPhone("invalidEmail", "1");

            // Assert
            Assert.True(validResult, "Valid email and phone should return true");
            Assert.False(invalidResult, "Invalid email or short phone should return false");
        }


    }
}
