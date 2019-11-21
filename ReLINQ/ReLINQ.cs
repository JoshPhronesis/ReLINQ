using System;
using System.Collections.Generic;

namespace ReLINQ
{
    public static class ReLinq
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            if (predicate==null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (sequence==null)
            {
                throw  new ArgumentNullException(nameof(sequence));
            }

            return WhereImplementation(sequence, predicate);
        }

        /// <summary>
        /// Where inner implementation
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private static IEnumerable<TSource> WhereImplementation<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            foreach (TSource item in sequence)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Where Method which takes a predicate with an int parameter representing the index
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> sequence, Func<TSource,int, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            return WhereImplementation(sequence, predicate);
        }

        /// <summary>
        /// Inner implementation
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private static IEnumerable<TSource> WhereImplementation<TSource>(this IEnumerable<TSource> sequence, Func<TSource,int, bool> predicate)
        {
            int index = 0;
            foreach (TSource item in sequence)
            {
                if (predicate(item, index))
                {
                    yield return item;
                }
                index++;
            }
        }

        /// <summary>
        /// Projects items of <see cref="IEnumerable{TSource}"/> into <see cref="IEnumerable{TResult}"/>
        /// </summary>
        /// <typeparam name="TSource">source sequence</typeparam>
        /// <typeparam name="TResult">projection result</typeparam>
        /// <param name="sequence"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> sequence,
            Func<TSource, TResult> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            return SelectImplementation(sequence, selector);
        }

        /// <summary>
        /// Inner implementation of select
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static IEnumerable<TResult> SelectImplementation<TSource, TResult>(this IEnumerable<TSource> sequence, Func<TSource,TResult> selector)
        {
            foreach (TSource item in sequence)
            {
                yield return selector(item);
            }
        }

    }
}
