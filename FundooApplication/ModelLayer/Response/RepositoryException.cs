﻿using System.Runtime.Serialization;

namespace fundoo_application.Controllers
{
    [Serializable]
    public class RepositoryException : Exception
    {
        public RepositoryException()
        {
        }

        public RepositoryException(string? message) : base(message)
        {
        }

        public RepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}