using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Microsoft.CSharp;
using MuonLab.Commons.DI;

namespace MuonLab.Web
{
	public class AutoHtmlEncodingCSharpCodeProvider : CSharpCodeProvider
	{
		public AutoHtmlEncodingCSharpCodeProvider()
		{
		}

		public AutoHtmlEncodingCSharpCodeProvider(IDictionary<string, string> providerOptions)
			: base(providerOptions)
		{
		}

		public override void GenerateCodeFromStatement(CodeStatement statement, TextWriter writer, CodeGeneratorOptions options)
		{
			var codeExpressionStatement = statement as CodeExpressionStatement;
			if (codeExpressionStatement != null)
			{
				var methodInvokeExpression = codeExpressionStatement.Expression as CodeMethodInvokeExpression;
				if (methodInvokeExpression != null)
				{
					if (methodInvokeExpression.Method.MethodName == "Write" && methodInvokeExpression.Parameters.Count == 1)
					{
						var parameter = methodInvokeExpression.Parameters[0] as CodeSnippetExpression;

						if ((parameter != null) && (!string.IsNullOrEmpty(parameter.Value)))
							parameter.Value = "global::" + GetType().FullName + ".PreProcessObject(this, " + parameter.Value + ")";
					}
				}
			}

			base.GenerateCodeFromStatement(statement, writer, options);
		}

		public static string PreProcessObject(object source, object value)
		{
			if (value is IRawHtml)
				return value.ToString();

			if (value == null)
				return null;

			var encoder = DependencyResolver.Current.TryGetInstance<IHtmlEncoder>();
			if (encoder != null)
				return encoder.HtmlAttributeEncode(value.ToString());

			return HttpUtility.HtmlAttributeEncode(value.ToString());
		}
	}
}


#region Full License
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion