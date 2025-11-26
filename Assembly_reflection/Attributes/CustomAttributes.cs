namespace Assembly_Reflection.Attributes;


// Stwórz atrybut PluginAttribute, którym będziesz oznaczał klasy pluginów.
//
// Wymagania (zrób sam, nie ma gotowego kodu):
// - Dziedziczy po Attribute.
// - Może być używany na klasach.
// - Zastanów się, czy może być dziedziczony i czy może wystąpić wiele razy.
// - Dodaj np. właściwości: string Name, int Version.
[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class PluginAttribute : Attribute
{
   
    public string Name { get; set; }
    public int Version { get; set; }
    
    public PluginAttribute(string name, int version)
    {
        Name = name;
        Version = version;
    }
} 
    



// Stwórz atrybut ColumnAttribute, którego użyjesz do oznaczania właściwości klasy User.
//
// Wymagania (do przemyślenia):
// - Dziedziczy po Attribute.
// - Atrybut na właściwościach.
// - Dodaj np. właściwość string ColumnName.
// - Opcjonalnie: bool IsKey, bool IsRequired
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ColumnAttribute : Attribute
{
   public string ColumnName { get; set; }
   public bool isKey {get;set;}
    public bool isRequired {get;set;}

    public ColumnAttribute(string columnname, bool iskey, bool isrequired)
    {
        ColumnName = columnname;
        isKey = iskey;
        isRequired = isrequired;
    }
    
}


