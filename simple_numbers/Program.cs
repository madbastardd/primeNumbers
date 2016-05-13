using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_numbers {
    class Program {
        static int max = 1000000;
        static SortedSet<int> primeNumbers = findPrimeNumbers();
        
        static SortedSet<int> findPrimeNumbers() {
            SortedSet<int> tmpSet = new SortedSet<int>();
            for (int i = 2; i <= max; ++i)
                tmpSet.Add(i);

            SortedSet<int> result = new SortedSet<int>();

            while (tmpSet.Count > 0) {
                int first = tmpSet.First();
                
                int tmp = first * 2;

                while (tmp <= max) {
                    tmpSet.Remove(tmp);
                    tmp += first;
                }

                if (!(first >= 10 && (first.ToString().Contains("0") ||
                    first.ToString().Contains("2") ||
                    first.ToString().Contains("4") ||
                    first.ToString().Contains("5") ||
                    first.ToString().Contains("6") ||
                    first.ToString().Contains("8")))) {
                    result.Add(first);
                }

                tmpSet.Remove(first);
            }

            return result;
        }

        static bool isCyclicShiftPrime(int i) {
            foreach (var item in returnCyclicInt(i))
                if (!primeNumbers.Contains(item))
                    return false;
            return true;
        }

        static IEnumerable<int> returnCyclicInt(int i) {
            yield return i;

            bool zero = i.ToString().Length > 2 && i.ToString()[1] == '0';
            int tmp = Int32.Parse(new String(i.ToString().Skip(1).ToArray()) + i.ToString().First());

            while (tmp != i) {
                yield return tmp;

                String analyze = new String(tmp.ToString().Skip(1).ToArray());

                if (!zero)
                    tmp = Int32.Parse(analyze + tmp.ToString().First());
                else
                    tmp = Int32.Parse(analyze + tmp.ToString().First() + '0');

                zero = analyze[0] == '0';
            }
        }
        static void Main(string[] args) {
            SortedSet<int> allPrime = new SortedSet<int>();
            foreach (var i in primeNumbers)
                if (!allPrime.Contains(i) && isCyclicShiftPrime(i))
                    foreach (var item in returnCyclicInt(i)) 
                        allPrime.Add(item);

            Console.WriteLine(allPrime.Count);
        }
    }
}
