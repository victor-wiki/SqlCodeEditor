using SqlCodeEditor.Document;
using SqlCodeEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SqlCodeEditor.Gui.CompletionWindow
{
    public delegate void InsertSelectedHandler(ICompletionData data, int offset, char key);

    public class SqlCompletionDataProvider : ICompletionDataProvider
    {
        private ImageList _imageList;
        public event InsertSelectedHandler InsertSelected;

        public SqlCompletionDataProvider()
        {
            Assembly assembly = typeof(SqlCompletionDataProvider).Assembly;
            Stream syntaxModeStream = assembly.GetManifestResourceStream("SqlCodeEditor.Resources.Keyword.png");

            this._imageList = new ImageList();
            this._imageList.Images.Add(Image.FromStream(this.GetImageStream(assembly, "Keyword")));
            this._imageList.Images.Add(Image.FromStream(this.GetImageStream(assembly, "Function")));
            this._imageList.Images.Add(Image.FromStream(this.GetImageStream(assembly, "Table")));
            this._imageList.Images.Add(Image.FromStream(this.GetImageStream(assembly, "View")));
            this._imageList.Images.Add(Image.FromStream(this.GetImageStream(assembly, "TableColumn")));
            this._imageList.Images.Add(Image.FromStream(this.GetImageStream(assembly, "Schema")));
        }

        private Stream GetImageStream(Assembly assembly, string name)
        {
            return assembly.GetManifestResourceStream($"SqlCodeEditor.Resources.{name}.png");
        }


        public ImageList ImageList
        {
            get
            {
                return this._imageList;
            }
        }

        public string PreSelection => string.Empty;

        public int DefaultIndex => 0;

        public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
        {
            throw new NotImplementedException();
        }

        public ICompletionData[] GenerateCompletionData(SqlWordToken[] tokens, TextArea textArea, char charTyped)
        {
            DefaultCompletionData[] datas = new DefaultCompletionData[tokens.Length];

            for (int i = 0; i < tokens.Length; i++)
            {
                int imageIndex = -1;

                switch (tokens[i].Type)
                {
                    case SqlWordTokenType.Keyword: imageIndex = 0; break;
                    case SqlWordTokenType.BuiltinFunction:
                    case SqlWordTokenType.Function: imageIndex = 1; break;
                    case SqlWordTokenType.Table: imageIndex = 2; break;
                    case SqlWordTokenType.View: imageIndex = 3; break;
                    case SqlWordTokenType.TableColumn: imageIndex = 4; break;
                    case SqlWordTokenType.Schema: imageIndex = 5; break;
                }

                datas[i] = new DefaultCompletionData(tokens[i].Text, imageIndex);
            }

            return datas;
        }

        public bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key)
        {
            if (this.InsertSelected != null)
            {
                this.InsertSelected(data, insertionOffset, key);
            }

            return true;
        }

        public CompletionDataProviderKeyResult ProcessKey(char key)
        {
            throw new NotImplementedException();
        }
    }
}
