using System.Text.RegularExpressions;
using System.Net;
using System.Text.Json;

namespace Zadanie7
{
    public class workStr
    {
        public string processedStr { get; set; }
        public Dictionary<char,int> countLetters { get; set; }
        public string subString { get; set; }
        public string sortedString { get; set; }
        public string cutSortedString { get; set; }
        public  static string checkStr(string str)
        {
            string letters = "";

            FromJson js = ReadJson.Read();
            foreach(string q in js.Settings["BlackList"])
            {
                if (str == q) letters = "BlackList";
            }

            if (!Regex.IsMatch(str, "^[a-z]+$"))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (!Regex.IsMatch(str[i].ToString(), "^[a-z]+$"))
                    {
                        letters += str[i];
                    }
                }
                return letters;
            }
            return letters;
        }
        public static string firstTask(string str)
        {
            if (str.Length % 2 == 0)
            {
                string str1 = "", str2 = "";
                for (int i = 0; i < str.Length; i++)
                {
                    if (i < str.Length / 2)
                    {
                        str1 += str[i];
                    }
                    else
                    {
                        str2 += str[i];
                    }
                }
                string newStr = workWithStr.ReveseStr(str1) + workWithStr.ReveseStr(str2);
                return newStr;
            }
            else
            {
                string revStr = workWithStr.ReveseStr(str);
                string newStr = revStr + str;
                return newStr;
            }
        }
        public static Dictionary<char,int> secondTask(string str)
        {
            return workWithStr.countLetters(str);
        }
        public static string thirdTask(string str)
        {
            return workWithStr.findSubString(str);
        }
        public static string fourthTask(string str, string sort)
        {
            return string.Join("", workWithStr.chooseSort(str, sort));
        }
        public static string fifthTask(int len, string str)
        {

            return webApi.sendRequest(len,str);
        }
    }
}

public static class ReadJson
{
    public static FromJson Read()
    {
        string text = File.ReadAllText("appsettings.json");
        FromJson js = JsonSerializer.Deserialize<FromJson>(text);
        return js;
    }
}

public class FromJson
{
    public string RandomApi { get; set; }
    public Dictionary<string,string[]> Settings { get; set; }    
}

public static class workWithStr
{
    public static string[] changeStr(string str)
    {
        string[] newStr = new string[str.Length];
        for (int i = 0; i < str.Length; i++)
        {
            newStr[i] = str[i].ToString();
        }
        return newStr;
    }
    public static string[] removeByIndex(string str, int index)
    {
        string[] newStr = new string[str.Length - 1];

        for (int i = 0; i < newStr.Length; i++)
        {
            if (i >= index)
            {
                newStr[i] = str[i + 1].ToString();
            }
            else
            {
                newStr[i] = str[i].ToString();
            }
        }

        return newStr;
    }
    public static string chooseSort(string qwe, string sort)
    {
        if (sort == "qs")
        {
            return string.Join("", quickSort.startQuickSort(changeStr(qwe), 0, qwe.Length - 1));
        }
        else if(sort == "ts")
        {
            return string.Join("", treeSort.startTreeSort(changeStr(qwe)));
        }
        else
        {
            return "";
        }
        
    }
    public static void outputStr(string[] str)
    {
        foreach (string a in str)
        {
            Console.Write(a);
        }
    }
    public static string ReveseStr(string qwe)
    {
        char[] arrStr = qwe.ToCharArray();
        Array.Reverse(arrStr);
        return new string(arrStr);
    }
    public static Dictionary<char,int> countLetters(string qwe)
    {
        Dictionary<char, int> map = new();
        foreach (char q in qwe)
        {
            if (map.ContainsKey(q))
            {
                map[q]++;
            }
            else
            {
                map[q] = 1;
            }
        }
        return map;
    }
    public static string findSubString(string qwe)
    {
        int startIndex = -1, endIndex = -1;
        for (int i = 0; i < qwe.Length; i++)
        {
            if (Regex.IsMatch(qwe[i].ToString(), "[aeiouy]+$"))
            {
                if (startIndex < 0) startIndex = i;
                else endIndex = i;
            }
        }

        if (endIndex < 0)
        {
            return "Подстроки начинающийся и заканчивающийся с 'aeiouy' нет";
        }
        string subStr = qwe.Substring(startIndex, endIndex - startIndex + 1);
        return subStr;
    }
}
public class webApi
{
    public static string sendRequest(int strLen, string str)
    {
        FromJson js = ReadJson.Read();
        int number;
        string[] newStr = new string[str.Length];
        Random rand = new Random();
        WebRequest request = WebRequest.Create(js.RandomApi + strLen + "&count=1");
        WebResponse response = null;
        try
        {
            response = request.GetResponse();
            string line;
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                if ((line = sr.ReadLine()) != null)
                {
                    number = Convert.ToInt32(line[1].ToString());
                    newStr = workWithStr.removeByIndex(str, number);
                }
                return string.Join("", newStr);
            }
        }
        catch (WebException e)
        {
            response = e.Response;
            if (response == null)
            {
                number = rand.Next(0, strLen);
                newStr = workWithStr.removeByIndex(str, number);
            }
            return string.Join("", newStr);
        }
    }
}

public class treeSort
{
    public treeSort(string data)
    {
        Data = data;
    }
    public string Data { get; set; }
    public treeSort Left { get; set; }
    public treeSort Right { get; set; }
    public void Insert(treeSort line)
    {
        if (char.Parse(line.Data) < char.Parse(Data))
        {
            if (Left == null)
            {
                Left = line;
            }
            else
            {
                Left.Insert(line);
            }
        }
        else
        {
            if (Right == null)
            {
                Right = line;
            }
            else
            {
                Right.Insert(line);
            }
        }
    }
    public string[] Transform(List<string> elements = null)
    {
        if (elements == null)
        {
            elements = new List<string>();
        }
        if (Left != null)
        {
            Left.Transform(elements);
        }
        elements.Add(Data);
        if (Right != null)
        {
            Right.Transform(elements);
        }
        return elements.ToArray();
    }
    public static string[] startTreeSort(string[] str)
    {
        treeSort treeLine = new treeSort(str[0].ToString());
        for (int i = 1; i < str.Length; i++)
        {
            treeLine.Insert(new treeSort(str[i].ToString()));
        }
        return treeLine.Transform();
    }
}
public static class quickSort
{
    static void swap(string[] qwe, int a, int b)
    {
        string c = qwe[a];
        qwe[a] = qwe[b];
        qwe[b] = c;
    }
    static int getSupElement(string[] str, int minIndex, int maxIndex)
    {
        int supElement = minIndex - 1;
        for (int i = minIndex; i <= maxIndex; i++)
        {
            if (char.Parse(str[i]) < char.Parse(str[maxIndex]))
            {
                supElement++;
                swap(str, supElement, i);
            }
        }
        supElement++;
        swap(str, supElement, maxIndex);
        return supElement;
    }
    public static string[] startQuickSort(string[] str, int minIndex, int maxIndex)
    {
        if (minIndex >= maxIndex)
        {
            return str;
        }

        int supElementIndex = getSupElement(str, minIndex, maxIndex);
        startQuickSort(str, minIndex, supElementIndex - 1);
        startQuickSort(str, supElementIndex + 1, maxIndex);
        return str;
    }
}