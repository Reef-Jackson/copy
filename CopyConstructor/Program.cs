using Spectre.Console;

class Program {
	
	public static List<Person> personList = new List<Person>();

	public static void Main(string[] args) {
		showInterface();
	}
	
	private static void showInterface() {
		bool isRunning = true;

		while (isRunning) {

			var selection = AnsiConsole.Prompt(
					new SelectionPrompt<string>()
					.Title("Select your option")
					.PageSize(10)
					.MoreChoicesText("More choices up and down.")
					.AddChoices(new[] { "Add", "View", "Delete", "Duplicate", "Exit" })
					);

			switch (selection) {
				case "View":
					printPeople();
					break;
				case "Add":
					createPerson();
					break;
				case "Delete":
					deletePerson(); //This deletes all instances of name, needs to be changed to a dictionary.
					break;
				case "Duplicate":
					duplicatePerson();
					break;
				case "Exit":
					isRunning = false;
					break;
			}
		}
	}

	private static void printPeople() {
		foreach (Person person in personList) {
			int index = personList.IndexOf(person);
			AnsiConsole.WriteLine($"Name: {person.Name} | Age: {person.Age} | Index: {index}");
			// Make this an AnsiConsole.Table
		}
		AnsiConsole.WriteLine("--------------------");
	}

	private static void createPerson() {
		string name = AnsiConsole.Ask<string>("Name:");
		int age = AnsiConsole.Ask<int>("Age:");

		Person newPerson = Person.CreatePerson(name, age);
		personList.Add(newPerson);

		AnsiConsole.WriteLine("Person Added!");
	}

	private static void duplicatePerson() {

		List<string> people = new List<string>();

		foreach (Person person in personList) {
			people.Add(person.Name);
		}

		var duplicateSelection = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
				.Title("Which person to duplicate?")
				.PageSize(10)
				.MoreChoicesText("Up and down for more choices...")
				.AddChoices(people)
				);

		Person selectedPerson = personList.Find(person => person.Name == duplicateSelection);
		Person clonedPerson = Person.DuplicatePerson(selectedPerson);

		personList.Add(clonedPerson);
	}

	private static void deletePerson() {
		List<string> people = new List<string>();

		foreach (Person person in personList) {
			people.Add(person.Name);
		}

		var deleteSelection = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
				.Title("Which person to delete?")
				.PageSize(10)
				.MoreChoicesText("Up and down for more options")
				.AddChoices(people)
				);
		
		personList.RemoveAll(person => person.Name == deleteSelection);
	}
}

class Person {
	private string _name = "";
	private int _age;

	public string Name {
		get => _name;
		set => _name = value;
	}

	public int Age {
		get => _age;
		set => _age = value;
	}

	public Person(Person person) {
		this.Name = person.Name;
		this.Age = person.Age;
	}

	public Person() {
		_name = "";
		_age = 0;
	}

	public static Person DuplicatePerson(Person originalPerson) {
		return new Person(originalPerson);
	}

	public static Person CreatePerson(string name, int age) {
		Person person = new Person();
		person.Name = name;
		person.Age = age;

		return person;
	}
}


