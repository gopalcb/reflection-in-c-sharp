## Reflection in C# Example:

Reflection provides objects (of type Type) that describe assemblies, modules and types. We can use reflection to dynamically create an instance of a type, bind the type to an existing object, or get the type from an existing object and invoke its methods or access its fields and properties. If we use attributes in our code, reflection enables us to access them.

Here's a simple example of reflection using the static method GetType - inherited by all types from the Object base class - to obtain the type of a variable.

```C#
// Using GetType to obtain type information:   
int i = 42;  
System.Type type = i.GetType();   
System.Console.WriteLine(type);  
```
 
The output is:

```C#
System.Int32
```
 
The following example uses reflection to obtain the full name of the loaded assembly.

```C#
// Using Reflection to get information from an Assembly:  
System.Reflection.Assembly info = typeof(System.Int32).Assembly;  
System.Console.WriteLine(info);
```

The output is:

```C#
mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
```

## Implementation:

Firstly, we will create a C# console application and add two classes named Student.cs and StudentFunction.cs. The Student.cs class contains some properties of Student and the StudentFunction.cs class contains methods returning different properties value.

Student.cs class

```C#
public class Student  
{  
   public string Name { get; set; }  
   public string University { get; set; }  
   public int Roll { get; set; }  
} 
```

StudentFunction.cs class

```C# 
class StudentFunction  
    {  
        private Student student;  
        public StudentFunction()  
        {  
            student = new Student  
            {  
                Name = "Gopal C. Bala",   
                University = "Jahangirnagar University",   
                Roll = 1424  
            };  
        }  
  
        public string GetName()  
        {  
            return student.Name;  
        }  
  
        public string GetUniversity()  
        {  
            return student.University;  
        }  
  
        public int GetRoll()  
        {  
            return student.Roll;  
        }  
    } 
```

Our goal is to dynamically create an instance of StudentFunction and get values from GetName(), GetUniversity() and GetRoll() method at the compile time.

In Program.cs class we need to create a Dictionary which contains the method name as string. The name of the method here as string will be compared with the given string with dynamic tag formatted as [tag name]. If both match, then we need to get the method type.

The Dictionary will be like the following:

```C#
private static Dictionary<string, string> GetMethodsDictionary()  
{  
   var dictionary = new Dictionary<string, string>  
   {  
      {"GetName", "GetName"},   
      {"GetUniversity", "GetUniversity"},  
      {"GetRoll","GetRoll"}  
   };  
   return dictionary;  
}
```

In Main function under Program.cs class we have to call GetMethodsDictionary().

```C#
_methodDictionary = new Dictionary<string, string>();  
_methodDictionary = GetMethodsDictionary(); 
```

Now we’ll write the code for creating the instance of StudentFunction at compile time.

```C#
var type = typeof(StudentFunction);  
var studentFunctionInstance = Activator.CreateInstance(type, new object[] { }); 
```

The string will be like the following that contains the dynamic tag based on which we have to retrieve value of student properties.

```C#
var testString = "Hello [GetName], your university name is [GetUniversity] and roll is [GetRoll]";
```

Here [GetName], [GetUniversity] and [GetRoll] are actually method names which will be compared with the methods declared in StudentFunction class.

Now we will check the mentioned tag ([GetName] and others..) from the defined string using Regular Expression and invoke the value receiving from the methods of StudentFunction.

```C#
var match = Regex.Matches(testString, @"\[([A-Za-z0-9\-]+)]", RegexOptions.IgnoreCase);  
foreach (var v in match)  
{  
   var originalString = v.ToString();  
   var x = v.ToString();  
   x = x.Replace("[", "");  
   x = x.Replace("]", "");  
   x = _methodDictionary[x];  
  
   var toInvoke = type.GetMethod(x);  
   var result = toInvoke.Invoke(studentFunctionInstance, null);  
   testString = testString.Replace(originalString, result.ToString());  
}
```

So the entire Program.cs class will be like the following:

```C#
class Program  
{  
    private static Dictionary<string, string> _methodDictionary;  
    static void Main(string[] args)  
    {  
        _methodDictionary = new Dictionary<string, string>();  
        _methodDictionary = GetMethodsDictionary();  
  
        var type = typeof(StudentFunction);  
        var studentFunctionInstance = Activator.CreateInstance(type);  
  
        var testString = "Hello [GetName], your university name is [GetUniversity] and roll is [GetRoll]";  
        var match = Regex.Matches(testString, @"\[([A-Za-z0-9\-]+)]", RegexOptions.IgnoreCase);  
        foreach (var v in match)  
        {  
            var originalString = v.ToString();  
            var x = v.ToString();  
            x = x.Replace("[", "");  
            x = x.Replace("]", "");  
            x = _methodDictionary[x];  
  
            var toInvoke = type.GetMethod(x);  
            var result = toInvoke.Invoke(studentFunctionInstance, null);  
            testString = testString.Replace(originalString, result.ToString());  
        }  
  
        Console.WriteLine(testString);  
    }  
  
    private static Dictionary<string, string> GetMethodsDictionary()  
    {  
        var dictionary = new Dictionary<string, string>  
        {  
            {"GetName", "GetName"},   
            {"GetUniversity", "GetUniversity"},  
            {"GetRoll","GetRoll"}  
        };  
        return dictionary;  
    }  
} 
```
