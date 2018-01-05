using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class OtherMetods
    {
        private static Random rndNumber = new Random((int)DateTime.Now.Ticks);

        public static string RandomNumber(int maxNum)
        {
            return rndNumber.Next(maxNum).ToString();
        }
    }
}
