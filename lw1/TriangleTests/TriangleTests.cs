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
            while (!reader.EndOfStream)
            {
                string str = reader.ReadLine();
                string[] args = str.Split(' ');
                triangle.Main(args);
                str = reader.ReadLine();
                if (str == triangle.result)
                {
                    writer.WriteLine("success");
                }
                else
                {
                    writer.WriteLine("error");
                }
            }
            reader.Close();
            writer.Close();
        }
    }
}