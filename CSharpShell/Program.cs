
using System;
using Mono;
using Mono.CSharp;
using RunCSharp;

namespace de.ahzf.MonoCompilerAsAService
{

    public class Program
    {

        private static Runner _Compiler;

        public static void Main(String[] myArgs)
        {
             _Compiler = new Runner();

            //var a = _Compiler.Execute("Math.Abs(-42);");
            //Assert.AreEqual(42, a);

            //var b = _Compiler.Execute("class Fact { public int Run(int n) { return n <= 0 ? 1 : n*Run(n-1); } }");
            //var c = _Compiler.Execute("new Fact().Run(5);");
            //Assert.AreEqual(120, c);

            //var d = _Compiler.Execute("\"abcdefgh\".Substring(1, 2);");
            //Assert.AreEqual("bc", d);

            //var e = _Compiler.Execute("var test = 123;");
            //Assert.AreEqual("bc", e);


            #region Feel free to step through...

            _Compiler = new Runner();
            var a = _Compiler.Execute("Math.Abs(-42);");
            var b = _Compiler.Execute("Math.Sin(Math.PI / 6);");
            var c = _Compiler.Execute("class Fact { public int Run(int n) { return n <= 0 ? 1 : n*Run(n-1); } }");
            var d = _Compiler.Execute("new Fact().Run(5);");
            var e = _Compiler.Execute("\"abcdefgh\".Substring(1, 2);");
            var f = _Compiler.Execute("class Echo { public Object Print(Object o) { return o; } }");
            var g = _Compiler.Execute("var test = 123;");
            var h = _Compiler.Execute("new Echo().Print(test);");
            
            #endregion

            #region Start the interactive (read-eval-print loop) shell...

            var _Report = new Report(new ConsoleReportPrinter());
            var _CLP    = new CommandLineParser(_Report);
                _CLP.UnknownOptionHandler += Mono.Driver.HandleExtraArguments;

            var _Settings = _CLP.ParseArguments(myArgs);
            if (_Settings == null || _Report.Errors > 0)
                Environment.Exit(1);

            var _Evaluator = new Evaluator(_Settings, _Report)
            {
                InteractiveBaseClass    = typeof(InteractiveBaseShell),
                DescribeTypeExpressions = true
            };

            //// Adding a assembly twice will lead to delayed errors...
            //_Evaluator.ReferenceAssembly(typeof(YourAssembly).Assembly);

            var _CSharpShell = new CSharpShell(_Evaluator).Run();

            #endregion

        }

    }

}
