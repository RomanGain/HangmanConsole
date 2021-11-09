using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hang.Models
{
    public class Word
    {
        public int Length { get; set; }


        private string value;
        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                SetWordLength();
            }
        }

        public Word(string value)
        {
            this.Value = value.ToLower();
        }

        private void SetWordLength()
        {
            int length = Value.Length;
            this.Length = length;
        }
    }
}