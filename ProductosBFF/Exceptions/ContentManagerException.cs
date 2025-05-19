using System;
using System.Runtime.Serialization;

namespace ProductosBFF.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ContentManagerException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public ContentManagerException() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ContentManagerException(string message) : base(message) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public ContentManagerException(string message, Exception ex) : base(message, ex) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ContentManagerException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            base.GetObjectData(info, context);
        }

    }
}