namespace CoreLab.App
{
    internal class Sort
    {
        public static string apply (int[] numbers)
        {
            var result = "START: " + string.Join(",", numbers) + "\n";

            for (var i = 0; i < numbers.Length; i++)
            {
                for (var j = 0; j < numbers.Length; j++)
                {
                    result += (i * j) + ": " + string.Join(",", numbers) + "\n";
                    if (numbers[i] < numbers[j])
                    {
                        var temp = numbers[i];
                        numbers[i] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }

            result += "END: " + string.Join(",", numbers) + "\n";

            return result;
        }
    }
}