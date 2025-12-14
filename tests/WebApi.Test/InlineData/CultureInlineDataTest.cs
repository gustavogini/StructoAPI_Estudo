using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Test.InlineData
{
    public class CultureInlineDataTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "en" }; //yield return retorna um array de objetos com a cultura "en"
            //yield return new object[] { "pt-PT" };
            yield return new object[] { "pt-BR" };
            //yield return new object[] { "fr" };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
