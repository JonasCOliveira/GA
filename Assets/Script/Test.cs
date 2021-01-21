using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
	[Header("Genetic Algorithm")]
	[SerializeField] float fitnessTarget = 10f;
	[SerializeField] int sizeTarget = 1;

	[SerializeField] int[] validRhythm = {-3, -2, -1, 0, 1, 2, 3};

	private float  rhythm;
	[SerializeField] int populationSize = 200;
	[SerializeField] float mutationRate = 0.01f;
	[SerializeField] int elitism = 5;

	[Header("Other")]

	[SerializeField] Text target;
	[SerializeField] Text bestText;

	[SerializeField] Text bestFitnessText;
	private float bestFitness = 0;
	[SerializeField] Text numGenerationsText;

	private int numGenerations = 0; 
	[SerializeField] Text textPrefab;

	private GeneticAlgorithm<float> ga;
	private System.Random random;

	void Start()
	{
		target.text = (fitnessTarget).ToString();
		numGenerationsText.text = numGenerations.ToString();

		random = new System.Random();
		ga = new GeneticAlgorithm<float>(populationSize, sizeTarget, random, GetRhythm, FitnessFunction, elitism, mutationRate);

	}

	void Update()
	{
		ga.NewGeneration();
		numGenerations += 1;

		numGenerationsText.text = numGenerations.ToString();
		bestFitnessText.text = ga.BestFitness.ToString();

		if (ga.BestFitness == fitnessTarget)
		{
			this.enabled = false;
		}
	}

	private float GetRhythm()
	{
		int i = random.Next(validRhythm.Length);
		Debug.Log(validRhythm[i]);
		return validRhythm[i];
	}

	private float FitnessFunction(int index)
	{
		float score = 0;
		DNA<float> dna = ga.Population[index];

		rhythm = GetRhythm();
		score += rhythm;

		return score;
	}

}
