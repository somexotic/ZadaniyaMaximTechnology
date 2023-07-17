using System.Text.RegularExpressions;

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
}
else
{
    string revStr = workWithStr.ReveseStr(str);
    string newStr = revStr + str;
    Console.WriteLine(newStr);
    workWithStr.countLetters(newStr);
}
public static class workWithStr
{
    public static string ReveseStr(string qwe)
    {
        char[] arrStr = qwe.ToCharArray();
        Array.Reverse(arrStr);
        return new string(arrStr);
    }
    public static void countLetters(string qwe)
    {
        Dictionary<char, int> map = new();
        foreach(char q in qwe)
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
        foreach(var pair in map)
        {
            Console.WriteLine($"{pair.Key} = {pair.Value}");
        }
    }
}