using ContactsMenu.Interfaces;
using System.Text.Json;



namespace ContactsMenu
{
    internal class Program : IContactsMenu
    {
        private const string jsonFilePath = @"E:\NetProject-\ContactsMenu\ContactsMenu\text.json";
        static List<Dictionary<string, string>> contactsList = new List<Dictionary<string, string>>();
        //Masseges Console
        public void MassegesConsole(string Message)
        {
            if (Message == "PaKtRtTmM") { Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine("Press Enter to return to the main menu."); Console.ResetColor(); }
            

        }


        /// <summary>
        /// Loads contacts from a JSON file.
        /// </summary>
        public void LoadContactsFromJson()
        {
            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("Error: Contacts file not found!");
                Console.WriteLine(jsonFilePath + "\n");
                MassegesConsole("PaKtRtTmM");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            else
            {
                string jsonText = File.ReadAllText(jsonFilePath);
                contactsList = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonText);
            }
        }

        public void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to the Contact Manager application!\n"); //my app notice
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Select an option by entering the respective number:");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("1 View all contacts");
            Console.WriteLine("2 Add a new contact");
            Console.WriteLine("3 Update an existing contact");
            Console.WriteLine("4 Delete a contact");
            Console.WriteLine("5 Exit");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays all contacts in a summarized format.
        /// </summary>
        public void DisplayContacts()
        {

            for (int i = 0; i < contactsList.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                string firstName = contactsList[i]["FirstName"];
                string lastName = contactsList[i]["LastName"];
                Console.WriteLine($"{i + 1}: {firstName} {lastName}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Adds a new contact to the contact list.
        /// </summary>
        /// <param name="name">Optional name parameter.</param>
        /// <param name="FullFunc">Flag to determine whether to perform full function or an alternative.</param>
        public void AddContact(string name = "", bool FullFunc = true)
        {
            if (FullFunc == true) Console.Write("------Add New Contact------\n\n\n");

            else Console.Write("------Add New alternativ Contact ------\n\n\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Enter FirstName: "); string firstName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter LastName: "); string lastName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Enter Phone: "); string phone = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter Address: "); string Address = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Enter Email: "); string Email = Console.ReadLine();

            


            if (ValidateEmailPhone(Email, phone) == true)
            {


                if (!FindContactByEmail(Email))
                {
                    var contact = new Dictionary<string, string>
             {
                {"FirstName", firstName },
                {"LastName", lastName },
                {"Phone", phone },
                {"Address", Address },
                {"Email", Email }
             };
                    contactsList.Add(contact);
                    SaveToJsonFile(contactsList);
                    Console.Clear(); Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"New Contact Added Successfully"); Console.ResetColor();
                    Console.WriteLine();
                    MassegesConsole("PaKtRtTmM");


                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Email: {Email} is already in the contacts list!\n\n");
                    MassegesConsole("PaKtRtTmM");
                }
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Invalid Phone Number Or Email\n");
                Console.ResetColor();
                MassegesConsole("PaKtRtTmM");
            }
            
        }

        public void UpdateContact()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Enter existing Email: "); string oldEmail = Console.ReadLine();
            Console.ResetColor();

            if (FindContactByEmail(oldEmail))
            {
                DeleteContact(oldEmail, false);
                Console.Clear();
                AddContact("", false);
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine($"email: {oldEmail} Not Found!");
                Console.ResetColor();
                Console.WriteLine();
                MassegesConsole("PaKtRtTmM");
            }
        }

        public void DeleteContact(string Email = null, bool FullFunc = true)
        {
            if (Email != null && FullFunc == true)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("------Delete Contact------\n\n\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Enter the Email to delete the contact: ");
                Console.ResetColor();
                Email = Console.ReadLine();
            }

            if (FindContactByEmail(Email))
            {
                DeleteContactByEmail(Email);
                SaveToJsonFile(contactsList);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Email:{Email} Deleted successfully\n");
                Console.ResetColor();
                MassegesConsole("PaKtRtTmM");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Email:{Email} Not Found!\n");
                Console.ResetColor();
                MassegesConsole("PaKtRtTmM");
            }
        }

        public void DeleteContactByEmail(string Email)
        {
            contactsList.RemoveAll(contact => contact.ContainsKey("Email") && contact["Email"] == Email);
        }

        public bool FindContactByEmail(string Email)
        {
            foreach (var contact in contactsList)
            {
                if (contact["Email"] == Email)
                {
                    return true;
                }
            }
            return false;
        }

        public void DisplayContactsDetail(int Contactindex)
        {
            if (Contactindex >= 0 && Contactindex < contactsList.Count)
            {
                var contact = contactsList[Contactindex];

                string firstName = contact["FirstName"];
                string lastName = contact["LastName"];
                string phone = contact["Phone"];
                string Address = contact["Address"];
                string email = contact["Email"];
                Console.WriteLine("-----Contacts Detail----\n\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Full Name: {firstName} {lastName}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Phone:     {phone}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Address:   {Address}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Email:     {email}\n\n\n");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Press any key to return.");
                Console.ResetColor();
                Console.ReadKey();

                Console.Clear();
                DisplayContacts();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Press Index to Display Contacts Detail.");
                
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Press Enter to return to main menu.");
                Console.ResetColor();
                string inputIndex = Console.ReadLine();
                int.TryParse(inputIndex, out int index);
                if (inputIndex == "0")
                {
                    Console.Clear();
                    MainMenu();

                }
                else if (index > 0 && index <= contactsList.Count)
                {
                    Console.Clear();
                    DisplayContactsDetail(index - 1); 
                    Console.WriteLine();
                }
                else
                {

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid index. Please provide a valid index.\n\n");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Press Enter to return to main menu.");
                    Console.ResetColor();
                }

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid index. Please provide a valid index.\n\n");
                Console.WriteLine("Press Enter to return to main menu.");
            }
        }

        public void SaveToJsonFile(List<Dictionary<string, string>> data)
        {
            data.Sort((contact1, contact2) =>
                string.Compare(contact1["FirstName"], contact2["FirstName"], StringComparison.OrdinalIgnoreCase));

            File.WriteAllText(jsonFilePath, JsonSerializer.Serialize(data));
        }

        /// <summary>
        /// Validates the format of an email and phone number.
        /// </summary>
        /// <param name="email">Email to be validated.</param>
        /// <param name="phone">Phone number to be validated.</param>
        /// <returns>True if both email and phone are valid; otherwise, false.</returns>
        public bool ValidateEmailPhone(string email, string phone)
        {
            if (phone.Length <= 10 && phone.Length >= 7 && email.Length >= 4 && email.Contains("@"))
            {
                return true; 
            }

            return false; 
        }





        static void Main(string[] args)
        {
            Program program = new Program(); // Create an instance of the Program class
            program.LoadContactsFromJson();
            program.MainMenu();
            bool exit = true;


            while (exit)
            {
                string selectedOption = Console.ReadKey().KeyChar.ToString();


                switch (selectedOption)
                {
                    case "1":
                        Console.Clear();
                        program.DisplayContacts();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Press Index to Display Contacts Detail.");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Press Enter to return to main menu.");
                        Console.ResetColor();
                        string inputIndex = Console.ReadLine();
                        int.TryParse(inputIndex, out int index);
                        if (inputIndex == "0")
                        {
                            Console.Clear();
                            program.MainMenu();
                            break;
                        }
                        else if (index > 0 && index <= contactsList.Count)
                        {
                            Console.Clear();
                            program.DisplayContactsDetail(index - 1); // Adjust index to 0-based
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid index. Please provide a valid index.\n\n");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Press Enter to return to main menu.");
                            Console.ResetColor();
                        }
                        break;

                    case "2":
                        Console.Clear();
                        program.AddContact();
                        Console.WriteLine();
                        break;

                    case "3":
                        Console.Clear();
                        program.UpdateContact();
                        Console.WriteLine();
                        break;

                    case "4":
                        Console.Clear();
                        program.DeleteContact("");
                        Console.WriteLine();
                        break;

                    case "5":
                        exit = false;
                        break;

                    default:
                        Console.Clear();
                        program.MainMenu();
                        break;
                }
            }
        }
    }
}
