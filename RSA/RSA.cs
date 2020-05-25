using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class RSA
    {
        Random rnd = new Random();
        
        public double RelativPrim() //i a kisebb, modulus a nagyobb
        {
            double i = 1;
            double lnko = 0;
            double modulus = Modulus();
            for ( i = 1; i < modulus; i++)
            {

                for (double j = 1; j < i; j++)
                {
                    if (i % j == 0 && modulus % j == 0)
                    {
                        lnko = j;
                        Console.WriteLine(lnko);
                        if (lnko == 1)
                            break;
                    }
                }
                
            }
            return i;
        }

        public double Modulus()
        {
            double p;
            double q;
            do
            {
                p = rnd.Next(50, 150);
            }
            while (PrimCheck(p) != true);
            do
            {
                q = rnd.Next(50, 150);
            }
            while (PrimCheck(q) != true);
            Console.WriteLine(p);
            Console.WriteLine(q);
            return p * q;
        }

        public bool PrimCheck(double num)
        {
            for (int i = 2; i <= num/2; i++)
            {
                if(num % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
