using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiling
{
    class Generator
    {
        public static string GenerateDeclarations()
        {
            var builder = new StringBuilder();
            foreach (var fieldCount in Constants.FieldCounts)
            {
                builder.Append(
                    GetElementString(fieldCount, "struct S") +
                    GetElementString(fieldCount, "class C"));
            }
            return builder.ToString();
        }

        private static string GetElementString(int fieldsAmount, string elementName)
        {
            return "\n" + elementName + fieldsAmount +
                   "\n{\n" +
                   GetByteFields(fieldsAmount) +
                   "\n}";
        }

        private static string GetByteFields(int fieldsAmount)
        {
            var result = "";
            for (var i = 0; i < fieldsAmount; i++)
                result += (i == 0 ? "" : " ") + "byte Value" + i + ";";
            return result;
        }


        public static string GenerateArrayRunner()
        {
            var builder = new StringBuilder("public class ArrayRunner : IRunner\r\n{");
            foreach (var fieldCount in Constants.FieldCounts)
            {
                var fc = fieldCount.ToString();
                builder.Append(
                    "void PC" + fc + "()\r\n" +
                    "{\r\n" +
                    "var array = new C" + fc + "[Constants.ArraySize];\r\n" +
                    "for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C" + fc + "();\r\n" +
                    "}\r\n" +
                    "void PS" + fc + "()\r\n" +
                    "{\r\n" +
                    "var array = new S" + fc + "[Constants.ArraySize];\r\n" +
                    "}\r\n"
                );
            }
            builder.Append(GenerateCallArrayMethod());
            return builder.ToString();
        }
		
        public static string GenerateCallArrayMethod()
        {
            var builder= new StringBuilder("public void Call(bool isClass, int size, int count)\r\n{");
            foreach (var fieldCount in Constants.FieldCounts)
            {
                var fc = fieldCount.ToString();
                builder.Append(
                    "if (isClass && size == " + fc + ")\r\n" +
                    "{\r\n" +
                    "for (int i = 0; i < count; i++) PC" + fc + "();\r\n" +
                    "return;\r\n" +
                    "}\r\n" +
                    "if (!isClass && size == " + fc + ")\r\n" +
                    "{\r\n" +
                    "for (int i = 0; i < count; i++) PS" + fc + "();\r\n" +
                    "return;\r\n" +
                    "}\r\n"
                );
            }
            builder.Append("throw new ArgumentException();\r\n}\r\n}");
            return builder.ToString();
        }

        public static string GenerateCallRunner()
        {
            var builder = new StringBuilder("public class CallRunner : IRunner\r\n{");
            FillWithMethods(builder);
            GenerateCallRunnerMethod(builder);
            return builder.ToString();
        }

        private static void FillWithMethods(StringBuilder builder)
        {
            foreach (var fieldCount in Constants.FieldCounts)
            {
                var fc = fieldCount.ToString();
                builder.Append("void PC" + fc + "(C" + fc + " o) { }\r\n" +
                               "void PS" + fc + "(S" + fc + " o) { }");
            }
        }

        private static void GenerateCallRunnerMethod(StringBuilder builder)
        {
            builder.Append("public void Call(bool isClass, int size, int count)\r\n{");
            foreach (var fieldCount in Constants.FieldCounts)
            {
                var fc = fieldCount.ToString();
                builder.Append(
                    "  if (isClass && size == " + fc + ")\r\n" +
                    "{\r\n" +
                    "var o = new C" + fc + "(); for (int i = 0; i < count; i++) PC" + fc + "(o);\r\n" +
                    "return;\r\n" +
                    "}\r\n" +
                    "if (!isClass && size == " + fc + ")\r\n" +
                    "{\r\n" +
                    "var o = new S" + fc + "(); for (int i = 0; i < count; i++) PS" + fc + "(o);\r\n" +
                    "return;\r\n}"
                );
            }
            builder.Append("throw new ArgumentException();\r\n}\r\n}");
        }
    }
}