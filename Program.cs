using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class Program
    {
        public static void Main()
        {
            Console.WriteLine();
            string input = Console.ReadLine();

            List<string> elements = new List<string>();
            string[] split = input.Split(' ');
            for (int i = 0; i < split.Length; i++)
            {
                elements.Add((string)split[i]);

            }
            foreach (string c in ToRPN(elements))
            {
                Console.WriteLine(c);
            }
        }


        static List<string> ToRPN(List<string> elements)
        {
            List<string> st = new List<string>();
            List<string> rpn = new List<string>();
            bool isHighPriorety = false;
            for (int i = 0; i < elements.Count; i++)
            {
                string z = elements[i];
                if (int.TryParse(elements[i], out int result))
                {
                    rpn.Add(z);
                }
                if (z == "*" | z == "/")
                {
                    st.Add(z);
                    isHighPriorety = true;
                }
                if ((z == "+" | z == "-") & (isHighPriorety == false))
                {
                    st.Add(z);
                    isHighPriorety = false;
                }
                if ((z == "+" | z == "-") & (isHighPriorety == true))
                {
                    if (st.Contains("("))
                    {
                        int br = st.IndexOf("(");
                        for (int j = st.Count - 1; j > br; j--)
                        {
                            rpn.Add(st[j]);
                            st.RemoveAt(j);
                        }
                        st.Add(z);
                    }
                    else
                    {
                        for (int j = st.Count - 1; j >= 0; j--)
                        {
                            rpn.Add(st[j]);
                            st.RemoveAt(j);

                        }
                        st.Add(z);
                    }
                }

                if (z == "(")
                {
                    st.Add(z);
                    isHighPriorety = false;
                }
                if (z == ")")
                {
                    int br = st.IndexOf("(");
                    for (int j = st.Count - 1; j >= br; j--)
                    {
                        rpn.Add(st[j]);
                        st.RemoveAt(j);
                    }
                    rpn.Remove(")");
                    rpn.Remove("(");
                }
            }

            for (int j = st.Count - 1; j >= 0; j--)
            {
                rpn.Add(st[j]);
                st.RemoveAt(j);

            }
            return rpn;
        }



    }
}