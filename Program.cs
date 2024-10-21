using System.Text.RegularExpressions;
using System.Globalization;

class Program
{
    static void Main()
    {
        // Prompt for user's name
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        string birthdateInput;
        
        // Regex pattern to validate birthdate in mm/dd/yyyy format
        Regex dateRegex = new Regex(@"^(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])/\d{4}$");

        // Input birthdate and validate format
        do
        {
            Console.Write("Enter your birthdate (mm/dd/yyyy): ");
            birthdateInput = Console.ReadLine();
            if (!dateRegex.IsMatch(birthdateInput))
            {
                Console.WriteLine("Invalid format! Use mm/dd/yyyy.");
            }
        } while (!dateRegex.IsMatch(birthdateInput));

        // Calculate age based on birthdate
        DateTime birthdate = DateTime.Parse(birthdateInput);
        int age = DateTime.Now.Year - birthdate.Year;
        if (DateTime.Now.DayOfYear < birthdate.DayOfYear) age--;

        // Save user details to file
        string filePath = "user_info.txt";
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine($"Name: {name}");
            sw.WriteLine($"Age: {age}");
        }

        Console.WriteLine("\nUser info saved to 'user_info.txt'.");

        // Read and display the contents of the file
        Console.WriteLine("\nReading from 'user_info.txt':");
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }

        // Prompt for directory path and list files in that directory
        Console.Write("\nEnter a directory path: ");
        string directoryPath = Console.ReadLine();
        if (Directory.Exists(directoryPath))
        {
            Console.WriteLine("\nFiles in the directory:");
            string[] files = Directory.GetFiles(directoryPath);
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
        }
        else
        {
            Console.WriteLine("Directory does not exist.");
        }

        // Prompt for a string and format it to title case
        Console.Write("\nEnter a string to format in title case: ");
        string inputString = Console.ReadLine();
        
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        string titleCaseString = textInfo.ToTitleCase(inputString.ToLower());

        Console.WriteLine($"\nFormatted in title case: {titleCaseString}");

        // Trigger garbage collection explicitly
        Console.WriteLine("\nRunning garbage collection...");
        GC.Collect(); // Forces garbage collection
        GC.WaitForPendingFinalizers(); // Wait for any finalizers to finish

        Console.WriteLine("Garbage collection complete.");
    }
}
