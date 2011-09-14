using System;
using System.Runtime.Serialization;

namespace MuonLab.Commons.DI
{
    public class DependencyResolverException : Exception
    {
        public DependencyResolverException()
        {
        }

        public DependencyResolverException(string message) : base(message)
        {
        }

        public DependencyResolverException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DependencyResolverException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}