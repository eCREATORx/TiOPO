using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using triangle = Triangle.Triangle;

namespace Triangle_test
{
    [TestClass]
    public class Triangle_test
    {
        [TestMethod]
        public void TestFromFile()
        {
            FileStream fileIn = new FileStream("../../../input.txt", FileMode.Open);
            FileStream fileOut = new FileStream("../../../output.txt", FileMode.Create);
            StreamReader reader = new StreamReader(fileIn);
            StreamWriter writer = new StreamWriter(fileOut);
            int i = 1;
            while (!reader.EndOfStream)
            {
                string str = reader.ReadLine();
                string[] args = str.Split(' ');
                triangle.Main(args);
                str = reader.ReadLine();
                if (str == triangle.result)
                {
                    writer.WriteLine("{0} success", i);
                }
                else
                {
                    writer.WriteLine("{0} error", i);
                }
                i++;
            }
            reader.Close();
            writer.Close();
        }
    }
}