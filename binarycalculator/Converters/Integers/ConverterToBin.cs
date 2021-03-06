﻿using System;
using System.Collections.Generic;
using binary_calculator.Wrappers.Integers;
using binary_calculator.EnumsAndConstants;
using binary_calculator.Wrappers.UnfixedSize;

namespace binary_calculator.Converters.Integers
{
    public class ConverterToBin: GenericConverter
    {
        #region "constants for whole class"

        #endregion

        #region "Properties"
        



        #endregion

        #region "Public Methods"
        public ConverterToBin()
        {
        }

        public BinInt Convert(DecInt input)
        {
            long toBeConverted = input.StoredNumber;
            int bitNumber = input.allowedNumberOfBits;
            string result = FromDecIntPos(toBeConverted, bitNumber);
            BinInt output = new BinInt(result, bitNumber);

            return output;
        }
        
        //TO DO: Add back once PowOfTwoInteger is adjusted
        //public BinInt Convert(PowOfTwoInteger input)
        //{
        //    int bitNumber = input.allowedNumberOfBits;
        //    NumberBases baseChoice = input.GetNumberBase();
        //    string storedInput = input.storedInput;
        //    string result = FromPowerOfTwoIntPos(storedInput, baseChoice);

        //    BinInteger output = new BinInteger(bitNumber, result);

        //    return output;

        //}

        public UnfixedBinInteger Convert(UnfixedDecInteger input)
        {
            return UDecToUBin(input);
        }

        public UnfixedBinInteger Convert(UnfixedPowOfTwoInteger input)
        {
            //string toBeConverted = input.StoredInput;
            //NumberBases numberBase = input.GetNumberBase();

            //string result = FromPowerOfTwoIntPos(toBeConverted, numberBase);

            //UnfixedBinInteger bin = new UnfixedBinInteger(result);
            //return bin;

            ConverterToDec converter = new ConverterToDec();
            UnfixedDecInteger udec = converter.Convert(input);
            UnfixedBinInteger ubin = UDecToUBin(udec);

            return ubin;

        }

        

        #endregion

        #region "Private Methods"
        private UnfixedBinInteger UDecToUBin(UnfixedDecInteger udec)
        {
            ConverterToAny converterAny = new ConverterToAny();
            UnfixedAnyInteger any = converterAny.Convert(udec, NumberBases.BASE_TWO);
            UnfixedBinInteger ubin = new UnfixedBinInteger(any.StoredInput);
            return ubin;
        }

        private string FromDecIntPos(long input, int bitNumber = 8)
        {
            string result = "";
            Stack<string> resultAsStack = new Stack<string>();
            for (int i = 0; i < bitNumber; i++)
            {
                string temp = (input % 2).ToString();
                resultAsStack.Push(temp);
                input = (long)(input / 2);
            }
            result = String.Concat(resultAsStack.ToArray());

            return result;
        }

          

        //this method might only be for floating point numbers. Needs investigation
        private string FromPowerOfTwoIntPos(string input,NumberBases choice)
        {
            string result = "";
            int numBase = (int)choice;
            int exponent = 0;
            int dictionaryValueSize = 4;

            //get the power of 2. Ex. 8=2^3, 16=2^4
            exponent = (int)Math.Log(numBase, 2);

            /*Note on substringStartingPoint
             * 4 is the number of bin digits for each hex char
            if exponent = 4(for hex) then substringStartingPoint = 0
            if exponent= 3(for oct) then substringStartingPoint = 1*/
            int substringStartingPoint = dictionaryValueSize - exponent;

            //load dictionary of binary representations of each oct and hex character
            dictionaries.DictionaryForBinary binDictionary = new dictionaries.DictionaryForBinary();

            //one string of 1's and 0's for each hex or oct character
            Queue<string> binQueue = new Queue<string>();
            string temp;

            //convert each character to binary
            foreach (char digit in input)
            {
                temp = binDictionary.GetEquivalent(digit);
                temp = temp.Substring(substringStartingPoint); //see note on substringStartingPoint
                binQueue.Enqueue(temp);
            }

            result = string.Concat(binQueue.ToArray());
            result = outputAdjuster(result); 

            return result;
        }

        //this method is to correct errors that got introduced after converting from octal       
        private static string outputAdjuster(string strToFix)
        {
            //4 is the number of bin digits for each hex char
            int correctStrStartingPoint = strToFix.Length % 4;

            return strToFix.Substring(correctStrStartingPoint);
        }
        #endregion
    }
}
