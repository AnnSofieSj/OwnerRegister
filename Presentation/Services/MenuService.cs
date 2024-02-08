
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Services;

namespace Presentation.Services;

public class MenuService(BreedsService breedsService, HorsesService horsesService, OwnersService ownersService, BreedersService breedersService, AddressesService addressesService)
{
    private readonly BreedsService _breedsService = breedsService;
    private readonly HorsesService _horsesService = horsesService;
    private readonly OwnersService _ownersService = ownersService;
    private readonly BreedersService _breedersService = breedersService;
    private readonly AddressesService _addressesService = addressesService;

    public void ShowMainMenu()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine();
            MenuTitle("****  Välkommen till Hästägarregistret! ****");
            Console.WriteLine($"{"1.",-3} Visa alla hästar i databasen");
            Console.WriteLine($"{"2.",-3} Visa information om en häst");
            Console.WriteLine($"{"3.",-3} Lägg till ny Häst");
            Console.WriteLine($"{"4.",-3} Uppdatera information om en häst");
            Console.WriteLine($"{"5.",-3} Ta bort häst från databasen");
            Console.WriteLine($"{"6.",-3} Hantera information om ägare och uppfödare");

            Console.WriteLine($"{"7.",-3} Avsluta program");
            Console.WriteLine($"{"8.",-3} Lägg till ras");   //TEST
            Console.WriteLine();
            Console.Write("Menyval: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ShowAllHorses();
                    break;

                //case "2":
                //    ShowOneHorse();
                //    break;

                case "3":
                    ShowAddNewHorse();
                    break;

                //case "4":
                //    ShowUpdateHorse(); 
                //    break;

                //case "5":
                //    ShowDeleteHorse();
                //    break;

                case "6":
                    ShowOwnerBreederMenu();
                    break;

                case "7":
                    ShowExitApp();
                    break;

                case "8":
                    AddNewBreed();
                    break;

                case "9":
                    ShowAllBreeds();
                    break;

                //case "4":
                //    ShowOneBreed();
                //    break;

                default:
                    Console.WriteLine("Ogiltigt val. Tryck på valfri knapp för att välja igen.");
                    break;

            }

            Console.ReadKey();

        }
    }





    private void ShowOwnerBreederMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            MenuTitle("**** Hantera ägare och uppfödare ****");
            Console.WriteLine($"{"1.",-3} Uppdatera information om ägare");
            Console.WriteLine($"{"2.",-3} Uppdatera information om uppfödare");
            Console.WriteLine($"{"3.",-3} Ta bort ägare ur registret");
            Console.WriteLine($"{"4.",-3} Ta bort uppfödare ur registret");
            Console.WriteLine($"{"5.",-3} Tillbaks till huvudmenyn");
            Console.WriteLine();
            Console.Write("Menyval: ");


            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    ShowUpdateOwner();
                    break;
                case "2":
                    ShowUpdateBreeder();
                    break;
                case "3":
                    ShowDeleteOwner();
                    break;
                case "4":
                    ShowDeleteBreeder();
                    break;
                case "5":
                    ShowMainMenu();
                    break;
            }
        }

    }
    private async void ShowAllHorses() 
    { 
        var horses = await _horsesService.GetAllHorsesAsync();
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();
        MenuTitle("**** Alla hästar i registret ****");

        foreach (var horse in horses)
        {
            Console.WriteLine($"Registreringsnummer: {horse.RegistrationId} Namn: {horse.HorseName} Kön: {horse.Gender} Färg: {horse.Color} ");
        }
        Console.ReadKey();
    }

    private async void ShowAddNewHorse()
    {
        var newHorse = new AddHorseDto();

        Console.Clear();
        MenuTitle("**** Registrera ny häst ****");
        Console.WriteLine();

        Console.Write("Hästens namn : ");
        newHorse.HorseName = Console.ReadLine()!;
        newHorse.HorseName = char.ToUpper(newHorse.HorseName[0]) + newHorse.HorseName.Substring(1);

        Console.Write("Hästens kön : ");
        newHorse.Gender = Console.ReadLine()!;
        newHorse.Gender = char.ToUpper(newHorse.Gender[0]) + newHorse.Gender.Substring(1);

        Console.Write("Hästens födelseår : ");
        newHorse.YearOfBirth = Console.ReadLine()!;

        Console.Write("Hästens färg (frivilligt) : ");
        newHorse.Color = Console.ReadLine()!;
        newHorse.Color = char.ToUpper(newHorse.Color[0]) + newHorse.Color.Substring(1);

        Console.Write("Länk till bild (frivilligt) : ");
        newHorse.Picture = Console.ReadLine()!;

        Console.Write("Hästens ras : ");
        newHorse.NameOfBreed = Console.ReadLine()!;
        newHorse.NameOfBreed = char.ToUpper(newHorse.NameOfBreed[0]) + newHorse.NameOfBreed.Substring(1);

        Console.Write("Ägarens förnamn : ");
        newHorse.OwnerFirstName = Console.ReadLine()!;
        newHorse.OwnerFirstName = char.ToUpper(newHorse.OwnerFirstName[0]) + newHorse.OwnerFirstName.Substring(1);

        Console.Write("Ägarens efternamn : ");
        newHorse.OwnerLastName = Console.ReadLine()!;
        newHorse.OwnerLastName = char.ToUpper(newHorse.OwnerLastName[0]) + newHorse.OwnerLastName.Substring(1);

        Console.Write("Ägarens epostadress : ");
        newHorse.OwnerEmail = Console.ReadLine()!;

        Console.Write("Ägarens gatuadress : ");
        newHorse.Street = Console.ReadLine()!;
        newHorse.Street = char.ToUpper(newHorse.Street[0]) + newHorse.Street.Substring(1);

        Console.Write("Gatunummer : ");
        newHorse.StreetNr = Console.ReadLine()!;

        Console.Write("Postnummer : ");
        newHorse.PostalCode = Console.ReadLine()!;

        Console.Write("Stad : ");
        newHorse.City = Console.ReadLine()!;
        newHorse.City = char.ToUpper(newHorse.City[0]) + newHorse.City.Substring(1);

        Console.Write("Uppfödarens förnamn : ");
        newHorse.BreederFirstName = Console.ReadLine()!;
        newHorse.BreederFirstName = char.ToUpper(newHorse.BreederFirstName[0]) + newHorse.BreederFirstName.Substring(1);

        Console.Write("Uppfödarens efternamn : ");
        newHorse.BreederLastName = Console.ReadLine()!;
        newHorse.BreederLastName = char.ToUpper(newHorse.BreederLastName[0]) + newHorse.BreederLastName.Substring(1);

        Console.Write("Uppfödarens epostadress : ");
        newHorse.BreederEmail = Console.ReadLine()!;



        await _horsesService.AddHorseAsync(newHorse);

        Console.WriteLine("Kontakt skapad. Tryck Enter för att komma tillbaks till huvudmenyn.");
        Console.ReadKey();



    } //Färdig

    private async void AddNewBreed() //TEST
    {
        var breed = new BreedsDto();

        Console.WriteLine("Rasens namn : ");


        await _breedsService.CreateBreedAsync(breed.NameOfBreed = Console.ReadLine()!);

        Console.ReadKey();
    }

    private async void ShowAllBreeds()
    {

        var breeds = _breedsService.GetAllBreedsAsync();

        Console.Clear();
        Console.WriteLine("Alla raser i databasen");
        foreach (var breed in await breeds)
        {
            Console.WriteLine($" Id: {breed.Id} Ras: {breed.NameOfBreed}");
        }
        Console.ReadKey();
    }

    private void ShowOneBreed()   // får inte ihop att hämta en
    {
        Console.Clear();
        Console.WriteLine("Vilken ras vill du visa? Ang Id : ");
        var id = Console.ReadLine()!;
        //var breed = await _breedsService.GetBreedAsync(/*x => x.Id == id*/);

        //Console.WriteLine($"{breed.id} ");
        Console.ReadKey();




    }

    private void ShowExitApp()
    {
        Console.Clear();
        Console.Write("Är du säker på att du vill avsluta programmet? (J - ja / N - nej): ");
        var option = Console.ReadLine() ?? "";

        if (option.Equals("j", StringComparison.CurrentCultureIgnoreCase))
        {
            Environment.Exit(0);

        }
        else
        {
            ShowMainMenu();
        }
    } //Färdig

    private void MenuTitle(string title)
    {
        Console.Clear();
        Console.WriteLine($"*** {title} ***");
        Console.WriteLine();
    } //Färdig


    //Undermeny Ägare och Uppfödare
    private async void ShowUpdateOwner()
    {
        var updatedOwner = new OwnersDto();
        Console.Clear();
        MenuTitle("**** Uppdatera information om ägare ****");
        Console.WriteLine();

        Console.Write("Ange ägarens Id :");
        updatedOwner.Id = int.Parse(Console.ReadLine()!);
        Console.Write("Förnamn: ");
        updatedOwner.FirstName = Console.ReadLine()!;
        Console.Write("Efternamn: ");
        updatedOwner.LastName = Console.ReadLine()!;
        Console.Write("E-postadress: ");
        updatedOwner.Email = Console.ReadLine()!;

        await _ownersService.UpdateOwnerAsync(updatedOwner);

    } //Färdig

    private async void ShowUpdateBreeder()
    {
        var updatedBreeder = new BreedersDto();
        Console.Clear();
        MenuTitle("**** Uppdatera information om ägare ****");
        Console.WriteLine();

        Console.Write("Ange ägarens Id :");
        updatedBreeder.Id = int.Parse(Console.ReadLine()!);
        Console.Write("Förnamn: ");
        updatedBreeder.FirstName = Console.ReadLine()!;
        Console.Write("Efternamn: ");
        updatedBreeder.LastName = Console.ReadLine()!;
        Console.Write("E-postadress: ");
        updatedBreeder.Email = Console.ReadLine()!;

        await _breedersService.UpdateBreederAsync(updatedBreeder);
    } //Färdig

    private void ShowDeleteOwner()
    {

    }

    private void ShowDeleteBreeder()
    {

    }
} 
