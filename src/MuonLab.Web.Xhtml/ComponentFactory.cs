using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MuonLab.Web.Xhtml.Components;
using MuonLab.Web.Xhtml.Components.Implementations;
using MuonLab.Web.Xhtml.Configuration;

namespace MuonLab.Web.Xhtml
{
	public class ComponentFactory<TModel> : IComponentFactory<TModel> where TModel : class
	{
		private readonly IFormConfiguration configuration;
		private readonly IComponentNameResolver nameResolver;
		private readonly IComponentIdResolver idResolver;
		private readonly IComponentLabelResolver labelResolver;

		public IErrorProvider ErrorProvider { get; set; }

		public ComponentFactory(
			IFormConfiguration configuration,
			IComponentNameResolver nameResolver,
			IComponentIdResolver idResolver,
			IComponentLabelResolver labelResolver)
		{
			this.configuration = configuration;
			this.nameResolver = nameResolver;
			this.idResolver = idResolver;
			this.labelResolver = labelResolver;
		}

		public IHiddenFieldComponent<TProperty> HiddenFieldFor<TProperty>(Expression<Func<TModel, TProperty>> property, TModel entity, Func<TProperty, string> toStringFunc)
		{
			var hidden = new HiddenFieldComponent<TModel, TProperty>(toStringFunc);
			InitializeComponent(hidden, entity, property);
			return hidden;
		}

		public ITextBoxComponent<TProperty> TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> property, TModel entity)
		{
			var textBox = new TextBoxComponent<TModel, TProperty>();
			InitializeComponent(textBox, entity, property);
			return textBox;
		}

		public IPasswordBoxComponent PasswordBoxFor(Expression<Func<TModel, string>> property, TModel entity)
		{
			var passwordBox = new PasswordBoxComponent<TModel>();
			InitializeComponent(passwordBox, entity, property);
			return passwordBox;
		}

		public ITextAreaComponent<TProperty> TextAreaFor<TProperty>(Expression<Func<TModel, TProperty>> property, TModel entity)
		{
			var textAreaComponent = new TextAreaComponent<TModel, TProperty>();

			InitializeComponent(textAreaComponent, entity, property);

			return textAreaComponent;
		}

		public IDropDownComponent<TProperty> DropDownFor<TProperty, TData>(Expression<Func<TModel, TProperty>> property, TModel entity, IEnumerable<TData> items, Func<TProperty, string> propertyValueFunc, Func<TData, string> itemValueFunc, Func<TData, string> itemTextFunc)
		{
			var dropDown = new DropDownComponent<TModel, TProperty, TData>(items, propertyValueFunc, itemValueFunc, itemTextFunc);

			InitializeComponent(dropDown, entity, property);

			return dropDown;
		}

		public ICheckBoxComponent CheckBoxFor(Expression<Func<TModel, bool>> property, TModel entity)
		{
			var checkBoxComponent = new CheckBoxComponent<TModel>();

			InitializeComponent(checkBoxComponent, entity, property);

			return checkBoxComponent;
		}

		public IRadioButtonsForBoolComponent RadioButtonFor(Expression<Func<TModel, bool?>> property, TModel entity)
		{
			var radioButtonComponent = new RadioButtonsForBoolComponent<TModel>(labelResolver.ResolveBool(true), labelResolver.ResolveBool(false));

			InitializeComponent(radioButtonComponent, entity, property);

			return radioButtonComponent;
		}

		public IRadioButtonsForBoolComponent RadioButtonFor(Expression<Func<TModel, bool>> property, TModel model)
		{
			throw new NotImplementedException();
		}

		public IFileUploadComponent FileUploadFor<TProperty>(Expression<Func<TModel, TProperty>> property, TModel entity)
		{
			var fileUploadComponent = new FileUploadComponent<TModel, TProperty>();

			InitializeComponent(fileUploadComponent, entity, property);

			return fileUploadComponent;
		}

		protected virtual void InitializeComponent<TEntity, TProperty>(Component<TEntity, TProperty> component, TEntity model, Expression<Func<TEntity, TProperty>> property) where TEntity : class
		{
			// Set the Name
			component.WithName(this.nameResolver.ResolveName(property));

			// Set the Value
			if (model != null)
			{
				try
				{
					component.WithValue(property.Compile().Invoke(model));
				}
				catch (NullReferenceException e)
				{
					throw new NullReferenceException("Could not set component value, some part of the property chain is null: " + property, e);
				}
			}

			// Set the Id
			component.WithId(this.idResolver.ResolveId(property, component.ControlPrefix));

			// run the config on the component
			if (this.configuration != null)
				this.configuration.Initialize(component);
		}

		protected virtual void InitializeComponent<TEntity, TProperty>(VisibleComponent<TEntity, TProperty> component, TEntity model, Expression<Func<TEntity, TProperty>> property) where TEntity : class
		{
			// Set the Name
			component.WithName(this.nameResolver.ResolveName(property));

			// Set the Value
			if (model != null)
			{
				if (this.ErrorProvider.HasErrors(component.Name))
				{
					component.WithAttemptedValue(this.ErrorProvider.GetAttemptedValue(component.Name)); 
				}
				else
				{
					try
					{
						component.WithValue(property.Compile().Invoke(model));
					}
					catch (NullReferenceException e)
					{
						throw new NullReferenceException("Could not set component value, some part of the property chain is null: " + property, e);
					}
				}
			}

			// Set the Id
			component.WithId(this.idResolver.ResolveId(property, component.ControlPrefix));

			// set the default label, then hide it as it should be hidden by default.
			component.WithLabel(this.labelResolver.ResolveLabel(property)).WithoutLabel();

			// run the config on the component
			if (this.configuration != null)
				this.configuration.Initialize(component);

			component.WithErrors(this.ErrorProvider.GetErrorsFor(component.Name));
		}
	}
}