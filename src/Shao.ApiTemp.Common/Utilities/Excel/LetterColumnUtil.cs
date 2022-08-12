using System.Text;

namespace Shao.ApiTemp.Common.Utilities.Excel;

public static class LetterColumnUtil
{
    public static int ToIndex(string letters)
    {
        if (letters.Length == 1) return letters[0] - 'A';

        int columnIndex = default;
        int rank = letters.Length - 1;

        for (int i = 0; i < letters.Length; i++)
        {
            var charDiff = letters[i] - 'A';

            var isFirst = i == default;
            var isLast = i == letters.Length - 1;
            // 以下 if else 不要改变顺序，不要优化
            if (isFirst)
            {
                columnIndex += (charDiff + 1) * (int)Math.Pow(26, rank);
            }
            else if (isLast)
            {
                columnIndex += charDiff;
            }
            else if (charDiff > 0)
            {
                columnIndex += (charDiff + 1) * (int)Math.Pow(26, rank);
            }
            rank--;
        }
        return columnIndex;
    }

    public static string ToLetters(int columnIndex)
    {
        if (columnIndex < 26) return ToLetter(columnIndex).ToString();

        int rank = 1;
        while (true)
        {
            var remainder = columnIndex / (int)Math.Pow(26, rank);
            if (remainder < 26) break;
            rank++;
        }

        int leftover = columnIndex;
        var sb = new StringBuilder();
        for (int i = rank; i > 0; i--)
        {
            var rankNbr = (int)Math.Pow(26, i);
            var remainder = leftover / rankNbr;
            sb.Append(ToLetter(remainder == default ? 0 : remainder - 1));
            leftover -= rankNbr * remainder;
        }
        sb.Append(ToLetter(leftover));

        var letters = sb.ToString();
        return letters;
    }

    private static char ToLetter(int columnIndex)
    {
        return (char)('A' + columnIndex);
    }
}