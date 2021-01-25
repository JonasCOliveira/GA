using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Test : MonoBehaviour
{
	[Header("Genetic Algorithm")]
	[SerializeField] float fitnessTarget = 100;
	[SerializeField] int sizeTarget = 10;

	private List<int[]> GenesList = new List<int[]>();
	private string[] objectList = {"spyke", "opossum", "eagle", "lifePoint"};

	//Bonificação, Num Ações e Complexidade
	private int[] spyke = {1,1,2};
	private int[] opossum = {2,1,2};
	private int[] eagle = {2, 1, 2};
	private int[] lifePoint = {1,1,1};
	private int[] element;

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

	private GeneticAlgorithm ga;
	private System.Random random;

	void Start()
	{

		GenesList.Add(spyke);
		GenesList.Add(opossum);
		GenesList.Add(eagle);
		GenesList.Add(lifePoint);


		target.text = (fitnessTarget).ToString();
		numGenerationsText.text = numGenerations.ToString();

		random = new System.Random();
		ga = new GeneticAlgorithm(populationSize, sizeTarget, random, GetElement, FitnessFunction, elitism, mutationRate);

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

	private int[] GetElement()
	{
		int i = random.Next(GenesList.Count);
		element  = GenesList[i];
		Debug.Log(objectList[i]);

		return element;

	}

	private float FitnessFunction(int index)
	{
		float score = 0;
		DNA individuo = ga.Population[index];

		// Debug.Log("numero de genes de um individuo:" + individuo.Genes.Count);

		// Calcula o score de um elemento
		for(int i = 0; i < individuo.Genes.Count; i++){  
			float temp = 1;

    		for(int j = 0; j < individuo.Genes[i].Length; j++){ 

				// Debug.Log("Tamanho da lista de genes: " + GenesList[i].Length);
				// temp *= (int) GenesList[i].GetValue(j); 
				 temp *= (int) individuo.Genes[i].GetValue(j);
			
    		}
			
			// Debug.Log("Score dentro do loop: " + score);
			score += temp;	
		}

		Debug.Log("Score: " + score);
		return score;
	}

}
