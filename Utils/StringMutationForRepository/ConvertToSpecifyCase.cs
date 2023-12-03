namespace Exam.Utils.StringMutationForRepository;

public static class ConvertToSpecifyCase
{
    public static string ConvertToSnakeCase(string input)
    {
        var output = string.Empty;

        for (var i = 0; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]))
            {
                switch (i)
                {
                    case > 0 when input[i - 1] == 's':
                        output = output[..^1] + "_";
                        break;
                    case > 0:
                        output += "_";
                        break;
                }

                output += char.ToLower(input[i]);
            }
            else
            {
                output += input[i];
            }
        }

        return output;
    }
}