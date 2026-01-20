// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using SqlCodeEditor.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace SqlCodeEditor.Document
{
	public class ResourceSyntaxModeProvider : ISyntaxModeFileProvider
	{
		List<SyntaxMode> syntaxModes = null;
		
		public ICollection<SyntaxMode> SyntaxModes {
			get {
				return syntaxModes;
			}
		}
       

        public ResourceSyntaxModeProvider()
		{
			string configFolder = PathHelper.GetSyntaxHighlightingConfigFolder();

			string fileName = "SyntaxModes.xml";

			string filePath = Path.Combine(configFolder, fileName);

			if(!File.Exists(filePath))
			{
				throw new FileNotFoundException($@"""{fileName}"" is not found in folder ""{configFolder}"".");
			}

			using (Stream syntaxModeStream = File.OpenRead(filePath))
			{
                if (syntaxModeStream != null)
                {
                    syntaxModes = SyntaxMode.GetSyntaxModes(syntaxModeStream);
                }
                else
                {
                    syntaxModes = new List<SyntaxMode>();
                }
            }				
		}
		
		public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
		{
            string configFolder = PathHelper.GetSyntaxHighlightingConfigFolder();

			string fileName = syntaxMode.FileName;

            string filePath = Path.Combine(configFolder, fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($@"""{fileName}"" is not found in folder ""{configFolder}"".");
            }
           
			return new XmlTextReader(filePath);
		}
		
		public void UpdateSyntaxModeList()
		{
			// resources don't change during runtime
		}
	}
}
