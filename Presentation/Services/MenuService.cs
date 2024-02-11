
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

    //Huvudmeny
    public async Task ShowMainMenu()
    {
        while (true)
        {            
            MenuTitle("****  Välkommen till Hästägarregistret! ****");
            Console.WriteLine();        
            Console.WriteLine($"{"1.",-3} Visa alla hästar i databasen");
            Console.WriteLine($"{"2.",-3} Visa information om en häst");
            Console.WriteLine($"{"3.",-3} Lägg till ny häst");
            Console.WriteLine($"{"4.",-3} Uppdatera information om en häst");
            Console.WriteLine($"{"5.",-3} Ta bort häst från databasen");
            Console.WriteLine($"{"6.",-3} Hantera information om ägare och uppfödare");
            Console.WriteLine($"{"7.",-3} Avsluta program");            
            Console.WriteLine();

            Console.Write("Menyval: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await ShowAllHorses();
                    break;

                case "2":
                    await ShowOneHorse();
                    break;

                case "3":
                    await ShowAddNewHorse();
                    break;

                case "4":
                    await ShowUpdateHorse();
                    break;

                case "5":
                    await ShowDeleteHorse();
                    break;

                case "6":
                    await ShowOwnerBreederMenu();
                    break;

                case "7":
                    await ShowExitApp();
                    break;               

                default:
                    Console.WriteLine("Ogiltigt val. Tryck på valfri knapp för att välja igen.");
                    break;

            }

            Console.ReadKey();

        }
    }

    // Undermeny ägare/uppfödare
    private async Task ShowOwnerBreederMenu()
    {
        while (true)
        {            
            MenuTitle("**** Hantera information om ägare och uppfödare ****");
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
                    await ShowUpdateOwner();
                    break;
                case "2":
                    await ShowUpdateBreeder();
                    break;
                case "3":
                    await ShowDeleteOwner();
                    break;
                case "4":
                    await ShowDeleteBreeder();
                    break;
                case "5":
                    await ShowMainMenu();
                    break;
            }
        }

    }

    
    private async Task ShowAllHorses() 
    {
        MenuTitle("**** Alla hästar i registret ****");
        Console.Clear();
        var horses = await _horsesService.GetAllHorsesAsync();
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();        

        foreach (var horse in horses)
        {
            Console.WriteLine($"{"*",-4} Registreringsnummer: {horse.RegistrationId}");
            Console.WriteLine($"{" ",-4} Namn: {" ",-14} {horse.HorseName}");            
            Console.WriteLine($"{" ",-4} Kön: {" ",-15} {horse.Gender}");            
            Console.WriteLine($"{" ",-4} Ras: {" ",-15} {horse.NameOfBreed}");
            Console.WriteLine($"{" ",-4} Uppfödare: {" ",-9} {horse.BreederFirstName} {horse.BreederLastName}");
            Console.WriteLine($"{" ",-4} Ägare: {" ",-13} {horse.OwnerFirstName} {horse.OwnerLastName}");
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine();            
        }
        Console.ReadKey();
        await ShowMainMenu();
    }
    
    private async Task ShowOneHorse()
    {
        MenuTitle("**** Visa information om en häst ****");
        Console.WriteLine();
        Console.Write("Ange Registreringnummer för den häst som du vill visa: ");
        var id = int.Parse(Console.ReadLine()!);
        var horse = await _horsesService.GetOneHorseAsync(x => x.RegistrationId == id);

        Console.Clear();
        Console.WriteLine("Detaljerad information om en häst");
        Console.WriteLine();
        Console.WriteLine($"{"*",-4} Registreringsnummer: {horse.RegistrationId}");
        Console.WriteLine($"{" ",-4} Namn: {" ",-14} {horse.HorseName}");
        Console.WriteLine($"{" ",-4} Kön: {" ",-15} {horse.Gender}");
        Console.WriteLine($"{" ",-4} Färg: {" ",-14} {horse.Color}");
        Console.WriteLine($"{" ",-4} Ras: {" ",-15} {horse.NameOfBreed}");
        Console.WriteLine($"{" ",-4} Bildlänk: {" ",-10} {horse.Picture}");
        Console.WriteLine();
        Console.WriteLine($"{" ",-4} Uppfödare: {" ",-9} {horse.BreederFirstName} {horse.BreederLastName}");
        Console.WriteLine($"{" ",-4} E-post: {" ",-12} {horse.BreederEmail}");
        Console.WriteLine();
        Console.WriteLine($"{" ",-4} Ägare: {" ",-13} {horse.OwnerFirstName} {horse.OwnerLastName}, {horse.City}");
        Console.WriteLine($"{" ",-4} E-post: {" ",-12} {horse.OwnerEmail}");

        Console.ReadKey();
        await ShowMainMenu();

    }

    private async Task ShowAddNewHorse()
    {
        var newHorse = new AddHorseDto();

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

        var result = await _horsesService.AddHorseAsync(newHorse);
        Console.Clear();

        if (result == true)
        {
            Console.Clear();
            Console.WriteLine("Hästen sparad!");
            Console.WriteLine("Tryck enter för att återgå till huvudmenyn.");
            Console.ReadKey();
            await ShowMainMenu();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Något gick fel!");
            Console.WriteLine("Tryck enter för att återgå till huvudmenyn.");
            Console.ReadKey();
            await ShowMainMenu();
        }      

    } //Färdig
      
         
    private async Task ShowUpdateHorse()
    {
        var updateHorse = new HorseDto();
        
        MenuTitle("**** Uppdatera information om befintlig häst ****");
        Console.WriteLine();        
        Console.WriteLine();        
        Console.Write("Ange registreringsnummret för hästen du vill uppdatera: ");
        updateHorse.RegistrationId = int.Parse(Console.ReadLine()!);
        Console.Write("Hästens namn : ");
        updateHorse.HorseName = Console.ReadLine()!;
        updateHorse.HorseName = char.ToUpper(updateHorse.HorseName[0]) + updateHorse.HorseName.Substring(1);

        Console.Write("Hästens kön : ");
        updateHorse.Gender = Console.ReadLine()!;
        updateHorse.Gender = char.ToUpper(updateHorse.Gender[0]) + updateHorse.Gender.Substring(1);

        Console.Write("Hästens födelseår : ");
        updateHorse.YearOfBirth = Console.ReadLine()!;

        Console.Write("Hästens färg (frivilligt) : ");
        updateHorse.Color = Console.ReadLine()!;
        updateHorse.Color = char.ToUpper(updateHorse.Color[0]) + updateHorse.Color.Substring(1);

        Console.Write("Länk till bild (frivilligt) : ");
        updateHorse.Picture = Console.ReadLine()!;

        Console.Write("Hästens ras : ");
        updateHorse.NameOfBreed = Console.ReadLine()!;
        updateHorse.NameOfBreed = char.ToUpper(updateHorse.NameOfBreed[0]) + updateHorse.NameOfBreed.Substring(1);

        Console.Write("Ägarens förnamn : ");
        updateHorse.OwnerFirstName = Console.ReadLine()!;
        updateHorse.OwnerFirstName = char.ToUpper(updateHorse.OwnerFirstName[0]) + updateHorse.OwnerFirstName.Substring(1);

        Console.Write("Ägarens efternamn : ");
        updateHorse.OwnerLastName = Console.ReadLine()!;
        updateHorse.OwnerLastName = char.ToUpper(updateHorse.OwnerLastName[0]) + updateHorse.OwnerLastName.Substring(1);

        Console.Write("Ägarens epostadress : ");
        updateHorse.OwnerEmail = Console.ReadLine()!;

        Console.Write("Ägarens gatuadress : ");
        updateHorse.Street = Console.ReadLine()!;
        updateHorse.Street = char.ToUpper(updateHorse.Street[0]) + updateHorse.Street.Substring(1);

        Console.Write("Gatunummer : ");
        updateHorse.StreetNr = Console.ReadLine()!;

        Console.Write("Postnummer : ");
        updateHorse.PostalCode = Console.ReadLine()!;

        Console.Write("Stad : ");
        updateHorse.City = Console.ReadLine()!;
        updateHorse.City = char.ToUpper(updateHorse.City[0]) + updateHorse.City.Substring(1);

        Console.Write("Uppfödarens förnamn : ");
        updateHorse.BreederFirstName = Console.ReadLine()!;
        updateHorse.BreederFirstName = char.ToUpper(updateHorse.BreederFirstName[0]) + updateHorse.BreederFirstName.Substring(1);

        Console.Write("Uppfödarens efternamn : ");
        updateHorse.BreederLastName = Console.ReadLine()!;
        updateHorse.BreederLastName = char.ToUpper(updateHorse.BreederLastName[0]) + updateHorse.BreederLastName.Substring(1);

        Console.Write("Uppfödarens epostadress : ");
        updateHorse.BreederEmail = Console.ReadLine()!;

        var result = await _horsesService.UpdateHorseAsync(updateHorse);
        Console.Clear();

        if (result != null)
        {
            Console.Clear();
            Console.WriteLine("Uppdatering sparad!");
            Console.WriteLine("Tryck enter för att återgå till huvudmenyn.");
            Console.ReadKey();
            await ShowMainMenu();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Något gick fel!");
            Console.WriteLine("Tryck enter för att återgå till huvudmenyn.");
            Console.ReadKey();
            await ShowMainMenu();
        }
    }
        

    private async Task ShowDeleteHorse()
    {
        Console.Clear();
        Console.Write("Id för häst att ta bort: ");
        var id = int.Parse(Console.ReadLine()!);
        var horse = await _horsesService.DeleteHorseAsync(x => x.RegistrationId == id);
        Console.Clear();
        if (horse == true)
        {
            Console.WriteLine("Häst borttagen");
        }
        else
        {
            Console.WriteLine("Något gick fel");
        }

    }

    private async Task ShowDeleteBreed()
    {
        Console.Clear();

        Console.Write("Id för rasen att ta bort: ");
        var id = int.Parse(Console.ReadLine()!);
        var breed = await _breedsService.DeleteBreedAsync(x => x.Id == id);
        Console.Clear();
        if (breed == true)
        {
            Console.WriteLine("Ras borttagen");
        }
        else
        {
            Console.WriteLine("Något gick fel");
        }       

    }



    //Undermeny Ägare och Uppfödare

    
    private async Task ShowUpdateOwner()
    {
        Console.Clear();
        var owners = await _ownersService.GetAllOwnersAsync();
        Console.Clear();
        MenuTitle("**** Uppdatera information om ägare ****");
        Console.WriteLine();
        Console.WriteLine("Alla ägare i databasen");
        Console.WriteLine();
        foreach (var owner in owners)
        {
            Console.WriteLine();
            Console.WriteLine($" Id: {owner.Id} Namn: {owner.FirstName} {owner.LastName}");
        }

        var updatedOwner = new OwnersDto();
        
        Console.WriteLine();
        Console.WriteLine();
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

    private async Task ShowUpdateBreeder()
    {
        Console.Clear();
        var breeders = await _breedersService.GetAllBreedersAsync();
        Console.Clear();
        MenuTitle("**** Uppdatera information om uppfödare ****");
        Console.WriteLine();
        Console.WriteLine("Alla uppfödare i databasen");
        Console.WriteLine();
        foreach (var breeder in breeders)
        {
            Console.WriteLine();
            Console.WriteLine($" Id: {breeder.Id} Namn: {breeder.FirstName} {breeder.LastName}");
        }

        var updatedBreeder = new BreedersDto();

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();

        Console.Write("Ange uppfödarens Id :");
        updatedBreeder.Id = int.Parse(Console.ReadLine()!);
        Console.Write("Förnamn: ");
        updatedBreeder.FirstName = Console.ReadLine()!;
        Console.Write("Efternamn: ");
        updatedBreeder.LastName = Console.ReadLine()!;
        Console.Write("E-postadress: ");
        updatedBreeder.Email = Console.ReadLine()!;

        await _breedersService.UpdateBreederAsync(updatedBreeder);
    } //Färdig

    private async Task ShowDeleteOwner()
    {
        Console.Clear();
        var owners = await _ownersService.GetAllOwnersAsync();
        Console.Clear();
        MenuTitle("**** Ta bort ägare ur registret ****");
        Console.WriteLine();
        Console.WriteLine("Alla ägare i databasen");
        Console.WriteLine();
        foreach (var owner in owners)
        {
            Console.WriteLine();
            Console.WriteLine($" Id: {owner.Id} Namn: {owner.FirstName} {owner.LastName}");
        }

        Console.WriteLine();
        Console.WriteLine();
        Console.Write("Id för ägaren att ta bort: ");
        var id = int.Parse(Console.ReadLine()!);
        var deleteOwner = await _ownersService.DeleteOwnerAsync(x => x.Id == id);
        Console.Clear();
        if (deleteOwner == true)
        {
            Console.WriteLine("Ägare borttagen");
        }
        else
        {
            Console.WriteLine("Något gick fel");
        }
        Console.ReadKey();


    } //Färdig

    private async Task ShowDeleteBreeder()
    {
        Console.Clear();
        var breeders = await _breedersService.GetAllBreedersAsync();
        Console.Clear();
        MenuTitle("**** Ta bort uppfödare ur registret ****");
        Console.WriteLine();
        Console.WriteLine("Alla uppfödare i databasen");
        Console.WriteLine();
        foreach (var breeder in breeders)
        {
            Console.WriteLine();
            Console.WriteLine($" Id: {breeder.Id} Namn: {breeder.FirstName} {breeder.LastName}");
        }

        Console.WriteLine();
        Console.WriteLine();
        Console.Write("Id för uppfödare att ta bort: ");
        var id = int.Parse(Console.ReadLine()!);
        var deleteBreeder = await _breedersService.DeleteBreederAsync(x => x.Id == id);
        Console.Clear();
        if (deleteBreeder == true)
        {
            Console.WriteLine("Uppfödare borttagen");
        }
        else
        {
            Console.WriteLine("Något gick fel");
        }
        Console.ReadKey();

    } //Färdig

    private async Task ShowExitApp()
    {
        Console.Clear();
        Console.Write("Är du säker på att du vill avsluta programmet? (j - ja / n - nej): ");
        var option = Console.ReadLine() ?? "";

        if (option.Equals("j", StringComparison.CurrentCultureIgnoreCase))
        {
            Environment.Exit(0);

        }
        else
        {
            await ShowMainMenu();
        }
    } //Färdig

    private void MenuTitle(string title)
    {
        Console.Clear();
        Console.WriteLine($"*** {title} ***");
        Console.WriteLine();
    } //Färdig
} 
