using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeInput
{
    private int prime;
    private bool isCorrect;
    private bool isChecked;

    public PrimeInput(int prime) {
        this.prime = prime;
    }

    public int GetPrime()
    {
        return prime;
    }

    public void Check(CompositeNumber number) {
        isCorrect = IsFactorOf(number);
        isChecked = true;
    }

    public bool IsCorrect() {
        if (!isChecked) throw new UncheckedAnswerException();
        return isCorrect;
    }

    public bool IsFactorOf(CompositeNumber number) {
        return number.GetValue() % prime == 0;
    }

    public int GetScore() {
        return IsCorrect() ? prime : 0;
    }
}

class UncheckedAnswerException : Exception { }
