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

	private System.Random random;
	private Func<int[]> getRandomGene;
	private Func<int, float> fitnessFunction;

	public DNA(int size, System.Random random, Func<int[]> getRandomGene, Func<int, float> fitnessFunction, bool shouldInitGenes = true)
	{
		Genes =  new List<int[]>(size);
		this.random = random;
		this.getRandomGene = getRandomGene;
		this.fitnessFunction = fitnessFunction;

		if (shouldInitGenes)
		{
			for (int i = 0; i < Genes.Count; i++)
			{
				Genes[i] = getRandomGene(); //GetElement() 
				Debug.Log(Genes[i]);
			}
		}
	}

	public float CalculateFitness(int index)
	{
		Fitness = fitnessFunction(index);
		return Fitness;
	}

	public DNA Crossover(DNA otherParent)
	{
		DNA child = new DNA(Genes.Count, random, getRandomGene, fitnessFunction, shouldInitGenes: false);

		// for (int i = 0; i < Genes.Count; i++)
		// {
		// 	child.Genes[i] = random.NextDouble() < 0.5 ? Genes[i] : otherParent.Genes[i];
		// }

		for(int i = 0; i < Genes.Count; i++){ 
			
			Debug.Log(Genes[i].Length);

    		for(int j = 0; j < Genes[i].Length; j++){ 
        		 child.Genes[i].SetValue(random.NextDouble() < 0.5 ? Genes[i].GetValue(j):otherParent.Genes[i].GetValue(j),j);
				 Debug.Log(child.Genes[i].GetValue(j));
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