using System;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm
{
	public List<DNA> Population { get; private set; }
	public int Generation { get; private set; }
	public float BestFitness { get; private set; }
	public List<int[]> BestGenes { get; private set; }

	public int Elitism;
	public float MutationRate;
	
	private List<DNA> newPopulation;
	private System.Random random;
	private float fitnessSum;
	private int dnaSize;
	private Func<int[]> getRandomGene;
	private Func<int, float> fitnessFunction;

	public GeneticAlgorithm(int populationSize, int dnaSize, System.Random random, Func<int[]> getRandomGene, Func<int, float> fitnessFunction,
		int elitism, float mutationRate = 0.01f)
	{
		Generation = 1;
		Elitism = elitism;
		MutationRate = mutationRate;
		Population = new List<DNA>(populationSize);
		newPopulation = new List<DNA>(populationSize);
		this.random = random;
		this.dnaSize = dnaSize;
		this.getRandomGene = getRandomGene;
		this.fitnessFunction = fitnessFunction;

		BestGenes = new List<int[]>(dnaSize);

		for (int i = 0; i < populationSize; i++)
		{
			Population.Add(new DNA(dnaSize, random,  getRandomGene, fitnessFunction, shouldInitGenes: true));
		}
	}

	public void NewGeneration(int numNewDNA = 0, bool crossoverNewDNA = false)
	{
		int finalCount = Population.Count + numNewDNA;

		if (finalCount <= 0) {
			return;
		}

		if (Population.Count > 0) {
			CalculateFitness();
			Population.Sort(CompareDNA);
		}
		newPopulation.Clear();

		for (int i = 0; i < finalCount; i++)
		{
			if (i < Elitism && i < Population.Count)
			{
				newPopulation.Add(Population[i]);
			}
			else if (i < Population.Count || crossoverNewDNA)
			{
				DNA parent1 = ChooseParent();
				DNA parent2 = ChooseParent();
				DNA child = parent1.Crossover(parent2);

				child.Mutate(MutationRate);

				newPopulation.Add(child);
			}
			else
			{
				newPopulation.Add(new DNA(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
			}
		}

		List<DNA> tmpList = Population;
		Population = newPopulation;
		newPopulation = tmpList;

		Generation++;
	}
	
	private int CompareDNA(DNA a, DNA b)
	{
		if (a.Fitness > b.Fitness) {
			return -1;
		} else if (a.Fitness < b.Fitness) {
			return 1;
		} else {
			return 0;
		}
	}

	private void CalculateFitness()
	{
		fitnessSum = 0;
		DNA best = Population[0];

		// Debug.Log("População:" + Population.Count);

		for (int i = 0; i < Population.Count; i++)
		{
			fitnessSum += Population[i].CalculateFitness(i);
			// Debug.Log("Somatório Fitness:" + fitnessSum);

			if (Population[i].Fitness > best.Fitness)
			{
				best = Population[i];
			}
		}

		BestFitness = best.Fitness;
		// Debug.Log("Best Fitness: " + BestFitness);
		// Debug.Log("Best Gene: " + best.Genes.Count);
	

		// best.Genes.CopyTo(BestGenes, 0);
		for(int i = 0; i < BestGenes.Count; i++){ 
			
    		for(int j = 0; j < BestGenes[i].Length; j++){ 
        		 best.Genes[i].SetValue(BestGenes[i].GetValue(j),j);  

    		}
		}

	}

	private DNA ChooseParent()
	{
		double randomNumber = random.NextDouble() * fitnessSum; 
		
		for (int i = 0; i < Population.Count; i++)
		{
			if (randomNumber < Population[i].Fitness) 
			{
				return Population[i];
			}

			randomNumber -= Population[i].Fitness;
		}

		return null;
	}
}
