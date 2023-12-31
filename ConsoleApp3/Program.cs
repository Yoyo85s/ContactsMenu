using ContactsMenu.Interfaces;
using System.Text.Json;



namespace ContactsMenu
{
    internal class Program : IContactsMenu
    {
        private const string jsonFilePath = @"E:\New folder\ContactsMenu\ConsoleApp3\text.json";
        static List<Dictionary<string, string>> contactsList = new List<Dictionary<string, string>>();
        //Masseges Console
        public void MassegesConsole(string Message)
        {
            if (Message == "PaKtRtTmM") { Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine("Press any key to return to the main menu."); Console.ResetColor(); }
          //  if (Message == "PaKtRtTmM") { Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine("Press any key to return to the main menu."); Console.ResetColor(); }

        }



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
            Console.WriteLine("Choose an option:\n\a"); //my app notice
            Console.WriteLine("1 View all contacts");
            Console.WriteLine("2 Add a new contact");
            Console.WriteLine("3 Update an existing contact");
            Console.WriteLine("4 Delete a contact");
            Console.WriteLine("5 Exit");
        }

        public void DisplayContacts()
        {

            for (int i = 0; i < contactsList.Count; i++)
            {
                string firstName = contactsList[i]["FirstName"];
                string lastName = contactsList[i]["LastName"];
                Console.WriteLine($"{i + 1}: {firstName} {lastName}");
            }
        }

        public void AddContact(string name = "", bool FullFunc = true)
        {
            if (FullFunc == true) Console.Write("------Add New Contact------\n\n\n");
            else Console.Write("------Add New alternativ Contact ------\n\n\n");
            Console.Write("Enter FirstName: "); string firstName = Console.ReadLine();
            Console.Write("Enter LastName: "); string lastName = Console.ReadLine();
            Console.Write("Enter Phone: "); string phone = Console.ReadLine();
            Console.Write("Enter Address: "); string Address = Console.ReadLine();
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
            else { Console.Clear(); Console.WriteLine("Error: Invalid Phone Number Or Email\n"); MassegesConsole("PaKtRtTmM"); }
        }

        public void UpdateContact()
        {
            Console.Write("Enter existing Email: "); string oldEmail = Console.ReadLine();

            if (FindContactByEmail(oldEmail))
            {
                DeleteContact(oldEmail, false);
                Console.Clear();
                AddContact("", false);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"email: {oldEmail} Not Found!");
                Console.WriteLine();
                MassegesConsole("PaKtRtTmM");
            }
        }

        public void DeleteContact(string Email = null, bool FullFunc = true)
        {
            if (Email != null && FullFunc == true)
            {
                Console.Write("------Delete Contact------\n\n\n");
                Console.Write("Enter the Email to delete the contact: ");
                Email = Console.ReadLine();
            }

            if (FindContactByEmail(Email))
            {
                DeleteContactByEmail(Email);
                SaveToJsonFile(contactsList);
                Console.WriteLine($"Email:{Email} Deleted successfully\n");
                MassegesConsole("PaKtRtTmM");
            }
            else
            {
                Console.WriteLine($"Email:{Email} Not Found!\n");
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
                Console.WriteLine($"Full Name: {firstName} {lastName}");
                Console.WriteLine($"Phone:     {phone}");
                Console.WriteLine($"Address:   {Address}");
                Console.WriteLine($"Email:     {email}\n\n\n");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();

                Console.Clear();
                DisplayContacts();
                Console.WriteLine();
                Console.WriteLine("Press Index to Display Contacts Detail.");
                Console.WriteLine("Press 0 key to return to main menu.");
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
                    DisplayContactsDetail(index - 1); // Adjust index to 0-based
                    Console.WriteLine();
                }
                else
                {

                    Console.Clear();
                    Console.WriteLine("Invalid index. Please provide a valid index.\n\n");
                    Console.WriteLine("Press any key to return to main menu.");
                }

            }
            else
            {   Console.Clear();
                Console.WriteLine("Invalid index. Please provide a valid index.\n\n");
                Console.WriteLine("Press any key to return to main menu.");
            }
        }

        public void SaveToJsonFile(List<Dictionary<string, string>> data)
        {
            data.Sort((contact1, contact2) =>
                string.Compare(contact1["FirstName"], contact2["FirstName"], StringComparison.OrdinalIgnoreCase));

            File.WriteAllText(jsonFilePath, JsonSerializer.Serialize(data));
        }

        public bool ValidateEmailPhone(string email, string phone)
        {
            if (phone.Length <= 10 && phone.Length >= 7 &&  email.Length >= 4 && email.Contains("@"))
            {
                return true; // Valid phone and email
            }

            return false; // Invalid phone or email
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

                        Console.WriteLine("Press Index to Display Contacts Detail.");
                        Console.WriteLine("Press 0 key to return to main menu.");
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
                            Console.WriteLine("Invalid index. Please provide a valid index.\n\n");
                            Console.WriteLine("Press any key to return to main menu.");
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
