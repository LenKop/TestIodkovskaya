using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace unit1
{

	class MainClass

	{
		public static void Main (string[] args)

		{
			string path = "";
			Console.WriteLine("Please, enter path to your file for reading:");
			path = Console.ReadLine();
			Console.WriteLine();
			string str = string.Empty;
			StreamReader read = File.OpenText(path);
			str = read.ReadToEnd();
			Console.WriteLine ("Received data from file:");
			Console.WriteLine();
			Console.WriteLine(str);
			str = str.ToLower();

			char res;
			foreach(char ch in str)
			{
				if (((int)ch >= 97 && (int)ch <= 122) || (int)ch == 32 || (int)ch == 45)
				{
					res = ch;
				} 
				else 
				{
					str = str.Replace(ch, ' ');
				}

			}
			var ResultStr = new Regex (@"\s+").Replace(str," ");

			string[] words = ResultStr.Split (new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
			var result = words.GroupBy (x => x)
				.Select (x => new {Word = x.Key, quantity = x.Count()});
			var sorteResult = from u in result
				orderby u.Word.Substring(0, 1), u.quantity descending 
			select u;

			Console.WriteLine();
			Console.WriteLine("Please, enter path to your file for writing:");
			path = Console.ReadLine();
			Console.WriteLine();
			StreamWriter sw = new StreamWriter(path);

			Console.WriteLine("Result:");
			Console.WriteLine();
			string sort = "";
			foreach (var item in sorteResult) 
			{
				string sortNew = item.Word.Substring(0,1);
				int rez = String.Compare(sort,sortNew);
				if (rez != 0) 
				{
					if (sort != "") 
					{
						sw.WriteLine();
					}
					sw.WriteLine(sortNew);
					Console.WriteLine(sortNew);
				}

				Console.Write(item.Word);
				Console.Write ("\t");
				Console.WriteLine(item.quantity);
				sw.Write(item.Word);
				sw.Write ("\t");
				sw.WriteLine(item.quantity);
				sort = sortNew;
			}
			sw.Close();
			Console.WriteLine();
			Console.WriteLine("Data successfully uploaded to "+path);
			Console.ReadLine ();
		}

	}
}
