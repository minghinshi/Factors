using System.Collections.Generic;

public class FactoringAttempt
{
    private bool isFirstAttempt;
    private int[] primesInput;
    private bool[] arePrimesCorrect;
    private int newCompositeNumber;

    public FactoringAttempt(bool isFirstAttempt, int[] primesInput, bool[] arePrimesCorrect, int newCompositeNumber)
    {
        if (primesInput.Length != arePrimesCorrect.Length)
            throw new System.Exception("primesInput and arePrimesCorrect have different array sizes");
        this.isFirstAttempt = isFirstAttempt;
        this.primesInput = primesInput;
        this.arePrimesCorrect = arePrimesCorrect;
        this.newCompositeNumber = newCompositeNumber;
    }

    public int GetCountOfCorrectPrimes() {
        int count = 0;
        foreach (bool isPrimeCorrect in arePrimesCorrect)
            if (isPrimeCorrect) count++;
        return count;
    }

    public int GetCountOfIncorrectPrimes() {
        return arePrimesCorrect.Length - GetCountOfCorrectPrimes();
    }

    public int[] GetCorrectPrimes() {
        List<int> correctPrimes = new List<int>();
        for (int i = 0; i < primesInput.Length; i++)
            if (arePrimesCorrect[i]) correctPrimes.Add(primesInput[i]);
        return correctPrimes.ToArray();
    }

    public bool IsPerfect() {
        return isFirstAttempt && GetCountOfIncorrectPrimes() == 0 && newCompositeNumber == 1;
    }
}
