using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsFriday
{
    class Program
    {
        //public event EventHandler MyEvent;

        public delegate int m_event_handler_delegate(object sender, EventArgs e);
        public event m_event_handler_delegate MyEvent1;
        public EventHandler MyEvent2;

        public delegate void foo_delegate(int x);
        public foo_delegate my_del1;
        public Action<int> my_del2;

        public delegate double signature_returns_double_no_args();
        public delegate string signature_returns_string_no_args();
        public delegate void signature_returns_void_no_args();
        public delegate void signature_returns_void_string_string(string a, string b);
        public delegate void signature_returns_void_args_int_array(int[] arr);

        static signature_returns_void_args_int_array function_list_signature_returns_void_args_int_array;

        static void function_signature_returns_void_args_int_array(int[] arr)
        {

        }

        static void foo(int[] arr)
        {

        }

        static void foo2(string[] arr)
        {

        }
       

        static void Do1() { Console.WriteLine("1"); }
        static void Do2() { Console.WriteLine("2"); }
        static void Do3() { Console.WriteLine("3"); }

        public delegate void func_empty();

        // no return value --> Action
        // return value --> Func

        static int Add1(double x)
        {
            return (int)(x + 1);
        }

        public delegate int func_get_int_returns_int(double x);
        func_get_int_returns_int del_int_int;
        Func<double, int> del_get_int_returns_double;

        static void DoMathOpertion(double number, Func<double, double> oper)
        {
            Console.WriteLine(oper(number));
        }

        // Dictionary<string, Action<string>>

        static void Main(string[] args)
        {

            if (function_list_signature_returns_void_args_int_array == null)
            {

            }

            function_list_signature_returns_void_args_int_array =
                new signature_returns_void_args_int_array(function_signature_returns_void_args_int_array);

            function_list_signature_returns_void_args_int_array += foo;
            // function_list_signature_returns_void_args_int_array += foo2; // error

            function_list_signature_returns_void_args_int_array += (int[] arr) => { }; // add to list
            function_list_signature_returns_void_args_int_array = (int[] arr) => { }; // replace list

            Action<int[]> list_funcs_arr = function_signature_returns_void_args_int_array;
            list_funcs_arr += foo;

            //new Action(() => { Console.WriteLine("hi");
            //int[] arr = new int[100000];
            //}).Invoke();

            func_empty list_of_func = Do1;
            list_of_func += Do2;
            list_of_func += Do3;

            list_of_func();
            //list_of_func.BeginInvoke // async

            var list1 = list_of_func.GetInvocationList();

            Action list_of_func_action = Do1;
            list_of_func_action += Do2;
            list_of_func_action += Do3;


            BankOfAmerica boa = new BankOfAmerica();
            StocksBoard board = new StocksBoard(boa);

            boa.StockChanged();

            List<int> numbers = new List<int>() { 5, 8, 10, 15, 22 };
            //numbers.FindIndex


        }
    }
}
