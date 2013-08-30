﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace binary_calculator.Wrappers.UnfixedSize
{
    public class UnfixedDecInteger: UndefinedWrapper
    {

        #region "constants for whole class"

        #endregion

        #region "Properties"

        public override string storedInput
        {
            get
            {
                return base.storedInput;
            }
            protected set
            {
                long temp;
                if (value.Length == 0) value = "0";
                bool validLong = (long.TryParse(value, out temp));
                if (validLong)
                {
                    base.storedInput = value;
                }
            }
        }
        #endregion

        #region "Public Methods"
        public UnfixedDecInteger(string input = "")
        {
            storedInput = input;
        }
        #endregion

        #region "Private Methods"


        #endregion

        
    }
}
