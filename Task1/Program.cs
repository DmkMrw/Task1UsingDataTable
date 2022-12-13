using System.Data;
using System.Text.RegularExpressions;

namespace task1
{
	class Program
	{
		static void Main(string[] args)
		{
			Count("22 + 33,22 * 33 - 12 - 0001b + 0xFF");
		}
		public static void Count(string arg)
		{
			string[] argSplit = arg.Split(" ");

			Regex intRegex = new Regex(@"^[0-9]+$");
			Regex hexRegex = new Regex(@"[0-9]+[a-z]+[A-Z]+");
			Regex decimalRegex = new Regex(@"[0-9]+\W[0-9]+");
			Regex binRegex = new Regex(@"[0-1]+[b]");
			Regex signsRegex = new Regex(@"^\W$");
			string result = "";

			foreach (var item in argSplit)
			{
				if (intRegex.IsMatch(item))
				{
					result += item;
				}
				else if (hexRegex.IsMatch(item))
				{
					result += Convert.ToInt32(item, 16);
				}
				else if (decimalRegex.IsMatch(item))
				{
					result += item.Replace(',', '.');
				}
				else if (binRegex.IsMatch(item))
				{
					result += Convert.ToInt32(item.Remove(item.Length - 1), 2);
				}
				else if (signsRegex.IsMatch(item) && item == "*")
				{
					result += '*';
				}
				else if (signsRegex.IsMatch(item) && item == "+")
				{
					result += '+';
				}
				else if (signsRegex.IsMatch(item) && item == "-")
				{
					result += '-';
				}
			}

			DataTable dataTable = new();
			var resultUsingDataTable = dataTable.Compute(result, "");

			Console.WriteLine("Result: " + resultUsingDataTable);
		}
	}
}
