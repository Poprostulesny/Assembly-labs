

using Assembly_Reflection.Attributes;

namespace Assembly_Reflection.Models;

// Użyjesz tego do zadania z dziedziczeniem (3)
public abstract class JobBase
{
    public abstract string Name { get; }
    public abstract void Execute();
}

// Kilka przykładowych implementacji:
[PluginAttribute("EmailJob", 1)]
public class EmailJob : JobBase
{
    public override string Name => "Email";
    public override void Execute()
    {
        // TODO: możesz zostawić puste, to nie jest istotne dla reflection
    }
}
[PluginAttribute("SmsJob", 2)]
public class SmsJob : JobBase
{
    public override string Name => "SMS";
    public override void Execute()
    {
        // TODO
    }
}

// Użyjesz tego do zadań z właściwościami i atrybutami (4, 6)
public class User
{
    // Np. to będzie klucz główny
    [Column("Id", true,true )]
    public int Id { get; set; }

    // Np. wymagane pole, użyjesz własnego atrybutu
    [Column("FirstName", false,true )]
    public string FirstName { get; set; }
    [Column("LastName", false,true )]
    public string LastName { get; set; }

    // Np. pole, które ma być ignorowane przy mapowaniu
   
    public string InternalNotes { get; set; }
}