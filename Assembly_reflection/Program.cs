
#define TASK2
#define TASK3
 #define TASK4
 #define TASK5
 #define TASK6
using System.Reflection;
using Assembly_Reflection.Models;
using Assembly_Reflection.Attributes;
using Assembly_Reflection.Abstractions;
using Assembly_Reflection.ReflectionExercises;

// Prosty "test runner" do ręcznego i pół-automatycznego sprawdzania zadań.

Console.WriteLine("=== Assembly_Reflection – test harness ===\n");

var assembly = Assembly.GetExecutingAssembly();

// Po kolei odpalamy testy do Twoich zadań:
#if TASK2
RunTask2_TypesWithPluginAttribute(assembly);
#endif

#if TASK3
RunTask3_DerivedTypes(assembly);
#endif

#if TASK4
RunTask4_PropertiesWithColumnAttribute();
#endif

#if TASK5
RunTask5_GenericInterfaceCheck();
#endif

#if TASK6
RunTask6_SetPropertiesFromDictionary();
#endif

Console.WriteLine("\n=== Wszystkie testy wykonane. Wciśnij dowolny klawisz, aby zakończyć. ===");
Console.ReadKey();


// ---------------------- Zadanie 2 ----------------------

static void RunTask2_TypesWithPluginAttribute(Assembly assembly)
{
    Console.WriteLine("== Zadanie 2: typy z [PluginAttribute] ==");

    var pluginTypes = ReflectionTasks.GetTypesWithAttribute<PluginAttribute>(assembly).ToList();

    if (pluginTypes.Count == 0)
    {
        Console.WriteLine("Brak znalezionych typów z [PluginAttribute]. Sprawdź:");
        Console.WriteLine("- czy oznaczyłeś klasy (np. EmailJob, SmsJob) atrybutem [PluginAttribute],");
        Console.WriteLine("- czy poprawnie zaimplementowałeś GetTypesWithAttribute.\n");
        return;
    }

    Console.WriteLine("Znalezione typy pluginów:");
    foreach (var t in pluginTypes)
    {
        Console.WriteLine($" - {t.FullName}");
    }

    // Proste sprawdzenie oczekiwanych typów (jeśli takie masz)
    var typeNames = pluginTypes.Select(t => t.Name).ToList();

    bool hasEmailJob = typeNames.Contains("EmailJob");
    bool hasSmsJob = typeNames.Contains("SmsJob");

    if (hasEmailJob && hasSmsJob)
    {
        Console.WriteLine("OK: znaleziono EmailJob i SmsJob jako pluginy.\n");
    }
    else
    {
        Console.WriteLine("UWAGA: nie znaleziono wszystkich oczekiwanych pluginów (EmailJob, SmsJob).\n");
    }
}


// ---------------------- Zadanie 3 ----------------------

static void RunTask3_DerivedTypes(Assembly assembly)
{
    Console.WriteLine("== Zadanie 3: klasy dziedziczące po JobBase ==");

    var jobTypes = ReflectionTasks.GetDerivedTypes<JobBase>(assembly).ToList();

    if (jobTypes.Count == 0)
    {
        Console.WriteLine("Brak znalezionych typów dziedziczących po JobBase.");
        Console.WriteLine("Sprawdź, czy:");
        Console.WriteLine("- masz klasy dziedziczące po JobBase (np. EmailJob, SmsJob),");
        Console.WriteLine("- poprawnie implementujesz GetDerivedTypes.\n");
        return;
    }

    Console.WriteLine("Znalezione klasy jobów:");
    foreach (var t in jobTypes)
    {
        Console.WriteLine($" - {t.FullName}");
    }

    var typeNames = jobTypes.Select(t => t.Name).ToList();
    bool hasEmailJob = typeNames.Contains("EmailJob");
    bool hasSmsJob = typeNames.Contains("SmsJob");

    if (hasEmailJob && hasSmsJob)
    {
        Console.WriteLine("OK: znaleziono EmailJob i SmsJob jako klasy dziedziczące po JobBase.\n");
    }
    else
    {
        Console.WriteLine("UWAGA: nie znaleziono wszystkich oczekiwanych klas (EmailJob, SmsJob).\n");
    }
}


// ---------------------- Zadanie 4 ----------------------

static void RunTask4_PropertiesWithColumnAttribute()
{
    Console.WriteLine("== Zadanie 4: właściwości User z [ColumnAttribute] ==");

    var userType = typeof(User);
    var propsWithColumn = ReflectionTasks
        .GetPropertiesWithAttribute<ColumnAttribute>(userType)
        .ToList();

    if (propsWithColumn.Count == 0)
    {
        Console.WriteLine("Brak właściwości z ColumnAttribute.");
        Console.WriteLine("Sprawdź, czy:");
        Console.WriteLine("- oznaczyłeś właściwości klasy User atrybutem [Column],");
        Console.WriteLine("- poprawnie zaimplementowałeś GetPropertiesWithAttribute.\n");
        return;
    }

    Console.WriteLine($"Znalezione właściwości z ColumnAttribute w typie {userType.FullName}:");
    foreach (var p in propsWithColumn)
    {
        Console.WriteLine($" - {p.Name}");
    }

    Console.WriteLine("Ręcznie porównaj powyższą listę z tym, co oznaczyłeś [ColumnAttribute] w klasie User.\n");
}


// ---------------------- Zadanie 5 ----------------------

static void RunTask5_GenericInterfaceCheck()
{
    Console.WriteLine("== Zadanie 5: sprawdzenie implementacji IRepository<> ==");

    var genericRepoDefinition = typeof(IRepository<>);

    var userRepoType = typeof(UserRepository);
    var jobRepoType  = typeof(JobRepository);
    var userType     = typeof(User);       // typ, który NIE jest repozytorium

    bool userRepoImplements = ReflectionTasks.ImplementsGenericInterface(userRepoType, genericRepoDefinition);
    bool jobRepoImplements  = ReflectionTasks.ImplementsGenericInterface(jobRepoType,  genericRepoDefinition);
    bool userImplements     = ReflectionTasks.ImplementsGenericInterface(userType,     genericRepoDefinition);

    Console.WriteLine($"UserRepository implements IRepository<>: {userRepoImplements}");
    Console.WriteLine($"JobRepository  implements IRepository<>: {jobRepoImplements}");
    Console.WriteLine($"User           implements IRepository<>: {userImplements}");

    if (userRepoImplements && jobRepoImplements && !userImplements)
    {
        Console.WriteLine("OK: Implementacja ImplementsGenericInterface wygląda poprawnie.\n");
    }
    else
    {
        Console.WriteLine("UWAGA: coś jest nie tak z ImplementsGenericInterface (sprawdź warunki).\n");
    }
}


// ---------------------- Zadanie 6 ----------------------

static void RunTask6_SetPropertiesFromDictionary()
{
    Console.WriteLine("== Zadanie 6: ustawianie właściwości User ze słownika ==");

    var user = new User();

    Console.WriteLine("Stan początkowy obiektu User:");
    PrintUser(user);

    var values = new Dictionary<string, object?>
    {
        ["Id"]        = 1,
        ["FirstName"] = "Jarek",
        ["LastName"]  = "Testowy",
        // możesz dodać inne właściwości, jeśli chcesz
    };

    ReflectionTasks.SetPropertiesFromDictionary(user, values);

    Console.WriteLine("Stan po wywołaniu SetPropertiesFromDictionary:");
    PrintUser(user);

    bool ok =
        user.Id == 1 &&
        user.FirstName == "Jarek" &&
        user.LastName == "Testowy";

    if (ok)
    {
        Console.WriteLine("OK: właściwości zostały poprawnie ustawione.\n");
    }
    else
    {
        Console.WriteLine("UWAGA: właściwości nie zostały ustawione tak, jak oczekiwano.\n");
    }
}

static void PrintUser(User user)
{
    Console.WriteLine($"  Id        = {user.Id}");
    Console.WriteLine($"  FirstName = {user.FirstName}");
    Console.WriteLine($"  LastName  = {user.LastName}");
    Console.WriteLine($"  Notes     = {user.InternalNotes}");
}
