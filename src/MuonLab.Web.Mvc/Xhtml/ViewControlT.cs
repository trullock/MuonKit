using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MuonLab.Commons.DI;
using MuonLab.Commons;
using MuonLab.Commons.English;
using MuonLab.Web.Xhtml;
using MuonLab.Web.Xhtml.Components;
using TagBuilder=System.Web.Mvc.TagBuilder;
using IEntity=MuonLab.NHibernate.IEntity;

namespace MuonLab.Web.Mvc.Xhtml
{
	public abstract class ViewControl<TModel> : ViewUserControl<TModel> where TModel : class
	{
		protected IComponentFactory<TModel> Xhtml;

		protected ViewControl() : this(DependencyResolver.Current.GetInstance<IComponentFactory<TModel>>())
		{
		}

		protected ViewControl(IComponentFactory<TModel> xhtml)
		{
			this.Xhtml = xhtml;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.Xhtml.ErrorProvider = new MvcErrorProvider(this.ViewData.ModelState);
		}

		protected IHiddenFieldComponent<TProperty> HiddenFieldFor<TProperty>(Expression<Func<TModel, TProperty>> property)
		{
			if (typeof(IEntity).IsAssignableFrom(typeof(TProperty)))
				return this.Xhtml.HiddenFieldFor(property, ViewData.Model, x => (x as IEntity).Id.ToString());

			return this.Xhtml.HiddenFieldFor(property, ViewData.Model, x => x.ToString());
		}

		protected ITextBoxComponent<TProperty> TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> property)
		{
			return this.Xhtml.TextBoxFor(property, ViewData.Model);
		}

		protected IPasswordBoxComponent PasswordBoxFor(Expression<Func<TModel, string>> property)
		{
			return this.Xhtml.PasswordBoxFor(property, ViewData.Model);
		}

		protected IDropDownComponent<TProperty> DropDownFor<TProperty>(Expression<Func<TModel, TProperty>> property, IEnumerable<TProperty> items, Func<TProperty, string> itemTextProperty) where TProperty : IEntity
		{
			return this.Xhtml.DropDownFor(property, ViewData.Model, items, x => x.Id.ToString(), x => x.Id.ToString(), itemTextProperty);
		}

		protected IDropDownComponent<Guid> DropDownFor<TData>(Expression<Func<TModel, Guid>> property, IEnumerable<TData> items, Func<TData, Guid> itemValueFunc, Func<TData, string> itemTextFunc)
		{
			return this.Xhtml.DropDownFor(property, ViewData.Model, items, g => g.ToString(), v => itemValueFunc(v).ToString(), itemTextFunc);
		}

		protected IDropDownComponent<TProperty> DropDownFor<TData, TProperty>(Expression<Func<TModel, TProperty>> property, IEnumerable<TData> items, Func<TProperty, string> propertyValueFunc, Func<TData, string> itemValueFunc, Func<TData, string> itemTextFunc)
		{
			return this.Xhtml.DropDownFor(property, ViewData.Model, items, propertyValueFunc, itemValueFunc, itemTextFunc);
		}

		protected IDropDownComponent<TProperty> DropDownFor<TProperty>(Expression<Func<TModel, TProperty>> property)
		{
			IEnumerable<TProperty> values;

			var enumType = typeof(TProperty);

			if (enumType.IsEnum)
				values = Enumerator<TProperty>.GetAll();
			else if (enumType.IsGenericType && enumType.GetGenericTypeDefinition() == typeof(Nullable<>))
				values = Enumerator.GetAll(enumType.GetGenericArguments()[0]).Cast<TProperty>();
			else
				throw new ArgumentException("TProperty: `" + enumType + "` must be an Enum");

			return this.Xhtml.DropDownFor(property, ViewData.Model, values, x => x.ToString(), x => x.ToString(), x => x.ToString().ToEnglish());
		}

		protected ITextAreaComponent TextAreaFor(Expression<Func<TModel, string>> property)
		{
			return this.Xhtml.TextAreaFor(property, this.Model);
		}
		
		protected ITextAreaComponent TextAreaFor(Expression<Func<TModel, IEnumerable<string>>> property)
		{
			return this.Xhtml.TextAreaFor(property, this.Model);
		}

		protected ICheckBoxComponent CheckBoxFor(Expression<Func<TModel, bool>> property)
		{
			return this.Xhtml.CheckBoxFor(property, this.Model);
		}

		protected IFileUploadComponent FileUploadFor(Expression<Func<TModel, HttpPostedFileBase>> property)
		{
			return this.Xhtml.FileUploadFor(property, this.Model);
		}

		protected Raw ValidationSummary()
		{
			return ValidationSummary(ValidationSummaryMode.ListWithAnchors);
		}

		protected Raw ValidationSummary(ValidationSummaryMode mode)
		{
			return ValidationSummary("Oops! There were the following problems with the data you entered:", mode);
		}

		protected Raw ValidationSummary(string headerText, ValidationSummaryMode mode)
		{
			if (this.ViewData.ModelState.Any(k => k.Value.Errors.Any()))
			{
				var builder = new StringBuilder("<div class=\"validation-summary\">");

				var pTag = new TagBuilder("p");
				pTag.AddCssClass("validation-summary-message");
				pTag.InnerHtml = headerText.HtmlEncode();

				builder.Append(pTag.ToString());

				if (mode != ValidationSummaryMode.HeaderOnly)
				{
					if (mode == ValidationSummaryMode.List)
						builder.Append(this.Html.ValidationSummary());
					else
					{
						builder.Append("<ul class=\"validation-summary-errors\">");

						foreach (var state in this.ViewData.ModelState)
						foreach (var error in state.Value.Errors)
						{
							if (state.Key == string.Empty)
								builder.Append("<li>" + error.ErrorMessage + "</li>");
							else
								builder.Append("<li><a href=\"#" + state.Key + "\" title=\"Click to jump to erroneous field\">" + error.ErrorMessage + "</a></li>");
						}

						builder.Append("</ul>");
					}
				}

				builder.Append("</div>");

				return (Raw)(builder.ToString());
			}
			else
				return null;
		}
	}
}