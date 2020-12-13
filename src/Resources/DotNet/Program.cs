using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Threading;
using System.Text;

namespace TemplateConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<object> parameters = new List<object>();
            List<string> input = new List<string>() {"userInput"};

            Type programType = typeof(Program);
            object programInstance = Activator.CreateInstance(programType);

            MethodInfo toInvoke = programType.GetMethod("{MethodName}",
                BindingFlags.Public | 
                BindingFlags.Static | 
                BindingFlags.NonPublic |
                BindingFlags.Instance);

            ParameterInfo[] methodParameters = toInvoke.GetParameters();

            var inputCount = 0;

            foreach (ParameterInfo parameterInfo in methodParameters)
            {
                Type type = parameterInfo.ParameterType;
                
                var parameter = StringToObject(type, input[inputCount++]);

                parameters.Add(parameter);
            }
            
            Console.WriteLine(toInvoke.Invoke(programInstance, parameters.ToArray()));
        }
        
        public static object StringToObject(Type type, string input)
        {
            if (!(type.IsGenericType || type.IsArray))
            {
                return (object)Convert.ChangeType(input, type);
            }

            var elementType = type.IsArray ? type.GetElementType() : type.GetGenericArguments()[0];

            string[] entries = input.Split(',');
            int numberOfEntries = entries.Length;

            Array array = Array.CreateInstance(elementType, numberOfEntries);

            for(int i = 0; i < numberOfEntries; i++)
                array.SetValue(Convert.ChangeType(entries[i], elementType), i);
            
            if (type.IsGenericType)
            {
                Type genericListType = typeof(List<>);
                Type concreteListType = genericListType.MakeGenericType(elementType);

                object list = Activator.CreateInstance(concreteListType, new object[] { array });

                return (object)(list);
            }

            return (object)array;
        }

        {newCode}
    }
}