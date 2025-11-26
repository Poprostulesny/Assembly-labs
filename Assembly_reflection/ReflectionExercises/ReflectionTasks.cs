using System.Reflection;
using Assembly_Reflection.Attributes;
using Assembly_Reflection.Abstractions;

namespace Assembly_Reflection.ReflectionExercises;

public static class ReflectionTasks
{
    // 2) Z assembly wyciągnąć typy oznaczone danym atrybutem
    //
    // Zadanie:
    // - Zwróć wszystkie typy z podanego assembly, które mają atrybut TAttribute.
    // - Użyjesz tego razem z PluginAttribute.
    //
    // Podpowiedzi:
    // - Assembly ma metodę GetTypes().
    // - Type ma metody do sprawdzania atrybutów (np. sprawdź nazwy metod z "CustomAttributes").
    public static IEnumerable<Type> GetTypesWithAttribute<TAttribute>(Assembly assembly)
        where TAttribute : Attribute
    {
        var all_types = assembly.GetTypes();
        foreach (var type in all_types)
        {
            if (type.GetCustomAttributes(typeof(TAttribute), false).Length > 0)
            {
                yield return type;
            }
        }
        
    }

    // 3) Z assembly wyciągnąć typy dziedziczące po abstrakcyjnej klasie
    //
    // Zadanie:
    // - Zwróć wszystkie nieabstrakcyjne klasy, które dziedziczą po TBase.
    // - Użyjesz tego z JobBase -> EmailJob / SmsJob.
    //
    // Podpowiedzi:
    // - Znowu: assembly.GetTypes().
    // - Sprawdź: czy typ jest klasą, czy nie jest abstrakcyjny.
    // - Poszukaj właściwości/metody typu Type, która pozwala sprawdzić,
    //   czy jeden typ jest "rodzajem" drugiego (relacja dziedziczenia).
    public static IEnumerable<Type> GetDerivedTypes<TBase>(Assembly assembly)
    {
       var  all_types = assembly.GetTypes();
       foreach (var type in all_types)
       {
           //if type is not abstrackt
           if (type.IsAbstract || type.IsInterface || type.IsEnum)
           {
               continue;
           }

           if (type.IsSubclassOf(typeof(TBase)) || typeof(TBase).IsAssignableFrom(type)) 
           {
               yield return type;
           }
       }
    }

    // 4) Z typu wyciągnąć właściwości oznaczone atrybutem
    //
    // Zadanie:
    // - Dla podanego typu znajdź wszystkie jego właściwości, które mają atrybut TAttribute.
    // - Zwróć kolekcję PropertyInfo.
    //
    // Podpowiedzi:
    // - Type ma metodę zwracającą właściwości (sprawdź nazwy typu "GetProperties").
    // - PropertyInfo ma metody do sprawdzania atrybutów (bardzo podobne do Type).
    public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<TAttribute>(Type type)
        where TAttribute : Attribute
    {
        var all_properties = type.GetProperties();
        foreach (var property in all_properties)
        {
            var attributes = property.GetCustomAttributes(typeof(TAttribute), false);
            if (attributes.Length > 0)
            {
                yield return property;
            }
           
        }
    }

    // 5) Sprawdzić, czy typ implementuje generyczny interfejs
    //
    // Zadanie:
    // - Dla typu "type" sprawdź, czy implementuje interfejs, którego definicja generyczna
    //   to genericInterfaceDefinition (np. IRepository<>).
    // - Przykład użycia: ImplementsGenericInterface(typeof(UserRepository), typeof(IRepository<>))
    //   -> powinno zwrócić true.
    //
    // Podpowiedzi:
    // - Type ma metodę zwracającą listę interfejsów, które implementuje.
    // - Interfejs może być generyczny: sprawdź właściwość typu "IsGenericType".
    // - Dla generycznego interfejsu możesz pobrać jego "szablon" (definicję): poszukaj metody
    //   z nazwą w stylu "GetGenericTypeDefinition".
    public static bool ImplementsGenericInterface(Type type, Type genericInterfaceDefinition)
    {
        var interfaces = type.GetInterfaces();
        foreach (var _interface in interfaces)
        {
            if (_interface.IsGenericType && _interface.GetGenericTypeDefinition() == genericInterfaceDefinition)
            {
                return true;
            }
        }

        return false;
    }

    // 6) Ustawianie wartości właściwości na instancji (.SetValue)
    //
    // Zadanie:
    // - Masz obiekt "target" oraz słownik: nazwaWłaściwości -> wartość.
    // - Dla każdej pary w słowniku znajdź właściwość po nazwie i ustaw jej wartość na obiekcie.
    // - Wykorzystasz to do "mapowania" danych na obiekty (np. User).
    //
    // Podpowiedzi:
    // - Type możesz dostać z target.GetType().
    // - Znajdź właściwość po nazwie (Type ma metodę typu "GetProperty").
    // - PropertyInfo ma metodę, która pozwala ustawić wartość: zwróć uwagę na nazwę "SetValue".
    // - Uważaj na różnice typów – jeśli chcesz, możesz spróbować użyć konwersji (np. Convert.ChangeType),
    //   ale na początek możesz założyć, że typy już pasują.
    public static void SetPropertiesFromDictionary(object target, IDictionary<string, object?> values)
    {
        var target_properties = target.GetType().GetProperties();
        foreach (var property in target_properties)
        {
            if (values.ContainsKey(property.Name) )
            {
                property.SetValue(target, values[property.Name], null);
            }
            
        }
    }
}
