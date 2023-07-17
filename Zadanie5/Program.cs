using System.Text.RegularExpressions;
using System.Collections.Generic;

Console.WriteLine("Введите строку");
string str = Console.ReadLine();

string letters = "";

if (!Regex.IsMatch(str, "^[a-z]+$"))
{
    for (int i = 0; i < str.Length; i++)
    {
        if (!Regex.IsMatch(str[i].ToString(), "^[a-z]+$"))
        {
            letters += str[i];
        }
    }
    Console.Write("В строке есть неподходящие символы: ");
    Console.Write(letters);
    return;
}

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
    Console.WriteLine(newStr);
    workWithStr.countLetters(newStr);
    workWithStr.findSubString(newStr);
    workWithStr.chooseSort(newStr);
}
else
{
    string revStr = workWithStr.ReveseStr(str);
    string newStr = revStr + str;
    Console.WriteLine(newStr);
    workWithStr.countLetters(newStr);
    workWithStr.findSubString(newStr);
    workWithStr.chooseSort(newStr);
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
    public static void chooseSort(string qwe)
    {
        string str = "";
        while(str != "qs" || str!= "ts")
        {
            Console.WriteLine("Выберите сортировку: qs - QuickSort, ts - TreeSort");
            str = Console.ReadLine();
            if (str == "qs" || str == "ts") break;
        }
        if(str == "qs")
        {
            string[] sortedStr = quickSort.startQuickSort(changeStr(qwe), 0, qwe.Length - 1);
            workWithStr.outputStr(sortedStr);
        }
        else
        {
            string[] sortedStr = treeSort.startTreeSort(changeStr(qwe));
            workWithStr.outputStr(sortedStr);
        }
    }
    public static void outputStr(string[] str)
    {
        Console.Write("Отсортированная строка: ");
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
    public static void countLetters(string qwe)
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
        foreach (var pair in map)
        {
            Console.WriteLine($"{pair.Key} = {pair.Value}");
        }
    }
    public static void findSubString(string qwe)
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
            Console.WriteLine("Подстроки начинающийся и заканчивающийся с 'aeiouy' нет");
            return;
        }
        string subStr = qwe.Substring(startIndex, endIndex - startIndex + 1);
        Console.Write("Подстрока начинающаяся и заканчивающаяся с 'aeiouy': ");
        Console.Write(subStr);
        Console.WriteLine("");
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
            if(Right == null)
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
        if(elements == null)
        {
            elements = new List<string>();
        }
        if(Left != null)
        {
            Left.Transform(elements);
        }
        elements.Add(Data);
        if(Right != null)
        {
            Right.Transform(elements);
        }
        return elements.ToArray();
    }
    public static string[] startTreeSort(string[] str)
    {
        treeSort treeLine = new treeSort(str[0].ToString());
        for( int i = 1; i < str.Length; i++)
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