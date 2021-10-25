using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Availity_01
{
    class Program
    {
        static void Main(string[] args)
        {
            //source of Lisp code:  http://www.lispworks.com/documentation/lcl50/ug/ug-22.html#HEADING22-0

            string input_good = @"(defun queue-next (queue ptr) 'Increment a queue pointer by 1 and wrap around if needed.'
                            _ (let((length(length(queue - elements queue)))
                            _ (try (the fixnum(1 + ptr))))
                            _ (if (= try length) 0 try)))";
            string input_bad = @"(defun queue-next (queue ptr) 'Increment a queue pointer by 1 and wrap around if needed.'
                            _ (let((length(length(queue - elements queue)))
                            _ (try (the fixnum(1 + ptr)))))()
                            _ (if (= try length) 0 try)))";
            //Console.WriteLine(input_good);

            string pattern = @"[()]";
            Regex rx = new Regex(pattern);

            List<string> parens = Regex.Matches(input_good, pattern).Cast<Match>().Select(x => x.Value).ToList();

            List<string[]> starting = new List<string[]>();
            int num_open_paren = 0;
            List<string[]> result = FindPairs(parens, parens[0], ref starting, ref num_open_paren);

            /*
            if ((parens.Count % 2) > 0)
            {
                Console.WriteLine("");
            }
            else
            {
                bool result = (Recurse(parens, parens[0])) ? true : false;

            }
            */

            Console.WriteLine("Result: " + result.ToString());
            Console.Read();


            /*
            MatchCollection rx_result = rx.Matches(input_good);
            //MatchCollection rx_result = rx.Matches(input_bad);
            
            List<string> parens = new List<string>();
            int count = 0;
            foreach (Match result in rx_result)
            {
                string x = result.ToString();
                if (string.Equals(x, "("))
                    count++;
                else if (string.Equals(x, ")"))
                    count--;
            }
            
            Console.WriteLine(count == 0 ? "String closed and nested." : "String has wrong parentheses.");
            */
        }

        private static List<string[]> FindPairs(List<string> parens, string curr, ref List<string[]> pairs, ref int num)
        {
            string next = parens[1];

            //if (num >= pairs.Count)
            //{
            string[] pair = { curr, string.Empty };
            pairs.Add(pair);
            //}

            if (string.Equals(curr, "(") && string.Equals(next, ")"))
            {
                pairs.ElementAt(num)[1] = next;
                parens.RemoveAt(0);

                return pairs;
            }
            else //if (parens.Count > 0)
            {
                parens.RemoveAt(0);
                num++;
                FindPairs(parens, next, ref pairs, ref num);
            }
            if (parens.Count > 0)
            {
                //num++;
                FindPairs(parens, curr, ref pairs, ref num);
            }
            return pairs;
        }

        private static bool Recurse(List<string> parens, string prev)
        {
            bool result = false;
            string curr = parens[1];
            if (string.Equals(prev, "(") && string.Equals(curr, ")")) {
                parens.RemoveAt(0);
                return true;
            }
            else {
                result = false;
                if (parens.Count > 0) {
                    parens.RemoveAt(0);
                    result = Recurse(parens, curr);
                }
            }
            if (parens.Count > 0)
            {
                result = Recurse(parens, prev);
            }
            return result;
        }
    }
}
