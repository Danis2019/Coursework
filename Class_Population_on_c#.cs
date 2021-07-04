using System;
using System.Collections.Generic;

namespace kursach
{
	class Population
	{
		public List<List<int>> MyPopulation;
		public Population(List<List<int>> population)
		{
			this.MyPopulation = population;
		}
		public void All_Chromosomes(List<int>  chromosome1, List<int> chromosome2, List<int> chromosome3, List<int> chromosome4)
		{
			Console.WriteLine("chromosome1:[" + chromosome1[0] + "," + chromosome1[1] + "," + chromosome1[2] + "," + chromosome1[3] + "]");
			Console.WriteLine("chromosome2:[" + chromosome2[0] + "," + chromosome2[1] + "," + chromosome2[2] + "," + chromosome2[3] + "]");
			Console.WriteLine("chromosome3:[" + chromosome3[0] + "," + chromosome3[1] + "," + chromosome3[2] + "," + chromosome3[3] + "]");
			Console.WriteLine("chromosome4:[" + chromosome4[0] + "," + chromosome4[1] + "," + chromosome4[2] + "," + chromosome4[3] + "]");
		}
		// Вернем массив tuple
		public Tuple<int, int, int, int, List<List<int>>> Min_Max_Chromosome(Population population, List<List<int>> graph) 
		{
			var chromosome = new List<List<int>>();
			for(int i = 0; i < 4; i++) 
			{
				chromosome.Add(new List<int>());
				for (int j = 0; j < 4; j++)
                {
					chromosome[i].Add(graph[j][population.MyPopulation[i][j]]);
				}				
			}
			var list = new List<int>();
			for (int i = 0; i < 4; i++) 
			{
				int sum = 0;
				for(int j = 0; j < 4; j++) 
				{
					sum += chromosome[i][j];
				}
				list.Add(sum);
			}
			

			int max_val = list[0];
			int min_val = list[0];
			int index_max_val = 0;
			int index_min_val = 0;
			for (int i = 1; i < 4; i++) 
			{
				if(list[i] < min_val) 
				{
					min_val = list[i];
					index_min_val = i;
				}
				if (list[i] > max_val)
				{
					max_val = list[i];
					index_max_val = i;
				}
			}

			return Tuple.Create(min_val, max_val, index_min_val, index_max_val, chromosome);
		}

		public Tuple<List<int>, List<List<int>>, Population> Delete_Worst_and_Find_Best(Population population, List<List<int>> chromosome, int index_min_val, int index_max_val) 
		{
			//List<List<int>> old_population = population.MyPopulation;
			List<int> StrongIndividual = population.MyPopulation[index_min_val]; // Вот он - самый сильный смешарик
			chromosome.Remove(population.MyPopulation[index_max_val]);
			if(index_min_val != 3) 
			{
				chromosome.Remove(chromosome[index_min_val]);
				population.MyPopulation.Remove(population.MyPopulation[index_min_val]);

			}
            else
            {
				chromosome.Remove(chromosome[index_min_val - 1]);
				population.MyPopulation.Remove(population.MyPopulation[index_min_val - 1]);
			}
			
			return Tuple.Create(StrongIndividual, chromosome, population);
		}
		public List<List<int>> Create_new_population(List<List<int>>  old_population, List<int> StrongIndividual, Population population, List<List<int>> graph) 
		{
			//7 хромосом, 3 наследуются от сильнейшей, ещё 4 от двух остальных
			List<int> new_chromosome = StrongIndividual;
			List<int> new_chromosome2 = new List<int>();
			new_chromosome2.Add(StrongIndividual[0]);
			new_chromosome2.Add(StrongIndividual[1]);
			new_chromosome2.Add(StrongIndividual[3]);
			new_chromosome2.Add(StrongIndividual[2]);
			List<int> new_chromosome3 = new List<int>();
			new_chromosome3.Add(StrongIndividual[1]);
			new_chromosome3.Add(StrongIndividual[0]);
			new_chromosome3.Add(StrongIndividual[2]);
			new_chromosome3.Add(StrongIndividual[3]);
			List<int> new_chromosome4 = new List<int>();
			new_chromosome4.Add(population.MyPopulation[0][0]);
			new_chromosome4.Add(population.MyPopulation[0][1]);
			new_chromosome4.Add(population.MyPopulation[0][3]);
			new_chromosome4.Add(population.MyPopulation[0][2]);
			List<int> new_chromosome5 = new List<int>();
			new_chromosome5.Add(population.MyPopulation[0][1]);
			new_chromosome5.Add(population.MyPopulation[0][0]);
			new_chromosome5.Add(population.MyPopulation[0][2]);
			new_chromosome5.Add(population.MyPopulation[0][3]);
			List<int> new_chromosome6 = new List<int>();
			new_chromosome6.Add(population.MyPopulation[1][0]);
			new_chromosome6.Add(population.MyPopulation[1][1]);
			new_chromosome6.Add(population.MyPopulation[1][3]);
			new_chromosome6.Add(population.MyPopulation[1][2]);
			List<int> new_chromosome7 = new List<int>();
			new_chromosome7.Add(population.MyPopulation[1][1]);
			new_chromosome7.Add(population.MyPopulation[1][0]);
			new_chromosome7.Add(population.MyPopulation[1][2]);
			new_chromosome7.Add(population.MyPopulation[1][3]);




			List<List<int>> new_population = new List<List<int>>();
			new_population.Add(new_chromosome);
			new_population.Add(new_chromosome2);
			new_population.Add(new_chromosome3);
			new_population.Add(new_chromosome4);
			new_population.Add(new_chromosome5);
			new_population.Add(new_chromosome6);
			new_population.Add(new_chromosome7);
			var chromosome = new List<List<int>>();
			for (int i = 0; i < 7; i++)
			{
				chromosome.Add(new List<int>());
				for (int j = 0; j < 4; j++)
				{
					chromosome[i].Add(graph[j][new_population[i][j]]);
				}
			}
			static void Remove_Worst(List<List<int>> chromosome, List<List<int>> new_population) 
			{
				var list = new List<int>();
				for (int i = 0; i < 4; i++)
				{
					int sum = 0;
					for (int j = 0; j < 4; j++)
					{
						sum += chromosome[i][j];
					}
					list.Add(sum);
				}
				int max_val = list[0];
				int index_max_val = 0;
				for (int i = 1; i < 4; i++)
				{
					if (list[i] > max_val)
					{
						index_max_val = i;
					}
				}
				chromosome.Remove(chromosome[index_max_val]);
				new_population.Remove(new_population[index_max_val]);
			}
			for(int i = 0; i < 3; i++) 
			{
				Remove_Worst(chromosome, new_population);
			}
			return new_population;
		}

		public Population Mutation(Population population, List<List<int>> graph)
		{
			//Console.WriteLine("Мутация");
			Tuple<int, int, int, int, List<List<int>>> Min_and_Max = population.Min_Max_Chromosome(population, graph); //Min_and_Max.Item4 -индекс самой слабой особи
			List<List<int>> new_population = new List<List<int>>();
			new_population = population.MyPopulation;
			int index_max_val = Min_and_Max.Item4;
			List<int> WeakIndividual = population.MyPopulation[index_max_val];
			//Console.WriteLine("Мутируем элемент с индексом:" + index_max_val);

			Random rnd = new Random();
			// Меняем два гена местами
			int i = rnd.Next(0, 3);
			int temp = population.MyPopulation[index_max_val][i];
			population.MyPopulation[index_max_val][i] = population.MyPopulation[index_max_val][population.MyPopulation.Count - 1 - i];
			population.MyPopulation[index_max_val][population.MyPopulation.Count - 1 - i] = temp;

			//population.All_Chromosomes(population.MyPopulation[0], population.MyPopulation[1], population.MyPopulation[2], population.MyPopulation[3]);
			return population;
		}


	}
}


