using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Mono.CSharp;

namespace RunCSharp
{
    class Program
    {

        static Runner _Compiler;

        static void Main(String[] args)
        {

            _Compiler = new Runner();
            var a = _Compiler.Execute("Math.Abs(-42);");
            var b = _Compiler.Execute("Math.Sin(Math.PI / 6);");
            var c = _Compiler.Execute("class Fact { public int Run(int n) { return n <= 0 ? 1 : n*Run(n-1); } }");
            var d = _Compiler.Execute("new Fact().Run(5);");

            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);
            Console.WriteLine(d);

        }

    }

}
