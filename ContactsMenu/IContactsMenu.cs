using System;
using System.Collections.Generic;

namespace ContactsMenu.Interfaces
{
    /// <summary>
    /// Represents a menu for managing contacts.
    /// </summary>
    public interface IContactsMenu
    {
        /// <summary>
        /// Loads contacts from a JSON file.
        /// </summary>
        void LoadContactsFromJson();

        /// <summary>
        /// Displays the main menu options.
        /// </summary>
        void MainMenu();

        /// <summary>
        /// Finds a contact by email.
        /// </summary>
        /// <param name="email">The email of the contact.</param>
        /// <returns>True if the contact is found; otherwise, false.</returns>
        bool FindContactByEmail(string email);

        /// <summary>
        /// Adds a new contact.
        /// </summary>
        /// <param name="name">The name of the contact.</param>
        /// <param name="fullFunc">True if using the full functionality; otherwise, false.</param>
        void AddContact(string name, bool fullFunc);

        /// <summary>
        /// Updates an existing contact.
        /// </summary>
        void UpdateContact();

        /// <summary>
        /// Deletes a contact.
        /// </summary>
        /// <param name="name">The name of the contact to delete.</param>
        /// <param name="fullFunc">True if using the full functionality; otherwise, false.</param>
        void DeleteContact(string name, bool fullFunc);

        /// <summary>
        /// Displays the list of contacts.
        /// </summary>
        void DisplayContacts();

        /// <summary>
        /// Displays detailed information about a contact.
        /// </summary>
        /// <param name="contactIndex">The index of the contact to display.</param>
        void DisplayContactsDetail(int contactIndex);

        /// <summary>
        /// Saves contacts to a JSON file.
        /// </summary>
        /// <param name="data">The list of contacts to save.</param>
        void SaveToJsonFile(List<Dictionary<string, string>> data);

        /// <summary>
        /// Deletes a contact by email.
        /// </summary>
        /// <param name="email">The email of the contact to delete.</param>
        void DeleteContactByEmail(string email);

        /// <summary>
        /// Validates an email and phone number.
        /// </summary>
        /// <param name="email">The email to validate.</param>
        /// <param name="phone">The phone number to validate.</param>
        /// <returns>True if both email and phone are valid; otherwise, false.</returns>
        bool ValidateEmailPhone(string email, string phone);
    }
}
