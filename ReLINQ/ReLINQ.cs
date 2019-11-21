using System;
using System.Collections;
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

        /// <summary>
        /// Generates a sequence of <see cref="int"/> within the specified range
        /// </summary>
        /// <param name="start">start value</param>
        /// <param name="count">number of integers</param>
        /// <returns></returns>
        public static IEnumerable<int> Range(int start, int count)
        {
            if (count < 0)
            {
                throw  new ArgumentOutOfRangeException(nameof(count));
            }

            if ((long)start + (long)count >int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return RangeImplementation(start, count);
        }

        /// <summary>
        /// Generates a sequence of <see cref="int"/> within the specified range
        /// </summary>
        /// <param name="start">start value</param>
        /// <param name="count">number of integers</param>
        /// <returns></returns>
        private static IEnumerable<int> RangeImplementation(int start, int count)
        {
            int finalElement = start + count;
            while (start < finalElement)
            {
                yield return start;
                start++;
            }
        }

        /// <summary>
        /// Creates a new empty sequence of type specified
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TResult> Empty<TResult>()
        {
            return EmptyEnumerable<TResult>.Instance;
        }

        private class EmptyEnumerable<T> : IEnumerable<T>, IEnumerator<T>
        {
            internal static readonly IEnumerable<T> Instance = new EmptyEnumerable<T>();
            // Prevent construction elsewhere
            private EmptyEnumerable()
            {
            }
            public IEnumerator<T> GetEnumerator()
            {
                return this;
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this;
            }
            public T Current => throw new InvalidOperationException();

            object IEnumerator.Current => throw new InvalidOperationException();

            public void Dispose()
            {
                // No-op
            }
            public bool MoveNext() => false;

            public void Reset()
            {
                // No-op
            }
        }
    }
}
