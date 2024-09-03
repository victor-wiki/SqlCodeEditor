using System.Collections.Generic;
using SqlCodeEditor.Document;

namespace SqlCodeEditor.Document.FoldingStrategy
{
    public interface IFoldingStrategyEx : IFoldingStrategy
    {
        List<string> GetFoldingErrors();
    }
}