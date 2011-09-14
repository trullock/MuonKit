using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rhino.Mocks;

namespace MuonLab.Testing
{
	public class AutoMocker
	{
		private Dictionary<Type, object> mocks;
		private Dictionary<Type, object> subjects;

		protected AutoMocker()
		{
			Reset();
		}

		protected T Subject<T>() where T : class
		{
			if (!this.subjects.ContainsKey(typeof(T)))
				this.subjects.Add(typeof(T), CreateSubjectUnderTest<T>());
			
			return this.subjects[typeof(T)] as T;
		}

		protected void Reset()
		{
			this.mocks = new Dictionary<Type, object>();
			this.subjects = new Dictionary<Type, object>();
		}

		protected TMockedClass Dependency<TMockedClass>() where TMockedClass : class
		{
			return MockOfType(typeof(TMockedClass)) as TMockedClass;
		}

		protected static TMockedClass Stub<TMockedClass>() where TMockedClass : class
		{
			return MockRepository.GenerateStub<TMockedClass>();
		}

		protected void Inject<TClass>(TClass instance) where TClass : class
		{
			this.mocks.Add(typeof(TClass), instance);
		}

		protected virtual T CreateSubjectUnderTest<T>() where T : class
		{
			return Activator.CreateInstance(typeof(T), GetParameterObjects<T>()) as T;
		}

		private object[] GetParameterObjects<T>()
		{
			var parameterObjects = new List<object>();
			var constructorInfo = GetGreediestConstructor(typeof(T));

			foreach (var parameter in constructorInfo.GetParameters())
				parameterObjects.Add(MockOfType(parameter.ParameterType));

			return parameterObjects.ToArray();
		}

		private static ConstructorInfo GetGreediestConstructor(Type type)
		{
			return type.GetConstructors()
				.OrderByDescending(c => c.GetParameters().Length)
				.First();
		}

		private object MockOfType(Type type)
		{
			EnsureStubExistsForType(type);
			return this.mocks[type];
		}

		private void EnsureStubExistsForType(Type type)
		{
			if (!this.mocks.ContainsKey(type))
				this.mocks.Add(type, MockRepository.GenerateStub(type));
		}
	}
}