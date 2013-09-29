﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using binary_calculator.Utilities;

namespace binary_calculator.Wrappers.UnfixedSize
{
    public class UnfixedDecInteger: Undefined
    {

        #region "constants for whole class"

        #endregion

        #region "Properties"
        private uint _storedNumber;

        private uint SafeStoredNumber
        {
            set
            {
                this._storedNumber = value;
            }
        }

        public uint StoredNumber
        {
            get { return this._storedNumber; }
            set 
            {
               this._storedNumber = value;
               this.SafeStoredInput = value.ToString();
            }
        }

        private string SafeStoredInput
        {
            set
            {
                base.StoredInput = value;
            }
        }

        public override string StoredInput
        {
            get
            {
                return base.StoredInput;
            }
            set
            {
                uint temp;

                DecUtilities.VerifyInput(value, this.StoredNumber, out temp, base.StoredInput == null);

                base.StoredInput = temp.ToString();
                this.SafeStoredNumber = temp;

                //value = value.TrimStart('0');
                //if (value.Length == 0) value = "0";
                //value = value.Replace("-", "");
                //bool validUint = (uint.TryParse(value, out temp));
                //if (validUint)
                //{
                //    base.StoredInput = value;
                //    this.SafeStoredNumber = temp;
                //}
                //else if (base.StoredInput == null)
                //{
                //    base.StoredInput = "0";
                //    this.SafeStoredNumber = 0;
                //}

            }
        }
        #endregion

        #region "constructor"
        public UnfixedDecInteger(string input = "")
        {
            StoredInput = input;
        }

        public UnfixedDecInteger(uint input = 0)
        {
            StoredNumber = input;
        }
        #endregion

        #region "Public Methods"

        #endregion

        #region "Private Methods"
        protected override void SetStoredInput(string input)
        {
            this.StoredInput = input;
        }
        #endregion

        
    }
}
