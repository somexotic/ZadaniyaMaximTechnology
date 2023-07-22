using Zadanie7;

namespace ZadaniyaTests
{
    public class Tests
    {
        workStr work = new();

        [TestCase("a", ExpectedResult = "aa")]
        [TestCase("abcdef", ExpectedResult = "cbafed")]
        [TestCase("abcde", ExpectedResult = "edcbaabcde")]
        public string Test1(string value)
        {
            return work.firstTask(value);
        }

        [TestCase("a", ExpectedResult = "")]
        [TestCase("@", ExpectedResult = "@")]
        public string Test2(string value)
        {
            return work.checkStr(value);
        }

        

        [Test]
        public void Test3()
        {
            Dictionary<char, int> DicTest1 = new Dictionary<char, int>()
            {
                {'a',2},
            };
            string strTest1 = work.firstTask("a");
            bool passTest1 = false;
            var DTest1 = work.secondTask(strTest1);
            foreach(var c in DTest1)
            {
                if (DicTest1.ContainsKey(c.Key) && DicTest1.ContainsValue(c.Value)) passTest1 = true;
                else passTest1 = false;
            }

            if (passTest1) Assert.Pass();
            else Assert.Fail();
        }

        [TestCase("ada",ExpectedResult = "adaada")]
        [TestCase("ad", ExpectedResult = "ѕодстроки начинающийс€ и заканчивающийс€ с 'aeiouy' нет")]
        public string Test4(string value)
        {
            string str = work.firstTask(value);
            return work.thirdTask(str);
        }

        [TestCase("bas",ExpectedResult = "aabbss")]
        [TestCase("sadfa", ExpectedResult = "aaaaddffss")]
        [TestCase("gfds", ExpectedResult = "dfgs")]
        public string Test5(string value)
        {
            return work.fourthTask(work.firstTask(value), "ts");
        }
    }
}