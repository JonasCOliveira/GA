using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DNA
{
	public List<int[]> Genes { get; private set; }
	public float Fitness { get; private set; }
	public int size;

	private System.Random random;
	private Func<int[]> getRandomGene;
	private Func<int, float> fitnessFunction;


	public DNA(int size, System.Random random, Func<int[]> getRandomGene, Func<int, float> fitnessFunction, bool shouldInitGenes = true)
	{

		Genes =  new List<int[]>(size);
		
		if (shouldInitGenes)
		{
			for (int i = 0; i < size; i++) 
			{
				Genes.Add(getRandomGene()); //GetElement()
 			}
		}

		this.random = random;
		this.getRandomGene = getRandomGene;
		this.fitnessFunction = fitnessFunction;
	}

	public float CalculateFitness(int index)
	{
		Fitness = fitnessFunction(index);
		return Fitness;
	}

	public DNA Crossover(DNA otherParent)
	{
		DNA child = new DNA(Genes.Count, random, getRandomGene, fitnessFunction, shouldInitGenes: false);

		for(int i = 0; i < Genes.Count; i++){ 

    		for(int j = 0; j < Genes[i].Length; j++){ 
        		 child.Genes[i].SetValue(random.NextDouble() < 0.5 ? Genes[i].GetValue(j):otherParent.Genes[i].GetValue(j),j);
    		}
		}




		return child;
	}

	public void Mutate(float mutationRate)
	{
		for (int i = 0; i < Genes.Count; i++)
		{
			if (random.NextDouble() < mutationRate)
			{
				Genes[i] = getRandomGene(); //GetElement() 
			}
		}
	}


}