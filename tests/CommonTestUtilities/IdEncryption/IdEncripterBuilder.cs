using Sqids;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtilities.IdEncryption
{
    public class IdEncripterBuilder
    {
        public static SqidsEncoder<long> Build()
        {
            return new SqidsEncoder<long>(new()
            {
                MinLength = 3,
                Alphabet = "sKnz3B2Rw0D8jJlo4vMbuZaNhQ5fXA9xFGrPWkCegiSpmIT7q6VYdOt1cUEHyL"
            });
        }
    }
}
