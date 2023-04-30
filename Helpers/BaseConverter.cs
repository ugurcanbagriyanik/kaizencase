namespace KaizenCase.Helpers;

public class BaseConverter
{
    
    public static string Encode(int value, char[] baseChars)
    {
        string result = string.Empty;
        var minusSign = false;
        if (value<0)
        {
            minusSign = true;
            value *= -1;
        }
        
        int targetBase = baseChars.Length;

        do
        {
            result = baseChars[value % targetBase] + result;
            value = value / targetBase;
        } 
        while (value > 0);

        if (minusSign)
        {
            result = result + result.MaxBy(l => baseChars.ToList().IndexOf(l));
        }
        else
        {
            result = result + result.MinBy(l => baseChars.ToList().IndexOf(l));
        }
        
        if (result.Length<8)
        {
            var zeroCount = 8 - result.Length;
            for (int i = 0; i < zeroCount; i++)
            {
                result = "A" + result;
            }
        }

        return result;
    }
    
    public static int Decode(string encodedString, char[] baseChars)
    {
        int result = 0;
        int sourceBase = baseChars.Length;
        int nextCharIndex = 0;
        
        var signChar = encodedString[7];
        encodedString = encodedString.Substring(0, 7);
        encodedString = encodedString.TrimStart('A');
        for (int currentChar = encodedString.Length - 1; currentChar >= 0; currentChar--)
        {
            char next = encodedString[currentChar];

            // For loop gets us: baseChar.IndexOf(char) => int
            for (nextCharIndex = 0; nextCharIndex < baseChars.Length; nextCharIndex++)
            {
                if (baseChars[nextCharIndex] == next)
                {
                    break;
                }
            }

            // For character N (from the end of the string), we multiply our value
            // by 64^N. eg. if we have "CE" in hex, F = 16 * 13.
            result += (int)Math.Pow(baseChars.Length, encodedString.Length - 1 - currentChar) * nextCharIndex;
        }

        if (signChar== encodedString.MaxBy(l => baseChars.ToList().IndexOf(l)))
        {
            result *= -1;
            
        }else if (signChar != encodedString.MinBy(l => baseChars.ToList().IndexOf(l)))
        {
            return 0;
        }

        return result;
    }
    
}