using System;

namespace HelperTools
{
    /// <summary>
    /// Helper class for work with null reference
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        /// Extension method representative following functionality: 
        /// if input value null return null else apply evaluation function for input object
        /// </summary>
        /// <param name="oInput">Input object</param>
        /// <param name="evaluator">Function such apply for none null value</param>
        /// <typeparam name="TInput">Generic type (class constraint)</typeparam>
        /// <typeparam name="TOutput">Generic type (class constraint)</typeparam>
        /// <returns></returns>
        public static TOutput With<TInput, TOutput>(this TInput oInput, Func<TInput,TOutput> evaluator)
            where TInput : class
            where TOutput : class
        {
            return oInput == null ? null : evaluator(oInput);
        }

        /// <summary>
        /// Extension method representative following functionality: 
        /// if input value null return result execute evaluation function, else return input value
        /// </summary>
        /// <param name="oInput">Input value</param>
        /// <param name="evaluator">If input value null return result execute this delegate</param>
        /// <param name="failValue">value returned if input null</param>
        /// <typeparam name="TInput">Generic type (class constraint)</typeparam>
        /// <typeparam name="TResult">Generic type </typeparam>
        /// <returns>If input value null return result execute evaluation function, else return input value</returns>
        public static TResult Returns<TInput,TResult>(this TInput oInput, Func<TInput,TResult> evaluator, TResult failValue)
            where TInput : class
        {
            return oInput == null ? failValue : evaluator(oInput);
        }

      
    }
}
