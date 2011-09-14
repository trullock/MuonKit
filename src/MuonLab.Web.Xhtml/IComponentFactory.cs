using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MuonLab.Web.Xhtml.Components;

namespace MuonLab.Web.Xhtml
{
	public interface IComponentFactory<TModel> where TModel : class
	{
		IHiddenFieldComponent<TProperty> HiddenFieldFor<TProperty>(Expression<Func<TModel, TProperty>> property, TModel entity, Func<TProperty, string> toStringFunc);
		ITextBoxComponent<TProperty> TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> property, TModel entity);
		IPasswordBoxComponent PasswordBoxFor(Expression<Func<TModel, string>> property, TModel entity);

		IDropDownComponent<TProperty> DropDownFor<TProperty, TData>(Expression<Func<TModel, TProperty>> property, TModel entity, IEnumerable<TData> items, Func<TProperty, string> propertyValueFunc, Func<TData, string> itemValueFunc, Func<TData, string> itemTextFunc);

		IFileUploadComponent FileUploadFor<TProperty>(Expression<Func<TModel, TProperty>> property, TModel entity);

		IErrorProvider ErrorProvider { get; set; }
		ITextAreaComponent<TProperty> TextAreaFor<TProperty>(Expression<Func<TModel, TProperty>> property, TModel entity);
		ICheckBoxComponent CheckBoxFor(Expression<Func<TModel, bool>> property, TModel entity);
		IRadioButtonsForBoolComponent RadioButtonFor(Expression<Func<TModel, bool?>> property, TModel model);
		IRadioButtonsForBoolComponent RadioButtonFor(Expression<Func<TModel, bool>> property, TModel model);
	}
}