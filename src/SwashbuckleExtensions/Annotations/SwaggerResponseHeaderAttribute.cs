using System;
using System.Net;

namespace AspNetWebApi.SwashbuckleExtensions.Annotations
{
    /// <summary>
    /// An attribute to add Swagger Response Header documentation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerResponseHeaderAttribute : Attribute
    {
        /// <summary>
        /// HTTP Status Code that the header applies to.
        /// </summary>
        public int StatusCode { get; private set; }

        /// <summary>
        /// The header name.
        /// </summary>
        public string Header { get; private set; }

        /// <summary>
        /// Header description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Header type, i.e. "string".
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="statusCode">HTTP Status Code that the header applies to.</param>
        /// <param name="header">Header name.</param>
        /// <param name="description">Header description.</param>
        /// <param name="type">Header type, i.e. "string".</param>
        public SwaggerResponseHeaderAttribute(HttpStatusCode statusCode, string header, string description = null, string type = null) : this((int)statusCode, header, description, type)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="statusCode">HTTP Status Code that the header applies to.</param>
        /// <param name="header">Header name.</param>
        /// <param name="description">Header description.</param>
        /// <param name="type">Header type, i.e. "string".</param>
        public SwaggerResponseHeaderAttribute(HttpStatusCode statusCode, HttpResponseHeader header, string description = null, string type = null) : this((int)statusCode, header.ToString(), description, type)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="statusCode">HTTP Status Code that the header applies to.</param>
        /// <param name="header">Header name.</param>
        /// <param name="description">Header description.</param>
        /// <param name="type">Header type, i.e. "string".</param>
        public SwaggerResponseHeaderAttribute(int statusCode, string header, string description = null, string type = null)
        {
            StatusCode = statusCode;
            Header = header;
            Description = description;
            Type = type;
        }
    }
}
